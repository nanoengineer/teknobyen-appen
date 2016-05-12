using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Teknobyen.Services.PrintService
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WashListPrintPage : Page
    {
        public WashListPrintPage()
        {
            this.InitializeComponent();
        }

        #region Roomnumber properties
        public int mon1 { get; set; }
        public int mon2 { get; set; }
        public int tue1 { get; set; }
        public int tue2 { get; set; }
        public int wed1 { get; set; }
        public int wed2 { get; set; }
        public int thu1 { get; set; }
        public int thu2 { get; set; }
        public int fri1 { get; set; }
        public int fri2 { get; set; }
        public int sat1 { get; set; }
        public int sat2 { get; set; }
        public int sun1 { get; set; }
        public int sun2 { get; set; }
        #endregion
    }
}
