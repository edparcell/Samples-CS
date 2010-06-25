using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemTray
{
    public partial class LogForm : Form
    {
        private bool doExit = false;

        public LogForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doExit = true;
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            if (!doExit)
            {
                e.Cancel = true;
            }
        }

        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
        }


    }
}
