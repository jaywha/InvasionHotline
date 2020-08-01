using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace InvasionHotline
{
    public class PopToPopColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var Population = (int)value;

            if (Population >= 500) return Brushes.IndianRed;
            else if (Population >= 400 && Population < 500) return Brushes.OrangeRed;
            else if (Population >= 300 && Population < 400) return Brushes.Gold;
            else if (Population >= 200 && Population < 300) return Brushes.DarkGoldenrod;
            else if (Population >= 100 && Population < 200) return Brushes.ForestGreen;
            else return Brushes.WhiteSmoke;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0; // do we need to track pop to convert back?
        }
    }
}
