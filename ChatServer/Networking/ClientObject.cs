using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using Core;
using ChatServer.Roles;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatServer
{
    public class ClientObject: IClientObject
    {
        private TcpClient Client;
        private NetworkStream Stream;
        private Thread WorkerThread;

        public ClientObject(TcpClient tcpClient)
        {
            this.Client = tcpClient;
            Stream = tcpClient.GetStream();
            this.Role = new UnknownUser(this);
            MessageRecieved += Role.Handle;
        }

        public override void Start()
        {
            WorkerThread = new Thread(new ThreadStart(Process));
            WorkerThread.Start();
        }

        private void Process()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        StreamReader sr = new StreamReader(Stream);
                        string message = sr.ReadLine();

                        if (message != null && message.Length > 0)
                        {
                            Console.WriteLine(message);
                            RaiseMessageReceived(this, message);
                        }
                        Thread.Sleep(10);
                    }
                    catch (Exception e)
                    {
                        Manager.UserDisconnect(Username);
                        string message = String.Format("{0}: покинул чат", Username);
                        Console.WriteLine(message);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                
                Close();
            }
        }

        public override void SendMessage(string message)
        {
            if (Stream == null)
            {
                return;
            }
           
            StreamWriter sw = new StreamWriter(Stream);

            sw.WriteLine(message + '\n');
            sw?.Flush();

            //    Console.WriteLine(e.Message);
           
            Console.WriteLine(this.Username + ": " + message);
        }

        public override void Close()
        {
            Manager.UserDisconnect(Username);
            //WorkerThread?.Abort();
           /* if (Stream != null)
                Stream.Close();
            if (Client != null)
                Client.Close();*/
        }
    }
}