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
    }
}
