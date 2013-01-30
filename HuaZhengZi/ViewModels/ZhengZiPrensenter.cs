using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HuaZhengZi.Resources;
using System.IO.IsolatedStorage;
using System.IO;

namespace HuaZhengZi.ViewModels
{
    public class ZhengZiPrensenter : INotifyPropertyChanged
    {
        public ZhengZiPrensenter() {
            this.ZhengZiPages = new ObservableCollection<ZhengZiPage>();

            IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;
            if (setting.TryGetValue<int>("CurrentPage", out _currentPage)) ;
            else {
                CurrentPage = 0;
            }

            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();

            string[] zhengZiFileNames = isf.GetFileNames(Path.Combine(ZhengZiPage.DefaultDictionary, "*.*"));
            foreach (string zhengZiFileName in zhengZiFileNames) {
                ZhengZiPages.Add(ZhengZiPage.Load(zhengZiFileName));
            }
        }

        public void Save() {
            IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;
            setting["CurrentPage"] = CurrentPage;
            setting.Save();

            foreach (ZhengZiPage zhengZiPage in ZhengZiPages) {
                zhengZiPage.Save(zhengZiPage.PageName);
            }
        }


        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ZhengZiPage> ZhengZiPages { get; private set; }

        private int _currentPage;
        public int CurrentPage {
            set {
                if (!(value == _currentPage)) {
                    _currentPage = value;
                    NotifyPropertyChanged("CurrentPage");
                }
            }
            get {
                return _currentPage;
            }
        }

        public bool IsDataLoaded {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData() {
            // Sample data; replace with real data
            
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}