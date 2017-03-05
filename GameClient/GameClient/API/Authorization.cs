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
    class Authorization
    {
        public Client client;
      
        
        public EventHandler LogIn;

        public Authorization(Client client)
        {
            this.client = client;
            
        }

        public void Dispacher(RequestObject info)
        {
            switch (info.Cmd)
            {
                case "LogIn": LogIn(info.Args.ToString(), null); break;
            }
        }
      
        public void SendRegistration(string name, string password)
        {
            Listener.SendMessage(new RequestObject("Auth", "Registration", new object[] { name, password }));
        }

        public void SendLogIn(string name, string password)
        {
            Listener.SendMessage(new RequestObject("Auth", "LogIn", new object[] { name, password }));
        }
        public void SendLogout(object sender, EventArgs e)
        {
            Listener.SendMessage(new RequestObject("Auth", "LogOut", "null"));
        }
    }
}
