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

namespace HuaZhengZi
{
    public partial class FastView : PhoneApplicationPage
    {
        ZhengZiPresenter zhengZiPresenter = App.ZhengZiViewModel;
        public FastView() {
            InitializeComponent();
            DataContext = zhengZiPresenter;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            App.ZhengZiViewModel.CurrentPage = PageListBox.SelectedIndex;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void AddBtn_Click(object sender, EventArgs e) {
            zhengZiPresenter.ZhengZiPages.Add(new ZhengZiPage());
        }

        private void DeleteBtn_Click(object sender, EventArgs e) {
            if (zhengZiPresenter.ZhengZiPages.Count > 0) {
                zhengZiPresenter.ZhengZiPages.Remove(PageListBox.SelectedItem as ZhengZiPage);
            } else {
                zhengZiPresenter.ZhengZiPages[0].PageName = ZhengZiPage.DefaultPageName;
                zhengZiPresenter.ZhengZiPages[0].ZhengZiCount = 0;
            }
            PageListBox.SelectedIndex = -1;
            zhengZiPresenter.CurrentPage = 0;
        }
    }
}