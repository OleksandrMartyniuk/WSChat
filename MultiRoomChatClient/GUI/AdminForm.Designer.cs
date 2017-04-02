namespace MultiRoomChatClient
{
    partial class AdminForm : SuperDuperChat
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
            this.btn_unban = new System.Windows.Forms.Button();
            this.btn_banForever = new System.Windows.Forms.Button();
            this.dateTime = new System.Windows.Forms.DateTimePicker();
            this.btn_ban = new System.Windows.Forms.Button();
            this.tb_selectedUser = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tree_Room
            // 
            this.tree_Room.LineColor = System.Drawing.Color.Black;
            // 
            // button1
            // 
            this.btn_unban.Location = new System.Drawing.Point(505, 39);
            this.btn_unban.Name = "btn_unban";
            this.btn_unban.Size = new System.Drawing.Size(104, 23);
            this.btn_unban.TabIndex = 7;
            this.btn_unban.Text = "Unban";
            this.btn_unban.UseVisualStyleBackColor = true;
            this.btn_unban.Click += new System.EventHandler(this.Unban_Click);
            // 
            // btn_banForever
            // 
            this.btn_banForever.Location = new System.Drawing.Point(504, 68);
            this.btn_banForever.Name = "btn_banForever";
            this.btn_banForever.Size = new System.Drawing.Size(105, 23);
            this.btn_banForever.TabIndex = 8;
            this.btn_banForever.Text = "Ban Forever";
            this.btn_banForever.UseVisualStyleBackColor = true;
            this.btn_banForever.Click += new System.EventHandler(this.btn_banForever_Click);
            // 
            // dateTime
            // 
            this.dateTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTime.Location = new System.Drawing.Point(507, 126);
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(104, 20);
            this.dateTime.TabIndex = 9;
            // 
            // Ban_Till
            // 
            this.btn_ban.Location = new System.Drawing.Point(506, 97);
            this.btn_ban.Name = "Ban_Till";
            this.btn_ban.Size = new System.Drawing.Size(105, 23);
            this.btn_ban.TabIndex = 10;
            this.btn_ban.Text = "Ban Till";
            this.btn_ban.UseVisualStyleBackColor = true;
            this.btn_ban.Click += new System.EventHandler(this.Ban_Till_Click);
            // 
            // tb_selectedUser
            // 
            this.tb_selectedUser.Enabled = false;
            this.tb_selectedUser.Location = new System.Drawing.Point(506, 13);
            this.tb_selectedUser.Name = "tb_selectedUser";
            this.tb_selectedUser.Size = new System.Drawing.Size(104, 20);
            this.tb_selectedUser.TabIndex = 11;
            // 
            // AdminForm
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AdminForm_Paint);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 314);
            this.Controls.Add(this.tb_selectedUser);
            this.Controls.Add(this.btn_ban);
            this.Controls.Add(this.dateTime);
            this.Controls.Add(this.btn_banForever);
            this.Controls.Add(this.btn_unban);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.Controls.SetChildIndex(this.tree_Room, 0);
            this.Controls.SetChildIndex(this.btn_unban, 0);
            this.Controls.SetChildIndex(this.btn_banForever, 0);
            this.Controls.SetChildIndex(this.dateTime, 0);
            this.Controls.SetChildIndex(this.btn_ban, 0);
            this.Controls.SetChildIndex(this.tb_selectedUser, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_unban;
        private System.Windows.Forms.Button btn_banForever;
        private System.Windows.Forms.DateTimePicker dateTime;
        private System.Windows.Forms.Button btn_ban;
        private System.Windows.Forms.TextBox tb_selectedUser;
        
    }
}