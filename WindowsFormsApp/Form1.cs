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

        private void Form1_Load(object sender, EventArgs e)
        {
            
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {          
          //  SettingsSaver.SettingsSaver.SaveFormSettings(this);
          //  MessageBox.Show("Success");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
          //  // SaveSettings.ShowFormInfo(SaveSettings.LoadFromXml("info.xml"));
          //  SettingsSaver.SettingsSaver.RestoreFormSettings(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
        }
    }
}
