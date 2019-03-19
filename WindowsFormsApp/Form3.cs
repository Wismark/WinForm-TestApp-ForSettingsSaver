using System;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            SettingsSaver.SettingsSaver.Instance.LookAfterForm(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SettingsSaver.SettingsSaver.SaveFormSettings(this);
            MessageBox.Show("success");
        }

        private void button2_Click(object sender, EventArgs e)
        {
           //SettingsSaver.SettingsSaver.RestoreFormSettings(this);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
