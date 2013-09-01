using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Serialization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Data.Linq.Mapping;

namespace HuaZhengZi.ViewModels
{
    [Table]
    public class ZhengZiPage :  INotifyPropertyChanged
    {
        public const string DefaultPageName = "Enter Your Title Here";



        [Column(IsPrimaryKey = true, CanBeNull = false, AutoSync = AutoSync.Default)]
        public string uid { private set; get; }

        [Column]
        public int Index { set; get; }

        public ZhengZiPage() {
            _pageName = DefaultPageName;
            _zhengZiCount = 0;
            Index = int.MinValue;
            uid = Guid.NewGuid().ToString();
        }

        
        private string _pageName;
        [Column]
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
        [Column]
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
        public void NotifyPropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}