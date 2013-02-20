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
        int currentPage;
        public int CurrentPage {
            set {
                currentPage = value;
            }
            get {
                return currentPage;
            }
        }

        public ZhengZiPresenter() {
            this.ZhengZiPages = new ObservableCollection<ZhengZiPage>();
            ZhengZiPages.CollectionChanged += ZhengZiPages_CollectionChanged;
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            if (!isf.DirectoryExists(ZhengZiPage.DefaultDictionary)) {
                isf.CreateDirectory(ZhengZiPage.DefaultDictionary);
            }
            if (!isf.DirectoryExists(StrokePattern.UserDictionary)) {
                isf.CreateDirectory(StrokePattern.UserDictionary);
            }
        }

        public void Save() {
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
                IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;
                setting["CurrentPage"] = CurrentPage;
                setting.Save();

                foreach (ZhengZiPage zhengZiPage in ZhengZiPages) {
                    zhengZiPage.Save("Page_" + zhengZiPage.Index.ToString());
                }
            }
        }


        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ZhengZiPage> ZhengZiPages { get; private set; }

        private StrokePattern _zhengZiPattern;
        public StrokePattern ZhengZiPattern {
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
                
                if (!setting.TryGetValue<int>("CurrentPage", out currentPage)){
                    CurrentPage = 0;
                    setting.Add("CurrentPage", 0);
                }
                if (!setting.TryGetValue<string>("DisplayingPattern", out patternPath)) {
                    patternPath = @"DefaultPattern_Zheng.xml";
                    setting.Add("DisplayingPattern", @"DefaultPattern_Zheng.xml");
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
                foreach (ZhengZiPage page in ZhengZiPages) {
                    page.GetPattern += page_GetPattern;
                }

                if (patternPath.StartsWith("Default")) {
                    ZhengZiPattern = StrokePattern.LoadDefault(patternPath);
                } else {
                    ZhengZiPattern = StrokePattern.Load(patternPath);
                }
            }         
            this.IsDataLoaded = true;
        }

        void ZhengZiPages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            for (int i = 0; i < ZhengZiPages.Count;i++ ) {
                ZhengZiPages[i].Index = i;
                if (!ZhengZiPages[i].IsPatternAttached) {
                    ZhengZiPages[i].GetPattern += page_GetPattern;
                }
            }
        }

        StrokePattern page_GetPattern() {
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