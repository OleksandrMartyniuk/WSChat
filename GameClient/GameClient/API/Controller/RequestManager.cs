using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Newtonsoft.Json;
using GameClient.API.Networking;

namespace GameClient
{
    public static class RequestManager
    {
        public static void SendRefreshClients()
        {
            Client.AddRequest(new RequestObject("Lobby", "refreshClients", "null"));
        }
        public static void SendMove(object[] args)
        {
            Client.AddRequest(new RequestObject("Game", "Move", args));
        }
    
    }
}
