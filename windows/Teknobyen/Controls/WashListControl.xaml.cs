using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Teknobyen.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Teknobyen.Controls
{
    public sealed partial class WashListControl : UserControl
    {

        #region Compositor Member vars 
        private Compositor _compositor; 
        
        
        #region Stagger constants 
        private const float ENTRANCE_ANIMATION_DURATION = 350; 
        private const float ENTRANCE_ANIMATION_OPACITY_STAGGER_DELAY = 25; 
        private const float ENTRANCE_ANIMATION_TEXT_STAGGER_DELAY = 25; 
        #endregion 
        
        
        #region PTR Member Variables 
        private CompositionPropertySet _scrollerViewerManipulation; 
        private ExpressionAnimation _rotationAnimation, _opacityAnimation, _offsetAnimation; 
        private ScalarKeyFrameAnimation _resetAnimation, _loadingAnimation; 
        private Visual _borderVisual; 
        private Visual _refreshIconVisual; 
        private float _refreshIconOffsetY; 
        private const float REFRESH_ICON_MAX_OFFSET_Y = 36.0f; 
        bool _refresh; 
        private DateTime _pulledDownTime, _restoredTime;
        #endregion

        #endregion

        public WashListControl()
        {
            this.InitializeComponent();

            InitializeCompositor();

            (this.Content as FrameworkElement).DataContext = this;
        }

        private void InitializeCompositor()
        { 
            // get the compositor 
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor; 
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

        private void WashListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            int index = args.ItemIndex;
            var root = args.ItemContainer.ContentTemplateRoot as UserControl;
            var item = args.Item as WashDayModel;

            if (!args.InRecycleQueue)
            {
                args.ItemContainer.Loaded += ItemContainer_Loaded;
            }

            args.Handled = true;
        }

        private void ItemContainer_Loaded(object sender, RoutedEventArgs e)
        {
            var itemsPanel = (ItemsStackPanel)WashListView.ItemsPanelRoot;
            var itemContainer = (ListViewItem)sender;
            var itemIndex = WashListView.IndexFromContainer(itemContainer);



            var uc = itemContainer.ContentTemplateRoot as Grid;
            var childPanel = uc;
                //= uc.FindName("ActivityListItemPanel") as RelativePanel;



            //Don't animate if we're not in the visible viewport 
            //
            //PPS:
            //Trying to change delay time as item 38 won't be show until after a long time
            //
            if (itemIndex >= itemsPanel.FirstVisibleIndex && itemIndex <= itemsPanel.LastVisibleIndex)
            {
                var itemVisual = ElementCompositionPreview.GetElementVisual(itemContainer);
                float width = (float)childPanel.RenderSize.Width;
                float height = (float)childPanel.RenderSize.Height;
                itemVisual.Size = new Vector2(width, height);
                itemVisual.CenterPoint = new Vector3(width / 2, height / 2, 0f);
                itemVisual.Scale = new Vector3(1, 1, 1); // new Vector3(0.25f, 0.25f, 0); 
                itemVisual.Opacity = 0f;
                itemVisual.Offset = new Vector3(0, 100, 0);

                //Item index relative to visible
                var relativeItemIndex = itemIndex - itemsPanel.FirstVisibleIndex;

                // Create KeyFrameAnimations 
                KeyFrameAnimation offsetAnimation = _compositor.CreateScalarKeyFrameAnimation();
                offsetAnimation.InsertExpressionKeyFrame(1f, "0");
                offsetAnimation.Duration = TimeSpan.FromMilliseconds(1250);
                offsetAnimation.DelayTime = TimeSpan.FromMilliseconds(relativeItemIndex * 100);



                Vector3KeyFrameAnimation scaleAnimation = _compositor.CreateVector3KeyFrameAnimation();
                scaleAnimation.InsertKeyFrame(0, new Vector3(1f, 1f, 0f));
                scaleAnimation.InsertKeyFrame(0.1f, new Vector3(0.05f, 0.05f, 0.05f));
                scaleAnimation.InsertKeyFrame(1f, new Vector3(1f, 1f, 0f));
                scaleAnimation.Duration = TimeSpan.FromMilliseconds(1000);
                scaleAnimation.DelayTime = TimeSpan.FromMilliseconds(relativeItemIndex * 100);



                KeyFrameAnimation fadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
                fadeAnimation.InsertExpressionKeyFrame(1f, "1");
                fadeAnimation.Duration = TimeSpan.FromMilliseconds(500);
                fadeAnimation.DelayTime = TimeSpan.FromMilliseconds(relativeItemIndex * 100);

                // Start animations 
                itemVisual.StartAnimation("Offset.Y", offsetAnimation);
                itemVisual.StartAnimation("Scale", scaleAnimation);
                itemVisual.StartAnimation("Opacity", fadeAnimation);
             }
            itemContainer.Loaded -= ItemContainer_Loaded;
        }

    }
}
