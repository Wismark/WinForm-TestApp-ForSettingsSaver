using System;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
            SettingsSaver.SettingsSaver.Instance.LookAfterForm(this);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
        }
    }
}
