namespace MultiRoomChatClient
{
    partial class SuperDuperChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuperDuperChat));
            this.tree_Room = new System.Windows.Forms.TreeView();
            this.btn_createRoom = new System.Windows.Forms.Button();
            this.btn_send = new System.Windows.Forms.Button();
            this.tb_message = new System.Windows.Forms.TextBox();
            this.tb_newRoom = new System.Windows.Forms.TextBox();
            this.btn_closeRoom = new System.Windows.Forms.Button();
            this.tabbedMessageList1 = new MultiRoomChatClient.TabbedMessageList();
            this.btn_HistoryUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tree_Room
            // 
            resources.ApplyResources(this.tree_Room, "tree_Room");
            this.tree_Room.Name = "tree_Room";
            this.tree_Room.Click += new System.EventHandler(this.tree_Room_Click);
            this.tree_Room.DoubleClick += new System.EventHandler(this.tree_Room_MouseDoubleClick);
            // 
            // btn_createRoom
            // 
            resources.ApplyResources(this.btn_createRoom, "btn_createRoom");
            this.btn_createRoom.Name = "btn_createRoom";
            this.btn_createRoom.UseVisualStyleBackColor = true;
            this.btn_createRoom.Click += new System.EventHandler(this.btn_createRoom_Click);
            // 
            // btn_send
            // 
            resources.ApplyResources(this.btn_send, "btn_send");
            this.btn_send.Name = "btn_send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // tb_message
            // 
            resources.ApplyResources(this.tb_message, "tb_message");
            this.tb_message.Name = "tb_message";
            this.tb_message.Enter += new System.EventHandler(this.tb_message_Entered);
            // 
            // tb_newRoom
            // 
            resources.ApplyResources(this.tb_newRoom, "tb_newRoom");
            this.tb_newRoom.Name = "tb_newRoom";
            this.tb_newRoom.Enter += new System.EventHandler(this.tb_newRoom_Entered);
            this.tb_newRoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_newRoom_KeyPress);
            // 
            // btn_closeRoom
            // 
            resources.ApplyResources(this.btn_closeRoom, "btn_closeRoom");
            this.btn_closeRoom.Name = "btn_closeRoom";
            this.btn_closeRoom.UseVisualStyleBackColor = true;
            this.btn_closeRoom.Click += new System.EventHandler(this.btn_closeRoom_Click);
            // 
            // tabbedMessageList1
            // 
            resources.ApplyResources(this.tabbedMessageList1, "tabbedMessageList1");
            this.tabbedMessageList1.Name = "tabbedMessageList1";
            // 
            // btn_HistoryUpdate
            // 
            resources.ApplyResources(this.btn_HistoryUpdate, "btn_HistoryUpdate");
            this.btn_HistoryUpdate.Name = "btn_HistoryUpdate";
            this.btn_HistoryUpdate.UseVisualStyleBackColor = true;
            this.btn_HistoryUpdate.Click += new System.EventHandler(this.UploadHistory_Click);
            // 
            // SuperDuperChat
            // 
            this.AcceptButton = this.btn_send;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_HistoryUpdate);
            this.Controls.Add(this.tabbedMessageList1);
            this.Controls.Add(this.btn_closeRoom);
            this.Controls.Add(this.tb_newRoom);
            this.Controls.Add(this.tb_message);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.btn_createRoom);
            this.Controls.Add(this.tree_Room);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SuperDuperChat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SuperDuperChat_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TreeView tree_Room;
        private System.Windows.Forms.Button btn_createRoom;
        private System.Windows.Forms.TextBox tb_message;
        private System.Windows.Forms.TextBox tb_newRoom;
        private System.Windows.Forms.Button btn_closeRoom;
        private TabbedMessageList tabbedMessageList1;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_HistoryUpdate;
    }
}

