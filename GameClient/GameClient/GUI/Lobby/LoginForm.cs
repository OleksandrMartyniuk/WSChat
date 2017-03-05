using AuthServer;
using Core;
using GameClient;
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
    public partial class LoginForm : Form
    {
        Listener listener;
        public LoginForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
          
            try
            {
                listener = new Listener();
                listener.connection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server Disconnect...");
            }
            listener.auth.LogIn += LogIn;
        }
        private void LogIn(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            new LobbyForm(listener,sender).ShowDialog();
           
        }

        private bool IsValid(string login,string password)
        {
            if (login == null || login == "")
            {
                MessageBox.Show("Enter name");
                login_box.Focus();
                return false;
            }
            else if (password == null || password == "")
            {
                MessageBox.Show("Enter password");
                password_box.Focus();
                return false;
            }
            return true;
        }
        private void btn_connect_Click(object sender, EventArgs e)
        {
            string login = login_box.Text;
            string password = password_box.Text;
            if (IsValid(login,password) == true)
            {
                try
                {
                    listener.auth.SendLogIn(login, password);
                }
                catch
                {
                    MessageBox.Show("Server Disconnect");
                }
            }
        }
        private void On_LoginFailed(string error)
        {
            login_box.BackColor = Color.Coral;
            password_box.BackColor = Color.Coral;
            MessageBox.Show(error);
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
            listener.auth.LogIn -= LogIn;
            this.Dispose();
        }

        private void btn_gmail_Click(object sender, EventArgs e)
        {
            ApiSend apiAuth = new ApiSend();
            string info = apiAuth.Google_Auth();
            if (info == null)
                return;
            string name = apiAuth.Tr(info);
            if (name != "")
            {
                listener.auth.LoginGmail(name);
            }
        }

        private void btn_facebook_Click(object sender, EventArgs e)
        {
            ApiSend apiAuth = new ApiSend();
            string info = apiAuth.Facebook_Auth();
            if (info == null)
                return;
            string name = apiAuth.Tr(info);
            if (name != null)
            {
                listener.auth.LoginFacebook(name);
            }
        }
        private bool IsValidEmail(string email)
        {
            bool f = new ApiSend().IsValidEmail(email);
            if (f == false)
            {
                MessageBox.Show("Validation email");
                login_box.Focus();
                return false;
            }
            return true;
        }
        private void btnReg_Click(object sender, EventArgs e)
        {
            string login = login_box.Text;
            string password = password_box.Text;
            if (IsValid(login,password) == true)
            {
                string email =  email_box.Text.ToString();
                if (IsValidEmail(email) == true)
                {
                    listener.auth.LoginReg(login, password, email);
                }
            }
        }

        private void btn_Forgot_Click(object sender, EventArgs e)
        {
            string email = email_box.Text.ToString();
            if (IsValidEmail(email) == true)
            {
                listener.auth.LoginForgot(email);
            }
        }

    }
}
