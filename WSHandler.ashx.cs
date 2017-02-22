using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebSockets;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using WSChat.ChatAPI;
using ChatServer;

namespace WSChat
{
    /// <summary>
    /// Summary description for WSHandler
    /// </summary>
    public class WSHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest || context.IsWebSocketRequestUpgrading)
            {
                //context.AcceptWebSocketRequest(new MyWebSocket());
            }
        }
        //public void ProcessRequest(HttpContext context)
        //{
        //    if (context.IsWebSocketRequest)
        //    {
        //        context.AcceptWebSocketRequest(ProcessWSChat);
        //    }
        //}

        public bool IsReusable { get { return true; } }

        //private async Task ProcessWSChat(AspNetWebSocketContext context)
        //{
        //    MyWebSocket ws =  new MyWebSocket(1024);
        //    ws.Start();
        //    await ws.Send("Hooy!");
           
        //}
    }
}