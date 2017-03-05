﻿namespace GameClient
{
    partial class MainForm
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
            this.btn_log_out = new System.Windows.Forms.Button();
            this.lst_clients = new System.Windows.Forms.ListBox();
            this.btn_invite = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_log_out
            // 
            this.btn_log_out.Location = new System.Drawing.Point(10, 12);
            this.btn_log_out.Name = "btn_log_out";
            this.btn_log_out.Size = new System.Drawing.Size(121, 23);
            this.btn_log_out.TabIndex = 4;
            this.btn_log_out.Text = "Log Out";
            this.btn_log_out.UseVisualStyleBackColor = true;
            this.btn_log_out.Visible = false;
            this.btn_log_out.Click += new System.EventHandler(this.btn_log_out_Click);
            // 
            // lst_clients
            // 
            this.lst_clients.Enabled = false;
            this.lst_clients.FormattingEnabled = true;
            this.lst_clients.Location = new System.Drawing.Point(12, 41);
            this.lst_clients.Name = "lst_clients";
            this.lst_clients.Size = new System.Drawing.Size(247, 199);
            this.lst_clients.TabIndex = 5;
            // 
            // btn_invite
            // 
            this.btn_invite.Enabled = false;
            this.btn_invite.Location = new System.Drawing.Point(139, 275);
            this.btn_invite.Name = "btn_invite";
            this.btn_invite.Size = new System.Drawing.Size(120, 23);
            this.btn_invite.TabIndex = 6;
            this.btn_invite.Text = "Invite";
            this.btn_invite.UseVisualStyleBackColor = true;
            this.btn_invite.Click += new System.EventHandler(this.btn_invite_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Enabled = false;
            this.btn_refresh.Location = new System.Drawing.Point(12, 246);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(119, 23);
            this.btn_refresh.TabIndex = 7;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "XO"});
            this.comboBox1.Location = new System.Drawing.Point(138, 248);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 14;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 311);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.btn_invite);
            this.Controls.Add(this.lst_clients);
            this.Controls.Add(this.btn_log_out);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button btn_log_out;
        public System.Windows.Forms.ListBox lst_clients;
        public System.Windows.Forms.Button btn_invite;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

