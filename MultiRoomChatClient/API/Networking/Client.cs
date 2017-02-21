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
            switch (Protocol.type)
            {
                case ProtocolTypes.TCP:
                    ConnectionClient = new ClientTCP();
                    break;
                case ProtocolTypes.WebSocket:
                    ConnectionClient = new WSClient();
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
