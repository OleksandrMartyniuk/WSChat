using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using AuthServer;

namespace ChatServer
{
    public class Facebook : LogInBase
    {
        public override bool Handle(IClientObject client, RequestObject request)
        {
            if (request.Module != "Facebook")
            {
                return false;
            }
            string name = request.Args.ToString();
           
            LogProvider.AppendRecord(string.Format("{0} loggin facebook user [{1}]", DateTime.Now.ToString(), name));
            client.Username = name;
            status(client);
            return true;
        }
    }
}