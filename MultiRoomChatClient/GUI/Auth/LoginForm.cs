using Core;
using MultiRoomChatClient.API;
using MultiRoomChatClient.GUI.Auth;
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
        public AuthServerClient authClient;

        public LoginForm()
        {
            InitializeComponent();
            authClient = new AuthServerClient();
            authClient.login_Successfull += OnLoginSuccessfull;
            authClient.login_WrongPassword += OnWrongPassword;
            authClient.login_WrongUsername += OnWrongUsername;


            ResponseHandler.loginSuccessfull += (login) => this.Invoke(new Action<string>(On_LoginUser), login); //this.Invoke(()=> On_LoginUser());
            ResponseHandler.loggedAsAdmin += (login) => this.Invoke(new Action<string>(On_LoginAdmin), login);
            ResponseHandler.loggedBanned += (login) => this.Invoke(new Action<string>(On_LoginBanned), login);
            ResponseHandler.loginFail += (msg) => this.Invoke(new Action<string>(On_LoginError), msg);
        }

        private void OnWrongUsername()
        {
            input_login.BackColor = Color.Coral;
            lbl_status.Text = "Login " + input_login.Text + " doesn't exist";
        }

        private void OnWrongPassword()
        {
            input_password.BackColor = Color.Coral;
            lbl_status.Text = "Wrong password";
        }

        private void OnInvalidUsername()
        {
            input_login.BackColor = Color.Coral;
            lbl_status.Text = "Login should be at least 3 letters or numbers";
        }

        private void OnInvalidPassword()
        {
            input_password.BackColor = Color.Coral;
            lbl_status.Text = "Password should be at least 3 letters or numbers";
        }

        private void OnLoginSuccessfull(string key)
        {
            Client.ConnectionOpened += () => RequestManager.Login(key);
            Client.StartClient();
        }

        private void On_LoginError(string message)
        {
            MessageBox.Show(message);
            Client.Disconnect();
        }

        private void link_forgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var passRecovery = new PasswordRecovery(authClient, this);
            passRecovery.Location = Location;
            passRecovery.StartPosition = StartPosition;
            passRecovery.Show();
            this.Hide();
        }

        private void link_Register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var regForm = new RegisterForm(authClient, this);
            regForm.Location = Location;
            regForm.StartPosition = StartPosition;
            regForm.Show();
            this.Hide();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            ResetErrors();

            string username = input_login.Text;
            string password = input_password.Text;

            bool validLogin = InputValidator.VlidateLogin(username);
            bool validPassword = InputValidator.VlidatePassword(password);

            if (!validLogin)
            {
                OnInvalidUsername();
                return;
            }
            if (!validPassword)
            {
                OnInvalidPassword();
                return;
            }

            authClient.Login(username, password);
        }

        private void ResetErrors()
        {
            lbl_status.Text = "";
            input_login.BackColor = Color.White;
            input_password.BackColor = Color.White;
        }

        private void On_LoginUser(string username)
        {
            Client.Username = username;
            var chat = new SuperDuperChat();
            chat.Location = Location;
            chat.StartPosition = StartPosition;
            chat.Show();
            this.Hide();
        }

        private void On_LoginBanned(string username)
        {
            Client.Username = username;
            var chat = new SuperDuperChat();
            chat.Ban();
            chat.Location = Location;
            chat.StartPosition = StartPosition;
            chat.Show();
            this.Hide();
        }

        private void On_LoginAdmin(string username)
        {
            Client.Username = username;
            var chat = new AdminForm();
            chat.Location = Location;
            chat.StartPosition = StartPosition;
            chat.Show();
            this.Hide();
        }

        /// <summary>
        /// ///////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picbox_google_Click(object sender, EventArgs e)
        {
            ApiSend auth = new ApiSend();
            string info = auth.Google_Auth();
            if (info == null)
                return;
            string name = auth.Tr(info);
            if (name != "")
            {
                //RequestManager.LoginGmail(name);
            }
        }

        private void picbox_facebook_Click(object sender, EventArgs e)
        {
            ApiSend auth = new ApiSend();
            string info = auth.Facebook_Auth();
            if (info == null)
                return;
            string name = auth.Tr(info);
            if (name != "")
            {
                //RequestManager.LoginFacebook(name);
            }
        }
    }
}
