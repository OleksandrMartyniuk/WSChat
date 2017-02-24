using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Logout : IHandlerModule
    {
        public bool Handle(IClientObject client, RequestObject request)
        {
            if (request.Module != "logout")
            {
                return false;
            }

            client.Close();
            LogProvider.AppendRecord(string.Format("{0} [{1}]: left chat", DateTime.Now.ToString(), client.Username));
            
            return true;
        }
    }
}
