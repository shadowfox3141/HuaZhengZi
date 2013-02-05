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
            typeof(ZhengZiPanel), null);
        public int Count {
            set {
                if (value > LayoutRoot.Children.Count * InkPresenterPattern.HighestCount) {
                    throw new Exception("ZhengZiPanel is all filled! ");
                }
                int fullZhengZi = (int)Math.Floor((double)value / 5.0);
                for (int i = 0; i < fullZhengZi; i++) {
                    ((InkPresenter)LayoutRoot.Children[i]).Strokes = Pattern.GetStrokeCollection();
                }
                if (fullZhengZi < LayoutRoot.Children.Count) {
                    ((InkPresenter)LayoutRoot.Children[fullZhengZi]).Strokes = Pattern.GetStrokeCollection(value - fullZhengZi * 5);
                }
                for (int i = fullZhengZi + 1; i < LayoutRoot.Children.Count; i++) {
                    ((InkPresenter)LayoutRoot.Children[i]).Strokes = Pattern.GetStrokeCollection(0);
                }
                SetValue(CountProperty, value);
            }
            get {
                int count = (int)GetValue(CountProperty);
                return count;
            }
        }

        private void LayoutRoot_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e) {
            try { 
                Count += 1; 
            } catch (Exception exc) {
                if (exc.Message == "ZhengZiPanel is all filled! ") {
                    MessageBox.Show("这一夜已经画满了哦~\n是什么事情发生了这么多次？");
                }
            }
        }
    }
}
