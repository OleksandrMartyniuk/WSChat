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
            throw new NotImplementedException();
        }

        private void OnWrongUsername()
        {
            throw new NotImplementedException();
        }

        private void OnWrongEmail()
        {
            throw new NotImplementedException();
        }

        private void OnSuccess()
        {
            throw new NotImplementedException();
        }

        private void OnInvalidUsername()
        {
            throw new NotImplementedException();
        }

        private void OnInvalidEmail()
        {
            throw new NotImplementedException();
        }

        private void OnInvalidPassword()
        {
            throw new NotImplementedException();
        }


        private void btn_login_Click(object sender, EventArgs e)
        {
            this.Close();
            loginForm.Show();
        }
    }
}
