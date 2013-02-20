using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HuaZhengZi.ViewModels;

namespace HuaZhengZi.UserControls
{
    public partial class InkPattern : UserControl
    {
        public InkPattern() {
            InitializeComponent();
        }

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count",
            typeof(int), typeof(InkPattern), new PropertyMetadata(0, RefreshPattern));
        public int Count {
            set {
                if (value != Count) {
                    SetValue(CountProperty, value);
                }
            }
            get {
                return (int)GetValue(CountProperty);
            }
        }

        public static readonly DependencyProperty PatternProperty = DependencyProperty.Register("Pattern",
            typeof(StrokePattern), typeof(InkPattern), new PropertyMetadata(RefreshPattern));
        public StrokePattern Pattern {
            set { SetValue(PatternProperty, value); }
            get {
                return (StrokePattern)GetValue(PatternProperty);
            }
        }

        public static readonly DependencyProperty BorderVisibilityProperty = DependencyProperty.Register("BorderVisibility",
            typeof(bool), typeof(InkPattern), new PropertyMetadata(false) );
        public bool BorderVisibility {
            set {
                if (value != BorderVisibility) {
                    SetValue(BorderVisibilityProperty, value);
                }
            }
            get {
                return (bool)GetValue(BorderVisibilityProperty);
            }
        }
        
        private static void RefreshPattern(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            InkPattern sender = d as InkPattern;
            if (sender.Count > StrokePattern.HighestCount) {
                throw new Exception("InkPattern is all filled! ");
            }
            sender.inkPresenter.Strokes = sender.Pattern.GetStrokeCollection(sender.Count);
        }
    }
}
