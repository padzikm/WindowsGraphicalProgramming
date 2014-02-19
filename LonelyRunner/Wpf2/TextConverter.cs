using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Wpf2
{
    class TextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string speeds = (string)value;
            string[] table = speeds.Split(new char[] { ',' });
            int[] pred = new int[table.Length];
            for (int i = 0; i < table.Length; ++i)
                if (!int.TryParse(table[i], out pred[i]))                
                    return Brushes.Red;                                    
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
