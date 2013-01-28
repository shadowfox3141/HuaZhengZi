using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HuaZhengZi.ViewModels
{
    public class ZhengZiPage : INotifyPropertyChanged
    {
        public ZhengZiPage() {
            _zhengZiCount = 0;
        }

        private string _pageName;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string PageName {
            get {
                return _pageName;
            }
            set {
                if (value != _pageName) {
                    _pageName = value;
                    NotifyPropertyChanged("PageName");
                }
            }
        }

        private int _zhengZiCount;
        public int ZhengZiCount {
            get {
                return _zhengZiCount;
            }
            set {
                if (value != _zhengZiCount) {
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