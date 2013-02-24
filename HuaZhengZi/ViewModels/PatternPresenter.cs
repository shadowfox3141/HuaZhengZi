using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HuaZhengZi.Resources;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows;
using HuaZhengZi.UserControls;
using System.Text.RegularExpressions;

namespace HuaZhengZi.ViewModels
{
    public class PatternPresenter:INotifyPropertyChanged
    {
        public PatternPresenter() {
            DefaultPatterns = new ObservableCollection<StrokePattern>();
            UserPatterns = new ObservableCollection<StrokePattern>();
            UserPatterns.CollectionChanged += Patterns_CollectionChanged;

            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            if (!isf.DirectoryExists(StrokePattern.UserDictionary)) {
                isf.CreateDirectory(StrokePattern.UserDictionary);
            }
        }

        private void Patterns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            ObservableCollection<StrokePattern> collection =(sender as ObservableCollection<StrokePattern>);
            for (int i=0;i< collection.Count;i++) {
                collection[i].SaveIndex = i;
            }
        }

        InkPattern _selectedPattern;
        public InkPattern SelectPattern {
            set {
                if (value != _selectedPattern) {
                    _selectedPattern = value;
                    NotifyPropertyChanged("SelectPattern");
                }
            }
            get {
                return _selectedPattern;
            }
        }

        public ObservableCollection<StrokePattern> DefaultPatterns {
            get;
            set;
        }
        public ObservableCollection<StrokePattern> UserPatterns { get; set; }

        public bool IsDataLoaded {
            set;
            get;
        }
        public void LoadData() {
            foreach (var strokeCollection in StrokePattern.LoadDefaultAll()) {
                DefaultPatterns.Add(strokeCollection);
            }
            foreach (var strokeCollection in StrokePattern.LoadAll()) {
                UserPatterns.Add(strokeCollection);
            }
            IsDataLoaded = true;
        }
        public void Save() {
            foreach (var pattern in UserPatterns) {
                pattern.Save();
            }
            if (UserPatterns.Count != 0) {
                IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
                string searchPath = Path.Combine(StrokePattern.UserDictionary, "*.*");
                Regex regex = new Regex("Pattern_[0-" + (UserPatterns.Count - 1).ToString() + "]");
                foreach (var name in isf.GetFileNames(searchPath)) {
                    if (!regex.IsMatch(name)) {
                        isf.DeleteFile(StrokePattern.UserDictionary + @"/" + name);
                    }
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
