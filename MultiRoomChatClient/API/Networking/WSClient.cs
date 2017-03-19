using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;

namespace MultiRoomChatClient.API.Networking
{
    public class WSClient : IClient
    {
        WebSocket socket = null;
        string wsURI = null;
        LinkedList<string> messageQue = new LinkedList<string>();

        public event responseHandler responseReceived;
        public event errorMessage NewErrorMessage;
        public event connectionHandler ConnectionOpened;

        public WSClient(string wsURI)
        {
            this.wsURI = wsURI;
        }

        public void StartClient()
        {
            socket = new WebSocket(wsURI);
            socket.Opened += (x,y) => ConnectionOpened?.Invoke();
            socket.Open();
            socket.MessageReceived += InvokeMessageReceived;

            socket.Error += (sender, args) => NewErrorMessage?.Invoke(args.Exception.Message);
        }

        private void InvokeMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            string message = e.Message;
            responseReceived(message);
        }

        public void AddRequest(string message)
        {
            socket.Send(message);
        }

        public void Disconnect()
        {
            socket.Close();
            socket = null;
        }
    }
}
