using ChatServer;
using ChatServer.Roles;
using Microsoft.AspNet.SignalR.WebSockets;
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
    public class MyWebSocket : WebSocketHandler, IClientObject
    {
        public string Username { get; set; }

        public RoleBase Role { get; set; }

        public event handler MessageRecieved;

        public void Start()
        {
            Role = new UnknownUser(this);
            Manager.Clients.AddLast(this);
        }

        public void SendMessage(string message)
        {
            Send(message);
        }

        public void Close()
        {
            Manager.UserDisconnect(this.Username);
        }

        /// <summary>
        /// /////////////////WEBSOCKET
        /// </summary>

        public override void OnOpen()
        {
            Start();
            Send("Hooy");
        }

        public override void OnMessage(string message)
        {
            MessageRecieved?.Invoke(this, message);
        }

        public override void OnClose()
        {
            // Free resources, close connections, etc.
            Close();
            base.OnClose();
        }
    }
}