using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Teknobyen.Converters
{
    class AssignmentToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int assigment = (int)value;
            if (assigment == 1)
            {
                return new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
            }
            else
            {
                return new SolidColorBrush(Windows.UI.Color.FromArgb(255, 230, 230, 230));
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
