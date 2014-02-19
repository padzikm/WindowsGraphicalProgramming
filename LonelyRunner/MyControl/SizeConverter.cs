using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace MyControl
{
    class SizeConverter : IValueConverter
    {
        //public double Wys { get; set; }
        //public double Szer { get; set; }

        //public SizeConverter()
        //{
        //    Wys = Szer = 0;
        //}

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Canvas canvas = value as Canvas;
            //string s = parameter as string;
            //double min;

            //if (canvas != null)
            //{
            //    Wys = canvas.ActualHeight;
            //    Szer = canvas.ActualWidth;
            //    return 0;
            //}
            //else if (s == "Canvas.Left")
            //    Szer = (double)value;
            //else if (s == "CenterY")
            //    Wys = (double)value;
            Size size = (Size)value;
            double wys = size.Height;
            double szer = size.Width;

            double min = Math.Min(wys, szer);
            return min * 0.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
