using System;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Teknobyen.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }


        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //var _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            //var animation = _compositor.CreateScalarKeyFrameAnimation();
            //animation.InsertKeyFrame(0.5f, 0.5f);
            //animation.Duration = TimeSpan.FromSeconds(5);

            //var visual = ElementCompositionPreview.GetElementVisual(ProjectorButton);

            //visual.StartAnimation("Scale.X", animation);
        }
    }
}
