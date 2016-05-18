using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Teknobyen.Controls
{
    public sealed partial class InfoToastControl : UserControl
    {
        DispatcherTimer timer;

        public InfoToastControl()
        {
            this.InitializeComponent();
            
        }


        public void Show(string text, int millisecondsToShow)
        {   
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0,1, millisecondsToShow);
            timer.Tick += Timer_Tick;

            InfoTextBox.Text = text;

            RootGrid.Visibility = Visibility.Visible;
            timer.Start();
            
        }

        SolidColorBrush TransparentBackground = new SolidColorBrush(Colors.Transparent);
        SolidColorBrush GrayBackground = new SolidColorBrush(Colors.LightGray);

        private void Display()
        {
            BackgroundRect.Fill = GrayBackground;
        }

        private void Timer_Tick(object sender, object e)
        {
            timer.Stop();
            RootGrid.Visibility = Visibility.Collapsed;
        }
    }
}
