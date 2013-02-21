using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HuaZhengZi.UserControls;
using HuaZhengZi.ViewModels;

namespace HuaZhengZi
{
    public partial class PatternMenager : PhoneApplicationPage
    {
        ViewModels.PatternPresenter patternPresenter = App.PatternViewModel;

        public PatternMenager() {
            InitializeComponent();
            DataContext = patternPresenter;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (!App.PatternViewModel.IsDataLoaded) {
                App.PatternViewModel.LoadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("/CreatingPage.xaml", UriKind.Relative));
        }
    }
}