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
        
        public static void Login(string key)
        {
            Client.AddRequest(new RequestObject("auth", "in", key));
        }
        //public static void LoginGmail(string name)
        //{
        //    Client.AddRequest(new RequestObject("login", "Gmail",  name ));
        //}
        //public static void LoginFacebook(string name)
        //{
        //    Client.AddRequest(new RequestObject("login", "Facebook", name));
        //}
        //public static void LoginReg(string name, string password, string email)
        //{
        //    Client.AddRequest(new RequestObject("login", "Registration", new object[] { name, password, email }));
        //}
        //public static void LoginForgot(string name)
        //{
        //    Client.AddRequest(new RequestObject("login", "Forgot", name));
        //}
        
        public static void Logout(string name)
        {
            Client.AddRequest(new RequestObject("logout", null, name));
        }

        public static void SendMessage(string msg, string room)
        {
            Client.AddRequest(new RequestObject("msg", "msg", 
                new object[] { room, new ChatMessage(Client.Username, msg, DateTime.Now) }));
        }

        public static void SetActiveRoom(string room)
        {
            Client.AddRequest(new RequestObject("msg", "active", room));
        }

        public static void LeaveRoom(string room)
        {
            Client.AddRequest(new RequestObject("msg", "leave", room));
        }

        public static void CreateRoom(string roomName)
        {
            Client.AddRequest(new RequestObject("room", "create", roomName));
        }

        public static void CloseRoom(string roomName)
        {
            Client.AddRequest(new RequestObject("room", "close", roomName));
        }

        public static void RequestData()
        {
                 
            Client.AddRequest(new RequestObject("info", "get", "null"));
        }

        public static void RequestMessageList(string room, DateTime last)
        {
            Client.AddRequest(new RequestObject("history", "room", new object[] { room, last }));
        }

        public static void RequestPrivateMessageList(string username, DateTime last)
        {
            Client.AddRequest(new RequestObject("history", "private", 
                new object[] { Client.Username, username, last }));
        }

        public static void SendPrivateMessage(string userName, ChatMessage msg)
        {
            Client.AddRequest(new RequestObject("private", userName, msg));
        }

        public static void AdminBan(string userName, DateTime exp)
        {         
            Client.AddRequest(new RequestObject("admin", "ban", new object[] { userName, exp }));
        }

        public static void AdminBanEternal(string userName)
        {
            Client.AddRequest(new RequestObject("admin", "ban", 
               new object[] { userName, DateTime.MaxValue }));
        }
        public static void AdminUnban(string userName)
        {
            Client.AddRequest(new RequestObject("admin", "unban", userName));
        }
    }
}
