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
using Windows.UI.Xaml.Media.Animation;
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

        #region LaundryMachineModel property
        public LaundryMachineStatusModel LaundryMachineModel
        {
            get { return (LaundryMachineStatusModel)GetValue(LaundryMachineModelProperty); }
            set
            {
                SetValue(LaundryMachineModelProperty, value);
                UpdateVisual();
            }
        }
        // Using a DependencyProperty as the backing store for LaundryMachineModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LaundryMachineModelProperty =
            DependencyProperty.Register(nameof(LaundryMachineModel), typeof(LaundryMachineStatusModel), typeof(WashingMachineControl), new PropertyMetadata(new LaundryMachineStatusModel()));
        #endregion

        private void UpdateVisual()
        {
            IdText.Text = LaundryMachineModel.MachineId.ToString();

            #region Set color
            switch (LaundryMachineModel.Status)
            {
                case MachineStatus.Available:
                    RootGrid.Background = new SolidColorBrush(Colors.Green);
                    break;
                case MachineStatus.Busy:
                    RootGrid.Background = new SolidColorBrush(Colors.Red);
                    break;
                case MachineStatus.Reserved:
                    RootGrid.Background = new SolidColorBrush(Colors.Orange);
                    break;
                case MachineStatus.Unknown:
                default:
                    RootGrid.Background = new SolidColorBrush(Colors.Gray);
                    break;
            }
            #endregion

            #region Set text
            switch (LaundryMachineModel.Status)
            {
                case MachineStatus.Available:
                    TimeLeftText.Text = "Ledig";
                    break;
                case MachineStatus.Busy:
                    TimeLeftText.Text = $"{LaundryMachineModel.MinutesLeft.ToString()} min igjen";
                    break;
                case MachineStatus.Reserved:
                    TimeLeftText.Text = $"Reservert fra kl {LaundryMachineModel.ReservedTime.TimeOfDay.ToString()}";
                    break;
                case MachineStatus.Unknown:
                    TimeLeftText.Text = "Ukjent";
                    break;
                default:
                    break;
            }
            #endregion
        }


        #region Events
        private void UserControl_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"machine x right tapped");
        }

        private void UserControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"machine x tapped");
        }
        #endregion

    }
}
