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

namespace GameClient
{
    public class Game
    {
        public EventHandler Close;
        public EventHandler Enabled;

        public RoomDialog roomdialog;
        Client client;
        string gameIndex;

        

        public Game(Client client)
        {
            roomdialog = new RoomDialog();
            this.client = client;
           
        }

        public void Dispacher(RequestObject info)
        {
            switch(info.Cmd)
            {
                case "Start":
                    Start(info.Args);
                    Close(null, null);
                    Enabled(null, null);
                    break;
                case "Move":
                    Move(info.Args);
                    break;
                case "Over":
                    End();
                    break;
            }
        }

        private void Start(object Args)
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(Args.ToString());

            this.gameIndex =  args[0].ToString();
            roomdialog.Init(client, args[1].ToString());
            //if (roomdialog.game is XO)
            //    roomdialog.game.MouseDown += SendMoveXO;
            Thread open = new Thread(new ThreadStart(OpenForm));
            open.Start();
        }

        private void End()
        {
            roomdialog.End();
        }

        private void Move(object Args)
        {
            roomdialog.Draw(Args);
        }

        private void SendMoveXO(object sender, MouseEventArgs e)
        {
            string x = "";
            string y = "";
            
            if (e.X >= 70 && e.X <= 120)
                y = "0";
            if (e.X >= 120 && e.X <= 170)
                y = "1";
            if (e.X >= 170 && e.X <= 220)
                y = "2";
            if (e.Y >= 70 && e.Y <= 120)
                x = "0";
            if (e.Y >= 120 && e.Y <= 170)
                x = "1";
            if (e.Y >= 170 && e.Y <= 220)
                x = "2";
            if(x!="" && y!="")
            {
                Listener.SendMessage(new RequestObject("Game", "Move", new object[] { gameIndex, x, y }));
            }
        }

        void OpenForm()
        {
            roomdialog.ShowDialog();
        }
    }
}
