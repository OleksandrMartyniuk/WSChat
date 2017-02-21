using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WSChat.ChatAPI
{
    public class WebSocketClient : ChatServer.IClientObject
    {
        WebSocket socket { get; set; }
        public WebSocketClient(WebSocket socket)
        {
            this.socket = socket;
            Manager.Clients.AddLast(this);
            Start();
        }
        public override void Close()
        {
            socket.Abort();
        }

        public override void SendMessage(string message)
        {
            Task t = Send(message);
        }

        private async Task Send(string message)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public override void Start()
        {
            Task t = ProcessWSChat();
            //t.Wait();
        }

        private async Task ProcessWSChat()
        {
            while (true)
            {
                
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                if (socket.State == WebSocketState.Open)
                {
                    string userMessage = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    RaiseMessageReceived(this, userMessage);
                    //userMessage = "You sent: " + userMessage + " at " + DateTime.Now.ToLongTimeString();
                    //buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(userMessage));
                    //await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    Manager.UserDisconnect(Username);
                    break;
                }
            }
        }
    }
}