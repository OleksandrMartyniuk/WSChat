using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    public class Server
    {
        TcpListener server { get; set; }
        Clients clients { get; set; }
       
        public Server(IPAddress ip, int port)
        {
            server = new TcpListener(ip, port);
            server.Start();
            clients = new Clients();
            
            CommandDispacher dispacher = new CommandDispacher(clients);
        }
        
        public void Start()
        {
           
            while (true)
            {
                clients.Add(server);
                Thread.Sleep(1000);
            }
        }
    }
}
