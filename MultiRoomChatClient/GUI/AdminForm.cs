using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient
{
    public partial class AdminForm :SuperDuperChat
    {
        public AdminForm() :base()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.treename += (x) =>
                tb_selectedUser.Invoke(new Action(() =>
                { 
                    tb_selectedUser.Text = x;
                }));
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void btn_banForever_Click(object sender, EventArgs e)
        {
            RequestManager.AdminBanEternal(tb_selectedUser.Text.ToString());
        }

        private void Unban_Click(object sender, EventArgs e)
        {
            RequestManager.AdminUnban(tb_selectedUser.Text.ToString());
        }

        private void Ban_Till_Click(object sender, EventArgs e)
        {
            RequestManager.AdminBan(tb_selectedUser.Text.ToString(), dateTime.Value);
        }
    }
}
