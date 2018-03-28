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
using System.Net;
using Connection;

namespace Server
{
    public partial class Server : Form
    {
        Random randNum = new Random();
        int guessAnswer;
        private Socket ListenSocket = null;
        private Connection.Connection ConnectionSocket = null;

        //delegates for outcomes
        private delegate void delVoidSocket(Socket soc);
        private delegate void delVoidString(string err);

        public Server()
        {
            InitializeComponent();

            //Start off with a fresh answer
            ChangeAnswer();
            //Start listening for a connection
            StartListen();
        }

        private delegate void delVoidVoid();
        //Callback functions for our server game
        private void ChangeAnswer()
        {
            //New number, range 1 - 1000
            guessAnswer = randNum.Next(1, 1001);
            //Display on the UI
            UI_label_Answer.Text = guessAnswer.ToString();
        }
        
        //When we receive the data from the connection
        private void cbDataReceived(int Data)
        {
            int reply = 0;

            if(Data > guessAnswer)
            {
                //Guess was too big
                reply = 1;
            }
            else if (Data < guessAnswer)
            {
                //Guess was too little
                reply = -1;
            }
            else //Data == guessAnswer
            {
                //Guess was correct
                reply = 0;
                try
                {
                    Invoke(new delVoidVoid(ChangeAnswer));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //Transmit reply
            ConnectionSocket.SendData(reply);
        }

        //Matches delegate type in connection class
        private void cbDisconnect(string inMessage)
        {
            ListenSocket = null;
            ConnectionSocket = null;
            //We disconnected from the client. Start listening again.
            StartListen();
        }

        private void StartListen()
        {
            ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                ListenSocket.Bind(new IPEndPoint(IPAddress.Any, 1666));
            }
            catch(SocketException se)
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to exit? (No to try again)", "Server already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    this.Close();
                }
                else
                {
                    StartListen();
                }
            }
            try
            {
                ListenSocket.Listen(5);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                ListenSocket.BeginAccept(cbAccept, ListenSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                Invoke(new Action(() => Text = "Server : Listening"));
            }
            catch
            {
                return;
            }
        }


        //cbAccept callback when listen sock forms a connection
        private void cbAccept(IAsyncResult ar)
        {
            Socket lsok = (Socket)(ar.AsyncState);
            try
            {
                Socket csok = lsok.EndAccept(ar);
                try
                {
                    Invoke(new delVoidSocket(cbHandleAccept), csok);
                }
                catch
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                try
                {
                    Invoke(new delVoidString(cbHandleError), ex.Message);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }

        //cbAccepts and cbErrors
        private void cbHandleAccept(Socket sok)
        {
            ConnectionSocket = new Connection.Connection(sok, cbDataReceived, cbDisconnect);
            ListenSocket.Close();
            ListenSocket = null;
            try
            {
                Invoke(new Action(() => Text = "Server : Connected"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
        }

        private void cbHandleError(string err)
        {
            MessageBox.Show(err, "Socket Error!");
        }

        private void DiscBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionSocket != null)
            {
                ConnectionSocket.SoftDisconnect();
                try
                {
                    Invoke(new Action(() => Text = "Server : Disconnected"));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
