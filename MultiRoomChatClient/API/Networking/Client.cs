using Core;
using MultiRoomChatClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiRoomChatClient
{
    public static class Client
    {
        public static string Username { get; set; }
        static TcpClient client;
        static NetworkStream stream;
        static LinkedList<string> messageQue = new LinkedList<string>();
        static Thread processThread = null;
        static bool working;
        public static HistoryDataprovider RoomHistory = new HistoryDataprovider("Msg");
        public static HistoryDataprovider PrivateHistory = new HistoryDataprovider("Private");

        public delegate void responseHandler(string json);
        public static event responseHandler responseReceived;

        public delegate void errorMessage(string msg);
        public static event errorMessage NewErrorMessage;

        public static void StartClient()
        {
            Client.Start("93-127-118-24.static.vega-ua.net", 8080);
        }

        static void Start(string host, int port)
        {
            if (client == null || stream == null)
            {
                client = new TcpClient();
            }
            else
            {
                return;
            }

            client.Connect(host, port);
            stream = client.GetStream();
            processThread = new Thread(new ThreadStart(Process));
            processThread.IsBackground = true;
            working = true;
            processThread.Start();
        }
        public static void AddRequest(string message)
        {
            messageQue.AddLast(message + Environment.NewLine);
        }

        static void WriteStream()
        {
            if (messageQue.Count > 0)
            {

                StreamWriter sw = new StreamWriter(stream);
                while (messageQue.Count > 0)
                {
                    string message = messageQue.First.Value;

                    sw.WriteLine(message);
                    sw.Flush();

                    messageQue.RemoveFirst();
                }
            }
        }

        private static void ReadStream()
        {
            StreamReader reader = new StreamReader(stream);
            string response = "";
            while (stream.DataAvailable)
            {
                response += reader.ReadLine();
            }
            if(response != null && response.Length > 0)
            responseReceived(response);
        }
        
        static void Process()
        {
            while (working)
            {
                try
                {
                    ReadStream();

                    WriteStream();

                    Thread.Sleep(20);
                }
                catch (Exception e)
                {
                    NewErrorMessage?.Invoke("Подключение прервано!");
                    Disconnect();
                }
            }
        }

        public static void Disconnect()
        {
            if (stream != null)
            {
                working = false;
                WriteStream();
                stream.Close();//отключение потока
                stream.Dispose();
            }
            if (client != null)
            {
                client.Close();//отключение клиента
                client = null;
            }
               
        }
    }
}
