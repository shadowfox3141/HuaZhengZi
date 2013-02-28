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
            
            DefaultList.SelectedItem = patternPresenter.SelectPattern;
            if (DefaultList.SelectedItem == null) {
                UserList.SelectedItem = patternPresenter.SelectPattern;
            }
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count != 0) {
                ListBox list = sender as ListBox;
                if (list.Equals(DefaultList)) {
                    UserList.SelectedItem = null;
                    App.PatternViewModel.SelectPattern = e.AddedItems[0] as StrokePattern;
                } else {
                    DefaultList.SelectedItem = null;
                    App.PatternViewModel.SelectPattern = e.AddedItems[0] as StrokePattern;
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e) {
            if (UserList.SelectedItem != null) {
                int nowIndex = UserList.SelectedIndex;
                App.PatternViewModel.UserPatterns.Remove(App.PatternViewModel.SelectPattern);
                if (UserList.Items.Count == 0) {
                    DefaultList.SelectedIndex = 0;
                } else {
                    UserList.SelectedIndex = nowIndex != 0 ? nowIndex - 1 : nowIndex;
                }
            } else {
                MessageBox.Show("不能删除系统默认的图样！", "操作有误", MessageBoxButton.OK);
            }
        }

        private void AddBtn_Click(object sender, EventArgs e) {
            NavigationService.Navigate(new Uri("/CreatingPage.xaml", UriKind.Relative));
        }
    }
}