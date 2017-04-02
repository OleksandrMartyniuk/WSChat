using MultiRoomChatClient.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient.GUI.Auth
{
    public partial class RegisterForm : Form
    {
        AuthServerClient client = null;
        LoginForm loginForm = null;

        public RegisterForm()
        {
            InitializeComponent();
        }

        public RegisterForm(AuthServerClient client, LoginForm form)
        {
            InitializeComponent();
            this.client = client;
            this.loginForm = form;
            client.reg_OK += OnSuccess;
            client.reg_EmailExists += OnWrongEmail;
            client.reg_UsernameExists += OnWrongUsername;
            client.reg_EmailUsernameExists += OnWrongEmailUsername;
        }

        private void OnWrongEmailUsername()
        {
            this.lbl_status.Text = ResourceProvider.GetValue("auth.messages.record-exists");
        }

        private void OnWrongUsername()
        {
            this.lbl_status.Text = ResourceProvider.GetValue("auth.messages.username-exists");
        }

        private void OnWrongEmail()
        {
            this.lbl_status.Text = ResourceProvider.GetValue("auth.messages.email-exists");
        }

        private void OnSuccess()
        {
            this.lbl_status.Text = ResourceProvider.GetValue("auth.messages.registration-seccess");
        }

        private void OnInvalidUsername()
        {
            this.lbl_status.Text = ResourceProvider.GetValue("auth.messages.invalid-login");
        }

        private void OnInvalidEmail()
        {
            this.lbl_status.Text = ResourceProvider.GetValue("auth.messages.invalid-email");
        }

        private void OnInvalidPassword()
        {
            this.lbl_status.Text = ResourceProvider.GetValue("auth.messages.invalid-password");
        }


        private void btn_login_Click(object sender, EventArgs e)
        {
            this.Close();
            loginForm.Show();
        }

        private void RegisterForm_Paint(object sender, PaintEventArgs e)
        {
            this.Text = ResourceProvider.GetValue("auth.titles.registration");
            this.lbl_email.Text = ResourceProvider.GetValue("auth.labels.email");
            this.lbl_login.Text = ResourceProvider.GetValue("auth.labels.login");
            this.lbl_password.Text = ResourceProvider.GetValue("auth.labels.password");
            this.lbl_title.Text = this.Text;
            this.btn_login.Text = ResourceProvider.GetValue("auth.buttons.login");
            this.btn_register.Text = ResourceProvider.GetValue("auth.buttons.register");
        }
    }
}
