using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        static ServerObject server; // сервер
        static Thread listenThread; // потока для прослушивания
        static void Main(string[] args)
       {
            while (true)
            {
                try
                {
                    server = new ServerObject();
                    ///////RoomManager.Host = new RoomObject("Host");
                    ///////RoomManager.BindEvents();
                    listenThread = new Thread(new ThreadStart(server.Listen));
                    listenThread.Start(); //старт потока
                }
                catch (Exception ex)
                {
                    server.Disconnect();
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
