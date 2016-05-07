using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teknobyen.ViewModels;
using Template10.Mvvm;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Teknobyen.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LaundryView : Page
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; CheckCanSave(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; CheckCanSave(); }
        }

        private void CheckCanSave () {
            if (Username == null || Password == null)
            {
                MyContentDialog.IsPrimaryButtonEnabled = false;
            }
            else
            {
                MyContentDialog.IsPrimaryButtonEnabled = true;
            }
        }

        public LaundryView()
        {
            this.InitializeComponent();
            //WasherWebView.Navigate(new Uri("http://129.241.152.11/LaundryState?lg=2&ly=9131"));

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Credentials")
            {
                if (ViewModel.Credentials == null)
                {
                    ShowUsernameAndPassworDialog();
                }
            }
        }

        private async void ShowUsernameAndPassworDialog()
        {
            var result = await MyContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var newCredentials = new PasswordCredential(App.APPID, Username, Password);
                ViewModel.Credentials = newCredentials;
                ViewModel.GetLaundryMachineStatusList();
            }
        }
        
        //Meget stygg måte
        private void CheckCanSave(object sender, KeyRoutedEventArgs e)
        {
            CheckCanSave();
        }
    }
}
