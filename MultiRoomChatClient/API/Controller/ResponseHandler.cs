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

        public static bool active = true;

        public static void ProcessResponse(string Json)
        {
            if (!active)
            {
                return;
            }
            RequestObject req = JsonConvert.DeserializeObject<RequestObject>(Json);

            switch (req.Module)
            {
                case "admin":
                    switch (req.Cmd)
                    {
                        case "ban":
                            string msg = "You got banned ";
                            if (req.Args != null)
                            {
                                msg += "till " + req.Args.ToString();
                            }
                            else
                            {
                                msg += "permanently";
                            }
                            Banned?.Invoke(msg);
                            
                            break;
                        case "unban":
                            msg = "You got Unbanned";
                            Unbanned?.Invoke(msg);
                            break;
                    }
                    break;
                case "info":
                    if (req.Cmd == "all")
                    {
                        RoomObj[] rooms = JsonConvert.DeserializeObject<RoomObj[]>(req.Args.ToString());
                        if (rooms.Length > 0)
                        {
                            roomDataReceived(rooms);
                        }
                    }
                    break;
                case "login":
                    switch (req.Cmd)
                    {
                        case "user":
                            loginSuccessfull?.Invoke((string)req.Args);
                            break;
                        case "admin":
                            loggedAsAdmin?.Invoke((string)req.Args);
                            break;
                        case "banned":
                            loggedBanned?.Invoke((string)req.Args);
                            break;
                        default:
                            loginFail?.Invoke((string)req.Args);
                            break;
                    }
                    break;
                case "msg":
                    switch (req.Cmd)
                    {
                        case "msg":
                            object[] args = JsonConvert.DeserializeObject<object[]>(req.Args.ToString());
                            messageRecieived?.Invoke((string)args[0], JsonConvert.DeserializeObject<ChatMessage>(args[1].ToString()));
                            break;
                        case "active":
                            args = JsonConvert.DeserializeObject<object[]>(req.Args.ToString());

                            RoomHistoryReceived?.Invoke((string)args[0], JsonConvert.DeserializeObject<ChatMessage[]>(args[1].ToString()));
                            break;
                        case "notify":
                            notificationReceived?.Invoke((string)req.Args);
                            break;
                        case "entered":
                            args = JsonConvert.DeserializeObject<string[]>(req.Args.ToString());
                            UserEntered?.Invoke((string)args[0], (string)args[1]);
                            break;
                        case "left":
                            args = JsonConvert.DeserializeObject<string[]>(req.Args.ToString());
                            UserLeft?.Invoke((string)args[0], (string)args[1]);
                            break;
                    }
                    break;
                case "private":
                    privateMessageReceived?.Invoke(JsonConvert.DeserializeObject<ChatMessage>(req.Args.ToString()));
                    break;
                case "room":
                    switch (req.Cmd)
                    {
                        case "created":
                            Dictionary<string, string> kv_args = JsonConvert.DeserializeObject<Dictionary<string,string>>(req.Args.ToString());
                            string roomname = kv_args["room"];
                            string creator = kv_args["creator"];
                            roomCreated?.Invoke(roomname, creator);
                            break;
                        case "removed":
                            roomRemoved?.Invoke((string)req.Args);
                            break;
                        default:
                            roomError?.Invoke((string)req.Args);
                            break;
                    }
                    break;
                case "history":
                    switch (req.Cmd)
                    {
                        case "room":
                            object[] args = JsonConvert.DeserializeObject<object[]>(req.Args.ToString());
                            ChatMessage[] history = JsonConvert.DeserializeObject<ChatMessage[]>(args[1].ToString());
                            RoomHistoryReceived?.Invoke((string)args[0], history);
                            break;
                        case "private":
                            args = JsonConvert.DeserializeObject<object[]>(req.Args.ToString());
                            string user = args[0].ToString();
                            history = JsonConvert.DeserializeObject<ChatMessage[]>(args[1].ToString());
                            PrivateHistoryReceived?.Invoke(user, history);
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

        public delegate void adminDelegate(string msg);
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
        public static event userMovedDelegate UserEntered;
        public static event userMovedDelegate UserLeft;

        public delegate void pmDelegate(ChatMessage msg);
        public static event pmDelegate privateMessageReceived;

        public delegate void roomDelegate(string roomName);
        public delegate void roomCreatedDelegate(string roomName, string creator);
        public static event roomCreatedDelegate roomCreated;
        public static event roomDelegate roomRemoved;
        public static event roomDelegate roomError;

        public delegate void RoomHistoryDelegate(string room, ChatMessage[] msgs);
        public delegate void PrivateHistoryDelegate(string user, ChatMessage[] msgs);
        public static event RoomHistoryDelegate RoomHistoryReceived;
        public static event PrivateHistoryDelegate PrivateHistoryReceived;
    }
}