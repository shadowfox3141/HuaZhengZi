using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace HuaZhengZi.Codes
{
    public class BorderVisibilityConverter : IValueConverter
    {
        public BorderVisibilityConverter() { }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((bool)value) {
                Thickness myThickness = new Thickness(3);
                return myThickness;
            } else {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }
}
