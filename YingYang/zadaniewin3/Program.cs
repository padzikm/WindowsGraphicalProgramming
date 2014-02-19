using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace zadaniewin3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Filter filter = new Filter();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.AddMessageFilter(filter);
            Application.Run(new Form1());
        }
    }
}
