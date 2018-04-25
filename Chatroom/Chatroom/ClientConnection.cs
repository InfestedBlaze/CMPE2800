using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Chatroom
{
    class ClientConnection
    {
        private TcpClient client;
        private Thread readThread;
        private bool continueRead;

        public Queue<string> receivedMessages
        {
            get { return new Queue<string>(receivedMessages); }
            private set { receivedMessages = value; }
        }

        public ClientConnection(TcpClient tcpClient)
        {
            client = tcpClient;
            receivedMessages = new Queue<string>();
            continueRead = true;

            readThread = new Thread(ReadingThread);
            readThread.IsBackground = true;
            readThread.Start();
        }

        private void ReadingThread()
        {
            //Get the stream to read from
            NetworkStream networkStream = client.GetStream();

            //Check if we can read from it
            if (!networkStream.CanRead) return;

            //Variables for use with the reading
            byte[] buffer;
            StringBuilder sb;
            int bytesRead;

            while (continueRead)
            {
                buffer = new byte[1024];
                sb = new StringBuilder();
                bytesRead = 0;

                if (networkStream.DataAvailable)
                {
                    //While we have some info, keep reading it, append it to our stringbuilder
                    do
                    {
                        bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                        sb.AppendFormat("{0}", Encoding.ASCII.GetString(buffer, 0, bytesRead));
                    } while (networkStream.DataAvailable);
                    //Add the message to our queue
                    lock (receivedMessages)
                        receivedMessages.Enqueue(sb.ToString());
                }
                else
                {
                    //Wait for some info to be available
                    Thread.Sleep(5);
                }
            }
        }

        public void Send(string message)
        {
            NetworkStream ns = client.GetStream();

            if (ns.CanWrite)
            {
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                ns.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
