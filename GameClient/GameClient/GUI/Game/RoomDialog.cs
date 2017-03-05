using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class RoomDialog : Form
    {
        public UserControl game;
        public RoomDialog()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        public void Init(Client client, string gameName)
        {
            switch (gameName)
            {
                case "XO":
                   // game = new XO(client);
                    break;
            }
            this.Controls.Add(game);
        }

        public void Draw(object Args)
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(Args.ToString());
           // if (game is XO)
            //    (game as XO).Draw(args[0].ToString(), args[1].ToString(), args[2].ToString());
        }

        public void End()
        {
            MessageBox.Show("Game over");
            Close();
        }

        private void btn_xo_Click(object sender, EventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            
        }
    }
}
