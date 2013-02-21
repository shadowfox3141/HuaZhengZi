using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using HuaZhengZi;
using System.Windows.Ink;

namespace HuaZhengZi.Codes
{
    public class StrokeCollection_PatternConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            ViewModels.StrokePattern strokePattern = value as ViewModels.StrokePattern;
            return strokePattern.GetStrokeCollection(ViewModels.StrokePattern.HighestCount);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            StrokeCollection strokeColletion = value as StrokeCollection;
            ViewModels.StrokePattern strokePattern = new ViewModels.StrokePattern();
            foreach (Stroke stroke in strokeColletion) {
                strokePattern.Items.Add(new StrokeCollection { stroke });
            }
            strokePattern.PatternName = parameter as string;
            return strokePattern;
        }
    }
}
