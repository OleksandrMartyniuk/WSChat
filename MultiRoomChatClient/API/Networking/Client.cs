using Core;
using MultiRoomChatClient;
using MultiRoomChatClient.API.Networking;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        static IClient ConnectionClient = null;
        
        public static HistoryDataprovider RoomHistory = new HistoryDataprovider("Msg");
        public static HistoryDataprovider PrivateHistory = new HistoryDataprovider("Private");

        public static event responseHandler responseReceived;
        public static event errorMessage NewErrorMessage;

        static Client()
        {
            string protocol = ConfigurationManager.AppSettings["protocol"];
            switch (protocol)
            {
                case "tcp":
                    ConnectionClient = new ClientTCP();
                    break;
                case "ws":
                    string uri = ConfigurationManager.AppSettings["wsuri"];
                    ConnectionClient = new WSClient(uri);
                    break;
                default: break;
            }
            ConnectionClient.NewErrorMessage += (x) => NewErrorMessage.Invoke(x);
            ConnectionClient.responseReceived += (x) => responseReceived.Invoke(x);
        }

        public static void StartClient()
        {
            ConnectionClient.StartClient();
        }

        public static void AddRequest(string message)
        {
            ConnectionClient.AddRequest(message);
        }

        public static void Disconnect()
        {
            ConnectionClient.Disconnect();
        }
    }
}
