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
    public class Login : IHandlerModule
    {
        public bool Handle(IClientObject client, RequestObject request)
        {
            if(request.Module != "login")
            {
                return false;
            }

            object[] user = JsonConvert.DeserializeObject<object[]>(request.args.ToString());
            string username = user[0].ToString();
            string password = user[1].ToString();
            // (string)request.args;

            if (Manager.FindClient(username) != null)
            {
                client.SendMessage(ResponseConstructor.GetErrorNotification("User with this username already exists", "login"));
                return true;
            }
            string answer = AuthProvider.RecordExists(username, password);
            if (answer == "false")
            {
                AuthProvider.AppendRecord(username, password);
                LogProvider.AppendRecord(string.Format("{0} registered new user [{1}]", DateTime.Now.ToString(), username));

            }
            else if (answer == "login")
            {
                client.SendMessage(ResponseConstructor.GetErrorNotification("The password is not corrent", "login"));
                return true;
            }

            client.Username = username;
            if (IsAdmin(username))
            {
                client.Role = new Admin(client);
                client.SendMessage(ResponseConstructor.GetLoginResultNotification("admin", username));
                LogProvider.AppendRecord(string.Format("{0} [{1}]: Logged in as admin", DateTime.Now.ToString(), username));
            }
            else if (IsInBlackList(username))
            {
                client.Role = new BannedUser(client);
                client.SendMessage(ResponseConstructor.GetLoginResultNotification("banned", username));
                LogProvider.AppendRecord(string.Format("{0} [{1}]: logged as banned user", DateTime.Now.ToString(), username));
            }
            else
            {
                client.Role = new User(client);
                client.SendMessage(ResponseConstructor.GetLoginResultNotification("ok", username));
                LogProvider.AppendRecord(string.Format("{0} [{1}]: logged in", DateTime.Now.ToString(), username));
            }
            Manager.Clients.AddLast(client);
            return true;
        }

        private bool IsInBlackList(string Username)
        {
            return BlackListProvider.RecordExists(Username);
        }

        private bool IsAdmin(string Username)
        {
            if(Username == "admin")
            {
                return true;
            }
            return false;
        }
    }
}
