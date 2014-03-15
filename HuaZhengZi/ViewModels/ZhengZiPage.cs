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
    public class ZhengZiPage : INotifyPropertyChanged
    {
        public const string DefaultPageName = "在这里输入页名";

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
                if (value != _zhengZiCount && value <= 80) {
                    _zhengZiCount = value;
                    NotifyPropertyChanged("ZhengZiCount");
                } else if (value > 80) {
                    MessageBox.Show("这一页已经都画满了哦~\n是什么事情发生了这么多次？");
                }
            }
        }

        private StrokePattern _SellectedPattern;
        public StrokePattern SellectedPattern {
            get {
                return App.PatternViewModel.SelectPattern;
            }
            set {
                if (value != _SellectedPattern) {
                    _SellectedPattern = value;
                    NotifyPropertyChanged("SellectedPattern");
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