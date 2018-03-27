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
using Connection;

namespace Client
{
    public partial class Client : Form
    {
        private int guess = 0;
        private Connection.Connection Connection = null;

        public Client()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Select a port to connect to 
            ClientConnectSelect();
            guess = UI_trackBar_Guess.Value;
        }

        private void ClientConnectSelect()
        {
            //Call the modal dialog for a socket number
            SocketSelect sSelect = new SocketSelect();
            if (sSelect.ShowDialog() == DialogResult.OK)
            {
                //Get the port number
                int port = sSelect.socketPort;
                string ip = sSelect.socketIP;
                //Write to the status bar
                UI_label_Status.Text = $"Trying to connect to port {port} and IP Address {ip}";

                //Start trying to connect
                SocketConnect(port, ip);
            }
        }

        private void UI_trackBar_Guess_Scroll(object sender, EventArgs e)
        {
            //Put in the value of the trackbar onto the UI
            UI_label_Current.Text = UI_trackBar_Guess.Value.ToString();
            guess = UI_trackBar_Guess.Value;
        }

        private void UI_button_SendGuess_Click(object sender, EventArgs e)
        {
            //Transmit guess
            Connection.SendData(guess);
        }

        private delegate void delVoidStatus(int i);

        private void TrackBarUpdate(int i)
        {
            if(i > 0)
            {
                UI_label_Status.Text = "Guess was too high.";
                guess -= 1;
                UI_trackBar_Guess.Maximum = guess;
                UI_label_Max.Text = (guess).ToString();
            }
            else if(i < 0)
            {
                UI_label_Status.Text = "Guess was too low.";
                guess += 1;
                UI_trackBar_Guess.Minimum = guess;
                UI_label_Min.Text = (guess).ToString();
            }
            else
            {
                UI_label_Status.Text = "Guess was correct!";
                UI_trackBar_Guess.Minimum = 1;
                UI_trackBar_Guess.Maximum = 1000;
                UI_label_Min.Text = "1";
                UI_label_Max.Text = "1000";
                MessageBox.Show("You win. Please play again.", "You won!");
            }

            //Put in the value of the trackbar onto the UI
            UI_label_Current.Text = UI_trackBar_Guess.Value.ToString();
        }

        //When we receive the data from the connection
        private void cbDataReceived(int Data)
        {
            Invoke(new delVoidStatus(TrackBarUpdate), Data);
        }

        //When we disconnect from our socket server
        private void cbDisconnect(string inMessage)
        {
            Invoke(new delVoidString(cbDisconnectPart2), inMessage);
        }

        private delegate void delVoidString(string x);
        private void cbDisconnectPart2(string inMessage)
        {
            UI_label_Status.Text = inMessage;

            //Select a new port
            ClientConnectSelect();
        }

        private delegate void delVoidVoid();
        //Socket connection function
        private void SocketConnect(int port, string ip)
        {
            Socket tryConnect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                tryConnect.BeginConnect(ip, port, cbConnectComplete, tryConnect);
            }
            catch
            {
                UI_label_Status.Text = "Couldn't connect to port."; 
            }
        }

        private void cbConnectComplete(IAsyncResult asyncResult)
        {
            Socket temp = (Socket)asyncResult.AsyncState;

            try
            {
                temp.EndConnect(asyncResult);
                //Tell the user the connection succeeded
                Invoke(new Action(() => UI_label_Status.Text = "Connection succeeded"));
                //Create our connection
                Connection = new Connection.Connection(temp, cbDataReceived, cbDisconnect);
                //Enable our guess button
                Invoke(new Action(() => UI_button_SendGuess.Enabled = true));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Tell the user that the connection failed
                Invoke(new Action(() => UI_label_Status.Text = "Connection failed"));

                //Select a new port
                Invoke(new delVoidVoid(ClientConnectSelect));
            }
        }

        private void UI_button_Disconnect_Click(object sender, EventArgs e)
        {
            if (Connection != null)
            {
                Connection.SoftDisconnect();
            }
        }
    }
}
