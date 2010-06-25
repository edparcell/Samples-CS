using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SystemTray
{
    static class Program
    {
        private static LogForm logForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            logForm = new LogForm();
            Application.Run();
        }

        public static LogForm LogForm { get { return logForm; } }
    }
}
