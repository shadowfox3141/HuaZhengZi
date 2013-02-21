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
using HuaZhengZi.ViewModels;

namespace HuaZhengZi.UserControls
{
    public partial class ZhengZiPanel : UserControl
    {
        public ZhengZiPanel() {
            InitializeComponent();
        }

        public static readonly DependencyProperty PatternProperty = DependencyProperty.Register("Pattern", typeof(StrokePattern),
            typeof(ZhengZiPanel), new PropertyMetadata(OnPatternChanged));

        private static void OnPatternChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ZhengZiPanel sender=d as ZhengZiPanel;
            foreach (var inkPattern in sender.LayoutRoot.Children) {
                (inkPattern as InkPattern).Pattern = ((StrokePattern)e.NewValue).Items;
            }
        }

        public StrokePattern Pattern {
            set { 
                SetValue(PatternProperty, value); 
            }
            get {
                return (StrokePattern)GetValue(PatternProperty);
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
            if ((int)e.NewValue > sender.LayoutRoot.Children.Count * StrokePattern.HighestCount) {
                MessageBox.Show("这一页已经都画满了哦~\n是什么事情发生了这么多次？");
                throw new Exception("ZhengZiPanel is all filled! ");
            }
            int fullZhengZi = (int)Math.Floor((int)e.NewValue / ((double)StrokePattern.HighestCount));
            for (int i = 0; i < fullZhengZi; i++) {
                ((InkPattern)sender.LayoutRoot.Children[i]).Count = StrokePattern.HighestCount;
            }
            if (fullZhengZi < sender.LayoutRoot.Children.Count) {
                ((InkPattern)sender.LayoutRoot.Children[fullZhengZi]).Count = (int)e.NewValue - fullZhengZi * StrokePattern.HighestCount;
            }
            for (int i = fullZhengZi + 1; i < sender.LayoutRoot.Children.Count - 1; i++) {
                ((InkPattern)sender.LayoutRoot.Children[i]).Count = 0;
            }
        }
    }
}
