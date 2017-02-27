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
    public partial class  PrivateMessageForm : Form
    {
        public string Recipient;
        private List<ChatMessage> Messages;
        public SuperDuperChat Parent;
        public PrivateMessageForm(string username, SuperDuperChat p)
        {
            InitializeComponent();
            this.Parent = p;
            Messages = new List<ChatMessage>();
            Recipient = username;
            Text = username;
            //////var h = Client.PrivateHistory.GetHistory(Client.Username + @"-" + Recipient);
            ////////if(h!= null)
            ////////{
            ////////    Messages.AddRange(h);
            ////////}
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public void AppendMessage(ChatMessage msg)
        {
            list_msg.Items.Add(msg);
            //Client.PrivateHistory.AppendMessage(Client.Username + @"-" + Recipient, msg);
            ScrollDown();
        }

        private void ScrollDown()
        {
            int visibleItems = list_msg.ClientSize.Height / list_msg.ItemHeight;
            list_msg.TopIndex = Math.Max(list_msg.Items.Count - visibleItems + 1, 0);
        }

        private void OnMessageHistoryReceived(string to, ChatMessage[] messages)
        {
            if(to != this.Recipient)
            {
                return;
            }

            
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string message = text_msg.Text;
            ChatMessage msg = new ChatMessage(Client.Username, message, DateTime.Now);
            if(message.Length > 0)
            {
                AppendMessage(msg);
                RequestManager.SendPrivateMessage(Recipient, msg);
                text_msg.Clear();
            }
        }

        private void PrivateMessageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Parent.PMFormRemove(this);
        }
    }
}
