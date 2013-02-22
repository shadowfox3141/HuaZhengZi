using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace HuaZhengZi.ViewModels
{
    public class StrokePattern : INotifyPropertyChanged
    {
        public StrokePattern() {
            Items = new List<StrokeCollection>(HighestCount);
        }

        public List<StrokeCollection> _items;
        public List<StrokeCollection> Items { get {
            return _items;
            }
            set {
                if (value != _items) {
                    _items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }

        private string _patternName;
        public string PatternName {
            get {
                return _patternName;
            }
            set {
                if (!(value == _patternName)) {
                    _patternName = value;
                    NotifyPropertyChanged("PatternName");
                }
            }
        }
        public int SaveIndex { set; get; }


        public override string ToString() {
            return PatternName;
        }

        public const int HighestCount = 5;
        public const string UserDictionary = "Patterns";
        public const string DefaultDictiony = "DefaultPatterns";

        public void Save() {
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
                IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
                if (!isf.DirectoryExists(UserDictionary)) {
                    isf.CreateDirectory(UserDictionary);
                }
                FileStream stream = isf.CreateFile(UserDictionary + "/" + "UserPattern_" + SaveIndex.ToString());
                StreamWriter writer = new StreamWriter(stream);
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                isf.Dispose();
                writer.Close();
            }
        }
        public static StrokePattern Load(string fileName) {
            StrokePattern pattern;
            if (System.ComponentModel.DesignerProperties.IsInDesignTool) {
                return new StrokePattern();
            } else {
                IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
                if (isf.FileExists(UserDictionary + "/" + fileName)) {
                    FileStream stream = isf.OpenFile(UserDictionary + "/" + fileName, FileMode.Open);
                    StreamReader reader = new StreamReader(stream);
                    XmlSerializer serializer = new XmlSerializer(typeof(StrokePattern));
                    pattern = (StrokePattern)serializer.Deserialize(reader);
                } else {
                    throw new KeyNotFoundException("No PatternName found");
                }
                return pattern;
            }
        }
        public static StrokePattern LoadDefault(string fileName) {
            StrokePattern pattern;
            FileStream stream = File.Open(DefaultDictiony + "/" + fileName, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            XmlSerializer serializer = new XmlSerializer(typeof(StrokePattern));
            pattern = (StrokePattern)serializer.Deserialize(reader);
            reader.Close();
            return pattern;
        }

        public static ObservableCollection<StrokePattern> LoadAll() {
            ObservableCollection<StrokePattern> userPatterns = new ObservableCollection<StrokePattern>();
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            foreach (var file in isf.GetFileNames(Path.Combine(UserDictionary, "*.*"))) {
                StrokePattern pattern;
                if (isf.FileExists(UserDictionary + "/" + file)) {
                    FileStream stream = isf.OpenFile(UserDictionary + "/" + file, FileMode.Open);
                    StreamReader reader = new StreamReader(stream);
                    XmlSerializer serializer = new XmlSerializer(typeof(StrokePattern));
                    pattern = (StrokePattern)serializer.Deserialize(reader);
                } else {
                    throw new KeyNotFoundException("No PatternName found");
                }
                userPatterns.Add(pattern);
            }
            return userPatterns;
        }

        public static ObservableCollection<StrokePattern> LoadDefaultAll() {
            ObservableCollection<StrokePattern> defaultPatterns = new ObservableCollection<StrokePattern>();
            DirectoryInfo DefaultDic = new DirectoryInfo(DefaultDictiony);
            foreach (var file in DefaultDic.GetFiles()) {
                FileStream stream = File.Open(file.FullName, FileMode.Open);
                StreamReader reader = new StreamReader(stream);
                XmlSerializer serializer = new XmlSerializer(typeof(StrokePattern));
                StrokePattern pattern = (StrokePattern)serializer.Deserialize(reader);
                reader.Close();
                defaultPatterns.Add(pattern);
            }
            return defaultPatterns;
        }

        public StrokeCollection GetStrokeCollection(int count = HighestCount) {
            StrokeCollection strokeCollection = new StrokeCollection();
            for (int i = 0; i < count; i++) {
                foreach (Stroke stroke in Items[i]) {
                    strokeCollection.Add(stroke);
                }
            }
            return strokeCollection;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

