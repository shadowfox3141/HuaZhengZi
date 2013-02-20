using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HuaZhengZi.Codes
{
    public class BorderVisibilityConverter : IValueConverter
    {
        public BorderVisibilityConverter() { }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((bool)value) {
                return 3.0;
            } else {
                return 0.0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }
}
