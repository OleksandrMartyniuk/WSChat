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
    public partial class PasswordRecovery : Form
    {
        AuthServerClient client = null;
        LoginForm loginForm = null;

        public PasswordRecovery()
        {
            InitializeComponent();
        }

        public PasswordRecovery(AuthServerClient client, LoginForm form)
        {
            InitializeComponent();
            this.client = client;
            this.loginForm = form;
            client.passRecovery_Successfull += OnSuccess;
            client.passRecovery_WrongEmail += OnWrongEmail;
        }

        private void PasswordRecovery_Paint(object sender, PaintEventArgs e)
        {
            this.Text = ResourceProvider.GetValue("auth.titles.passRecovery");
            this.lbl_email.Text = ResourceProvider.GetValue("auth.labels.email");
            this.lbl_title.Text = ResourceProvider.GetValue("auth.buttons.forgot");
            this.lbl_status.Text = ResourceProvider.GetValue("auth.messages.default-message");
            this.btn_log_in.Text = ResourceProvider.GetValue("auth.buttons.login");
            this.btn_send.Text = ResourceProvider.GetValue("auth.buttons.recover");
        }

        private void OnWrongEmail()
        {
            lbl_status.ForeColor = Color.Red;
            lbl_status.Text = ResourceProvider.GetValue("auth.messages.email-exists");
        }

        private void OnInvalidEmail()
        {
            lbl_status.ForeColor = Color.Red;
            lbl_status.Text = ResourceProvider.GetValue("auth.messages.invalid-email");
        }

        private void OnSuccess()
        {
            lbl_status.ForeColor = Color.Green;
            lbl_status.Text = ResourceProvider.GetValue("auth.messages.password-sent");
            this.AcceptButton = btn_log_in;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string email = input_email.Text;
            if (InputValidator.VlidateEmail(email))
            {
                client.PasswordRecovery(email);
            }
            else
            {
                OnInvalidEmail();
            }
            
        }

        private void btn_log_in_Click(object sender, EventArgs e)
        {
            this.Close();
            loginForm.Show();
        }
    }
}
