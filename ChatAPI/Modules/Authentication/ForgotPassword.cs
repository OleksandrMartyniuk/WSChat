using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServer
{
    public class ForgotPassword : IHandlerModule
    {
        public bool Handle(IClientObject client, RequestObject request)
        {
            if (request.Module != "Forgot")
            {
                return false;
            }
            bool flag = false;//new ApiAuth().api.ForgotPassword(request.Args.ToString());
            if (flag == true)
            {
                client.SendMessage(ResponseConstructor.GetErrorNotification("Success", "login"));
            }
            else
            {
                client.SendMessage(ResponseConstructor.GetErrorNotification("Error", "login"));
            }
            return true;
        }
    }
}