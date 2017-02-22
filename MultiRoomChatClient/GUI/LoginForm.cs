using Core;
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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            ResponseHandler.loginSuccessfull += (x) => Invoke(new Action<string>(On_LoginSuccessfull), x);
            ResponseHandler.loggedAsAdmin    += (x) => Invoke(new Action<string>(On_LoginAdmin), x);
            ResponseHandler.loggedBanned     += (x) => Invoke(new Action<string>(On_LoginBanned), x);
            ResponseHandler.loginFail        += (x) => Invoke(new Action<string>(On_LoginFailed), x);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public LinkedList<PrivateMessageForm> PmForms = new LinkedList<PrivateMessageForm>();

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (login_box.Text == null || login_box.Text == "")
            {
                MessageBox.Show("Enter name");
                login_box.Focus();
                return;
            }
            else if (password_box.Text == null || password_box.Text == "")
            {
                MessageBox.Show("Enter password");
                password_box.Focus();
                return;
            }
            try
            {
                Client.StartClient();
            }
            catch
            {
                MessageBox.Show("Server Disconnect");
            }
            RequestManager.Login(login_box.Text,password_box.Text);
        }
        private void On_LoginFailed(string error)
        {
            login_box.BackColor = Color.Coral;
            password_box.BackColor = Color.Coral;
            MessageBox.Show(error);
        }
        
        private SuperDuperChat On_Log(string UserName)
        {
            Client.Username = UserName;
            var chat = new SuperDuperChat();
            chat.Location = Location;
            chat.StartPosition = StartPosition;
            chat.Show();
            this.Hide();
            return chat;
        }

        private void On_LoginSuccessfull(string Username)
        {
            On_Log(Username);
        }

        private void On_LoginBanned(string Username)
        {
            //var chat = On_Log(Username);
            //chat.Ban();
            On_Log(Username);
        }
        
        private void On_LoginAdmin(string Username)
        {
            Client.Username = Username;
            var chat = new AdminForm();
            chat.Location = Location;
            chat.StartPosition = StartPosition;
            chat.Show();
            this.Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            if (Char.IsWhiteSpace(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsSeparator(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || login_box.Text.Length>=40 )
            {
                e.Handled = true;
                return;
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}
