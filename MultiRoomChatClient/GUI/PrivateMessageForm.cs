using Core;
using MultiRoomChatClient.GUI;
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
        SuperDuperChat Parent;

        public PrivateMessageForm(string username, SuperDuperChat p)
        {
            InitializeComponent();
            this.Parent = p;
            Recipient = username;
            Text = username;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ResponseHandler.PrivateHistoryReceived += (user,history) => Invoke(new Action<string, ChatMessage[]>(PrependHistory), user, history);
        }

        public void AppendMessage(ChatMessage msg)
        {
            list_msg.Items.Add(msg);
            ScrollDown();
        }

        private void ScrollDown()
        {
            int visibleItems = list_msg.ClientSize.Height / list_msg.ItemHeight;
            list_msg.TopIndex = Math.Max(list_msg.Items.Count - visibleItems + 1, 0);
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

        public void PrependHistory(string user, ChatMessage[] history)
        {
            if (user != this.Recipient)
            {
                return;
            }
            foreach (ChatMessage msg in history)
            {
                if(msg.Sender == Client.Username)
                {
                    msg.Sender = "Me";
                }
            }
            var msgs = list_msg.Items.Cast<ChatMessage>().ToList();
            list_msg.Items.Clear();
            List<ChatMessage> refreshed = new List<ChatMessage>(history);
            refreshed.AddRange(msgs);
            
            list_msg.Items.AddRange(refreshed.ToArray());
        }

        private void btn_updHistory_Click(object sender, EventArgs e)
        {
            if(list_msg.Items.Count > 0)
            {
                RequestManager.RequestPrivateMessageList(this.Recipient, (list_msg.Items[0] as ChatMessage).TimeStamp);
            }
            else
            {
                RequestManager.RequestPrivateMessageList(this.Recipient, DateTime.Now);
            }
            
        }

        private void PrivateMessageForm_Paint(object sender, PaintEventArgs e)
        {
            this.btn_send.Text = ResourceProvider.GetValue("chat.buttons.send");
            this.btn_updHistory.Text = ResourceProvider.GetValue("chat.buttons.get-history");
        }
    }
}
