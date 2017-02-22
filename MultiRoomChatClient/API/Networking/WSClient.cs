using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiRoomChatClient.API.Networking
{
    public class WSClient : IClient
    {
        ClientWebSocket socket = null;
        LinkedList<string> messageQue = new LinkedList<string>();

        public event responseHandler responseReceived;
        public event errorMessage NewErrorMessage;

        public WSClient()
        {
            socket = new ClientWebSocket();
        }

        public void StartClient()
        {
            Thread worker = new Thread(() =>
            {
                try
                {
                    Task t = ProcessWSChat();
                    t.Wait();
                }
                catch (Exception ex)
                {

                    var d = ex.Data;
                }
            });
            worker.Start();
        }

        private async Task ProcessWSChat()
        {
            Uri serverUri = new Uri("ws://localhost/WSChat/WSHandler.ashx");
            await socket.ConnectAsync(serverUri, CancellationToken.None);
            
            var buffer = WebSocket.CreateClientBuffer(1024, 1024);
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
                        responseReceived?.Invoke(recieved.ToString());
                        recieved.Clear();
                    }
                }
                Thread.Sleep(20);
            }
            Disconnect();
        }

        private async Task Send(string message)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public void AddRequest(string message)
        {
            Send(message);
        }

        public void Disconnect()
        {
            NewErrorMessage?.Invoke("Подключение прервано!");
            socket.Abort();
        }
    }
}
