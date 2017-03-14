using ChatServer.Roles;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class AdminModule : IHandlerModule
    {
    
        public bool Handle(IClientObject client, RequestObject request)
        {
            if (request.Module != "admin")
            {
                return false;
            }

            switch (request.Cmd)
            {
                case "ban":
                    object[] args = JsonConvert.DeserializeObject<object[]>(request.Args.ToString());
                    BanUser((string)args[0], (DateTime)args[1]); //[0] -username
                    break;
                case "unban":
                    UnBanUser((string)request.Args);
                    break;
                default: break;
            }
            return true;
        }
        
        private void BanUser(string username, DateTime duration)
        {
            BlackListProvider.AppendRecord(username, duration);

            IClientObject user = Manager.FindClient(username);
            if (user == null || user.ToString() == "")  //user not found
                return;
            LogProvider.AppendRecord(string.Format("[{0}]: banned user {1}", user.Username , username));
            user.SendMessage(ResponseConstructor.GetBannedNotification(duration));
            //user.Role = new BannedUser(user);
        }

        private void UnBanUser(string username)
        {
            BlackListProvider.RemoveRecord(username);

            IClientObject user = Manager.FindClient(username);
            if (user == null || user.ToString() == "")
                return;

            user.SendMessage(ResponseConstructor.GetUnBannedNotification(username));
            LogProvider.AppendRecord(string.Format("[{0}]: unbanned user {1}", user.Username, username));

            user.Role = new User(user);
        }
    }
}
