using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using GuessTrany;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Connection
{
    public class Connection
    {
        public delegate void delVoidInt(int i);
        public delegate void delVoidString(string i);

        private Socket connection = null;
        private delVoidInt OutputData = null;
        private delVoidString Disconnect = null;

        public Connection(Socket inSock, delVoidInt DataOutFunc, delVoidString DisconnectFunc)
        {
            connection = inSock;
            OutputData = DataOutFunc;
            Disconnect = DisconnectFunc;

            Thread thread = new Thread(ThreadReading);
            thread.IsBackground = true;
            thread.Start();
        }

        private void ThreadReading()
        {
            byte[] buffer = new byte[2000];

            //Ensure that we don't timeout on receiving
            connection.ReceiveTimeout = 0;

            while (true)
            {
                int iBytes = 0;

                try
                {
                    iBytes = connection.Receive(buffer);
                }
                catch (Exception err)
                {
                    //Invoke handle loss of connection
                    Disconnect("Hard disconnection: " + err.Message);
                    return;
                }

                //Soft disconnection
                if(iBytes == 0)
                {
                    //Invoke handle loss of connection
                    Disconnect("Soft Disconnection");
                    return;
                }

                //Got our data, deserialize it
                BinaryFormatter bf = new BinaryFormatter();
                object frame = bf.Deserialize(new MemoryStream(buffer));

                if(frame is GuessFrame)
                {
                    //Put it into our Data delegate function
                    OutputData((frame as GuessFrame)._Data);
                }
            }
        }

        public void SendData(int guess)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, new GuessFrame(guess));
                connection.Send(ms.GetBuffer(), (int)ms.Length, SocketFlags.None);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to send data");
            }
        }

        public void SoftDisconnect()
        {
            try
            {
                connection.Shutdown(SocketShutdown.Both);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            
            connection.Close();
        }
    }
}
