using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    public class CommandDispacher
    {
        private Clients clients { get; set; }
        Rooms rooms { get; set; }
     
        public CommandDispacher(Clients clients)
        {
            this.clients = clients;
            rooms = new Rooms();
            new Thread(new ThreadStart(StartDispacher)).Start();
        }
    
        private void writeClient(Client client)
        {
            if (client.netStream.DataAvailable)
            {
                RequestObject info = new RequestObject();
                string message = client.Read();
                Console.WriteLine(message);
                try
                {
                    info = JsonConvert.DeserializeObject<RequestObject>(message);
                }
                catch (Exception ex)
                {

                }
                switch (info.Module)
                {
                    case "Auth":  new Authorization(clients).Dispacher(client, info);       break;
                    case "Lobby": new Lobby().Dispacher(client, info, clients.clientsList); break;
                    case "HandShake":
                        var handShake = new HandShake(clients, rooms);
                        handShake.Dispacher(client, info);
                        break;
                    case "Game": new Game(rooms).Dispacher(client, info);                   break;
                }
            }
        }
        private void StartDispacher()
        {
            while (true)
            {
                for (int i = 0; i < clients.clientsList.Count; i++)
                {
                    writeClient(clients.clientsList[i]);
                }
                Thread.Sleep(1000);
            }
        }
    }
}

 