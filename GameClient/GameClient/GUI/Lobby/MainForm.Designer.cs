namespace GameClient
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
            this.txt_login = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.lbl_login = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_registration = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_log_out
            // 
            this.btn_log_out.Location = new System.Drawing.Point(207, 9);
            this.btn_log_out.Name = "btn_log_out";
            this.btn_log_out.Size = new System.Drawing.Size(91, 23);
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
            this.lst_clients.Location = new System.Drawing.Point(15, 85);
            this.lst_clients.Name = "lst_clients";
            this.lst_clients.Size = new System.Drawing.Size(120, 199);
            this.lst_clients.TabIndex = 5;
            // 
            // btn_invite
            // 
            this.btn_invite.Enabled = false;
            this.btn_invite.Location = new System.Drawing.Point(15, 324);
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
            this.btn_refresh.Location = new System.Drawing.Point(16, 295);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(119, 23);
            this.btn_refresh.TabIndex = 7;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            // 
            // txt_login
            // 
            this.txt_login.Location = new System.Drawing.Point(88, 9);
            this.txt_login.Name = "txt_login";
            this.txt_login.Size = new System.Drawing.Size(100, 20);
            this.txt_login.TabIndex = 8;
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(207, 9);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(91, 23);
            this.btn_login.TabIndex = 9;
            this.btn_login.Text = "Log In";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // lbl_login
            // 
            this.lbl_login.AutoSize = true;
            this.lbl_login.Location = new System.Drawing.Point(13, 12);
            this.lbl_login.Name = "lbl_login";
            this.lbl_login.Size = new System.Drawing.Size(33, 13);
            this.lbl_login.TabIndex = 10;
            this.lbl_login.Text = "Login";
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(13, 45);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(53, 13);
            this.lbl_password.TabIndex = 11;
            this.lbl_password.Text = "Password";
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(88, 42);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(100, 20);
            this.txt_password.TabIndex = 12;
            // 
            // btn_registration
            // 
            this.btn_registration.Location = new System.Drawing.Point(207, 42);
            this.btn_registration.Name = "btn_registration";
            this.btn_registration.Size = new System.Drawing.Size(91, 23);
            this.btn_registration.TabIndex = 13;
            this.btn_registration.Text = "Registration";
            this.btn_registration.UseVisualStyleBackColor = true;
            this.btn_registration.Click += new System.EventHandler(this.btn_registration_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "XO"});
            this.comboBox1.Location = new System.Drawing.Point(177, 85);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 14;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 437);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_registration);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.lbl_login);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_login);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.btn_invite);
            this.Controls.Add(this.lst_clients);
            this.Controls.Add(this.btn_log_out);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btn_log_out;
        public System.Windows.Forms.ListBox lst_clients;
        public System.Windows.Forms.Button btn_invite;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.TextBox txt_login;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Label lbl_login;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btn_registration;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

