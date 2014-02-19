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

namespace zadaniewin3
{
    public partial class Form1 : Form
    {
        private StartWindow startWindow = new StartWindow();
        private bool yin;
        private int size;
        private int[,] table;
        private int wolne;

        public Form1()
        {
            InitializeComponent();
            startWindow.ShowDialog();
            yin = true;
            size = 3;
            RozlozPlansze();
        }

        private void RozlozPlansze()
        {
            wolne = size * size;
            table = new int[size, size];

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            tableLayoutPanel1.ColumnCount = table.GetLength(0);
            tableLayoutPanel1.RowCount = table.GetLength(1);

            for (int i = 0; i < tableLayoutPanel1.ColumnCount; ++i)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, (float)100.0 / table.GetLength(0)));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)100.0 / table.GetLength(1)));

                for (int j = 0; j < tableLayoutPanel1.RowCount; ++j)
                {
                    RoundButton b = new RoundButton() { Text = "", Tag = new Point(i, j), Dock = DockStyle.Fill, BackColor = Color.Transparent, Margin = new Padding(0, 0, 0, 0), Visible = true };
                    b.ContextMenuStrip = contextMenuStrip1;
                    b.Click += roundButtonClick;
                    tableLayoutPanel1.Controls.Add(b);
                }
            }
            tableLayoutPanel1.Enabled = true;
            this.Refresh();
        }

        private void roundButtonClick(object sender, EventArgs e)
        {            
            RoundButton b = (RoundButton)sender;
            Point p = (Point)b.Tag;
            if (b.DomyslnyKsztalt)
            {
                b.DomyslnyKsztalt = false;
                if (yin)
                {
                    b.Odwrotnie = false;
                    yin = false;
                    table[p.X, p.Y] = 1;
                }
                else
                {
                    b.Odwrotnie = true;
                    yin = true;
                    table[p.X, p.Y] = -1;
                }
                --wolne;
            }
            this.Refresh();
            Sprawdz(p.X, p.Y);
        }

        private void Sprawdz(int x, int y)
        {
            bool koniec = false;
            bool jest = true;
            int i = x, j = y;
            while (jest && ++i < table.GetLength(1))
                jest = table[i, j] == table[x, y];
            i = x;
            while (jest && --i >= 0)
                jest = table[i, j] == table[x, y];
            if (jest)                            
                koniec = true;            
            jest = true;
            i = x;
            while (jest && ++j < table.GetLength(0))
                jest = table[i, j] == table[x, y];
            j = y;
            while (jest && --j >= 0)
                jest = table[i, j] == table[x, y];
            if (jest)                            
                koniec = true;            
            if (koniec)
                if (MessageBox.Show("Win - Shall we play a game?", "buhaha", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    RozlozPlansze();
                else
                    this.Close();
            else if (wolne == 0)
                if (MessageBox.Show("Draw - Shall we play a game?", "buhaha", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    RozlozPlansze();
                else
                    this.Close();
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            RoundButton b = (RoundButton)contextMenuStrip1.SourceControl;
            Point p = (Point)b.Tag;
            if (!b.DomyslnyKsztalt)
                ++wolne;
            b.DomyslnyKsztalt = true;
            table[p.X, p.Y] = 0;
            this.Refresh();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            size = trackBar1.Value + 2;
            RozlozPlansze();
        }
    }
}
