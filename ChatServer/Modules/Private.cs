using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Private : IHandlerModule
    {
        public bool Handle(IClientObject client, RequestObject request)
        {
            if (request.Module != "private")
            {
                return false;
            }

            IClientObject recipient = Manager.FindClient(request.Cmd);
            if(recipient != null)
            {
                recipient.SendMessage(JsonConvert.SerializeObject(new RequestObject("private", null, request.args)));
            }
            return true;
        }
    }
}
