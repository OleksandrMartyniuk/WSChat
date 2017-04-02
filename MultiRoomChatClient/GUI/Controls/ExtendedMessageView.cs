using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient.GUI.Controls
{
    public partial class ExtendedMessageView : UserControl
    {
        public RoomObjExt room;
        public ExtendedMessageView(RoomObjExt room)
        {
            this.room = room;
            InitializeComponent();
            listbox_main.DataSource = room.Messages;
            room.MessageReceived += (x) => Invoke(new Action(() => Rebind()));
            room.AllHistoryReceived += () => Invoke(new Action(() => lnk_getMsgs.Visible = false));
        }

        private void lnk_getMsgs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RequestManager.RequestMessageList(room.Name, room.Messages.Count > 0 ? room.Messages[0].TimeStamp : DateTime.Now);
        }

        private void Rebind()
        {
            listbox_main.DataSource = null;
            listbox_main.DataSource = room.Messages;
            int visibleItems = listbox_main.ClientSize.Height / listbox_main.ItemHeight;
            listbox_main.TopIndex = Math.Max(listbox_main.Items.Count - visibleItems + 1, 0);
        }

        private void ExtendedMessageView_Paint(object sender, PaintEventArgs e)
        {
            this.lnk_getMsgs.Text = ResourceProvider.GetValue("chat.buttons.get-history");
        }
    }
}
