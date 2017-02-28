using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient
{
    public partial class TabbedMessageList : UserControl
    {
        public delegate void  roomname(string name);
        public event roomname roomName;
        public TabPage selectedTab { get; set; }
        public TabbedMessageList()
        {
            InitializeComponent();
        }
        public int getCountRoom()
        {
            return this.tabControl1.TabPages.Count;
        }
        
        public void SelectActive(RoomObjExt room)
        {
            var tab = this.tabControl1.SelectedTab;
            roomName(tab.Text.ToString());
            (tab.Tag as RoomObjExt).SetActive();
        }

        public void AddRoom(RoomObjExt room)
        {
            foreach (TabPage tmp in this.tabControl1.TabPages)
            {
                if(tmp.Tag == room)
                {
                    return;
                }
            }
            TabPage tp = new TabPage(room.Name);
            tp.Tag = room;
            ListBox lb = new ListBox();
            
            lb.Dock = DockStyle.Fill;
            lb.HorizontalScrollbar = true;
            lb.DataSource = room.Messages;


           
            tabControl1.SelectedTab = tp;
            tp.Controls.Add(lb);

            //(selectedTab?.Tag as RoomObjExt)?.SetBg();
            room.Bind();
            room.SetActive();
            this.tabControl1.TabPages.Add(tp);
            selectedTab = tp;
            tp.Select();


            room.MessageReceived += (x) => {
                /////kostyl
                lb.Invoke(new Action(() => {
                    lb.DataSource = null;
                    lb.DataSource = room.Messages;
                    int visibleItems = lb.ClientSize.Height / lb.ItemHeight;
                    lb.TopIndex = Math.Max(lb.Items.Count - visibleItems + 1, 0);
                }));
            };
            room.NotificationUpdated += (x) =>
            {
                if(!(tp.Tag as RoomObjExt).active)
                {
                    if(x > 0)
                    {
                        string label = room.Name + "(" + x + ")";
                        tp.Invoke(new Action(() => tp.Text = label));
                    }
                }
                else
                {
                    tp.Invoke(new Action(() => tp.Text = room.Name));
                }
            };
        }

        public void CloseRoom()
        {
            var tab = this.tabControl1.SelectedTab;
            if (tab == null || tab.ToString() == "")
                return;
            this.tabControl1.TabPages.Remove(tab);
            RequestManager.LeaveRoom(((RoomObjExt)(tab.Tag)).Name);
            RoomObjExt room = tab.Tag as RoomObjExt;
            room.Unbind(); 
        }

        public void CloseAllRooms()
        {
            while(tabControl1.TabPages.Count > 0)
            {
                tabControl1.SelectedIndex = 0;
                CloseRoom();
            }
        }

        public void SendMessage(string msg)
        {
            var tab = tabControl1.SelectedTab;
            if (tab == null || tab.ToString() == "")
                return;
            RoomObjExt current = (RoomObjExt)tab.Tag;
            current.SendMessage(msg);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            (selectedTab?.Tag as RoomObjExt)?.SetBg();
            selectedTab = tabControl1.SelectedTab;
            (tabControl1.SelectedTab?.Tag as RoomObjExt)?.SetActive();
        }
    }
}
