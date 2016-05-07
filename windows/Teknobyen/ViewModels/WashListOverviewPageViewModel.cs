using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class WashListOverviewPageViewModel : ViewModelBase
    {



        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void GotoAdminPage()
        {
            //validate username and password agains laundry username and pwrd.

        }
    }
}
