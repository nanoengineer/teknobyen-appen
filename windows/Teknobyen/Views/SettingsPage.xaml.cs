using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teknobyen.Common;
using Teknobyen.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Teknobyen.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();            

            DoInitialRoomSelcetionSetup();
            CheckRoomnumberValidity();

            SetupLaundryLoginStatus();
        }

        private void SetupLaundryLoginStatus()
        {
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.LaundryLoginIsValid))
            {
                CkeckLaundryLoginValidity();
            }
        }

        private void CkeckLaundryLoginValidity()
        {
            switch (ViewModel.LaundryLoginIsValid)
            {
                case true:
                    LaundryLoginValidImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ok-96.png"));
                    break;
                case false:
                    LaundryLoginValidImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Cancel-96.png"));
                    break;
                default:
                    LaundryLoginValidImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Alert-96.png"));
                    break;
            }
        }

        private void DoInitialRoomSelcetionSetup()
        {
            RoomNumberComboBox.ItemsSource = RoomManager.AllRooms;
            CheckRoomnumberValidity();
        }

        #region NameTextBoxEvents
        private void NameTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            CheckNameValidity();
        }
        private void NameTextBox_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            CheckNameValidity();
        }
        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNameValidity();
        }
        #endregion



        private void CheckRoomnumberValidity()
        {
            if (RoomNumberComboBox.SelectedItem == null)
            {
                SetImageState(RoomNumberValidationImage, false);
            }
            else
            {
                SetImageState(RoomNumberValidationImage, true);
            }
        }

        private void CheckNameValidity()
        {
            if (NameTextBox.Text.Length > 1)
            {
                SetImageState(NameValidationImage, true);
            }
            else
            {
                SetImageState(NameValidationImage, false);
            }
        }

        private void SetImageState(Image image, bool isOk)
        {
            if (isOk)
            {
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Ok-96.png"));
            }
            else
            {
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Cancel-96.png"));
            }
        }

        private void RoomNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckRoomnumberValidity();
        }
    }
}
