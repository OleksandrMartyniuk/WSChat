
using AuthServer;
using ChatServer.Roles;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Login : ALogIn
    {
        public override bool Handle(IClientObject client, RequestObject request)
        {
            if(request.Module != "login")
            {
                return false;
            }

            object[] arg = JsonConvert.DeserializeObject<object[]>(request.args.ToString());
            Person user = new Person(arg[0].ToString(), arg[1].ToString(), null);

            string flag = new ApiAuth().api.LogIn(user);
            if (flag == "false")
            {
                client.SendMessage(ResponseConstructor.GetErrorNotification("Please check that you have entered your login and password correctly", "login"));
                return true;
            }
            client.Username = user.name;
            status(client);
            return true;
        }

      
    }
}
