using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Teknobyen.Controls
{
    class TwoWayCalendarDatePicker : CalendarDatePicker
    {
        public DateTimeOffset SelectedDate
        {
            get
            {
                return (DateTimeOffset)GetValue(SelectedDateProperty);
            }
            set
            {
                SetValue(SelectedDateProperty, value);
            }
        }
        // Using a DependencyProperty as the backing store for SelectedDate.This enables animation, tyling, binding, etc...
        public static readonly DependencyProperty
            SelectedDateProperty = DependencyProperty.Register("SelectedDate",
                typeof(DateTimeOffset),
                typeof(TwoWayCalendarDatePicker),
                new PropertyMetadata(null, (sender, e) =>
                {
                    if (e.NewValue != null)
                    {
                        ((TwoWayCalendarDatePicker)sender).Date = (DateTimeOffset)e.NewValue;
                    }
                    else
                    {
                        ((TwoWayCalendarDatePicker)sender).Date = null;
                    }
                }));

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.DateChanged += TwoWayCalendarDatePicker_DateChanged;
        }

        private void TwoWayCalendarDatePicker_DateChanged (CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate != args.OldDate)
            {
                if (args.NewDate != null && args.NewDate.HasValue)
                {
                    SelectedDate = args.NewDate.Value;
                }
                else if (args.OldDate != null && args.OldDate.HasValue)
                {
                    SelectedDate = args.OldDate.Value;
                    Date = args.OldDate;
                }
                else
                {
                    SelectedDate = DateTimeOffset.Now;
                }
            }
        }
    }
}
