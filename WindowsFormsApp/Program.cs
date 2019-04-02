using System;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;
            SettingsSaver.SettingsSaver.Instance.Init(indent:true);
            Application.Run(new FormTest());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            SettingsSaver.SettingsSaver.Instance.SaveData();
        }
    }
}
