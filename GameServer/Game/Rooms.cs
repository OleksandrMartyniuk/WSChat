using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace GameServer
{
    public class Rooms
    {
        public List<Room> rooms;

        public Rooms()
        {
            rooms = new List<Room>();
        }

        public void Add(List<Client> clients, string gameName)
        {
            rooms.Add(new Room(clients, gameName));
        }
        public void Remove(Room room)
        {
            for(int i=0; i<room.clients.Count; i++)
            {
                room.clients[i].inGame = false;
            }
            rooms.Remove(room);
        }
        public void SendMessage(Room room, object[] args)
        {
            for (int i = 0; i < room.clients.Count; i++)
            {
                if (room.clients[i].client != args[1])
                {
                    room.clients[i].Write(new RequestObject("Game", "Over", "game over user " + args[1]));
                    return;
                }
            }
                
        }
    }
}
