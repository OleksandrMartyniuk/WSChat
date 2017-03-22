namespace MultiRoomChatClient.GUI.Controls
{
    partial class ExtendedMessageView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listbox_main = new System.Windows.Forms.ListBox();
            this.lnk_getMsgs = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // listbox_main
            // 
            this.listbox_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listbox_main.FormattingEnabled = true;
            this.listbox_main.HorizontalScrollbar = true;
            this.listbox_main.Location = new System.Drawing.Point(0, 0);
            this.listbox_main.Name = "listbox_main";
            this.listbox_main.Size = new System.Drawing.Size(425, 235);
            this.listbox_main.TabIndex = 0;
            // 
            // lnk_getMsgs
            // 
            this.lnk_getMsgs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lnk_getMsgs.AutoSize = true;
            this.lnk_getMsgs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnk_getMsgs.Location = new System.Drawing.Point(179, 0);
            this.lnk_getMsgs.Name = "lnk_getMsgs";
            this.lnk_getMsgs.Size = new System.Drawing.Size(53, 13);
            this.lnk_getMsgs.TabIndex = 1;
            this.lnk_getMsgs.TabStop = true;
            this.lnk_getMsgs.Text = "load more";
            this.lnk_getMsgs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnk_getMsgs.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnk_getMsgs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_getMsgs_LinkClicked);
            // 
            // ExtendedMessageView
            // 
            this.Controls.Add(this.lnk_getMsgs);
            this.Controls.Add(this.listbox_main);
            this.Name = "ExtendedMessageView";
            this.Size = new System.Drawing.Size(425, 235);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listbox_main;
        private System.Windows.Forms.LinkLabel lnk_getMsgs;
    }
}
