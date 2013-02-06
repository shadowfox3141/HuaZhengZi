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
using System.Windows.Media;

namespace HuaZhengZi
{
    public partial class MainPage : PhoneApplicationPage
    {
        bool isTapAccessible = true;

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

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            if (isTapAccessible) {
                try {
                    zhengZiPrensenter.ZhengZiPages[PanoramaRoot.SelectedIndex].ZhengZiCount += 1;
                } catch (Exception exc){
                    zhengZiPrensenter.ZhengZiPages[PanoramaRoot.SelectedIndex].ZhengZiCount -= 1;
                }
            }
        }

        private void PanoramaTitleTextBox_GotFocus(object sender, RoutedEventArgs e) {
            ((TextBox)sender).Background = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
            isTapAccessible = false;
        }

        private void PanoramaTitleTextBox_LostFocus(object sender, RoutedEventArgs e) {
            ((TextBox)sender).Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            isTapAccessible = true;
        }
    }
}