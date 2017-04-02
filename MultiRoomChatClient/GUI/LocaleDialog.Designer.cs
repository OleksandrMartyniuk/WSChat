namespace MultiRoomChatClient.GUI
{
    partial class LocaleDialog
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
            this.combo_langs = new System.Windows.Forms.ComboBox();
            this.label_msg = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // combo_langs
            // 
            this.combo_langs.FormattingEnabled = true;
            this.combo_langs.Location = new System.Drawing.Point(30, 95);
            this.combo_langs.Name = "combo_langs";
            this.combo_langs.Size = new System.Drawing.Size(239, 21);
            this.combo_langs.TabIndex = 0;
            this.combo_langs.SelectedIndexChanged += new System.EventHandler(this.combo_langs_SelectedIndexChanged);
            // 
            // label_msg
            // 
            this.label_msg.AutoSize = true;
            this.label_msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_msg.Location = new System.Drawing.Point(46, 46);
            this.label_msg.Name = "label_msg";
            this.label_msg.Size = new System.Drawing.Size(0, 17);
            this.label_msg.TabIndex = 1;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(30, 143);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(194, 143);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // LocaleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 187);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.label_msg);
            this.Controls.Add(this.combo_langs);
            this.Name = "LocaleDialog";
            this.Text = "LocaleDialog";
            this.Load += new System.EventHandler(this.LocaleDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combo_langs;
        private System.Windows.Forms.Label label_msg;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
    }
}