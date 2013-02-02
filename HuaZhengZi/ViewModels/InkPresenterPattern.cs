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

namespace HuaZhengZi.ViewModels
{
    public class InkPresenterPattern :List<StrokeCollection>, INotifyPropertyChanged
    {
        public InkPresenterPattern() { 
        }

        public const int HighestCount = 5;
        public const string DefaultDictionary = "ZhengZiPatterns";

        public void Save() {
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
                IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
                if (!isf.DirectoryExists(@"Patterns")) {
                    isf.CreateDirectory("Pattern");
                }
                FileStream stream = isf.CreateFile(@"Patterns/" + PatternName);
                StreamWriter writer = new StreamWriter(stream);
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                isf.Dispose();
                writer.Close();
            }
        }
        public static InkPresenterPattern Load(string patternName) {
            InkPresenterPattern pattern;
            if(System.ComponentModel.DesignerProperties.IsInDesignTool){
                return new InkPresenterPattern();
            }else{
                IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
                if (isf.FileExists(@"Pattern/" + patternName)) {
                    FileStream stream = isf.OpenFile(@"Pattern/" + patternName, FileMode.Open);
                    StreamReader reader = new StreamReader(stream);
                    XmlSerializer serializer = new XmlSerializer(typeof(InkPresenterPattern));
                    pattern = (InkPresenterPattern)serializer.Deserialize(reader);
                } else {
                    throw new KeyNotFoundException("No PatternName found");
                }
                return pattern;
            }
        }
        public static InkPresenterPattern LoadDefault(string patternName) {
            InkPresenterPattern pattern;
            FileStream stream = File.Open(patternName, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            XmlSerializer serializer = new XmlSerializer(typeof(InkPresenterPattern));
            pattern = (InkPresenterPattern)serializer.Deserialize(reader);
            reader.Close();
            return pattern;
        }

        public StrokeCollection GetStrokeCollection(int count=HighestCount) {
            StrokeCollection strokeCollection = new StrokeCollection();
            for (int i = 0; i < count; i++) {
                foreach (Stroke stroke in this[i]) {
                    strokeCollection.Add(stroke);
                }
            }
            return strokeCollection;
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
