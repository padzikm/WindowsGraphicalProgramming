using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {                                    
            Animacja.StartAnim();
            //Animacja.wys = Animacja.ActualHeight;
            //Animacja.szer = Animacja.ActualWidth;
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            Animacja.StopAnim();
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            this.ResizeMode = ResizeMode.CanResize;            
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string speeds = TextBox.Text;
            string[] table = speeds.Split(new char[] { ',' });
            int[] pred = new int[table.Length];
            for (int i = 0; i < table.Length; ++i)
                if (!int.TryParse(table[i], out pred[i]))
                {
                    TextBox.BorderBrush = Brushes.Red;                    
                    return;
                }
            TextBox.BorderBrush = Brushes.Gray;
        }       
    }
}
