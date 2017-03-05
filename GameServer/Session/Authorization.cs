using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using System.Net.Mail;
using System.Net;
using AuthServer;
using Core;

namespace GameServer
{
    public class Authorization
    {
       
        private Clients clients { get; set; }
        private ApiAuth auth = new ApiAuth();
        
        private static string AuthFolder = "Users.json";

        public Authorization(Clients clients)
        {
            this.clients = clients;
        }

        public void Dispacher(Client client, RequestObject info)
        {
            switch (info.Cmd)
            {
                case "Registration": Registration(client, info.Args);   break;
                case "LogIn":        LogIn(client, info.Args);          break;
                case "LogOut":       LogOut(client);                    break;
                case "Forget":       ForgotPassword(client, info.Args); break;
                case "status":       LogIn(client, info.Args);          break;
                case "Facebook":     AuthFacebook(client,info.Args);    break;
            }
        }
        private void remove(Client client, string name)
        {
            for (int i = 0; i < clients.clientsList.Count; i++)
            {
                if (clients.clientsList[i].name == name)
                {
                    RemoveClient(clients.clientsList[i]);
                }
            }
        }
        private void AuthFacebook(Client client, object args)
        {
            string name = args.ToString();
            remove(client, name);
            LogProvider.AppendRecord(string.Format("{0} loggin facebook user [{1}]", DateTime.Now.ToString(), name));
            client.name = name;
            client.Write(new RequestObject("Auth", "LogIn", name));

        }
        private void Registration(Client client, object args)
        {
            object[] arg =  JsonConvert.DeserializeObject<object[]>(args.ToString());
            Person user = new Person(arg[0].ToString(), arg[1].ToString(), arg[2].ToString());
            string info = auth.api.Reg(user);
            Lobby.SendNotification(info, client);  
        }

        private void LogIn(Client client, object args)
        {
            object[] arg = JsonConvert.DeserializeObject<object[]>(args.ToString());
            Person user = new Person(arg[0].ToString(), arg[1].ToString(), null);
            remove(client, user.name);
            string flag = auth.api.LogIn(user);
            if (flag == "true")
            {
                client.name = user.name;
                client.Write(new RequestObject("Auth", "LogIn", user.name));
            }
            else
            {
                Lobby.SendNotification("Please check that you have entered your login and password correctly", client);
            }
        }
        public void ForgotPassword(Client client, object args)
        {
            bool flag = auth.api.ForgotPassword(args.ToString());
            if (flag == true)
            {
                client.Write(new RequestObject("Auth", "Forgot", "Success"));
            }
            else
            {
                client.Write(new RequestObject("Auth", "Forgot", "Error"));   
            }
        }
        private void LogOut(Client client)
        {
            client.name = null;
        }
        private void RemoveClient(Client client)
        {   
            clients.Dell(client);
        }
    }
}

