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

        private ZhengZiPresenter mainPageZhengZiPrensenter;

        // Constructor
        public MainPage() {
            InitializeComponent();
            //set datacontext
            mainPageZhengZiPrensenter = new ZhengZiPresenter();
            foreach (var page in App.AppZhengZiPageDataContext.Items.OrderBy(zhengziPage => zhengziPage.Index)) {
                mainPageZhengZiPrensenter.ZhengZiPages.Add(page);
            }
            DataContext = mainPageZhengZiPrensenter;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            PanoramaRoot.SetValue(Panorama.SelectedItemProperty, PanoramaRoot.Items[mainPageZhengZiPrensenter.CurrentPage]);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e) {
            mainPageZhengZiPrensenter.Save();
            base.OnNavigatingFrom(e);
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            if (isTapAccessible) {
                (PanoramaRoot.Items[fixedSelectedIndex] as ZhengZiPage).ZhengZiCount += 1;
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
            int currentIndex = fixedSelectedIndex;
            mainPageZhengZiPrensenter.ZhengZiPages.Add(new ZhengZiPage());
            PanoramaRoot.SetValue(Panorama.SelectedItemProperty, PanoramaRoot.Items[currentIndex]);
        }

        private void ApplicationBarIconButton_Delete_Click(object sender, EventArgs e) {
            int currentIndex = fixedSelectedIndex;
            if ((mainPageZhengZiPrensenter.ZhengZiPages.Count == 1) && (mainPageZhengZiPrensenter.ZhengZiPages[0].ZhengZiCount == 0)) {
                mainPageZhengZiPrensenter.ZhengZiPages[0].PageName = ZhengZiPage.DefaultPageName;
            } else if (mainPageZhengZiPrensenter.ZhengZiPages.Count == 1) {
                mainPageZhengZiPrensenter.ZhengZiPages[0].PageName = ZhengZiPage.DefaultPageName;
                mainPageZhengZiPrensenter.ZhengZiPages[0].ZhengZiCount = 0;
            } else {
                mainPageZhengZiPrensenter.ZhengZiPages.RemoveAt(fixedSelectedIndex);
                if (currentIndex >= PanoramaRoot.Items.Count) {
                    PanoramaRoot.SetValue(Panorama.SelectedItemProperty, PanoramaRoot.Items[0]);
                } else {
                    PanoramaRoot.SetValue(Panorama.SelectedItemProperty, PanoramaRoot.Items[currentIndex]);
                }
            }
        }

        int fixedSelectedIndex{
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
            NavigationService.Navigate(new Uri("/FastView.xaml", UriKind.Relative));
        }

        private void SZTY_Click(object sender, EventArgs e) {
            NavigationService.Navigate(new Uri("/PatternMenager.xaml", UriKind.Relative));
        }

        private void BZBtn_Click(object sender, EventArgs e) {
            NavigationService.Navigate(new Uri("/Help_About.xaml", UriKind.Relative));
        }
    }
}