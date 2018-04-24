using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Chatroom
{
    public partial class Form1 : Form
    {
        //If we are a client, this is our TCP connection
        TcpClient user;

        //If we are a server, this is our listener and clients
        TcpListener tcpListener;
        List<TcpClient> clientList = new List<TcpClient>();

        public Form1()
        {
            InitializeComponent();
        }

        private void AddToChat(string user, string message)
        {
            //Add the message to the chat box, with a new line appended
            UI_richTextBox_Display.Text += $"{user}: {message}";
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
                
                UI_toolStripMenuItem_Host.Enabled = false;
                UI_ToolStripMenuItem_Join.Enabled = false;
                Text = "Chatroom: Server";
            }
            catch(Exception err)
            {
                //Failed to listen, don't need to do anything
                Console.WriteLine(err.Message);
                throw new NotImplementedException();
            }
        }
        private void AcceptListen(IAsyncResult ar)
        {
            try
            {
                //Accept the client and add it to our client list
                TcpClient temp = tcpListener.EndAcceptTcpClient(ar);
                tcpListener.Stop();
                lock (clientList)
                    clientList.Add(temp);
            }
            catch
            {
                //Failed to Listen, don't need to do anything
            }

            //Begin listening for more clients
            StartListen();
        }
        
        //Connect functions are the functionality of the TCPClient, user
        private void StartConnect(string ipAddress, int port)
        {
            try
            {
                //Begin a connection attempt
                user.BeginConnect(ipAddress, port, AcceptConnect, null);
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
                user.EndConnect(ar);
                Invoke(new Action(() =>
                {
                    UI_toolStripMenuItem_Host.Enabled = false;
                    UI_ToolStripMenuItem_Join.Enabled = false;
                    Text = "Chatroom: Client";
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
            //Send it to the server
        }
        //We are a user, receiving a string from the server
        private void UserReceive()
        {

        }

        //We are a server, sending a string to all the users
        private void ServerSend(string message)
        {
            //Add message to the board
            AddToChat("Server", message);
            //Send it to all the clients
        }
        //We are a server, receiving a string from the user
        private void ServerReceive()
        {

        }

        //Form functions-----------------------------------------------------------

        //Shutdown all of the Listeners and Clients
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                tcpListener.Stop();
            }
            catch
            {
                //Probably already disposed. Ignore it
            }
            //If we have a 
            if(user != null) user.Close();
            clientList.ForEach(client => client.Close());
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
                    user = new TcpClient();
                    StartConnect(cc.IPAddress, cc.Port);
                }
            }
        }

        private void UI_button_Send_Click(object sender, EventArgs e)
        {
            if (user != null && user.Connected)
            {
                UserSend(UI_textBox_Input.Text);
            }
            else
            {
                ServerSend(UI_textBox_Input.Text);
            }

            UI_textBox_Input.Text = "";
        }
    }
}
