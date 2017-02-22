using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
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
    public delegate void handler(ChatServer.IClientObject sender, string message);

    public interface IClientObject
    {
        string Username { get; set; }
        Roles.RoleBase Role { get; set; }

        event handler MessageRecieved;

        void Start();

        void SendMessage(string message);

        void Close();
    }


}
