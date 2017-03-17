using ChatServer.Roles;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class RoomObject
    {
        public readonly string Name;

        public LinkedList<ChatMessage> Messages = new LinkedList<ChatMessage>();
        public LinkedList<RoomObserverBase> Clients = new LinkedList<RoomObserverBase>();

        public delegate void playerListDel(string room, string name);
        public delegate void messageDelegate(string Name, ChatMessage msg);

        public event playerListDel ClientAdded;
        public event playerListDel ClientRemoved;

        public event messageDelegate NewMessage;

        public RoomObject(string name)
        {
            Name = name;
            var history = HistoryDataprovider.GetHistory(name);

            if (history != null)
                foreach (ChatMessage msg in history)
                {
                    Messages.AddLast(msg);
                }
        }

        public RoomObj GetRoomObj()
        {
            RoomObj res = new RoomObj();
            res.Name = this.Name;
            List<string> users = new List<string>();
            foreach(RoomObserverBase clnt in Clients)
            {
                if(clnt.client.Role.GetType() != typeof(Admin))
                users.Add(clnt.client.Username);
            }
            res.clients = users;
            return res;
        }

        public ChatMessage[] GetMessageHistoryTo(DateTime time)
        {

            LinkedList<ChatMessage> msgs = new LinkedList<ChatMessage>();

            LinkedListNode<ChatMessage> next = Messages.Last;

            while( next != null && next.Value.TimeStamp >= time)// Messages.First)
            {
                next = next.Previous;
            }

            if (next == null)
            {
                return msgs.ToArray();
            }

            LinkedListNode<ChatMessage> current = next;

            for (int i = 0; i <= 10; i++)
            {
                if (current != null)
                {
                    msgs.AddFirst(current.Value);
                }
                else
                {
                    break;
                }
                current = current.Previous;
            }

            return msgs.ToArray();
        }

        public void AddListener(RoomObserverBase observer)
        {
            if (!Clients.Contains(observer))
            {
                Clients.AddLast(observer);
                if(observer.client.Role.GetType() != typeof(Admin))
                ClientAdded?.Invoke(Name, observer.client.Username);
            }
        }

        public void RemoveListener(RoomObserverBase observer)
        {
            if (Clients.Contains(observer))
            {
                Clients.Remove(observer);
                ClientRemoved?.Invoke(Name, observer.client.Username);
            }
        }

        public void RemoveListener(string observer)
        {
            foreach(RoomObserverBase ro in Clients)
            {
                if(ro.client.Username == observer)
                {
                    Clients.Remove(ro);
                    ClientRemoved?.Invoke(Name, ro.client.Username);
                    break;
                }
            }
        }

        public void BroadcastAll(string msg)
        {
            foreach(RoomObserverBase observer in Clients)
            {
                observer.SendMessage(msg);
            }
        }

        public void Broadcast(IClientObject excl, ChatMessage msg)
        {
            foreach (RoomObserverBase observer in Clients)
            {
                if(observer.client != excl)
                observer.On_MessageReceived(Name, msg);
                NewMessage?.Invoke(Name, msg);
            }
        }
    }
}
