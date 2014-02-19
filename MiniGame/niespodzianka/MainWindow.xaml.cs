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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace niespodzianka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int SizeX { get; set; }
        public static int SizeY { get; set; }                        
        private bool zaznaczony;
        private int zaznaczonyX, zaznaczonyY;        
        private DispatcherTimer timer;        
        private Border border;
        private int[] rozmiary;
        private Border[,] ksztalty;
        private int godziny;
        private int minuty;
        private int sekundy;
        private int tiki;
        private bool jestLinia;
        private int zolte;
        private int poczX;
        private int konX;
        private int poczY;
        private int konY;
        private int wynik;

        public MainWindow()
        {
            //img = new Image();
            //img.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/niespodzianka;component/Images/Banana/1.png"), UriKind.RelativeOrAbsolute));
            //img.Source = new BitmapImage(new Uri("pack://application:,,,/niespodzianka;component/Properties/Resources/cat1.png", UriKind.RelativeOrAbsolute));
            //img.Source = new BitmapImage(new Uri("pack://application:,,,/cat1", UriKind.RelativeOrAbsolute));
            //BitmapImage b = new BitmapImage(new Uri(@"/cat1.png", UriKind.Relative));
            //img.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/pwsg6;component/Images/{0}/{1}.png", animations[i],j), UriKind.RelativeOrAbsolute));
            wynik = 0;
            zolte = 0;
            godziny = 0;
            minuty = 0;
            sekundy = 0;
            tiki = 0;
            rozmiary = new int[] { 8, 8, 7, 9, 5, 7 };            
            SizeX = 10;
            SizeY = 10;
            ksztalty = new Border[SizeY, SizeX];
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(150 * 10000);
            timer.Tick += timer_Tick;
            InitializeComponent();
            timerLabel.Content = "Czas: 0:0:0";
            wynikLabel.Content = "Wynik: 0";
            RobPlansze();
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {            
            ++tiki;
            if (tiki == 7)
            {
                tiki = 0;
                ++sekundy;
                if (sekundy == 60)
                {
                    sekundy = 0;
                    ++minuty;
                    if (minuty == 60)
                    {
                        minuty = 0;
                        ++godziny;
                    }
                }
            }
            timerLabel.Content = string.Format("Czas: {0}:{1}:{2}", godziny, minuty, sekundy);
            int obrazek, klatka;
            Image image;
            foreach (Border b in plansza.Children)
            {
                image = (Image)b.Child;
                obrazek = int.Parse((string)b.Tag);
                klatka = int.Parse((string)image.Tag);
                klatka = (klatka + 1) % (rozmiary[obrazek] + 1);
                if (klatka == 0)
                    klatka = 1;
                image.Tag = klatka.ToString();
                image.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/niespodzianka;component/Images/{0}/{1}.png", obrazek, klatka), UriKind.RelativeOrAbsolute));
            }
            if (jestLinia)
                ++zolte;
            if (zolte == 10)
            {
                jestLinia = false;
                zolte = 0;
                Usun();
                Buduj();
                if (Sprawdz())
                {
                    jestLinia = true;
                    for (int i = poczX; i <= konX; ++i)
                        ksztalty[poczY, i].Background = Brushes.Yellow;
                    for (int i = poczY; i <= konY; ++i)
                        ksztalty[i, poczX].Background = Brushes.Yellow;
                    wynik += konX - poczX + konY - poczY + 1;
                    wynikLabel.Content = string.Format("Wynik: {0}", wynik);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1(SizeX, SizeY);
            if (w.ShowDialog() == true)
            {
                SizeX = w.newX;
                SizeY = w.newY;
                //MessageBox.Show(string.Format("sizeX: {0}, sizeY: {1}", SizeX, SizeY));
                ksztalty = new Border[SizeY, SizeX];
                RobPlansze();
            }

        }

        private void RobPlansze()
        {
            zaznaczony = false;
            plansza.Children.Clear();
            plansza.ColumnDefinitions.Clear();
            plansza.RowDefinitions.Clear();
            //MessageBox.Show(string.Format("sizeX: {0}, sizeY: {1}", SizeX, SizeY));
            for (int i = 0; i < SizeY; ++i)
            {
                RowDefinition row = new RowDefinition();
                plansza.RowDefinitions.Add(row);
            }
            for (int i = 0; i < SizeX; ++i)
            {
                ColumnDefinition col = new ColumnDefinition();
                plansza.ColumnDefinitions.Add(col);
            }
            //plansza.ShowGridLines = true;
            Random r = new Random((int)DateTime.Now.Ticks);
            int nast, ksztaltLewoRaz, ksztaltLewoDwa, ksztaltGoraRaz, ksztaltGoraDwa;
            for (int i = 0; i < SizeY; ++i)
                for (int j = 0; j < SizeX; ++j)
                {
                    nast = r.Next(0, rozmiary.Length);
                    ksztaltLewoRaz = (j == 0) ? -1 : int.Parse((string)ksztalty[i, j - 1].Tag);
                    ksztaltLewoDwa = (j < 2 ) ? -1 : int.Parse((string)ksztalty[i, j - 2].Tag);
                    ksztaltGoraRaz = (i == 0) ? -1 : int.Parse((string)ksztalty[i - 1, j].Tag);
                    ksztaltGoraDwa = (i < 2) ? -1 : int.Parse((string)ksztalty[i - 2, j].Tag);
                    while((ksztaltLewoRaz == ksztaltLewoDwa && ksztaltLewoRaz == nast) || (ksztaltGoraRaz == ksztaltGoraDwa && ksztaltGoraRaz == nast))
                        nast = r.Next(0, rozmiary.Length - 1);
                    Image cos = new Image();
                    cos.Tag = "1";
                    cos.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/niespodzianka;component/Images/{0}/{1}.png", nast, 1), UriKind.RelativeOrAbsolute));
                    Border bor = new Border();
                    bor.BorderBrush = Brushes.Black;
                    bor.BorderThickness = new Thickness(1);
                    bor.Child = cos;
                    bor.Tag = nast.ToString();
                    bor.MouseDown += b_MouseDown;
                    plansza.Children.Add(bor);
                    Grid.SetColumn(bor, j);
                    Grid.SetRow(bor, i);
                    ksztalty[i, j] = bor;
                }
        }

        void b_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (jestLinia)
                return;

            int x, y, startX, stopX, startY, stopY;
            Border tmp, b = (Border)sender;

            x = Grid.GetColumn(b);
            y = Grid.GetRow(b);            
            if (!zaznaczony)
            {
                zaznaczony = true;
                b.Background = Brushes.Gray;
                zaznaczonyX = x;
                zaznaczonyY = y;
                border = b;
                return;
            }
            if ((zaznaczonyY == y && (zaznaczonyX == x - 1 || zaznaczonyX == x + 1)) || (zaznaczonyX == x && (zaznaczonyY == y - 1 || zaznaczonyY == y + 1)))
            {
                tmp = ksztalty[y, x];
                ksztalty[y, x] = ksztalty[zaznaczonyY, zaznaczonyX];
                ksztalty[zaznaczonyY, zaznaczonyX] = tmp;
                if (!sprawdzPunkt(x, y, out startX, out stopX, out startY, out stopY))
                {
                    tmp = ksztalty[y, x];
                    ksztalty[y, x] = ksztalty[zaznaczonyY, zaznaczonyX];
                    ksztalty[zaznaczonyY, zaznaczonyX] = tmp;
                    return;
                }
                jestLinia = true;
                zaznaczony = false;
                border.Background = Brushes.White;
                poczX = startX;
                konX = stopX;
                poczY = startY;
                konY = stopY;
                Grid.SetColumn(ksztalty[y, x], x);
                Grid.SetRow(ksztalty[y, x], y);
                Grid.SetColumn(ksztalty[zaznaczonyY, zaznaczonyX], zaznaczonyX);
                Grid.SetRow(ksztalty[zaznaczonyY, zaznaczonyX], zaznaczonyY);
                if (startX == stopX)
                    for (int i = startY; i <= stopY; ++i)
                        ksztalty[i, startX].Background = Brushes.Yellow;
                else
                    for (int i = startX; i <= stopX; ++i)
                        ksztalty[startY, i].Background = Brushes.Yellow;
                wynik += konX - poczX + konY - poczY + 1;
                wynikLabel.Content = string.Format("Wynik: {0}", wynik);
                return;
            }            
            else if (zaznaczonyX == x && zaznaczonyY == y)
            {
                zaznaczony = false;
                b.Background = Brushes.White;
                return;
            }
            border.Background = Brushes.White;
            b.Background = Brushes.Gray;
            zaznaczonyX = x;
            zaznaczonyY = y;
            border = b;
        }

        private bool sprawdzPunkt(int x, int y, out int startX, out int stopX, out int startY, out int stopY)
        {
            int i, j, ile;
            ile = 0;
            i = y - 1;
            while (i >= 0 && int.Parse((string)ksztalty[i, x].Tag) == int.Parse((string)ksztalty[y, x].Tag))
            {
                ++ile;
                --i;
            }
            j = y + 1;
            while (j < SizeY && int.Parse((string)ksztalty[j, x].Tag) == int.Parse((string)ksztalty[y, x].Tag))
            {
                ++ile;
                ++j;
            }
            if (ile >= 2)
            {
                startX = x;
                stopX = x;
                startY = i + 1;
                stopY = j - 1;
                return true;
            }
            ile = 0;
            i = x - 1;
            while(i >= 0 && int.Parse((string)ksztalty[y, i].Tag) == int.Parse((string)ksztalty[y, x].Tag))
            {
                ++ile;
                --i;
            }
            j = x + 1;
            while (j < SizeX && int.Parse((string)ksztalty[y, j].Tag) == int.Parse((string)ksztalty[y, x].Tag))
            {
                ++ile;
                ++j;
            }
            if (ile >= 2)
            {
                startX = i + 1;
                stopX = j - 1;
                startY = y;
                stopY = y;
                return true;
            }
            startX = stopX = startY = stopY = -1;
            return false;
        }

        private void Usun()
        {
            int roznica, nast;       
            Random r = new Random((int)DateTime.Now.Ticks);
            Image cos;
            Border bor;
            if (poczX == konX)
            {
                roznica = konY - poczY + 1;
                for (int i = poczY - 1; i >= 0; --i)                                                        
                    ksztalty[i + roznica, poczX] = ksztalty[i, poczX]; 
                for(int i = 0; i < roznica; ++i)
                {
                    nast = r.Next(0, rozmiary.Length);
                    cos = new Image();
                    cos.Tag = "1";
                    cos.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/niespodzianka;component/Images/{0}/{1}.png", nast, 1), UriKind.RelativeOrAbsolute));
                    bor = new Border();
                    bor.BorderBrush = Brushes.Black;
                    bor.BorderThickness = new Thickness(1);
                    bor.Child = cos;
                    bor.Tag = nast.ToString();
                    bor.MouseDown += b_MouseDown;                    
                    ksztalty[i, poczX] = bor;
                }
                return;
            }            
            for(int i = poczX; i <= konX; ++i)
                for (int j = poczY - 1; j >= 0; --j)                                    
                    ksztalty[j + 1, i] = ksztalty[j, i];
            for (int i = poczX; i <= konX; ++i)
            {
                nast = r.Next(0, rozmiary.Length);
                cos = new Image();
                cos.Tag = "1";
                cos.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/niespodzianka;component/Images/{0}/{1}.png", nast, 1), UriKind.RelativeOrAbsolute));
                bor = new Border();
                bor.BorderBrush = Brushes.Black;
                bor.BorderThickness = new Thickness(1);
                bor.Child = cos;
                bor.Tag = nast.ToString();
                bor.MouseDown += b_MouseDown;
                ksztalty[0, i] = bor;
            }
        }

        private void Buduj()
        {
            plansza.Children.Clear();
            plansza.ColumnDefinitions.Clear();
            plansza.RowDefinitions.Clear();            
            for (int i = 0; i < SizeY; ++i)
            {
                RowDefinition row = new RowDefinition();
                plansza.RowDefinitions.Add(row);
            }
            for (int i = 0; i < SizeX; ++i)
            {
                ColumnDefinition col = new ColumnDefinition();
                plansza.ColumnDefinitions.Add(col);
            }

            for(int i = 0; i < SizeY; ++i)
                for (int j = 0; j < SizeX; ++j)
                {
                    plansza.Children.Add(ksztalty[i, j]);
                    Grid.SetRow(ksztalty[i, j], i);
                    Grid.SetColumn(ksztalty[i, j], j);
                }
        }

        private bool Sprawdz()
        {
            int ile, aktObrazek;
            for (int i = 0; i < SizeY; ++i) //wiersze
            {
                ile = 0;
                aktObrazek = int.Parse((string)ksztalty[i, 0].Tag);
                poczX = 0;
                konX = 0;
                poczY = i;
                konY = i;
                for (int j = 0; j < SizeX; ++j)
                    if (int.Parse((string)ksztalty[i, j].Tag) == aktObrazek)
                    {
                        ++ile;
                        konX = j;
                    }
                    else if (ile >= 3)
                        return true;
                    else
                    {
                        ile = 1;
                        aktObrazek = int.Parse((string)ksztalty[i, j].Tag);
                        poczX = j;
                        konX = j;
                        poczY = i;
                        konY = i;
                    }
                if (ile >= 3)
                    return true;
            }
            for (int j = 0; j < SizeX; ++j)
            {
                ile = 0;
                aktObrazek = int.Parse((string)ksztalty[0, j].Tag);
                poczX = j;
                konX = j;
                poczY = 0;
                konY = 0;
                for (int i = 0; i < SizeY; ++i)
                    if (int.Parse((string)ksztalty[i, j].Tag) == aktObrazek)
                    {
                        ++ile;
                        konY = i;
                    }
                    else if (ile >= 3)
                        return true;
                    else
                    {
                        ile = 1;
                        aktObrazek = int.Parse((string)ksztalty[i, j].Tag);
                        poczX = j;
                        konX = j;
                        poczY = i;
                        konY = i;
                    }
                if (ile >= 3)
                    return true;
            }
            return false;
        }
    }
}
