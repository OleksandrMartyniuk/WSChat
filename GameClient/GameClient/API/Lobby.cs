using Core;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    public class Lobby
    {
        public Lobby()
        {

        }
        
        public EventHandler RefreshClients;

        public EventHandler Notification;

        public void Dispacher(RequestObject tmpinfo)
        {
            switch (tmpinfo.Cmd)
            {
                case "refreshClients": RefreshClients(tmpinfo.Args, null); break;
                case "Notification":   Notification(tmpinfo.Args, null);   break;
            }
        }

        public void SendRefreshClients()
        {
            Client.SendMessage(new RequestObject("Lobby", "refreshClients", "null"));
        }
    }
}
