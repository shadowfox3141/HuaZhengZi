using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace HuaZhengZi
{
    public partial class CreatingPage : PhoneApplicationPage
    {
        Dictionary<int, Stroke> activeStokes = new Dictionary<int, Stroke>();

        public CreatingPage() {
            InitializeComponent();

            Touch.FrameReported += Touch_FrameReported;
        }

        public static readonly DependencyProperty DoneNumberProperty = DependencyProperty.Register(
            "DoneNumber", typeof(int), typeof(CreatingPage), new PropertyMetadata(DoneNumer_Changed));
        public int DoneNumber {
            get { 
                return (int)GetValue(DoneNumberProperty); }
            set {
                SetValue(DoneNumberProperty, value);
            }
        }
        private static void DoneNumer_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if ((int)e.NewValue != 0) {
                ((ApplicationBarIconButton)((d as CreatingPage).ApplicationBar.Buttons[0])).IsEnabled = true;
                ((ApplicationBarIconButton)((d as CreatingPage).ApplicationBar.Buttons[1])).IsEnabled = true;
            } else{
                ((ApplicationBarIconButton)((d as CreatingPage).ApplicationBar.Buttons[0])).IsEnabled = false;
                ((ApplicationBarIconButton)((d as CreatingPage).ApplicationBar.Buttons[1])).IsEnabled = false;
            }
            if (((int)e.NewValue != 5)&&((int)e.OldValue == 5)) {
                Touch.FrameReported += (d as CreatingPage).Touch_FrameReported;
                ((ApplicationBarIconButton)((d as CreatingPage).ApplicationBar.Buttons[2])).IsEnabled = false;
            }
            if ((int)e.NewValue == ViewModels.InkPresenterPattern.HighestCount) {
                foreach (ApplicationBarIconButton appBarBtn in (d as CreatingPage).ApplicationBar.Buttons) {
                    appBarBtn.IsEnabled = false;
                }
                Popup inputPopup = new Popup();
                inputPopup.Child = new UserControls.InputPopup();
                (inputPopup.Child as UserControls.InputPopup).CallPage = d as PhoneApplicationPage;
                (inputPopup.Child as UserControls.InputPopup).GetResault += CreatingPage_GetResault;
                Touch.FrameReported -= (d as CreatingPage).Touch_FrameReported;
                inputPopup.IsOpen=true;
            }
        }

        static void CreatingPage_GetResault(PhoneApplicationPage sender, string obj) {
            foreach (ApplicationBarIconButton appBarBtn in (sender as CreatingPage).ApplicationBar.Buttons) {
                appBarBtn.IsEnabled = true;
            }
        }


        void Touch_FrameReported(object sender, TouchFrameEventArgs e) {
            TouchPoint primaryTouchPoint = e.GetPrimaryTouchPoint(null);
            if (primaryTouchPoint != null && primaryTouchPoint.Action == TouchAction.Down) {
                e.SuspendMousePromotionUntilTouchUp();
            }
            TouchPoint touchPoint = (e.GetTouchPoints(inkPresenter))[0];
            Point pt = touchPoint.Position;
            int id = touchPoint.TouchDevice.Id;
            switch (touchPoint.Action) {
                case TouchAction.Down:
                    if (touchPoint.Position.IsInside(new Rect(-150, -150, 300, 300))) {
                        Stroke stroke = new Stroke();
                        stroke.DrawingAttributes.Color = Colors.DarkGray;
                        stroke.DrawingAttributes.Height = 4;
                        stroke.DrawingAttributes.Width = 4;
                        stroke.StylusPoints.Add(new StylusPoint(pt.X, pt.Y));

                        inkPresenter.Strokes.Add(stroke);
                        activeStokes.Add(id, stroke);
                    }
                    break;
                case TouchAction.Move:
                    if (touchPoint.Position.IsInside(new Rect(-150, -150, 300, 300))) {
                        if (activeStokes.ContainsKey(id)) {
                            activeStokes[id].StylusPoints.Add(new StylusPoint(pt.X, pt.Y));
                        }
                    } else {
                        if (activeStokes.ContainsKey(id)) {
                            activeStokes.Remove(id);
                            DoneNumber += 1;
                        }
                    }
                    break;
                case TouchAction.Up:
                    if (activeStokes.ContainsKey(id)) {
                        activeStokes[id].StylusPoints.Add(new StylusPoint(pt.X, pt.Y));
                        activeStokes.Remove(id);
                        DoneNumber += 1;
                    }              
                    break;
            }
        }

        private void Undo_Click(object sender, EventArgs e) {
            inkPresenter.Strokes.RemoveAt(inkPresenter.Strokes.Count - 1);
            DoneNumber -= 1;
        }

        private void Clear_Click(object sender, EventArgs e) {
            DoneNumber = 0;
            inkPresenter.Strokes.Clear();
        }

        private void Save_Click(object sender, EventArgs e) {

        }
    }
}