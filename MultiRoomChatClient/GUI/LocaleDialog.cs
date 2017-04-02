using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient.GUI
{
    public partial class LocaleDialog : Form
    {
        public LocaleDialog()
        {
            InitializeComponent();
        }

        private void LocaleDialog_Load(object sender, EventArgs e)
        {
            this.Text = ResourceProvider.GetValue("locale-dialog.title");
            this.label_msg.Text = ResourceProvider.GetValue("locale-dialog.label");
            this.combo_langs.Items.AddRange(ResourceProvider.GetLanguageList());
            this.combo_langs.SelectedText = ResourceProvider.lang;
            this.btn_OK.DialogResult = DialogResult.OK;
            this.btn_Cancel.DialogResult = DialogResult.Cancel;
            btn_OK.Text = ResourceProvider.GetValue("common.ok");
            btn_Cancel.Text = ResourceProvider.GetValue("common.cancel");
        }

        private void combo_langs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResourceProvider.SetLocale(combo_langs.SelectedItem.ToString());
        }
    }
}
