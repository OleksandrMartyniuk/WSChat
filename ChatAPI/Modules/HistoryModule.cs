using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            object[] args = JsonConvert.DeserializeObject<object[]>(request.Args.ToString());
            DateTime last;
            switch (request.Cmd)
            {
                case "room":
                    string rstr = args[0] as string;
                    string time = args[1].ToString();
                    last = DateTime.Parse(time).ToUniversalTime();
                    ChatMessage[] h = Manager.FindRoom(rstr)?.GetMessageHistoryTo(last);
                    client.SendMessage(ResponseConstructor.GetRoomHistoryResponse(rstr, h));
                    break;
                case "private":
                    string user1 = (string)args[0];
                    string user2 = (string)args[1];
                    time = args[2].ToString();
                    last = DateTime.Parse(time).ToUniversalTime();
                    h = HistoryDataprovider.GetPrivateHistory(user1, user2, last);
                    client.SendMessage(ResponseConstructor.GetPrivateHistoryResponse(user2, h));
                    break;
                default: break;
            }

            return true;
        }
    }
}