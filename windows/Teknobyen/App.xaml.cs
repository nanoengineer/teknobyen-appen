using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Data;

namespace Teknobyen
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki


    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {

        public static string APPID = "Teknobyen.App";

        public App() {
            InitializeComponent();
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            NavigationService.Navigate(typeof(Views.MainPage));
            await Task.CompletedTask;
        }

        public override Task OnPrelaunchAsync(IActivatedEventArgs args, out bool runOnStartAsync)
        {
            return base.OnPrelaunchAsync(args, out runOnStartAsync);
        }
    }
}

