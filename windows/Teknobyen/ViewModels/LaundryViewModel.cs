using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class LaundryViewModel : ViewModelBase
    {
        
        private Uri _washingUrl;
        public Uri WashingUrl
        {
            get { return _washingUrl; }
            set { _washingUrl = value; }
        }


        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            WashingUrl = (Uri)parameter;
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
