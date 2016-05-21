using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teknobyen.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Teknobyen.Controls
{
    public sealed partial class WashListControl : UserControl
    {
        public WashListControl()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public List<WashDayModel> WashList
        {
            get { return (List<WashDayModel>)GetValue(WashListProperty); }
            set
            {
                SetValueDp(WashListProperty, value);
                try
                {
                    if (WashList == null)
                    {
                        return;
                    }
                    var todaysWashDayModels = from m in WashList
                                              where m.Date == DateTime.Today && m.Assignment == 1
                                              select m;

                    if (todaysWashDayModels.Count() > 0)
                    {
                        WashListView.ScrollIntoView(todaysWashDayModels.First(), ScrollIntoViewAlignment.Leading);
                    }
                }
                catch (Exception)
                {
                    //Not found, ok
                }
               
            }
        }

        // Using a DependencyProperty as the backing store for WashList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WashListProperty =
            DependencyProperty.Register(nameof(WashList), typeof(List<WashDayModel>), typeof(WashListControl), new PropertyMetadata(0));

        //reusable
        public event PropertyChangedEventHandler PropertyChanged;
        void SetValueDp(DependencyProperty property, object value, [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
    }
}
