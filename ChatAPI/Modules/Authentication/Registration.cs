using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using Newtonsoft.Json;
using AuthServer;

namespace ChatServer
{
    public class Registration : IHandlerModule
    {
        public bool Handle(IClientObject client, RequestObject request)
        {
            if (request.Module != "Registration")
            {
                return false;
            }

            object[] arg = JsonConvert.DeserializeObject<object[]>(request.Args.ToString());
            Person user = new Person(arg[0].ToString(), arg[1].ToString(), arg[2].ToString());
            string info = new ApiAuth().api.Reg(user);
            client.SendMessage(ResponseConstructor.GetErrorNotification(info, "login"));
            
            return true;
        }
    }
}