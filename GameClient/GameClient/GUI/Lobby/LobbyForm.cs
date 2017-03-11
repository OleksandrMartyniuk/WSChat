using GameClient.API.Networking;
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
        //Listener listener;
        //delegate void InvokeDelegate();
        private WaitForm wait;
        private AnswerForm answerForm;
        private RoomDialog room;
        public LobbyForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Text_name.Text = Client.Username;
            games_name.SelectedIndex = 0;
            ResponseHandler.refreshClients += RefreshClientsHandler;
            ResponseHandler.notificationLobby += ShowNotificationHandler;
          
            ResponseHandler.answer += AnswerFormHandler;
            ResponseHandler.cancle += CancleHandler;
            ResponseHandler.wait += WaitHandler;
            ResponseHandler.start += (x) => room = new RoomDialog(x);
            ResponseHandler.enabled += EnabledHandler;
        }

       
        public void RefreshClientsHandler(object sender)
        {
            object[] clients = JsonConvert.DeserializeObject<object[]>(sender.ToString());
            lst_clients.Items.Clear();
            lst_clients.Items.AddRange(clients);
        }

        void ShowNotificationHandler(object sender)
        {
             this.Enabled = false;   
             MessageBox.Show(sender.ToString());
             this.Enabled = true;
        }
        private void btn_invite_Click(object sender, EventArgs e)
        {
            if (lst_clients.SelectedItem != null)
            {
                new Listener().SendInvite(new object[] { lst_clients.SelectedItem.ToString(), games_name.SelectedItem.ToString() });
            }
        }

        private void WaitHandler()
        {
            this.Enabled = false;
            wait = new WaitForm();
            wait.FormClosed += CloseFormHandler;
            wait.ShowDialog();
        }
        private void AnswerFormHandler(object sender)
        {
            this.Enabled = false;
             answerForm = new AnswerForm(sender);
            answerForm.FormClosed += CloseFormHandler;
            answerForm.ShowDialog();
        }

        private void CancleHandler()
        {
            wait.Close();
            this.Enabled = false;
            MessageBox.Show("Приглашение откланено");
            this.Enabled = true;
            wait = null;
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            RequestManager.SendRefreshClients();
        }
      
        private void btn_log_out_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            new LoginForm().ShowDialog();

        }

        private void CloseFormHandler(object sender, EventArgs e)
        {
            this.Close();
        }

        public void EnabledHandler()
        {
           // wait.FormClosed -= CloseFormHandler;
            this.Enabled = false;
        }

        private void WaitFormClose(object sender, EventArgs e)
        {
        //    if (wait != null)
        //    //    wait.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ResponseHandler.refreshClients -= RefreshClientsHandler;
            //ResponseHandler.notificationLobby -= ShowNotificationHandler;

            //ResponseHandler.answer -= AnswerFormHandler;
            //ResponseHandler.cancle -= CancleHandler;
            //ResponseHandler.wait -= WaitHandler;

            //ResponseHandler.start -= (x) => {new Game(x);};
            
            //ResponseHandler.enabled -= EnabledHandler;
            
            //this.Dispose();
            //this.Close();
        }

      
    }
}
