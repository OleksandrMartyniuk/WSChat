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

        private void OnWrongEmail()
        {
            lbl_status.ForeColor = Color.Red;
            lbl_status.Text = "Sorry, email doesn't exist";
        }

        private void OnInvalidEmail()
        {
            lbl_status.ForeColor = Color.Red;
            lbl_status.Text = "Email is not valid";
        }

        private void OnSuccess()
        {
            lbl_status.ForeColor = Color.Green;
            lbl_status.Text = "The password was successfully sent";
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
