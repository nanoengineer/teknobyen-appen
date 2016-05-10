using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Teknobyen.Converters
{
    class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var date = value as Nullable<DateTime>;
            if (!date.HasValue)
            {
                return "";
            }
            else
            {
                return date.Value.ToString(App.DATEFORMAT);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
