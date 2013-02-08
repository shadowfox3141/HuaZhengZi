using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;

namespace HuaZhengZi.UserControls
{
    public partial class InputPopup : UserControl
    {
        public InputPopup() {
            InitializeComponent();
        }

        public PhoneApplicationPage CallPage { set; get; }

        private void OKBtn_Click(object sender, RoutedEventArgs e) {
            Popup parent = this.Parent as Popup;
            if (GetResault != null) {
                GetResault(CallPage,NameBox.Text);
            }
            parent.IsOpen = false;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) {
            Popup parent = this.Parent as Popup;
            if (GetResault != null) {
                GetResault(CallPage,null);
            }
            parent.IsOpen = false;
        }

        public event Action<PhoneApplicationPage,string> GetResault;
    }
}
