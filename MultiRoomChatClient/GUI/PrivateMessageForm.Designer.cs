namespace MultiRoomChatClient
{
    partial class PrivateMessageForm
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
            this.list_msg = new System.Windows.Forms.ListBox();
            this.text_msg = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_updHistory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_msg
            // 
            this.list_msg.FormattingEnabled = true;
            this.list_msg.Location = new System.Drawing.Point(12, 12);
            this.list_msg.Name = "list_msg";
            this.list_msg.Size = new System.Drawing.Size(427, 173);
            this.list_msg.TabIndex = 0;
            // 
            // text_msg
            // 
            this.text_msg.Location = new System.Drawing.Point(12, 194);
            this.text_msg.Name = "text_msg";
            this.text_msg.Size = new System.Drawing.Size(330, 20);
            this.text_msg.TabIndex = 1;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(349, 192);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(90, 23);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_updHistory
            // 
            this.btn_updHistory.Location = new System.Drawing.Point(363, 12);
            this.btn_updHistory.Name = "btn_updHistory";
            this.btn_updHistory.Size = new System.Drawing.Size(75, 23);
            this.btn_updHistory.TabIndex = 3;
            this.btn_updHistory.Text = "GetMessages";
            this.btn_updHistory.UseVisualStyleBackColor = true;
            this.btn_updHistory.Click += new System.EventHandler(this.btn_updHistory_Click);
            // 
            // PrivateMessageForm
            // 
            this.AcceptButton = this.btn_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 225);
            this.Controls.Add(this.btn_updHistory);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.text_msg);
            this.Controls.Add(this.list_msg);
            this.MaximizeBox = false;
            this.Name = "PrivateMessageForm";
            this.Text = "PrivateMessageForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PrivateMessageForm_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PrivateMessageForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list_msg;
        private System.Windows.Forms.TextBox text_msg;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_updHistory;
    }
}