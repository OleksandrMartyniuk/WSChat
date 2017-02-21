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
            this.listBox_msg = new System.Windows.Forms.ListBox();
            this.btn_createRoom = new System.Windows.Forms.Button();
            this.btn_send = new System.Windows.Forms.Button();
            this.tb_message = new System.Windows.Forms.TextBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.textBox_newRoom = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tree_Room
            // 
            resources.ApplyResources(this.tree_Room, "tree_Room");
            this.tree_Room.Name = "tree_Room";
            this.tree_Room.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tree_Room_MouseDoubleClick);
            // 
            // listBox_msg
            // 
            this.listBox_msg.FormattingEnabled = true;
            resources.ApplyResources(this.listBox_msg, "listBox_msg");
            this.listBox_msg.Name = "listBox_msg";
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
            // 
            // btn_exit
            // 
            resources.ApplyResources(this.btn_exit, "btn_exit");
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // textBox_newRoom
            // 
            resources.ApplyResources(this.textBox_newRoom, "textBox_newRoom");
            this.textBox_newRoom.Name = "textBox_newRoom";
            // 
            // SuperDuperChat
            // 
            this.AcceptButton = this.btn_send;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox_newRoom);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.tb_message);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.btn_createRoom);
            this.Controls.Add(this.listBox_msg);
            this.Controls.Add(this.tree_Room);
            this.Name = "SuperDuperChat";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TreeView tree_Room;
        private System.Windows.Forms.ListBox listBox_msg;
        private System.Windows.Forms.Button btn_createRoom;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox tb_message;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.TextBox textBox_newRoom;
    }
}

