using System;
using System.Threading.Tasks;
using Teknobyen.Services.NotificationService;
using Teknobyen.Services.SettingsService;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Teknobyen
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki


    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {

        public static string APPID = "Teknobyen.App";
        public static string DATEFORMAT = "dd.MM.yyyy";
        public static string TIMEFORMAT = "HH.mm";

        public static Prism.Events.EventAggregator EventAggregator;

        public App() {
            InitializeComponent();
            EventAggregator = new Prism.Events.EventAggregator();
        }

        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            
            //var evt = EventAggregator.GetEvent<Messages.EventTest>();
            //var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            //timer.Tick += (s, e) => evt.Publish("Hei");
            //timer.Start();

            return Task.CompletedTask;
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            if (SettingsService.Instance.FirstRunCompleted)
            {
                NavigationService.Navigate(typeof(Views.MainPage));
            }
            else
            {
                NavigationService.Navigate(typeof(Views.SettingsPage));
            }

            NotificationService.Instance.HandleNextWashDayNotification();
            
            return Task.CompletedTask;
        }
    }
}

