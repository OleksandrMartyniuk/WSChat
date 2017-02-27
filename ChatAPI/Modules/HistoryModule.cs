using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using Newtonsoft.Json;

namespace ChatServer
{
    public class HistoryModule : IHandlerModule
    {
        public bool Handle(IClientObject client, RequestObject request)
        {
            if (request.Module != "history")
            {
                return false;
            }
            object[] args = JsonConvert.DeserializeObject<object[]>(request.args.ToString());
            DateTime last;
            switch (request.Cmd)
            {
                case "room":
                    string rstr = args[0] as string;
                    last = JsonConvert.DeserializeObject<DateTime>(args[1].ToString());
                    Manager.FindRoom(rstr)?.GetMessageHistoryTo(last);
                    break;
                case "private":
                    string user1 = (string)args[0];
                    string user2 = (string)args[1];
                    last = JsonConvert.DeserializeObject<DateTime>(args[2].ToString());
                    HistoryDataprovider.GetPrivateHistory(user1, user2, last);
                    break;
                default: break;
            }

            return true;
        }
    }
}