using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Room : IHandlerModule
    {
        public bool Handle(IClientObject client, RequestObject request)
        {
            if(request.Module != "room")
            {
                return false;
            }
            string roomName = (string)request.args;
            var room = Manager.FindRoom(roomName);
            if (request.Cmd == "create")
            { 
                if(room != null)
                {
                    client.SendMessage(ResponseConstructor.GetErrorNotification("This room already exists", "room"));
                    LogProvider.AppendRecord(string.Format("{0} [{1}]: tried to create existing room {2}", DateTime.Now.ToString(), client.Username, roomName));
                }
                else
                {
                    Manager.CreateRoom(roomName);
                    LogProvider.AppendRecord(string.Format("{0} [{1}]: created new room {2}", DateTime.Now.ToString(), client.Username, roomName));
                }
            }
            else if (request.Cmd == "close")
            {
                if (room == null)
                {
                    client.SendMessage(ResponseConstructor.GetErrorNotification("Can't delete this room as it doesn't exist", "room"));
                    LogProvider.AppendRecord(string.Format("{0} [{1}]: tried to close unexisting room {2}", DateTime.Now.ToString(), client.Username, roomName));
                }
                else
                {
                    Manager.CloseRoom((string)request.args);
                    LogProvider.AppendRecord(string.Format("{0} [{1}]: closed room {2}", DateTime.Now.ToString(), client.Username, roomName));
                }
            }
            return true;
        }
    }
}
