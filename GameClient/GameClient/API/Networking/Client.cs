using Core;
using GameClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameClient.API.Networking
{
    public static class Client
    {
        public static string Username { get; set; }
        static IClient ConnectionClient = null;

        public static event responseHandler responseReceived;
        public static event errorMessage NewErrorMessage;

        static Client()
        {
            ConnectionClient = new WSClient("ws://127.0.0.1:9898");
            ConnectionClient.NewErrorMessage += (x) => NewErrorMessage?.Invoke(x);
            ConnectionClient.responseReceived += (x) => responseReceived?.Invoke(x);
        }

        public static void StartClient()
        {
            ConnectionClient.StartClient();
        }

        public static void AddRequest(RequestObject message)
        {
            ConnectionClient.AddRequest(JsonConvert.SerializeObject(message));
        }

        public static void Disconnect()
        {
            ConnectionClient.Disconnect();
        }
    }
}
