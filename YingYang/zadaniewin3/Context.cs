using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace zadaniewin3
{
    class Context : ApplicationContext
    {
        private StartWindow s = new StartWindow();
        private Form1 f = new Form1();

        public Context()
        {
            this.MainForm = s;
            //s.ShowDialog();
            //Thread.Sleep(2000);
            //s.Close();
            //f.Show();
        }
    }
}
