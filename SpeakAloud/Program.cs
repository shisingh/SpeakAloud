using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeakAloud
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern void OutputDebugString(string lpOutputString);        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal);
            OutputMessage("Local user config path: {0}", config.FilePath);
            //SpeakAloud sa = new SpeakAloud();
            //sa.ShowDialog();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SpeakAloud());
        }

        public static void OutputMessage(string format, params object[] pars)
        {
            string msg = null;
            try
            {
                msg = string.Format(format, pars);
            }
            catch (Exception e)
            {
                msg = "Exceptoin : = " + e.Message;
            }
            OutputDebugString(msg);
        }
    }
}
