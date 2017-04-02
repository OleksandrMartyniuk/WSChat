using Core;
using MultiRoomChatClient.GUI;
using MultiRoomChatClient.GUI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient
{
    public partial class SuperDuperChat : Form
    {
        RoomManager Manager;

        public LinkedList<PrivateMessageForm> PMForms = new LinkedList<PrivateMessageForm>();
        public delegate void tree_user(string name);
        public event tree_user treename;
      
        public SuperDuperChat()
        {
            InitializeComponent();
        }

        private void SuperDuperChat_Load(object sender, EventArgs e)
        {
            Manager = new RoomManager();
            Manager.RoomDataUpdated += () => Invoke(new Action(onRoomDataUpdated));
            ResponseHandler.Banned += (msg) => Invoke(new Action<string>(Ban), msg);
            ResponseHandler.Unbanned += (msg) => Invoke(new Action<string>(unBan), msg);
            ResponseHandler.privateMessageReceived += (x) => Invoke(new Action<ChatMessage>(HandleMessage), x);
            ResponseHandler.roomError += (x) => Invoke(new Action<string>(OnRoomError), x);
            Client.NewErrorMessage += ShowConnectionError;
        }

        public void ShowConnectionError(string error)
        {
            MessageBox.Show(error);
        }

        public void HandleMessage(ChatMessage msg)
        {
            string sender = msg.Sender;
            PrivateMessageForm roomToHandle = null;
            foreach (PrivateMessageForm f in PMForms)
            {
                if (f.Recipient == sender)
                {
                    roomToHandle = f;
                }
            }
            if (roomToHandle == null)
            {
                roomToHandle = new PrivateMessageForm(sender, this);
                PMForms.AddLast(roomToHandle);
                roomToHandle.Show();
            }
            roomToHandle.AppendMessage(msg);
            roomToHandle.BringToFront();
        }

        public void PMFormRemove(PrivateMessageForm PMForm)
        {
            PMForms.Remove(PMForm);
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (tabbedMsgList.getCountRoom() == 0)
            {
                MessageBox.Show("Enter room");
                return;
            }
            string msg = tb_message.Text;
            if(msg.Length > 0)
            {
                tabbedMsgList.SendMessage(msg);   
            }
            tb_message.Clear();
        }

        private void onRoomDataUpdated()
        {
            tree_Room.Nodes.Clear();
            LinkedListNode<RoomObjExt> current = Manager.Rooms.First;
            while (current != null)
            {
                TreeNode roomNode = RoomToTreeNode(current.Value);
                tree_Room.Nodes.Add(roomNode);
                current = current.Next;
            }
        }
        private void OnRoomError(string error)
        {
            MessageBox.Show(error);
        }
        protected TreeNode RoomToTreeNode(RoomObjExt room)
        {
            TreeNode roomNode = new TreeNode(room.Name);
            roomNode.Tag = room;

            roomNode.ContextMenuStrip = new RoomContextMenu(room, this);

            if(room.Creator == Client.Username)
            {
                roomNode.NodeFont = new Font(SystemFonts.MenuFont, FontStyle.Bold);
            }

            foreach(string p in room.clients)
            {
                TreeNode clnt = new TreeNode(p);
                clnt.Tag = p;
                roomNode.Nodes.Add(clnt);
            }
            return roomNode;
        }

        private void btn_createRoom_Click(object sender, EventArgs e)
        {
            string newRoom = tb_newRoom.Text;
            foreach (var room in tree_Room.Nodes.Cast<TreeNode>().ToArray())
            {
                if (newRoom == room.Text)
                {
                    MessageBox.Show("Room already exists");
                    return;
                }
            }
            if (newRoom.Length > 0)
            {
                Manager.CreateRoom(newRoom);
            }
            tb_newRoom.Clear();
            tb_message.Focus();
        }

        public void Ban(string msg)
        {
            if (msg != null) MessageBox.Show(msg);
            btn_send.Enabled = false;
            tb_message.Enabled = false;
            btn_createRoom.Enabled = false;
        }
        public void unBan(string msg)
        {
            if(msg!= null) MessageBox.Show(msg);
            btn_send.Enabled = true;
            tb_message.Enabled = true;
            btn_createRoom.Enabled = true;
        }
        
        private void tree_Room_MouseDoubleClick(object sender, EventArgs e)
        {
            if (tree_Room.SelectedNode == null)
                return;

            var tag = tree_Room.SelectedNode.Tag;
          
            if (tag is RoomObjExt)
            {
                tabbedMsgList.AddRoom(tag as RoomObjExt);
            }
            else if ((tag is string) && tag.ToString() != Client.Username.ToString()) 
            {
                PrivateMessageForm PmForm = new PrivateMessageForm(tag as string, this);
                PMForms.AddLast(PmForm);
                PmForm.Show();
            }
        }
        private void btn_closeRoom_Click(object sender, EventArgs e)
        {
            tabbedMsgList.CloseRoom();
        }

        private void tree_Room_Click(object sender, EventArgs e)
        {
            if (tree_Room.SelectedNode == null)
                return;

            var tag = tree_Room.SelectedNode.Tag;
            if ((tag is string) && tag.ToString() != Client.Username.ToString())
            {
                this.treename?.Invoke(tag.ToString());
            }
        }
        private void tb_newRoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            if (Char.IsWhiteSpace(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsSeparator(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || tb_newRoom.Text.Length >= 40)
            {
                e.Handled = true;
                return;
            }
        }

        private void tb_newRoom_Entered(object sender, EventArgs e)
        {
            this.AcceptButton = btn_createRoom;
        }

        private void tb_message_Entered(object sender, EventArgs e)
        {
            this.AcceptButton = btn_send;
        }

        private void SuperDuperChat_FormClosed(object sender, FormClosedEventArgs e)
        {
            Manager = null;
            ResponseHandler.active = false;

            Client.NewErrorMessage -= ShowConnectionError;
            tabbedMsgList.CloseAllRooms();
            RequestManager.Logout(Client.Username.ToString());

            Client.Disconnect();
        }

        private void UploadHistory_Click(object sender, EventArgs e)
        {
            if(tabbedMsgList.selectedTab == null)
            {
                return;
            }
            var room = ((RoomObjExt)tabbedMsgList.selectedTab.Tag);
            string active = room.Name;
            if(room.Messages.Count == 0)
            {
                RequestManager.RequestMessageList(active, DateTime.Now);
            }
            else
            {
                RequestManager.RequestMessageList(active, room.Messages[0].TimeStamp);
            }
        }

        private void SuperDuperChat_Paint(object sender, PaintEventArgs e)
        {
            this.Text = ResourceProvider.GetValue("chat.titles.main") + " " + Client.Username;
            this.btn_createRoom.Text = ResourceProvider.GetValue("chat.buttons.create-room");
            this.btn_leaveRoom.Text = ResourceProvider.GetValue("chat.buttons.leave-room");
            this.btn_send.Text = ResourceProvider.GetValue("chat.buttons.send");
            this.link_languageSelect.Text = ResourceProvider.lang;
        }

        private void link_languageSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LocaleDialog d = new LocaleDialog();
            d.ShowDialog();
            Invalidate();
        }

       
    }
}
