using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using mdtypes;

public class Connection
{
    public delegate void delVoidString(string i);
    public delegate void delVoidLineSegment(LineSegment i);

    private Socket connection = null;
    private delVoidString Disconnect = null;
    private delVoidString ErrorOccurred = null;
    private delVoidLineSegment OutputLineSeg = null;

    private Queue<LineSegment> RxLineSegments = new Queue<LineSegment>();
    static private BinaryFormatter bf = new BinaryFormatter();

    private uint framesRx = 0;
    private uint fragments = 0;
    private uint receiveEvents = 0;
    private uint bytesRx = 0;

    public Connection(Socket inSock, delVoidString DisconnectFunc, delVoidString ErrorFunc, delVoidLineSegment OutputLine)
    {
        connection = inSock;
        Disconnect = DisconnectFunc;
        ErrorOccurred = ErrorFunc;
        OutputLineSeg = OutputLine;

        Thread thread = new Thread(ThreadReading);
        thread.IsBackground = true;
        thread.Start();

        thread = new Thread(ProcessThread);
        thread.IsBackground = true;
        thread.Start();
    }

    public Connection(string Address, int Port, delVoidString DisconnectFunc, delVoidString ErrorFunc, delVoidLineSegment OutputLine)
    {
        try
        {
            connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        catch (Exception err)
        {
            ErrorOccurred(err.Message);
            return;
        }
            
        Disconnect = DisconnectFunc;
        ErrorOccurred = ErrorFunc;
        OutputLineSeg = OutputLine;

        EstablishConnection(Address, Port);
    }

    private void EstablishConnection(string Address, int Port)
    {
        if (connection != null)
        {
            try
            {
                connection.BeginConnect(Address, Port, sokConnectCallback, connection);
            }
            catch (Exception err)
            {
                ErrorOccurred(err.Message);
            }
        }
    }
    private void sokConnectCallback(IAsyncResult asyncResult)
    {
        Socket tempSok = (Socket)(asyncResult.AsyncState);
        try
        {
            tempSok.EndConnect(asyncResult);
        }
        catch (Exception err)
        {
            //Couldn't connect, tell the parent
            ErrorOccurred(err.Message);
            return;
        }

        //Spin up the receive and process threads
        Thread thread = new Thread(ThreadReading);
        thread.IsBackground = true;
        thread.Start();

        thread = new Thread(ProcessThread);
        thread.IsBackground = true;
        thread.Start();
    }

    private void ThreadReading()
    {
        byte[] buffer = new byte[50];
        MemoryStream ms = new MemoryStream();

        //Ensure that we don't timeout on receiving
        connection.ReceiveTimeout = 0;

        int iBytes;
        while (true)
        {
            iBytes = 0;

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

            //Add how many bytes we've received
            bytesRx += (uint)iBytes;

            //Started a receive event
            receiveEvents++;

            //Soft disconnection
            if (iBytes.Equals(0))
            {
                //Invoke handle loss of connection
                Disconnect("Soft Disconnection");
                return;
            }

            //Append new data to the end of out memory stream
            long pos = ms.Position;
            ms.Seek(0, SeekOrigin.End);
            ms.Write(buffer, 0, iBytes);
            ms.Position = pos;

            // attempt to extract one or more compete objects
            do
            {
                // save position in event that deserialization fails
                long lStartPos = ms.Position;

                try
                {
                    // pull an object from the stream
                    object o = bf.Deserialize(ms);
                    // add this frame to the queue of rx'ed frames
                    // assume another thread will process the frames

                    if(o is LineSegment)
                    {
                        lock (RxLineSegments)
                            RxLineSegments.Enqueue(o as LineSegment);

                        //Count as gotten a frame
                        framesRx++;
                    }
                    else
                    {
                        SoftDisconnect();
                        Disconnect("Wrong data type received.");
                    }
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    // error, so put the ms pointer back to where is started
                    ms.Position = lStartPos;

                    //Got a fragment
                    fragments++;

                    // exit loop, maybe next time there will be enough data
                    break;
                }
            }
            while (ms.Position < ms.Length);

            

            if (ms.Position.Equals(ms.Length))
            {
                ms.Position = 0;
                ms.SetLength(0);
            }
        }
    }

    private void ProcessThread()
    {
        while (true)
        {
            int objCount = 0;

            lock (RxLineSegments)
                objCount = RxLineSegments.Count;

            if(objCount == 0)
            {
                Thread.Sleep(3);
            }
            else
            {
                lock (RxLineSegments)
                {
                    OutputLineSeg(RxLineSegments.Dequeue());
                }
            }
        }
    }

    public void SendData(LineSegment ls)
    {
        try
        {
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, ls);
            connection.Send(ms.GetBuffer(), (int)ms.Length, SocketFlags.None);
        }
        catch
        {
            ErrorOccurred("Unable to send data");
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

    public Dictionary<string, uint> ConnectionData()
    {
        Dictionary<string, uint> output = new Dictionary<string, uint>();

        output["Frames"] = framesRx;
        output["Fragments"] = fragments;
        output["Receives"] = receiveEvents;
        output["Bytes"] = bytesRx;

        return output;
    }
}
