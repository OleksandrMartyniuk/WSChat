using Core;
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

        public SuperDuperChat()
        {
            InitializeComponent();

            Manager = new RoomManager();

            Manager.MessageListUpdated += () => Invoke(new Action(kostylRefreshList));
            Manager.RoomDataUpdated    += () => Invoke(new Action(onRoomDataUpdated));
        }

        private void kostylRefreshList()
        {
            listBox_msg.DataSource = null;
            listBox_msg.DataSource = Manager.Active.Messages;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string msg = tb_message.Text;
            if(msg.Length > 0)
            {
                RequestManager.SendMessage(msg);
                Manager.AddMessage(new ChatMessage(Client.ClientObject, msg, DateTime.Now) );
                kostylRefreshList();
            }

            tb_message.Clear();
        }

        private void onRoomDataUpdated()
        {
            tree_Room.Nodes.Clear();
            foreach (RoomObjExt room in Manager.Rooms)
            {
                TreeNode roomNode = RoomToTreeNode(room);
                tree_Room.Nodes.Add(roomNode);
                if(Manager.Active == room)
                {
                    roomNode.BackColor = Color.Blue;
                }
            }
        }

        private TreeNode RoomToTreeNode(RoomObjExt room)
        {
            TreeNode roomNode = new TreeNode(room.Name);
            roomNode.Tag = room;
            foreach(Person p in room.clients)
            {
                TreeNode clnt = new TreeNode(p.userName);
                clnt.Tag = p;
                roomNode.Nodes.Add(clnt);
            }
            return roomNode;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            RequestManager.Logout();
            this.Close();
        }

        private void btn_createRoom_Click(object sender, EventArgs e)
        {
            string newRoom = textBox_newRoom.Text;
            if(newRoom.Length > 0)
            Manager.CreateRoom(newRoom);
            textBox_newRoom.Clear();
        }

        private void tree_Room_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(tree_Room.SelectedNode.Tag.ToString());
            var tag = tree_Room.SelectedNode.Tag;
            if (tag is RoomObjExt)
            {
                Manager.MoveTo(tag as RoomObjExt);
            }
            else if(tag is Person)
            {

                //// тут будет личка
            }
        }
    }
}
