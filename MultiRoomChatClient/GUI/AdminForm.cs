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
            string userName = tb_selectedUser.Text.ToString();
            if (userName == null || userName == "")
                return;
            RequestManager.AdminBanEternal(userName);
        }

        private void Unban_Click(object sender, EventArgs e)
        {
            string userName = tb_selectedUser.Text.ToString();
            if (userName == null || userName == "")
                return;

            RequestManager.AdminUnban(userName);
        }
        private void Ban_Till_Click(object sender, EventArgs e)
        {
            string userName = tb_selectedUser.Text.ToString();
            if (userName == null || userName == "")
                return;

            RequestManager.AdminBan(userName, dateTime.Value);
        }
    }
}
