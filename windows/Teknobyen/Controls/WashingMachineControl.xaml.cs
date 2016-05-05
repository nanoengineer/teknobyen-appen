using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teknobyen.Models;
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
    public sealed partial class WashingMachineControl : UserControl
    {
        public WashingMachineControl()
        {
            this.InitializeComponent();
        }

        public int Mid
        {
            get { return (int)GetValue(MidProperty); }
            set {
                SetValue(MidProperty, value);
                IdText.Text = value.ToString();
            }
        }
        // Using a DependencyProperty as the backing store for Mid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MidProperty =
            DependencyProperty.Register("Mid", typeof(int), typeof(WashingMachineControl), new PropertyMetadata(0));


        public int MMleft
        {
            get { return (int)GetValue(MMleftProperty); }
            set {
                SetValue(MMleftProperty, value);
                if (value <= 0)
                {
                    TimeLeftText.Text = "Ledig";
                }
                else
                {
                    TimeLeftText.Text = $"{value} min igjen";
                    RootGrid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                }
            }
        }
        // Using a DependencyProperty as the backing store for MMleft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MMleftProperty =
            DependencyProperty.Register("MMleft", typeof(int), typeof(WashingMachineControl), new PropertyMetadata(0));

        private void UserControl_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            
        }
    }
}
