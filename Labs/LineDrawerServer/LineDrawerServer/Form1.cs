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
using mdtypes;

namespace LineDrawerServer
{
    public partial class Form1 : Form
    {
        private delegate void delVoidSocket(Socket x);
        private delegate void delVoidVoid();

        private Socket listenerSocket = null;
        private List<Connection> clientList = new List<Connection>();
        private Queue<LineSegment> lines = new Queue<LineSegment>();

        private bool UpdateFlag = false;

        public Form1()
        {
            InitializeComponent();

            Thread thread = new Thread(SendLinesThread);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenerSocket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 1666));

                StartListen();
            }
            catch
            {
                cbError("Form1 Shown Error");
            }
        }

        private void StartListen()
        {
            try
            {
                //Listen for clients
                listenerSocket.Listen(5);
                //Begin accepting them
                listenerSocket.BeginAccept(EndAccept, null);
            }
            catch
            {
                cbError("Listen Error");
            }
        }

        private void EndAccept(IAsyncResult ar)
        {
            try
            {
                //Make a temporary socket that is the connection
                Socket connect = listenerSocket.EndAccept(ar);
                //Go into our handling function
                Invoke(new delVoidSocket(HandleAccept), connect);
            }
            catch
            {
                cbError("Accept Error");
            }

            //Listen for more clients
            StartListen();
        }

        private void HandleAccept(Socket connectedSocket)
        {
            //Make a temporary connection and add it to the client list
            Connection temp = new Connection(connectedSocket, cbDisconnect, cbError, cbLine);
            lock (clientList)
                clientList.Add(temp);

            UpdateFlag = true;
        }

        private void cbDisconnect(Connection connection, string message)
        {
            //Remove the connection from our client list
            lock (clientList)
                clientList.Remove(connection);
        }

        private void cbError(string message)
        {
            //Set the error message to the title bar
            Invoke(new Action(() => this.Text = message));
        }

        private void cbLine(LineSegment ls)
        {
            //Send each line segment to our queue
            lock (lines)
                lines.Enqueue(ls);

            Invoke(new Action(() => UpdateFlag = true));
        }
        
        private void SendLinesThread()
        {
            LineSegment sendLine;
            while (true)
            {
                if (lines.Count == 0)
                    continue;
                
                lock (lines)
                    sendLine = lines.Dequeue();

                lock (clientList)
                    clientList.ForEach(client => client.SendData(sendLine));
            }
        }

        private void bindData()
        {
            BindingSource bs = new BindingSource();
            lock(clientList)
                bs.DataSource = from client in clientList
                                select new
                                {
                                    Address = client.address,
                                    FramesReceieved = client.framesRx,
                                    DestackAverage = client.framesRx / (float)client.receiveEvents,
                                    Fragments = client.fragments
                                };
            UI_dataGridView_Clients.DataSource = bs;

            long Frames, Bytes;
            lock (clientList)
            {
                Frames = clientList.Sum(client => client.framesRx);
                Bytes = clientList.Sum(client => client.bytesRx);
            }

            int magnitude = 0;
            while (Bytes >= 1000)
            {
                Bytes /= 1000;
                magnitude++;
            }
            string mag = "";
            switch (magnitude)
            {
                case 1:
                    mag = "K";
                    break;
                case 2:
                    mag = "M";
                    break;
                case 3:
                    mag = "G";
                    break;
                case 4:
                    mag = "T";
                    break;
                case 5:
                    mag = "P";
                    break;
                default:
                    mag = "?";
                    break;
            }

            UI_toolStripStatusLabel_TotalFrames.Text = $"Total Frames: {Frames}";
            UI_toolStripStatusLabel_TotalBytes.Text = $"Total Bytes: {Bytes.ToString("F2")}{mag}B";
        }

        private void timer_Update_Tick(object sender, EventArgs e)
        {
            if (UpdateFlag)
            {
                bindData();
            }
        }
    }
}
