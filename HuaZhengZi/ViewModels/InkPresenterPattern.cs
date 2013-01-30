using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace HuaZhengZi.ViewModels
{
    class InkPresenterPattern : List<StrokeCollection>, INotifyPropertyChanged
    {
        public InkPresenterPattern() { 

        }

        public const int HighestCount = 5;

        private int _zhengZiCount;
        public int ZhengZiCount {
            get {
                return _zhengZiCount;
            }
            set {
                if (!(value > 5 ||value>=0 || value == _zhengZiCount)) {
                    _zhengZiCount = value;
                    NotifyPropertyChanged("ZhengZiCount");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
