using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer;
using Newtonsoft.Json;

namespace ChatServer
{
    public abstract class RoomObserverBase: IHandlerModule
    {
        public IClientObject client;
        //protected RoomObject Active;


        public virtual void On_MessageReceived(string room, ChatMessage msg)
        {
            if(msg.Sender == client.Username)
            {
                return;
            }

            //if(Active.Name == room)
            //{
                client.SendMessage(JsonConvert.SerializeObject(new RequestObject("msg", "msg", new object[] { room, msg })));
            //}
            //else
            //{
           //     client.SendMessage(JsonConvert.SerializeObject(new RequestObject("msg", "notify", room)));
            //}
        }

        public void SendMessage(string msg)
        {
            client.SendMessage(msg);
        }

        public virtual bool Handle(IClientObject client, RequestObject request)
        {
            if(request.Module != "msg")
            {
                return false;
            }
            
            switch (request.Cmd)
            {
                case "msg":
                    HandleMessage(client, request);
                    break;
                case "active":
                    HandleActive(client, request);
                    break;
                case "leave":
                    HandleLeave(client, request);
                    break;
                default: break;
            }
            return true;
        }


        protected virtual void HandleMessage(IClientObject client, RequestObject request)
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(request.Args.ToString());
            string rstr = args[0] as string;
            ChatMessage msg = JsonConvert.DeserializeObject<ChatMessage>(args[1].ToString());
            RoomObject r = Manager.FindRoom(rstr);
            r?.Broadcast(client, msg);
            LogProvider.AppendRecord(string.Format("[{0}]: Sent a message {1}", client.Username, msg.ToString()));
        }

        protected virtual void HandleActive(IClientObject client, RequestObject request)
        {
            RoomObject room = null;
            room = Manager.FindRoom(request.Args.ToString());

            if (room != null)
            {
                room.AddListener(this);

                LogProvider.AppendRecord(string.Format("[{0}]: entered room {1}",client.Username, room.Name));
                ChatMessage[] msgs = room.GetMessageHistoryTo(DateTime.Now);
                if (msgs.Length > 0)
                {
                    RequestObject req = new RequestObject("msg", "active", new object[] { room.Name, msgs });
                    client.SendMessage(JsonConvert.SerializeObject(req));
                }
            }
        }

        protected virtual void HandleLeave(IClientObject client, RequestObject request)
        {
            RoomObject room = Manager.FindRoom((string)request.Args);
            //if (room != null)
            //{
            room.RemoveListener(this);
            LogProvider.AppendRecord(string.Format("[{0}]: left room {1}", client.Username, room.Name));
            // if (Active == room)
            //{
            //     Active = Manager.Host;
            //}
            //}
        }
    }
}
