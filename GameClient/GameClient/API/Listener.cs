using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace GameClient
{
    public  class Listener
    {
        public Authorization auth;
        public Lobby lobby;
        public HandShake handShake;
        public Game game;
    
        public Listener()
        {
            this.auth = new Authorization();
            this.lobby = new Lobby();
            this.handShake = new HandShake();
            this.game = new Game();
            
        }
        public void connection()
        {
            new Client();
            new Thread(new ThreadStart(Listen)).Start();
        }
       
        private void Listen()
        {
            var info = new RequestObject();
            while (true)
            {
                StreamReader reader = new StreamReader(Client.netstream);
                try
                {
                    info = JsonConvert.DeserializeObject<RequestObject>(reader.ReadLine());
                }
                catch (Exception e)
                {

                }
                switch (info.Module)
                {
                    case "Auth":       auth.Dispacher(info);        break;
                    case "Lobby":      lobby.Dispacher(info);       break;
                    case "HandShake":  handShake.Dispacher(info);   break;
                    case "Game":       game.Dispacher(info);        break;
                }
            }
        }

        
    }
}
