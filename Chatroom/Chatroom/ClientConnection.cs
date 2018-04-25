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
        public bool alive { get; private set; }

        public Queue<string> messages;

        public ClientConnection(TcpClient tcpClient)
        {
            client = tcpClient;
            messages = new Queue<string>();
            continueRead = true;
            alive = true;

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
                    lock (messages)
                        messages.Enqueue(sb.ToString());
                }
                else
                {
                    //Wait for some info to be available
                    Thread.Sleep(5);
                }
            }

            client.Close();
            alive = false;
        }

        public void Send(string message)
        {
            if (!alive) return;

            NetworkStream ns = client.GetStream();

            if (ns.CanWrite)
            {
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                ns.Write(buffer, 0, buffer.Length);
            }
        }

        /// <summary>
        /// Dispose of the thread and the object, waits for the thread
        /// </summary>
        /// <returns>Returns true when the thread has ended</returns>
        public bool Dispose()
        {
            //Kill the reading thread
            continueRead = false;
            //wait for the thread to end
            while (alive) ;
            //Return that we can die now
            return true;
        }
    }
}
