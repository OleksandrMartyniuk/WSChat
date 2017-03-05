using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class MainForm : Form
    {
        Authorization auth;
        public static Client client;
        Lobby lobby;
        HandShake handShake;
        WaitForm wait;
        Game game;

        delegate void InvokeDelegate();

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        
            client = new Client();
            auth = new Authorization(client);
            lobby = new Lobby();
            handShake = new HandShake();
            game = new Game(client);
            
            auth.LogIn += LogInHandler;

            lobby.RefreshClients += RefreshClientsHandler;
            lobby.Notification   += ShowNotificationHandler;

            handShake.Answer += AnswerFormHandler;
            handShake.Cancle += CancleHandler;
            handShake.Wait += WaitHandler;

            game.Close += WaitFormClose;
            game.roomdialog.FormClosed += CloseFormHandler;
            game.Enabled += EnabledHandler;

            this.btn_refresh.Click += lobby.SendRefreshClients;
            this.btn_log_out.Click += auth.SendLogout;
            this.FormClosed        += auth.SendLogout;
        }
        private void btn_registration_Click(object sender, EventArgs e)
        {
            auth.SendRegistration(txt_login.Text, txt_password.Text);
            Listener listener = new Listener(auth, lobby, handShake, game);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            auth.SendLogIn(txt_login.Text, txt_password.Text);
            Listener listener = new Listener( auth, lobby, handShake, game);
        }
        public void LogInHandler(object sender, EventArgs e)
        {
           
            this.Text = (sender as string);
            lst_clients.Enabled = true;
            btn_refresh.Enabled = true;
            btn_invite.Enabled = true;
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 0;
            BeginInvoke(new InvokeDelegate(LogIn));
        }

        private void LogIn()
        {
            btn_login.Visible = false;
            btn_registration.Visible = false;

            txt_login.Visible = false;
            txt_password.Visible = false;

            lbl_login.Visible = false;
            lbl_password.Visible = false;

            btn_log_out.Visible = true;
        }

        public void RefreshClientsHandler(object sender, EventArgs e)
        {
         //   List<string> info = sender as List<string>;
            object[] clients = JsonConvert.DeserializeObject<object[]>(sender.ToString());
            //string[] clients = json. info.ToArray<sender as string>();
            lst_clients.Items.Clear();
            lst_clients.Items.AddRange(clients);
        }

        void ShowNotificationHandler(object sender, EventArgs e)
        {
            this.Enabled = false;
            
            MessageBox.Show(sender as string);
            this.Enabled = true;
        }
        private void btn_invite_Click(object sender, EventArgs e)
        {
            if (lst_clients.SelectedItem != null)
            {
                handShake.SendInvite(new object[] { lst_clients.SelectedItem.ToString(), comboBox1.SelectedItem.ToString() });
                
            }
        }

        private void WaitHandler(object sender, EventArgs e)
        {
            this.Enabled = false;
            wait = new WaitForm();
            wait.FormClosed += CloseFormHandler;
            Thread tr = new Thread(new ThreadStart(OpenWaitForm));
            tr.Start();
        }

        private void OpenWaitForm()
        {
            wait.ShowDialog();
        }

        private void AnswerFormHandler(object sender, EventArgs e)
        {
            this.Enabled = false;
            
            AnswerForm answerForm = new AnswerForm(handShake, sender);
            answerForm.FormClosed += CloseFormHandler;
            answerForm.ShowDialog();
        }

        private void CancleHandler(object sender, EventArgs e)
        {
            wait.Close();
            this.Enabled = false;
            MessageBox.Show("Приглашение откланено");
            this.Enabled = true;
            wait = null;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseFormHandler(object sender, EventArgs e)
        {
            this.Enabled = true;
        }

        public void EnabledHandler(object sender, EventArgs e)
        {
            this.Enabled = false;
        }

        private void WaitFormClose(object sender, EventArgs e)
        {
            if (wait != null)
                wait.Close();
        }
    }
}
