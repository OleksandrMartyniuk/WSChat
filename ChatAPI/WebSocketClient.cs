using ChatServer;
using ChatServer.Roles;
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
        LinkedList<string> queue = new LinkedList<string>();
        public WebSocketClient(WebSocket socket)
        {
            this.socket = socket;
            this.Role = new UnknownUser(this);
        }

        public override void Close()
        {
            socket.Abort();
            Manager.UserDisconnect(Username);
        }

        public override void SendMessage(string message)
        {
            Send(message);
        }

        private async Task Send(string message)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public override void Start()
        {
            Task t = ProcessWSChat();
            t.Wait();
        }

        private async Task ProcessWSChat()
        {
            var buffer = WebSocket.CreateServerBuffer(1024);
            StringBuilder recieved = new StringBuilder();
            while (socket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
                else 
                {
                    recieved.Append(Encoding.UTF8.GetString(buffer.ToArray(), 0, result.Count));
                    if (result.EndOfMessage)
                    {
                        RaiseMessageReceived(this, recieved.ToString());
                        recieved.Clear();
                    }
                }
                Thread.Sleep(20);
            }
            Close();
        }
    }
}