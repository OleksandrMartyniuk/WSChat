using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public abstract class IClientObject
    {
        public string Username { get; set; }
        public Roles.RoleBase Role { get; set; }

        public delegate void handler(ChatServer.IClientObject sender, string message);
        public event handler MessageRecieved;

        public abstract void Start();

        public abstract void SendMessage(string message);

        public abstract void Close();

        protected void RaiseMessageReceived(ChatServer.IClientObject sender, string message)
        {
            MessageRecieved?.Invoke(sender, message);
        }

    }
}
