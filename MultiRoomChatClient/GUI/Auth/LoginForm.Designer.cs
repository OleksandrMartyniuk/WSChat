namespace MultiRoomChatClient
{
    partial class LoginForm
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
            this.lbl_login = new System.Windows.Forms.Label();
            this.input_login = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.input_password = new System.Windows.Forms.TextBox();
            this.lbl_password = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.link_forgot = new System.Windows.Forms.LinkLabel();
            this.link_Register = new System.Windows.Forms.LinkLabel();
            this.link_languageSelect = new System.Windows.Forms.LinkLabel();
            this.picbox_facebook = new System.Windows.Forms.PictureBox();
            this.picbox_google = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_facebook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_google)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_login
            // 
            this.lbl_login.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_login.AutoSize = true;
            this.lbl_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_login.Location = new System.Drawing.Point(32, 15);
            this.lbl_login.Name = "lbl_login";
            this.lbl_login.Size = new System.Drawing.Size(71, 25);
            this.lbl_login.TabIndex = 0;
            this.lbl_login.Text = "Login:";
            // 
            // input_login
            // 
            this.input_login.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.input_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.input_login.Location = new System.Drawing.Point(112, 12);
            this.input_login.Name = "input_login";
            this.input_login.Size = new System.Drawing.Size(201, 31);
            this.input_login.TabIndex = 1;
            // 
            // btn_login
            // 
            this.btn_login.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_login.Location = new System.Drawing.Point(114, 129);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(128, 27);
            this.btn_login.TabIndex = 3;
            this.btn_login.Text = "Log In";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // input_password
            // 
            this.input_password.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.input_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.input_password.Location = new System.Drawing.Point(113, 56);
            this.input_password.Name = "input_password";
            this.input_password.PasswordChar = '*';
            this.input_password.Size = new System.Drawing.Size(199, 31);
            this.input_password.TabIndex = 2;
            // 
            // lbl_password
            // 
            this.lbl_password.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_password.AutoSize = true;
            this.lbl_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_password.Location = new System.Drawing.Point(2, 59);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(112, 25);
            this.lbl_password.TabIndex = 4;
            this.lbl_password.Text = "Password:";
            // 
            // lbl_status
            // 
            this.lbl_status.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_status.AutoEllipsis = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_status.ForeColor = System.Drawing.Color.Red;
            this.lbl_status.Location = new System.Drawing.Point(29, 90);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(282, 36);
            this.lbl_status.TabIndex = 26;
            this.lbl_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // link_forgot
            // 
            this.link_forgot.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.link_forgot.AutoSize = true;
            this.link_forgot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.link_forgot.Location = new System.Drawing.Point(98, 162);
            this.link_forgot.Name = "link_forgot";
            this.link_forgot.Size = new System.Drawing.Size(153, 17);
            this.link_forgot.TabIndex = 29;
            this.link_forgot.TabStop = true;
            this.link_forgot.Text = "Forgot your password?";
            this.link_forgot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.link_forgot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_forgot_LinkClicked);
            // 
            // link_Register
            // 
            this.link_Register.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.link_Register.AutoSize = true;
            this.link_Register.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.link_Register.Location = new System.Drawing.Point(141, 229);
            this.link_Register.Name = "link_Register";
            this.link_Register.Size = new System.Drawing.Size(61, 17);
            this.link_Register.TabIndex = 30;
            this.link_Register.TabStop = true;
            this.link_Register.Text = "Register";
            this.link_Register.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_Register_LinkClicked);
            // 
            // link_languageSelect
            // 
            this.link_languageSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.link_languageSelect.AutoSize = true;
            this.link_languageSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.link_languageSelect.Location = new System.Drawing.Point(268, 248);
            this.link_languageSelect.Name = "link_languageSelect";
            this.link_languageSelect.Size = new System.Drawing.Size(0, 17);
            this.link_languageSelect.TabIndex = 31;
            this.link_languageSelect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_languageSelect_LinkClicked);
            // 
            // picbox_facebook
            // 
            this.picbox_facebook.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picbox_facebook.BackgroundImage = global::MultiRoomChatClient.Properties.Resources.facebook_btn;
            this.picbox_facebook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picbox_facebook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picbox_facebook.InitialImage = null;
            this.picbox_facebook.Location = new System.Drawing.Point(186, 186);
            this.picbox_facebook.Name = "picbox_facebook";
            this.picbox_facebook.Size = new System.Drawing.Size(130, 30);
            this.picbox_facebook.TabIndex = 28;
            this.picbox_facebook.TabStop = false;
            this.picbox_facebook.Click += new System.EventHandler(this.picbox_facebook_Click);
            // 
            // picbox_google
            // 
            this.picbox_google.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picbox_google.BackgroundImage = global::MultiRoomChatClient.Properties.Resources.google_btn;
            this.picbox_google.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picbox_google.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picbox_google.InitialImage = global::MultiRoomChatClient.Properties.Resources.google_btn;
            this.picbox_google.Location = new System.Drawing.Point(40, 187);
            this.picbox_google.Name = "picbox_google";
            this.picbox_google.Size = new System.Drawing.Size(130, 30);
            this.picbox_google.TabIndex = 27;
            this.picbox_google.TabStop = false;
            this.picbox_google.Click += new System.EventHandler(this.picbox_google_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btn_login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 274);
            this.Controls.Add(this.link_languageSelect);
            this.Controls.Add(this.link_Register);
            this.Controls.Add(this.link_forgot);
            this.Controls.Add(this.picbox_facebook);
            this.Controls.Add(this.picbox_google);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.input_password);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.input_login);
            this.Controls.Add(this.lbl_login);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Text = "Login";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LoginForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.picbox_facebook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_google)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_login;
        private System.Windows.Forms.TextBox input_login;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.TextBox input_password;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.PictureBox picbox_google;
        private System.Windows.Forms.PictureBox picbox_facebook;
        private System.Windows.Forms.LinkLabel link_forgot;
        private System.Windows.Forms.LinkLabel link_Register;
        private System.Windows.Forms.LinkLabel link_languageSelect;
    }
}