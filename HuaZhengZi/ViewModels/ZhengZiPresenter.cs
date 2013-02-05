using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HuaZhengZi.Resources;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows;

namespace HuaZhengZi.ViewModels
{
    public class ZhengZiPresenter : INotifyPropertyChanged
    {
        public ZhengZiPresenter() {
            this.ZhengZiPages = new ObservableCollection<ZhengZiPage>();

            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            if (!isf.DirectoryExists(ZhengZiPage.DefaultDictionary)) {
                isf.CreateDirectory(ZhengZiPage.DefaultDictionary);
            }
            if (!isf.DirectoryExists(InkPresenterPattern.DefaultDictionary)) {
                isf.CreateDirectory(InkPresenterPattern.DefaultDictionary);
            }
        }

        public void Save() {
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
                IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;
                setting["CurrentPage"] = CurrentPage;
                setting.Save();

                foreach (ZhengZiPage zhengZiPage in ZhengZiPages) {
                    zhengZiPage.Save(zhengZiPage.PageName);
                }
            }
        }


        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ZhengZiPage> ZhengZiPages { get; private set; }

        private InkPresenterPattern _zhengZiPattern;
        public InkPresenterPattern ZhengZiPattern {
            get { return _zhengZiPattern; }
            set {
                if (!(value == _zhengZiPattern)) {
                    _zhengZiPattern = value;
                    NotifyPropertyChanged("ZhengZiPattern");
                    foreach (ZhengZiPage zhengZiPage in ZhengZiPages) {
                        zhengZiPage.NotifyPropertyChanged("Pattern");
                    }
                }
            }
        }

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
            string patternPath;
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
                IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;
                
                if (!setting.TryGetValue<int>("CurrentPage", out _currentPage)){
                    CurrentPage = 0;
                    setting.Add("CurrentPage", 0);
                }
                if (!setting.TryGetValue<string>("DisplayingPattern", out patternPath)) {
                    patternPath = @"DefaultPatterns/DefaultPattern_Zheng.xml";
                    setting.Add("DisplayingPattern", @"DefaultPatterns/DefaultPattern_Zheng.xml");
                }
               
                IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
                string searchPath = Path.Combine(ZhengZiPage.DefaultDictionary, "*.*");
                string[] zhengZiFileNames = isf.GetFileNames(searchPath);
                if (zhengZiFileNames.Length == 0) {
                    ZhengZiPages.Add(new ZhengZiPage());
                } else {
                    foreach (string zhengZiFileName in zhengZiFileNames) {
                        ZhengZiPages.Add(ZhengZiPage.Load(zhengZiFileName));
                    }
                }
                foreach (ZhengZiPage zhengZiPage in ZhengZiPages) {
                    zhengZiPage.GetPattern += zhengZiPage_GetPattern;
                }

                if (patternPath.StartsWith("Default")) {
                    ZhengZiPattern = InkPresenterPattern.LoadDefault(patternPath);
                } else {
                    ZhengZiPattern = InkPresenterPattern.Load(patternPath);
                }
            }
            this.IsDataLoaded = true;
        }

        InkPresenterPattern zhengZiPage_GetPattern() {
            return ZhengZiPattern;
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