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
    public partial class RoomContextMenu : ContextMenuStrip
    {
        RoomObjExt room;
        SuperDuperChat parent;
        public RoomContextMenu(RoomObjExt room, SuperDuperChat parent)
        {
            InitializeComponent();
            this.room = room;
            this.parent = parent;
            bool admin = parent.GetType() == typeof(AdminForm);

            ToolStripMenuItem btn_enter = new ToolStripMenuItem("Enter room");
            btn_enter.Click += (sender, args) => parent.tabbedMsgList.AddRoom(room);
            ToolStripMenuItem btn_close = new ToolStripMenuItem("Close room");
            btn_close.Click += (sender, args) => RequestManager.CloseRoom(room.Name);

            Items.Add(btn_enter);
            if(room.Creator == Client.Username || admin)
            {
                Items.Add(btn_close);
            }
        }
    }
}
