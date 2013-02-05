using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using System.Windows.Data;
using HuaZhengZi.ViewModels;

namespace HuaZhengZi
{
    public partial class MainPage : PhoneApplicationPage
    {
        ZhengZiPresenter zhengZiPrensenter = App.ViewModel;

        // Constructor
        public MainPage() {
            InitializeComponent();
            // Set the data context of the listbox control to the sample data      
            DataContext = zhengZiPrensenter;
            
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (!App.ViewModel.IsDataLoaded) {
                App.ViewModel.LoadData();
            }
        }

        private void ZhengZiPanel_Loaded(object sender, RoutedEventArgs e) {
            foreach (ZhengZiPage zhengZiPage in zhengZiPrensenter.ZhengZiPages) {
                zhengZiPage.NotifyPropertyChanged("ZhengZiCount");
            }
        }
    }
}