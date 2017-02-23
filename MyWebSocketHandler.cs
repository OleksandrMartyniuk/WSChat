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
    public class MyWebSocket : WebSocketHandler
    {
        MyWebSocketClient client = null;
        public override void OnOpen()
        {
            client = new MyWebSocketClient(this);
        }

        public override void OnMessage(string message)
        {
            client.RaiseMessageReceived(message);
        }

        public override void OnClose()
        {
            client.Close();
            base.OnClose();
        }
    }
}