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

namespace HuaZhengZi.ViewModels
{
    public class PatternPresenter:INotifyPropertyChanged
    {
        public ObservableCollection<StrokePattern> DefaultPatterns;
        public ObservableCollection<StrokePattern> UserPaterns;

        public bool IsDataLoaded {
            set;
            get;
        }
        public void LoadData() {
            DefaultPatterns = StrokePattern.LoadDefaultAll();
            UserPaterns = StrokePattern.LoadAll();
        }
        public void Save() {
            foreach (var pattern in UserPaterns) {
                pattern.Save();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
