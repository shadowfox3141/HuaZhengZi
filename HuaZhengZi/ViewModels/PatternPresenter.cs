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

namespace HuaZhengZi.ViewModels
{
    public class PatternPresenter:INotifyPropertyChanged
    {
        public PatternPresenter() {
            DefaultPatterns = new ObservableCollection<StrokePattern>();
            UserPaterns = new ObservableCollection<StrokePattern>();
            UserPaterns.CollectionChanged += Patterns_CollectionChanged;
        }

        private void Patterns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            throw new NotImplementedException();
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
            private set;
        }
        public ObservableCollection<StrokePattern> UserPaterns { get; private set; }

        public bool IsDataLoaded {
            set;
            get;
        }
        public void LoadData() {
            DefaultPatterns = StrokePattern.LoadDefaultAll();
            UserPaterns = StrokePattern.LoadAll();
            IsDataLoaded = true;
        }
        public void Save() {
            foreach (var pattern in UserPaterns) {
                pattern.Save();
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
