using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HuaZhengZi.ViewModels
{
    public partial class ZhengZiPanel : UserControl
    {
        public ZhengZiPanel() {
            InitializeComponent();
        }

        static readonly DependencyProperty countProperty = DependencyProperty.Register("Count", typeof(int),
            typeof(ZhengZiPanel), null);
        public int Count {
            set {
                SetValue(countProperty, value);
            }
            get {
                int count = (int)GetValue(countProperty);
                return count;
            }
        }
    }
}
