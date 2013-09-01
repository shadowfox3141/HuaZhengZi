using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HuaZhengZi.Resources;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;

namespace HuaZhengZi.ViewModels
{
    public class ZhengZiPresenter : INotifyPropertyChanged, INotifyPropertyChanging
    {        
        public ZhengZiPresenter() {
            this.ZhengZiPages = new ObservableCollection<ZhengZiPage>();
            this.ZhengZiPages.CollectionChanged += ZhengZiPages_CollectionChanged;

            IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;

            if (!setting.TryGetValue<int>("CurrentPage", out _currentPage)) {
                CurrentPage = 0;
                setting.Add("CurrentPage", 0);
            }
        }

        private int _currentPage;
        public int CurrentPage {
            set {
                if (value != _currentPage) {
                    NotifyPropertyChanging("CurrentPage");
                    _currentPage = value;
                    NotifyPropertyChanged("CurretnPage");
                }
            }
            get {
                return _currentPage;
            }
        }

        public ObservableCollection<ZhengZiPage> ZhengZiPages { get; private set; }

        void ZhengZiPages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            for (int i = 0; i < ZhengZiPages.Count; i++) {
                ZhengZiPages[i].Index = i;
            }
        }
        /// <summary>
        /// Save whole ZhengZiPresenter to IsoStorage and Database
        /// </summary>
        public void Save() {
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
                IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;
                setting["CurrentPage"] = CurrentPage;
                setting.Save();
            }

            using (Database.ZhengZiPageDataContext database = new Database.ZhengZiPageDataContext(Database.ZhengZiPageDataContext.DBConnectionString)) {
                database.Items.DeleteAllOnSubmit(database.Items);
                database.SubmitChanges();
                foreach (var page in ZhengZiPages) {
                    database.Items.InsertOnSubmit(page);
                }
                database.SubmitChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName) {
            PropertyChangingEventHandler handler = PropertyChanging;
            if (null != handler) {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

    }
}