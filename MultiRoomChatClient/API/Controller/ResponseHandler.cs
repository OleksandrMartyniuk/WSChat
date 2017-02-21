using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Core;

namespace MultiRoomChatClient
{
    public static class ResponseHandler
    {
        static ResponseHandler()
        {
            Client.responseReceived += ProcessResponse;
        }

        public static void ProcessResponse(string Json)
        {
            
            //Console.WriteLine(Json); 
            RequestObject req = JsonConvert.DeserializeObject<RequestObject>(Json);

            switch (req.Module)
            {
                case "admin":
                    switch (req.Cmd)
                    {
                        case "ban":
                            Banned?.Invoke();
                            break;
                        case "unban":
                            Unbanned?.Invoke();
                            break;
                    }
                    break;
                case "info":
                    if (req.Cmd == "all")
                    {
                        RoomObj[] rooms = JsonConvert.DeserializeObject<RoomObj[]>(req.args.ToString());
                        if (rooms.Length > 0)
                        {
                            roomDataReceived(rooms);//JsonConvert.DeserializeObject<RoomObj[]>((string)req.args));
                        }
                    }
                    break;
                case "login":
                    switch (req.Cmd)
                    {
                        case "ok":
                            loginSuccessfull?.Invoke((string)req.args);
                            break;
                        case "admin":
                            loggedAsAdmin?.Invoke((string)req.args);
                            break;
                        case "banned":
                            loggedBanned?.Invoke((string)req.args);
                            break;
                        default:
                            loginFail?.Invoke((string)req.args);
                            break;
                    }
                    break;
                case "msg":
                    switch (req.Cmd)
                    {
                        case "msg":
                            object[] args = JsonConvert.DeserializeObject<object[]>(req.args.ToString());
                            messageRecieived?.Invoke((string)args[0], JsonConvert.DeserializeObject<ChatMessage>(args[1].ToString()));
                            //messageRecieived?.Invoke(JsonConvert.DeserializeObject<ChatMessage>(req.args.ToString()));
                            break;
                        case "active":
                            args = JsonConvert.DeserializeObject<object[]>(req.args.ToString());
                            msgDataReceived?.Invoke((string)args[0], JsonConvert.DeserializeObject<ChatMessage[]>(args[1].ToString()));
                            break;
                        case "notify":
                            notificationReceived?.Invoke((string)req.args);
                            break;
                        case "entered":
                            args = JsonConvert.DeserializeObject<string[]>(req.args.ToString());
                            UserEntered?.Invoke((string)args[0], (string)args[1]);
                            break;
                        case "left":
                            args = JsonConvert.DeserializeObject<string[]>(req.args.ToString());
                            UserLeft?.Invoke((string)args[0], (string)args[1]);
                            break;
                    }
                    break;
                case "private":
                    privateMessageReceived?.Invoke(JsonConvert.DeserializeObject<ChatMessage>(req.args.ToString()));
                    break;
                case "room":
                    switch (req.Cmd)
                    {
                        case "created":
                            roomCreated?.Invoke((string)req.args);
                            break;
                        case "removed":
                            roomRemoved?.Invoke((string)req.args);
                            break;
                        default:
                            roomError?.Invoke((string)req.args);
                            break;
                    }
                    break;
            }
        }

        public delegate void adminDelegate();
        public static event adminDelegate Banned;
        public static event adminDelegate Unbanned;

        public delegate void roomDataDelegate(RoomObj[] rooms);
        public static event roomDataDelegate roomDataReceived;

        public delegate void loginDelegate(string username);
        public static event loginDelegate loginSuccessfull;
        public static event loginDelegate loggedAsAdmin;
        public static event loginDelegate loggedBanned;
        public static event loginDelegate loginFail;

        public delegate void messageDelegate(string room, ChatMessage msg);
        public delegate void notificationDelegate(string room);
        public delegate void dataDelegate(string room, ChatMessage[] msg);
        public delegate void userMovedDelegate(string username, string room);

        public static event messageDelegate messageRecieived;
        public static event notificationDelegate notificationReceived;
        public static event dataDelegate msgDataReceived;
        public static event userMovedDelegate UserEntered;
        public static event userMovedDelegate UserLeft;

        public delegate void pmDelegate(ChatMessage msg);
        public static event pmDelegate privateMessageReceived;

        public delegate void roomDelegate(string roomName);
        public static event roomDelegate roomCreated;
        public static event roomDelegate roomRemoved;
        public static event roomDelegate roomError;
    }
}