using Core;
using GameClient.API.Networking;
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
        public Authorization()
        {
        }
        public void SendRegistration(string name, string password)
        {
            Client.AddRequest(new RequestObject("Auth", "Registration", new object[] { name, password }));
        }

        public void SendLogIn(string name, string password)
        {
            Client.AddRequest(new RequestObject("Auth", "LogIn", new object[] { name, password }));
        }
        public void SendLogout(object sender, EventArgs e)
        {
            Client.AddRequest(new RequestObject("Auth", "LogOut", "null"));
        }
        public void LoginGmail(string name)
        {
            Client.AddRequest(new RequestObject("login", "Gmail", name));
        }
        public void LoginFacebook(string name)
        {
            Client.AddRequest(new RequestObject("login", "Facebook", name));
        }
        public void LoginReg(string name, string password, string email)
        {
            Client.AddRequest(new RequestObject("login", "Registration",
                new object[] { name, password, email }));
        }
        public void LoginForgot(string name)
        {
            Client.AddRequest(new RequestObject("login", "Forgot", name));
        }
    }
}
