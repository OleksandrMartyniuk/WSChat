﻿using ChatServer.Roles;
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
            string roomName = (string)request.Args;
            var room = Manager.FindRoom(roomName);
            if (request.Cmd == "create")
            { 
                if(room != null)
                {
                    client.SendMessage(ResponseConstructor.GetErrorNotification("This room already exists", "room"));
                    LogProvider.AppendRecord(string.Format("[{0}]: tried to create existing room {1}", client.Username, roomName));
                }
                else
                {
                    Manager.CreateRoom(roomName, client.Username);
                    LogProvider.AppendRecord(string.Format("[{0}]: created new room {1}", client.Username, roomName));
                }
            }
            else if (request.Cmd == "close")
            {
                if (room == null)
                {
                    client.SendMessage(ResponseConstructor.GetErrorNotification("Can't delete this room as it doesn't exist", "room"));
                    LogProvider.AppendRecord(string.Format("[{0}]: tried to close unexisting room {1}", client.Username, roomName));
                }
                else if(room.Creator != client.Username && client.Role.GetType() != typeof(Admin))
                {
                    client.SendMessage(ResponseConstructor.GetErrorNotification("Can't delete room " + roomName + " . No permission.", "room"));
                    LogProvider.AppendRecord(string.Format("[{0}]: tried to close room {1} but had no permission", client.Username, roomName));
                }
                else
                {
                    Manager.CloseRoom((string)request.Args);
                    LogProvider.AppendRecord(string.Format("[{0}]: closed room {1}", client.Username, roomName));
                }
            }
            return true;
        }
    }
}
