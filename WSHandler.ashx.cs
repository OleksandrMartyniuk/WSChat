using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebSockets;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using ChatServer;
using Microsoft.Web.WebSockets;

namespace WSChat
{
    /// <summary>
    /// Summary description for WSHandler
    /// </summary>
    public class WSHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest || context.IsWebSocketRequestUpgrading) //Проверка на веб сокет соединение!
            {
                context.AcceptWebSocketRequest(new MyWebSocket());          //Передача управления в обработчик клиента
            }
        }

        public bool IsReusable => false;
    }
}