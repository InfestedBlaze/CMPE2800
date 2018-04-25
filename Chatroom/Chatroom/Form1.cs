using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Chatroom
{
    public partial class Form1 : Form
    {
        delegate void delVoidString(string i);

        enum Role { Waiting, Server, Client}
        Role ourRole = Role.Waiting;

        //If we are a client, this is our TCP connection
        ClientConnection user;

        //If we are a server, this is our listener and clients
        TcpListener tcpListener;
        List<ClientConnection> clientList = new List<ClientConnection>();

        //The messages we can put on our screen
        Queue<string> messages = new Queue<string>();
        //Thread to read all the messages and write them to the screen
        Thread reading;

        public Form1()
        {
            InitializeComponent();
        }

        private void AddToChat(string message)
        {
            //Add the message to the chat box, with a new line appended
            UI_richTextBox_Display.Text += $"{message}\n";
        }

        //Tcp Functions-----------------------------------------------------------

        //Listen functions are all the functionality of the TCPListener
        private void StartListen()
        {
            try
            {
                //Listen for a new TCP Client
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(AcceptListen, null);
                
                //Disable the host/join buttons
                UI_toolStripMenuItem_Host.Enabled = false;
                UI_ToolStripMenuItem_Join.Enabled = false;
                //Display our role
                Text = "Chatroom: Server";
                ourRole = Role.Server;
                //Start reading from our clients
                reading = new Thread(ServerReceive);
                reading.IsBackground = true;
                reading.Start();
            }
            catch
            {
                //Failed to listen, don't need to do anything
            }
        }
        private void AcceptListen(IAsyncResult ar)
        {
            try
            {
                //Accept the client and add it to our client list
                ClientConnection temp = new ClientConnection(tcpListener.EndAcceptTcpClient(ar));
                tcpListener.Stop();
                lock (clientList)
                    clientList.Add(temp);
            }
            catch
            {
                //Failed to Listen, don't do anything
            }

            //Begin listening for more clients
            StartListen();
        }
        
        //Connect functions are the functionality of the TCPClient, user
        private void StartConnect(string ipAddress, int port, TcpClient connection)
        {
            try
            {
                //Begin a connection attempt
                connection.BeginConnect(ipAddress, port, AcceptConnect, connection);
            }
            catch
            {
                //Failed to connect, don't do anything
            }
        } 
        private void AcceptConnect(IAsyncResult ar)
        {
            try
            {
                TcpClient connection = (TcpClient)ar.AsyncState;
                connection.EndConnect(ar);
                Invoke(new Action(() =>
                {
                    //Assign our connection to our user field
                    user = new ClientConnection(connection);
                    //Disable the host/join buttons
                    UI_toolStripMenuItem_Host.Enabled = false;
                    UI_ToolStripMenuItem_Join.Enabled = false;
                    //Display our role
                    Text = "Chatroom: Client";
                    ourRole = Role.Client;
                    //Start reading from the server
                    reading = new Thread(UserReceive);
                    reading.IsBackground = true;
                    reading.Start();
                }));
            }
            catch
            {
                //Failed to connect, don't do anything
            }
        }

        //We are a user, sending a string to the server
        private void UserSend(string message)
        {
            user.Send(message);
        }
        //We are a user, receiving a string from the server
        private void UserReceive()
        {
            while (true)
            {
                //Transfer all the messages from the user queue
                while (user.messages.Count > 0)
                    messages.Enqueue(user.messages.Dequeue());
                //Write them to our screen
                while(messages.Count > 0)
                    Invoke(new delVoidString(AddToChat), messages.Dequeue());
            }
        }

        //We are a server, sending a string to all the users
        private void ServerSend(string message)
        {
            //Add message to the board
            AddToChat(message);
            //Send it to all the clients
            lock (clientList)
                clientList.ForEach(client => client.Send(message));
        }
        //We are a server, receiving a string from the user
        private void ServerReceive()
        {
            while (true)
            {
                foreach(ClientConnection cc in clientList.ToList())
                {
                    //haven't made a connection yet
                    if (cc == null) continue;

                    //If the client is dead
                    if(!cc.alive)
                    {
                        clientList.Remove(cc);
                        continue;
                    }

                    //Transfer all the messages from the user queue
                    while (cc.messages.Count > 0)
                        messages.Enqueue(cc.messages.Dequeue());
                    //Write them to our screen
                    while (messages.Count > 0)
                    {
                        Invoke(new delVoidString(ServerSend), messages.Dequeue());
                    }
                }
            }
        }

        //Form functions-----------------------------------------------------------

        //Shutdown all of the clients
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(user != null) user.Dispose();
            clientList.ForEach(client => client.Dispose());
        }

        //Button to host or join, will disable both on success
        private void UI_toolStripMenuItem_Host_Click(object sender, EventArgs e)
        {
            using (ServerConnect sc = new ServerConnect())
            {
                if(sc.ShowDialog() == DialogResult.OK)
                {
                    tcpListener = new TcpListener(System.Net.IPAddress.Any, sc.Port);
                    StartListen();
                }
            }
        }
        private void UI_toolStripMenuItem_Join_Click(object sender, EventArgs e)
        {
            using (ClientConnect cc = new ClientConnect())
            {
                if (cc.ShowDialog() == DialogResult.OK)
                {
                    StartConnect(cc.IPAddress, cc.Port, new TcpClient());
                }
            }
        }

        //Send out the data
        private void UI_button_Send_Click(object sender, EventArgs e)
        {
            //We have no role, return
            if (ourRole == Role.Waiting) return;

            //Check if we are a client or server
            if (ourRole == Role.Client)
            {
                UserSend(UI_textBox_Input.Text);
            }
            else if(ourRole == Role.Server)
            {
                ServerSend(UI_textBox_Input.Text);
            }

            //Clear input box
            UI_textBox_Input.Text = "";
        }
    }
}
