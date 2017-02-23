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

            if(request.Cmd == "create")
            {
                var room  = Manager.FindRoom((string)request.args);
                if(room != null)
                {
                    client.SendMessage(ResponseConstructor.GetErrorNotification("This room already exists", "room"));
                }
                else
                {
                    Manager.CreateRoom((string)request.args);
                }
            }
            else if (request.Cmd == "close")
            {
                var room = Manager.FindRoom((string)request.args);
                if (room == null)
                {
                    client.SendMessage(ResponseConstructor.GetErrorNotification("Can't delete this room as it already exists", "room"));
                }
                else
                {
                    Manager.CloseRoom((string)request.args);
                }
            }
            return true;
        }
    }
}
