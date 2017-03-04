using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core;
using System.Threading.Tasks;

namespace GameServer
{
    public class Lobby
    {
        public void Dispacher(Client client, RequestObject info, List<Client> clientsList)
        {
            switch (info.Cmd)
            {
                case "refreshClients": SendClients(client, clientsList);  break;
            }
        }
        public static void SendClients(Client client, List<Client> clientsList)
        {
            List<string> str = new List<string>();
            foreach (var item in clientsList)
            {
                if (item == client || item.name == "" || item.inGame==true || item.name==null)
                    continue;
                 str.Add(item.name);
            }
            client.Write(new RequestObject("Lobby", "refreshClients", str));
        }
        public static void SendNotification(string notification, Client client)
        {
            client.Write(new RequestObject("Lobby", "Notification", notification));
        }
    }
}
