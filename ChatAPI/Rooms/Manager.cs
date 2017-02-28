using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public static class Manager
    {
        public static RoomObject Host { get; set; }
        public static LinkedList<RoomObject> Rooms = new LinkedList<RoomObject>();
        public static LinkedList<object> Clients = new LinkedList<object>();

        static Manager()
        {
            CreateRoom("Host");
            Host = Rooms.First.Value;
        }

        public static IClientObject FindClient(string Name)
        {
            foreach (IClientObject client in Clients)
            {
                if (client.Username == Name)
                {
                    return client;
                }
            }
            return null;
        }

        public static void AddClient(IClientObject client)
        {
            Clients.AddLast(client);
        }
        
        public static RoomObject FindRoom(string name)
        {
            if(name == null || name == "Host")
            {
                return Host;
            }

            foreach(RoomObject r in Rooms)
            {
                if(r.Name == name)
                {
                    return r;
                }
            }
            return null;
        }

        public static void CreateRoom(string roomName)
        {
            if(FindRoom(roomName) == null)
            {
                RoomObject room = new RoomObject(roomName);
                Rooms.AddLast(room);
                OnRoomCreated(roomName);
                room.NewMessage += HistoryDataprovider.AppendMessage;
                room.ClientAdded += OnClientAdded;
                room.ClientRemoved += OnClientLeft;
            }
        }

        public static void CloseRoom(string roomName)
        {
            RoomObject room = FindRoom(roomName);
            if(room != null )
            {
                Rooms.Remove(room);
                room.NewMessage -= HistoryDataprovider.AppendMessage;
                OnRoomDeleted(roomName);
            }
        }

        public static void BroadcastAll(string message)
        {
            foreach (IClientObject client in Clients)
            {
                 client.SendMessage(message); // remove admin - message  //////////
            }
        }

        public static void OnClientAdded(string room, string username)
        {
            BroadcastAll(ResponseConstructor.GetUserEnteredNotification(room, username));
        }

        public static void UserDisconnect(string username)
        {
            foreach(RoomObject r in Rooms)
            {
                r.RemoveListener(username);
            }
            foreach(IClientObject client in Clients)
            {
                if(client.Username == username)
                {
                    Clients.Remove(client);
                    break;
                }
            }
        }

        public static void OnClientLeft(string room, string username)
        {
            BroadcastAll(ResponseConstructor.GetUserLeftNotification(room, username));
        }

        public static void OnRoomCreated(string room)
        {
            BroadcastAll(ResponseConstructor.GetRoomCreatedNotification(room));
        }

        public static void OnRoomDeleted(string room)
        {
            BroadcastAll(ResponseConstructor.GetUserLeftNotification(room));
        }

        public static RoomObj[] GetAllInfo()
        {
            List<RoomObj> info = new List<RoomObj>();
            foreach(RoomObject room in Rooms)
            {
                info.Add(room.GetRoomObj());
            }
            return info.ToArray();
        }
    }
}
