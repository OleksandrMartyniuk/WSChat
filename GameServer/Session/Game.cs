using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace GameServer
{
   public class Game
    {
        Rooms rooms;

        public Game(Rooms rooms)
        {
            this.rooms = rooms;
        }

        public void Dispacher(Client client, RequestObject info)// комсомолькая 52б 16:30
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(info.Args.ToString());
            int index = Convert.ToInt32(args[0]);
            Room room = rooms.rooms[index];
            switch(info.Cmd)
            {
                case "Move":   Move(room, client, info.Args); break;
                case "Remove": Remove(room, args);            break;
            }
            
        }
        void Remove(Room room, object[] args)
        {
            rooms.SendMessage(room, args);
            rooms.Remove(room);   
        }
        void Move(Room room, Client client, object args)
        {
            room.Move(client.name, args.ToString());
            if (room.IsOver())
            {  
                rooms.Remove(room);
            }
        }
    }
}
