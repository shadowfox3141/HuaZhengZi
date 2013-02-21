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

        ZhengZiPresenter zhengZiPrensenter = App.ZhengZiViewModel;

        // Constructor
        public MainPage() {
            InitializeComponent();
            // Set the data context of the listbox control to the sample data      
            DataContext = zhengZiPrensenter;
            PanoramaRoot.SetValue(Panorama.SelectedItemProperty, PanoramaRoot.Items[zhengZiPrensenter.CurrentPage]);
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (!App.ZhengZiViewModel.IsDataLoaded) {
                App.ZhengZiViewModel.LoadData();
            }
            
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e) {
            zhengZiPrensenter.CurrentPage = FixSelectedIndex;
            base.OnNavigatingFrom(e);
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            if (isTapAccessible) {
                try {
                    zhengZiPrensenter.ZhengZiPages[FixSelectedIndex].ZhengZiCount += 1;
                } catch (Exception exc){
                    zhengZiPrensenter.ZhengZiPages[FixSelectedIndex].ZhengZiCount -= 1;
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

        private void ApplicationBarIconButton_Add_Click(object sender, EventArgs e) {
            int currentIndex = FixSelectedIndex;
            zhengZiPrensenter.ZhengZiPages.Add(new ZhengZiPage());
            PanoramaRoot.SetValue(Panorama.SelectedItemProperty, PanoramaRoot.Items[currentIndex]);
        }

        private void ApplicationBarIconButton_Delete_Click(object sender, EventArgs e) {
            int currentIndex = FixSelectedIndex;
            if ((zhengZiPrensenter.ZhengZiPages.Count == 1) && (zhengZiPrensenter.ZhengZiPages[0].ZhengZiCount == 0)) {
                zhengZiPrensenter.ZhengZiPages[0].PageName = ZhengZiPage.DefaultPageName;
            } else if (zhengZiPrensenter.ZhengZiPages.Count == 1) {
                zhengZiPrensenter.ZhengZiPages[0].PageName = ZhengZiPage.DefaultPageName;
                zhengZiPrensenter.ZhengZiPages[0].ZhengZiCount = 0;
            } else {
                zhengZiPrensenter.ZhengZiPages.RemoveAt(FixSelectedIndex);
                if (currentIndex >= PanoramaRoot.Items.Count) {
                    PanoramaRoot.SetValue(Panorama.SelectedItemProperty, PanoramaRoot.Items[0]);
                } else {
                    PanoramaRoot.SetValue(Panorama.SelectedItemProperty, PanoramaRoot.Items[currentIndex]);
                }
            }
        }

        int FixSelectedIndex{
            get {
                ZhengZiPage currentPage = PanoramaRoot.SelectedItem as ZhengZiPage;
                if (currentPage == null) {
                    currentPage = ((PanoramaItem)PanoramaRoot.SelectedItem).DataContext as ZhengZiPage;
                }
                int index = currentPage.Index;
                return index;
            }
        }

        private void KSST_Click(object sender, EventArgs e) {
            
        }

        private void SZTY_Click(object sender, EventArgs e) {
            NavigationService.Navigate(new Uri("/PatternMenager.xaml", UriKind.Relative));
        }
    }
}