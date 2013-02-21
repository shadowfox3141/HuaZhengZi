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
using System.Windows.Ink;

namespace HuaZhengZi.UserControls
{
    public partial class InkPattern : UserControl
    {
        public InkPattern() {
            InitializeComponent();
        }

        public static readonly DependencyProperty PatternProperty = DependencyProperty.Register("Pattern",
            typeof(List<StrokeCollection>), typeof(InkPattern), new PropertyMetadata(onPatternChanged));
        public List<StrokeCollection> Pattern {
            set {
                SetValue(PatternProperty, value);
            }
            get {
                return (List<StrokeCollection>)GetValue(PatternProperty);
            }
        }

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count",
            typeof(int), typeof(InkPattern), new PropertyMetadata(0, onCountChanged));
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
        
        private static void onCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            InkPattern sender = d as InkPattern;
            if (sender.Count > StrokePattern.HighestCount) {
                throw new Exception("InkPattern is all filled! ");
            }
            sender.RefreshPattern();
        }
        private static void onPatternChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            InkPattern sender = d as InkPattern;
            sender.RefreshPattern();
        }

        public void RefreshPattern() {
            if (Pattern != null) {
                StrokeCollection collection = new StrokeCollection();
                for (int i = 0; i < Count; i++) {
                    foreach (var stroke in Pattern[i]) {
                        collection.Add(stroke);
                    }
                }
                inkPresenter.Strokes = collection;
            }
        }
    }
}
