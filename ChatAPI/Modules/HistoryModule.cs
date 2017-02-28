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
            object[] args = JsonConvert.DeserializeObject<object[]>(request.args.ToString());
            DateTime last;
            switch (request.Cmd)
            {
                case "room":
                    string rstr = args[0] as string;
                    string time = args[1].ToString();
                    DateTime dt = DateTime.Parse(time);
                    //last = JsonConvert.DeserializeObject<DateTime>(time, new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy hh:mm:ss" });
                    ChatMessage[] h = Manager.FindRoom(rstr)?.GetMessageHistoryTo(dt);
                    if(h.Length > 0)
                    client.SendMessage(ResponseConstructor.GetRoomHistoryResponse(rstr, h));
                    break;
                case "private":
                    string user1 = (string)args[0];
                    string user2 = (string)args[1];
                    last = JsonConvert.DeserializeObject<DateTime>(args[2].ToString());
                    h = HistoryDataprovider.GetPrivateHistory(user1, user2, last);
                    if (h.Length > 0)
                        client.SendMessage(ResponseConstructor.GetRoomHistoryResponse(user2, h));
                    break;
                default: break;
            }

            return true;
        }
    }
}