using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace niespodzianka
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public int newX { get; set; }
        public int newY { get; set; }        

        public Window1(int x, int y)
        {            
            InitializeComponent();
            textBox1.Text = y.ToString();
            textBox2.Text = x.ToString();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int t1, t2;
            t1 = int.Parse(textBox1.Text);
            t2 = int.Parse(textBox2.Text);
            if (t1 >= 2 && t1 <= 20 && t2 >= 2 && t2 <= 20)
            {
                newX = t2;
                newY = t1;
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
