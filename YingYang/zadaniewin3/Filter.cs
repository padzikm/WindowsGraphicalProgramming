using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;

namespace zadaniewin3
{
    class Filter : IMessageFilter
    {
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x0201)
            {
                SendMessage(m.HWnd, 0x204, m.WParam, m.LParam);
                return true;
            }
            else if (m.Msg == 0x0202)
            {
                SendMessage(m.HWnd, 0x205, m.WParam, m.LParam);
                return true;
            }
            else if (m.Msg == 0x204)
            {
                SendMessage(m.HWnd, 0x201, m.WParam, m.LParam);
                return true;
            }
            else if (m.Msg == 0x205)
            {
                SendMessage(m.HWnd, 0x202, m.WParam, m.LParam);
                return true;
            }
            else
                return false;
        }
    }
}
