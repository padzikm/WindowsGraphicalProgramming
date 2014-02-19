using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace MyControl
{
    class StartPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double szer = (double)values[0];
            double wys = (double)values[1];
            double min = Math.Min(szer, wys);
            min = min * 0.5;
            string speeds = (string)values[2];
            string[] table = speeds.Split(new char[] { ',' });
            double alfa = 180 / table.Length;
            double sin = Math.Sin(alfa * Math.PI / 180);
            double cos = Math.Cos(alfa * Math.PI / 180);
            double x = min * sin;
            double y = min - min * cos;
            double elipsa = (double)values[3];
            y = y + elipsa / 2;            
            return new Point(-x, y);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
