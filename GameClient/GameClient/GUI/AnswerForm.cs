using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class AnswerForm : Form
    {
        object args;
        public AnswerForm(object arg)
        {
            InitializeComponent();
            object[] args = JsonConvert.DeserializeObject<object[]>(arg.ToString());
            label1.Text = args[0] + " приглашает вас поиграть в " + args[1];
           this.args = args;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            new Listener().SendOk(args);
            this.Close();
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            new Listener().SendCancle(args);
            this.Close();
        }
    }
}
