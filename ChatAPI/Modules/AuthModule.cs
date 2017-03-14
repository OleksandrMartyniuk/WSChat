using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using ChatServer.AuthApi;
using ChatServer.Roles;
using Core.Models;

namespace ChatServer.ChatAPI.Modules
{
    public class AuthModule : IHandlerModule
    {
        public bool Handle(IClientObject client, RequestObject request)
        {
            if(request.Module != "auth")
            {
                return false;
            }

            string key = request.Args.ToString();

            AuthPool.PoolObject obj = AuthPool.GetRecordByKey(key);

            if(obj == null)
            {
                client.SendMessage(ResponseConstructor.GetErrorNotification("authorization failed", "login"));
            }
            else
            {
                switch (obj.status){
                    case AuthStatus.User:
                        client.Role = new User(client);
                        client.SendMessage(ResponseConstructor.GetLoginResultNotification("user", obj.Username));
                        LogProvider.AppendRecord(string.Format("[{0}]: Logged in as user", client.Username));
                        break;
                    case AuthStatus.Banned:
                        client.Role = new BannedUser(client);
                        client.SendMessage(ResponseConstructor.GetLoginResultNotification("banned", obj.Username));
                        LogProvider.AppendRecord(string.Format("[{0}]: Logged in as banned user", client.Username));
                        break;
                    case AuthStatus.Admin:
                        client.Role = new Admin(client);
                        client.SendMessage(ResponseConstructor.GetLoginResultNotification("admin", obj.Username));
                        LogProvider.AppendRecord(string.Format("[{0}]: Logged in as admin", client.Username));
                        break;
                }
            }
            return true;
        }
    }
}