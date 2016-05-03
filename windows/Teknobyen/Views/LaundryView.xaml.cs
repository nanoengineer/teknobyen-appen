using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teknobyen.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        // strongly-typed view models enable x:bind

        public LaundryView()
        {
            this.InitializeComponent();
            //WasherWebView.Navigate(new Uri("http://129.241.152.11/LaundryState?lg=2&ly=9131"));

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "HtmlContent")
            {
                //Do nothing
            }
        }
    }
}
