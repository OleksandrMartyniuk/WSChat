using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameClient.API.Networking;
using Core;

namespace GameClient
{
    public partial class XO : UserControl
    {
        string gamestorage;
        int roomNumber;
        RoomDialog room;
        public XO(RoomDialog room,object[] args)
        {
            InitializeComponent();
            roomNumber = Convert.ToInt32(args[0].ToString());
            this.room = room;
            room.Controls.Add(this);
            room.Location = new Point(21, -1);
            room.Name = "xo";
            room.Size = new Size(429, 292);
            room.TabIndex = 0;
            statusPlay(args[2].ToString());
            ResponseHandler.move += (x) => btn_xo_Move(x);
        }
     
        
        private void btn_xo_Click(object sender, EventArgs e)
        {
            RequestManager.SendMove(new object[]{ roomNumber, gamestorage, Convert.ToInt32((sender as Button).Tag), Client.Username });
        }
      
        private void statusPlay(string args)
        {
            string name = Client.Username;
            if (args.ToString() == name)
            {
                gamestorage = "X";
                label_turn.Text = "play: " + name + " go";
            }
            else
            {
                gamestorage = "O";
                label_turn.Text = "play: " + gamestorage + " wait";
            }
        }

        private void btn_xo_Move(object arg)
        {
            if (gamestorage == arg.ToString())
            {
                label_turn.Text = "play: " + gamestorage + " wait";
            }
            else
            {
                label_turn.Text = "play: " + gamestorage + " go";
            }
          
            switch (arg.ToString())
            {
                case "0":
                    btn_xo1.Text = arg.ToString();
                    break;
                case "1":
                    btn_xo2.Text = arg.ToString();
                    break;
                case "2":
                    btn_xo3.Text = arg.ToString();
                    break;
                case "3":
                    btn_xo4.Text = arg.ToString();
                    break;
                case "4":
                    btn_xo5.Text = arg.ToString();
                    break;
                case "5":
                    btn_xo6.Text = arg.ToString();
                    break;
                case "6":
                    btn_xo7.Text = arg.ToString();
                    break;
                case "7":
                    btn_xo8.Text = arg.ToString();
                    break;
                case "8":
                    btn_xo9.Text = arg.ToString();
                    break;
            }
        }
    }
}
