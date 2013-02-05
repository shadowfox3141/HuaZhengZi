using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Serialization;
using System.IO;
using System.IO.IsolatedStorage;

namespace HuaZhengZi.ViewModels
{
    public class ZhengZiPage :  INotifyPropertyChanged
    {
        public void Save(string fileName) {
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            if (!isf.DirectoryExists(@"ZhengZiPages")) {
                try {
                    isf.CreateDirectory(@"ZhengZiPages");
                } catch (IsolatedStorageException e) {
                    Debug.WriteLine(e.ToString());
                    return;
                }
            }
            IsolatedStorageFileStream isfStream = isf.CreateFile(@"ZhengZiPages/" + fileName);
            StreamWriter writer = new StreamWriter(isfStream);
            XmlSerializer serializer = new XmlSerializer(typeof(ZhengZiPage));
            serializer.Serialize(writer, this);

            writer.Close();
            isf.Dispose();
        }

        public static ZhengZiPage Load(string fileName) {
            ZhengZiPage zhengZiPage;
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            if (!isf.FileExists(@"ZhengZiPages/" + fileName)) {
                return null;
            }
            IsolatedStorageFileStream stream = isf.OpenFile(@"ZhengZiPages/" + fileName,FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(ZhengZiPage));
            StreamReader reader = new StreamReader(stream);
            zhengZiPage = (ZhengZiPage)serializer.Deserialize(reader);

            reader.Close();
            isf.Dispose();
            return zhengZiPage;
        }

        public const string DefaultDictionary = "ZhengZiPages";

        public ZhengZiPage() {
            _pageName = "EnterYourTitleHere";
            _zhengZiCount = 0;
        }

        [XmlIgnore]
        public InkPresenterPattern Pattern {
            get {
                return GetPattern();
            }
        }
        public event Func<InkPresenterPattern> GetPattern;


        private string _pageName;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string PageName {
            get {
                return _pageName;
            }
            set {
                if (value != _pageName) {
                    _pageName = value;
                    NotifyPropertyChanged("PageName");
                }
            }
        }

        private int _zhengZiCount;
        public int ZhengZiCount {
            get {
                return _zhengZiCount;
            }
            set {
                if (value != _zhengZiCount) {
                    _zhengZiCount = value;
                    NotifyPropertyChanged("ZhengZiCount");
                }
            }
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