using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teknobyen.Models;
using Teknobyen.Services.PrintService;
using Teknobyen.Services.WashListService;
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
    public sealed partial class WashListAdminPage : Page
    {
        public WashListAdminPage()
        {
            this.InitializeComponent();
        }

        PrintService _printService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _printService = new PrintService(this);
            _printService.RegisterForPrinting();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _printService.UnregisterForPrinting();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var washListToPrint = WashListService.Instance.GetPrintableWashList(ViewModel.GeneratedWashDayList);
                if (washListToPrint.Count < 1)
                {
                    return;
                }
                _printService.PreparePrintContent(washListToPrint);
                await _printService.ShowPrintUIAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

            
        }
    }
}
