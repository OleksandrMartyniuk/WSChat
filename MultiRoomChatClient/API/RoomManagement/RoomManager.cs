using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRoomChatClient
{
    public class RoomManager
    {
        public LinkedList<RoomObjExt> Rooms = new LinkedList<RoomObjExt>();

        public delegate void refreshDelegate();
        public event refreshDelegate MessageListUpdated;
        public event refreshDelegate RoomDataUpdated;

        public RoomManager()
        {
            HistoryDataprovider HistoryProvider = new HistoryDataprovider("Msg");  

            ResponseHandler.roomDataReceived += onRoomDataReceived;
            ResponseHandler.UserEntered += AddUser;
            ResponseHandler.UserLeft += RemoveUser;
            RequestManager.RequestData();
            ResponseHandler.roomCreated += (x) => AddRoom(new RoomObj(x));
            ResponseHandler.roomRemoved += (x) => RemoveRoom(new RoomObj(x));
        }

        private void RemoveUser(string room, string username)
        {
            FindRoom(room)?.clients.Remove(username);
            RoomDataUpdated?.Invoke();
        }

        private void AddUser(string room, string username)
        {
            FindRoom(room)?.clients.Add(username);
            RoomDataUpdated?.Invoke();
        }

        public RoomObjExt FindRoom(string name)
        {
            RoomObjExt room = null;
            foreach (RoomObjExt r in Rooms)
            {
                if(r.Name == name)
                {
                    room = r;
                    break;
                }
            }
            return room;
        }

        public void CreateRoom(string room)
        {
            RoomObjExt r = new RoomObjExt(room);
            AddRoom(r);
            RequestManager.CreateRoom(room);
            r.OnDataReceived(Client.RoomHistory.GetHistory(room));
            MessageListUpdated?.Invoke();
            RoomDataUpdated?.Invoke();
        }

        private void AddRoom(RoomObj room)
        {
            if (!Rooms.Contains(room))
            {
                Rooms.AddLast(new RoomObjExt(room));
                RoomDataUpdated?.Invoke();

            }
        }

        private void RemoveRoom(RoomObj room)
        {
            if (!Rooms.Contains(room))
            {
                Rooms.Remove(new RoomObjExt(room));
                RoomDataUpdated?.Invoke();
            }
        }

        private void onRoomDataReceived(RoomObj[] rooms)
        {
            Rooms.Clear();
            foreach (RoomObj room in rooms)
            {
                Rooms.AddLast(new RoomObjExt(room));
            }
            RoomDataUpdated?.Invoke();
        }

    }
}
