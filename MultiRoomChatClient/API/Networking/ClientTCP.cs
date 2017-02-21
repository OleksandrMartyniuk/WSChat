using Core;
using MultiRoomChatClient;
using MultiRoomChatClient.API.Networking;
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
    public class ClientTCP : IClient
    {
        TcpClient client;
        NetworkStream stream;
        LinkedList<string> messageQue = new LinkedList<string>();
        Thread processThread = null;
        bool working;

        public event responseHandler responseReceived;
        public event errorMessage NewErrorMessage;

        public void StartClient()
        {
            Start("93-127-118-24.static.vega-ua.net", 8080);
        }

        void Start(string host, int port)
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
        public void AddRequest(string message)
        {
            messageQue.AddLast(message + Environment.NewLine);
        }

        void WriteStream()
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

        private void ReadStream()
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
        
        void Process()
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

        public void Disconnect()
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
