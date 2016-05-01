using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Teknobyen.Views;

namespace Teknobyen.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        public void GotoClothesWashMenu()
        {
            this.NavigationService.Navigate(typeof(LaundryView));
        }
    }
}

