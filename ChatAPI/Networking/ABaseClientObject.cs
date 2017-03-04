using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServer
{
    public delegate void handler(IClientObject sender, string message);
    public abstract class BaseClientObject
    {
        public string Username { get; set; }
        public Roles.RoleBase Role { get; set; }

        public event handler MessageRecieved;

        public abstract void Start();

        public abstract void SendMessage(string message);

        public abstract void Close();

        protected void RaiseMessageReceived(IClientObject sender, string message)
        {
            MessageRecieved?.Invoke(sender, message);
        }
    }
  
}