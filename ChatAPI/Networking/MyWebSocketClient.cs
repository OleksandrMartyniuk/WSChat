using ChatServer;
using ChatServer.Roles;
using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WSChat
{
    public class MyWebSocketClient : IClientObject
    {
        WebSocketHandler socket { get; set; }

        public string Username { get; set; }

        public RoleBase Role { get; set; }

        LinkedList<string> queue = new LinkedList<string>();

        public event handler MessageRecieved;

        public MyWebSocketClient(WebSocketHandler socket)
        {
            Start();
            this.socket = socket;
        }

        internal void RaiseMessageReceived(string message)
        {
            MessageRecieved?.Invoke(this, message);
        }

        public void Close()
        {
            socket.Close();
            Manager.UserDisconnect(Username);
            LogProvider.AppendRecord(string.Format("{0} [{1}]: disconnected", DateTime.Now.ToString(), Username));
        }

        public void SendMessage(string message)
        {
            socket.Send(message);
        }


        public void Start()
        {
            this.Role = new UnknownUser(this);
            MessageRecieved += Role.Handle;
        }

    }
}