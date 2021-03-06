﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using ChatServer.Roles;

namespace ChatServer
{
    public abstract class LogInBase : IHandlerModule
    {
        abstract public bool Handle(IClientObject client, RequestObject request);

        protected void ResolveStatus(IClientObject client)
        {
            if (IsAdmin(client.Username))
            {
                client.Role = new Admin(client);
                client.SendMessage(ResponseConstructor.GetLoginResultNotification("admin", client.Username));
                LogProvider.AppendRecord(string.Format("[{0}]: Logged in as admin", client.Username));
            }
            else if (BlackListProvider.RecordExists(client.Username))
            {
                client.Role = new BannedUser(client);
                client.SendMessage(ResponseConstructor.GetLoginResultNotification("banned", client.Username));
                LogProvider.AppendRecord(string.Format("[{0}]: logged as banned user", client.Username));
            }
            else
            {
                client.Role = new User(client);
                client.SendMessage(ResponseConstructor.GetLoginResultNotification("ok", client.Username));
                LogProvider.AppendRecord(string.Format("[{0}]: logged in", client.Username));
            }
            Manager.AddClient(client);
        }
        
        private bool IsAdmin(string Username)
        {
            if (Username == "admin")
            {
                return true;
            }
            return false;
        }
    }
}