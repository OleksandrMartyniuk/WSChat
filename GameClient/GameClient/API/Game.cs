using Core;
using GameClient.API.Networking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public class Game
    {
        
        
        public Game(object Args)
        {
            RoomDialog roomdialog = new RoomDialog();
            UserControl game;
            object[] args = JsonConvert.DeserializeObject<object[]>(Args.ToString());
            switch (args[1].ToString())
            {
                case "XO":
                    game = new XO(roomdialog, args);
                    break;
            }
            roomdialog.Init();
            roomdialog.ShowDialog();
        }
        
    }
}
