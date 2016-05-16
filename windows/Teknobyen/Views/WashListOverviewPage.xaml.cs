using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teknobyen.Models;
using Teknobyen.Services.SettingsService;
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
    public sealed partial class WashListOverviewPage : Page
    {
        public WashListOverviewPage()
        {
            this.InitializeComponent();

            if (SettingsService.Instance.IsAdmin) AdminPageButton.Visibility = Visibility.Visible;
        }

        private void ListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            try
            {
                sender.ScrollIntoView((from m in ((sender.ItemsSource) as List<WashDayModel>)
                                       where m.Date.Date == DateTime.Today && m.Assignment == 1
                                       select m).ToList().First(), ScrollIntoViewAlignment.Leading);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                //Do nothing
            }            
        }
    }
}
