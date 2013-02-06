using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Ink;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Data;

namespace HuaZhengZi.ViewModels
{
    public partial class ZhengZiPanel : UserControl
    {
        public ZhengZiPanel() {
            InitializeComponent();
        }

        public static readonly DependencyProperty PatternProperty = DependencyProperty.Register("Pattern", typeof(InkPresenterPattern),
            typeof(ZhengZiPanel), null);

        public InkPresenterPattern Pattern {
            set { SetValue(PatternProperty, value); }
            get {
                return (InkPresenterPattern)GetValue(PatternProperty);
            }
        }

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(int),
            typeof(ZhengZiPanel), new PropertyMetadata(ModifyPanel));
        public int Count {
            set {               
                SetValue(CountProperty, value);
            }
            get {
                int count = (int)GetValue(CountProperty);
                return count;
            }
        }

        private static void ModifyPanel(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ZhengZiPanel sender = d as ZhengZiPanel;
            if ((int)e.NewValue > sender.LayoutRoot.Children.Count * InkPresenterPattern.HighestCount) {
                MessageBox.Show("这一页已经都画满了哦~\n是什么事情发生了这么多次？");
                throw new Exception("ZhengZiPanel is all filled! ");
            }
            int fullZhengZi = (int)Math.Floor((int)e.NewValue / 5.0);
            for (int i = 0; i < fullZhengZi; i++) {
                ((InkPresenter)sender.LayoutRoot.Children[i]).Strokes = sender.Pattern.GetStrokeCollection();
            }
            if (fullZhengZi < sender.LayoutRoot.Children.Count) {
                ((InkPresenter)sender.LayoutRoot.Children[fullZhengZi]).Strokes = sender.Pattern.GetStrokeCollection((int)e.NewValue - fullZhengZi * 5);
            }
            for (int i = fullZhengZi + 1; i < sender.LayoutRoot.Children.Count; i++) {
                ((InkPresenter)sender.LayoutRoot.Children[i]).Strokes = sender.Pattern.GetStrokeCollection(0);
            }
        }
    }
}
