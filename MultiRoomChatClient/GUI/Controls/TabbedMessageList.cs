﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiRoomChatClient.GUI.Controls;

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

            ExtendedMessageView emv = new ExtendedMessageView(room);
            emv.Dock = DockStyle.Fill;
            tp.Controls.Add(emv);
           
            tabControl1.SelectedTab = tp;
            
            room.Bind();
            room.SetActive();
            this.tabControl1.TabPages.Add(tp);
            selectedTab = tp;
            tp.Select();

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
