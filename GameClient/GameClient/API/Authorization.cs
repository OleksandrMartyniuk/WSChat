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
    public class Authorization
    {
      
        public EventHandler LogIn;

        public Authorization()
        {
           
        }

        public void Dispacher(RequestObject info)
        {
            switch (info.Cmd)
            {
                case "LogIn": LogIn(info.Args, null); break;
            }
        }
      
        public void SendRegistration(string name, string password)
        {
            Client.SendMessage(new RequestObject("Auth", "Registration", new object[] { name, password }));
        }

        public void SendLogIn(string name, string password)
        {
            Client.SendMessage(new RequestObject("Auth", "LogIn", new object[] { name, password }));
        }
        public void SendLogout(object sender, EventArgs e)
        {
            Client.SendMessage(new RequestObject("Auth", "LogOut", "null"));
        }
        public void LoginGmail(string name)
        {
            Client.SendMessage(new RequestObject("login", "Gmail", name));
        }
        public void LoginFacebook(string name)
        {
            Client.SendMessage(new RequestObject("login", "Facebook", name));
        }
        public void LoginReg(string name, string password, string email)
        {
            Client.SendMessage(new RequestObject("login", "Registration",
                new object[] { name, password, email }));
        }
        public void LoginForgot(string name)
        {
            Client.SendMessage(new RequestObject("login", "Forgot", name));
        }
    }
}
