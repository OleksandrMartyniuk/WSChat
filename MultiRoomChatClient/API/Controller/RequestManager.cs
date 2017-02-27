using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Newtonsoft.Json;

namespace MultiRoomChatClient
{
    public static class RequestManager
    {
        
        public static void Login(string name,string password)
        {
            Client.AddRequest(JsonConvert.SerializeObject(new RequestObject("login", "in", new object[] { name, password })));
        }

        public static void Logout(string name)
        {
            Client.AddRequest(JsonConvert.SerializeObject(new RequestObject("logout", null, name)));
        }

        public static void SendMessage(string msg, string room)
        {
            RequestObject reqObj = new RequestObject("msg", "msg", new object[] { room, new ChatMessage(Client.Username, msg, DateTime.Now) });
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));
        }

        public static void SetActiveRoom(string room, object args)
        {
            RequestObject reqObj = new RequestObject("msg", "active", args);
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));
        }

        public static void LeaveRoom(string room)
        {
            RequestObject reqObj = new RequestObject("msg", "leave", room);
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));
        }

        public static void CreateRoom(string roomName)
        {
            RequestObject reqObj = new RequestObject("room", "create", roomName);
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));
        }

        public static void CloseRoom(string roomName)
        {
            RequestObject reqObj = new RequestObject("room", "close", roomName);
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));
        }

        public static void RequestData()
        {
            RequestObject msg = new RequestObject("info","get",null);           
            Client.AddRequest(JsonConvert.SerializeObject(msg));
        }

        public static void RequestMessageList(string room, DateTime last = default(DateTime))
        {
            RequestObject msg = new RequestObject("history", "room", new object[] { room, last });
            Client.AddRequest(JsonConvert.SerializeObject(msg));
        }

        public static void RequestPrivateMessageList(string room, DateTime last = default(DateTime))
        {
            RequestObject msg = new RequestObject("history", "private", new object[] { room, last });
            Client.AddRequest(JsonConvert.SerializeObject(msg));
        }

        public static void SendPrivateMessage(string userName, ChatMessage msg)
        {
            RequestObject message = new RequestObject("private", userName, msg);
            Client.AddRequest(JsonConvert.SerializeObject(message));
        }

        public static void AdminBan(string userName, DateTime exp)
        {
            if (userName == null || userName == "")
                return;

            RequestObject message = new RequestObject("admin", "ban", new object[] { userName, exp });
            Client.AddRequest(JsonConvert.SerializeObject(message));
        }

        public static void AdminBanEternal(string userName)
        {
            if (userName == null || userName == "")
                return;

            RequestObject message = new RequestObject("admin", "ban", new object[] { userName, DateTime.MaxValue });
            Client.AddRequest(JsonConvert.SerializeObject(message));
        }
        public static void AdminUnban(string userName)
        {
            if (userName == null || userName == "")
                return;

            RequestObject message = new RequestObject("admin", "unban", userName);
            Client.AddRequest(JsonConvert.SerializeObject(message));
        }
    }
}
