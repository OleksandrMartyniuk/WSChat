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
    public partial class LobbyForm : Form
    {
        Listener listener;
        delegate void InvokeDelegate();
        public LobbyForm(Listener listener,object sender)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.listener = listener;
            Text_name.Text = "name: " + sender.ToString();

            listener.lobby.RefreshClients += RefreshClientsHandler;
            listener.lobby.Notification   += ShowNotificationHandler;

            listener.handShake.Answer += AnswerFormHandler;
            listener.handShake.Cancle += CancleHandler;
            listener.handShake.Wait += WaitHandler;

            listener.game.Close += WaitFormClose;
            listener.game.roomdialog.FormClosed += CloseFormHandler;
            listener.game.Enabled += EnabledHandler;
        }

        public void RefreshClientsHandler(object sender, EventArgs e)
        {
            object[] clients = JsonConvert.DeserializeObject<object[]>(sender.ToString());
            lst_clients.Items.Clear();
            lst_clients.Items.AddRange(clients);
        }

        void ShowNotificationHandler(object sender, EventArgs e)
        {
          //  this.Enabled = false;
            
        //    MessageBox.Show(sender as string);
          //  this.Enabled = true;
        }
        private void btn_invite_Click(object sender, EventArgs e)
        {
            if (lst_clients.SelectedItem != null)
            {
                listener.handShake.SendInvite(new object[] { lst_clients.SelectedItem.ToString(), comboBox1.SelectedItem.ToString() });
            }
        }

        private void WaitHandler(object sender, EventArgs e)
        {
            //this.Enabled = false;
            //wait = new WaitForm();
            //wait.FormClosed += CloseFormHandler;
            //Thread tr = new Thread(new ThreadStart(OpenWaitForm));
            //tr.Start();
        }

        private void OpenWaitForm()
        {
           // wait.ShowDialog();
        }

        private void AnswerFormHandler(object sender, EventArgs e)
        {
          //  this.Enabled = false;
            
            AnswerForm answerForm = new AnswerForm(listener.handShake, sender);
            answerForm.FormClosed += CloseFormHandler;
            answerForm.ShowDialog();
        }

        private void CancleHandler(object sender, EventArgs e)
        {
            //wait.Close();
            //this.Enabled = false;
            //MessageBox.Show("Приглашение откланено");
            //this.Enabled = true;
            //wait = null;
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            listener.lobby.SendRefreshClients();
        }
      
        private void btn_log_out_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            new LoginForm().ShowDialog();

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
        //    if (wait != null)
            //    wait.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            listener.lobby.RefreshClients -= RefreshClientsHandler;
            listener.lobby.Notification -= ShowNotificationHandler;
            listener.game.Close -= WaitFormClose;
            listener.game.roomdialog.FormClosed -= CloseFormHandler;
            listener.game.Enabled -= EnabledHandler;
            listener.handShake.Answer -= AnswerFormHandler;
            listener.handShake.Cancle -= CancleHandler;
            listener.handShake.Wait -= WaitHandler;

            this.Close();
            this.Dispose();
        }

      
    }
}
