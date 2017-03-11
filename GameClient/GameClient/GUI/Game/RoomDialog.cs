using GameClient.API.Networking;
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
       
        public RoomDialog(object x)
        {
            InitializeComponent();
            UserControl game;
            object[] args = JsonConvert.DeserializeObject<object[]>(x.ToString());
            switch (args[1].ToString())
            {
                case "XO":
                    game = new XO(this, args);
                    break;
            }
            this.Init();
            this.ShowDialog();
            CheckForIllegalCrossThreadCalls = false;
            ResponseHandler.end += End;
        }
        public void Init()
        {
            username.Text = "Name: "+ Client.Username;
        }
      

        public void End()
        {
            MessageBox.Show("Game over");
            Close();
        }

        private void btn_xo_Click(object sender, EventArgs e)
        {
        //    string tag = (sender as Button).Tag.ToString();
            
        }
    }
}
