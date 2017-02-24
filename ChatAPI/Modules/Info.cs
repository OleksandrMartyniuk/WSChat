using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Info : IHandlerModule
    {
        public bool Handle(IClientObject client, RequestObject request)
        {
            if(request.Module != "info")
            {
                return false;
            }
            var arg = Manager.GetAllInfo();
            RequestObject robj = new RequestObject("info", "all", arg);
            client.SendMessage(JsonConvert.SerializeObject(robj));
            LogProvider.AppendRecord(string.Format("{0} [{1}]: requested info {2}", DateTime.Now.ToString(), client.Username));
            return true;
        }
    }
}
