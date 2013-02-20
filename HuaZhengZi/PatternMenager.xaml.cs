using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HuaZhengZi
{
    public partial class PatternMenager : PhoneApplicationPage
    {
        ViewModels.PatternPresenter patternPresenter = App.PatternViewModel;

        public PatternMenager() {
            InitializeComponent();

            DataContext = patternPresenter;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("/CreatingPage.xaml", UriKind.Relative));
        }
    }
}