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
using System.ComponentModel;
using LonelyRunnerLogic;

namespace MyControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Kontrolka : UserControl, INotifyPropertyChanged
    {
        private static LonelyRunnerInstance LonelyRunner = new LonelyRunnerInstance(new int[] { 1 });
        private static int predkosc = 100;
        private static string runnerSpeeds = new string(new char[] {'1'});
        private static bool animuj = false;
        
        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(int), typeof(Kontrolka), new FrameworkPropertyMetadata(100, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnSpeedChange), new CoerceValueCallback(OnSpeedCoerce)));
        public static readonly DependencyProperty ShowArcsProperty = DependencyProperty.Register("ShowArcs", typeof(bool), typeof(Kontrolka), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnShowArcsChange)));
        public static readonly DependencyProperty RunnerSpeedsProperty = DependencyProperty.Register("RunnerSpeeds", typeof(string), typeof(Kontrolka), new FrameworkPropertyMetadata("1", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnRunnerSpeedsChange), new CoerceValueCallback(OnRunnerSpeedsCoerce)));

        public int Speed
        {
            get { return (int)GetValue(SpeedProperty); }
            set
            {
                if (value >= 20 && value <= 750)
                {
                    SetValue(SpeedProperty, value); NotifyPropertyChanged("Speed");
                }
            }
        }
        public bool ShowArcs
        {
            get { return (bool)GetValue(ShowArcsProperty); }
            set { SetValue(ShowArcsProperty, value); NotifyPropertyChanged("ShowArcs"); }
        }

        public string RunnerSpeeds
        {
            get { return (string)GetValue(RunnerSpeedsProperty); }
            set { SetValue(RunnerSpeedsProperty, value); NotifyPropertyChanged("RunnerSpeeds"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static void OnSpeedChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int speed = (int)e.NewValue;
            predkosc = speed;
            LonelyRunner.Speed = speed;
        }

        private static object OnSpeedCoerce(DependencyObject d, object value)
        {
            int speed = (int)value;
            if (speed >= 20 && speed <= 750)
                return speed;
            else if (speed < 20)
                return 20;
            else
                return 750;
        }

        private static void OnShowArcsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool show = (bool)e.NewValue;
            Kontrolka kontrolka = (Kontrolka)d;
            kontrolka.ShowArcs = show;
        }

        private static void OnRunnerSpeedsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string speeds = (string)e.NewValue;
            Kontrolka kontrolka = (Kontrolka)d;
            if(runnerSpeeds == speeds)
                return;
            runnerSpeeds = speeds;
            string[] table = speeds.Split(new char[] { ',' });
            int[] pred = new int[table.Length];
            for (int i = 0; i < table.Length; ++i)
                pred[i] = int.Parse(table[i]);            
            LonelyRunner = new LonelyRunnerInstance(pred);
            LonelyRunner.Speed = predkosc;
            kontrolka.DataContext = LonelyRunner;
            if(animuj)
                LonelyRunner.Run();     
        }

        private static object OnRunnerSpeedsCoerce(DependencyObject d, object value)
        {
            string speeds = (string)value;
            string[] table = speeds.Split(new char[] { ',' });
            int[] pred = new int[table.Length];
            for (int i = 0; i < table.Length; ++i)
                if (!int.TryParse(table[i], out pred[i]))
                    return runnerSpeeds;            
            return speeds;
        }

        public Kontrolka()
        {
            InitializeComponent();
            DataContext = LonelyRunner;
        }

        public void StartAnim()
        {  
            animuj = true;
            LonelyRunner.Run();
        }

        public void StopAnim()
        {
            animuj = false;
            if (LonelyRunner != null)
                LonelyRunner.Stop();
        }       
    }
}