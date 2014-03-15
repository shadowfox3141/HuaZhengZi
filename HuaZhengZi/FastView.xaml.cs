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
using System.IO.IsolatedStorage;

namespace HuaZhengZi
{
    public partial class FastView : PhoneApplicationPage
    {
        ZhengZiPresenter fastViewZhengZiPresenter;
        public FastView() {
            InitializeComponent();
            fastViewZhengZiPresenter = new ZhengZiPresenter();
            foreach (var page in App.AppZhengZiPageDataContext.Items.OrderBy(zhengziPage => zhengziPage.Index)) {
                fastViewZhengZiPresenter.ZhengZiPages.Add(page);
            }
            DataContext = fastViewZhengZiPresenter;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            fastViewZhengZiPresenter.CurrentPage = PageListBox.SelectedIndex;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void AddBtn_Click(object sender, EventArgs e) {
            fastViewZhengZiPresenter.ZhengZiPages.Add(new ZhengZiPage());
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e) {
            fastViewZhengZiPresenter.Save();
            base.OnNavigatingFrom(e);
        }

        private void DeleteBtn_Click(object sender, EventArgs e) {
            if (fastViewZhengZiPresenter.ZhengZiPages.Count > 0) {
                fastViewZhengZiPresenter.ZhengZiPages.Remove(PageListBox.SelectedItem as ZhengZiPage);
            } else {
                fastViewZhengZiPresenter.ZhengZiPages[0].PageName = ZhengZiPage.DefaultPageName;
                fastViewZhengZiPresenter.ZhengZiPages[0].ZhengZiCount = 0;
            }
            PageListBox.SelectedIndex = -1;
            fastViewZhengZiPresenter.CurrentPage = 0;
        }

        private void AdControl_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e) {

        }
    }
}