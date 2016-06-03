using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace RejestracjaPacjenta.Controls
{
    public sealed partial class SliderControl : UserControl
    {
        public event EventHandler ValueChanged;
        public bool SliderValue { get; set; }
        public SliderControl()
        {
            this.InitializeComponent();
        }

        public void Pointer_Entered(PointerPoint point)
        {
            WhiteText.Visibility = Visibility.Collapsed;
            
            if (point.Position.X >= 0 && point.Position.X <= RootGrid.ActualWidth - RootGrid.ActualHeight)
            {
                FillGrid.Width = point.Position.X;
                KnobGrid.Width = point.Position.X + 30;
            }
        }

        public void Pointer_Moved(PointerPoint point)
        {
            WhiteText.Visibility = Visibility.Collapsed;
            if (point.Position.X >= 0 && point.Position.X <= RootGrid.ActualWidth - RootGrid.ActualHeight)
            {
                
                FillGrid.Width = point.Position.X;
                KnobGrid.Width = point.Position.X + RootGrid.ActualHeight;
            }
        }

        
        public void Pointer_Exited(PointerPoint point)
        {
            if (point.Position.X < 0.5 * RootGrid.ActualWidth)
            {
                SliderValue = false;
                TransformMargin(KnobGrid, KnobGrid.ActualWidth, RootGrid.ActualHeight);
                TransformMargin(FillGrid, FillGrid.ActualWidth, 0);
            }
            else
            {
                SliderValue = true;
                TransformMargin(KnobGrid, KnobGrid.ActualWidth, RootGrid.ActualWidth);
                TransformMargin(FillGrid, FillGrid.ActualWidth, RootGrid.ActualWidth - RootGrid.ActualHeight);
            }
            if (ValueChanged != null)
            {
                ValueChanged(SliderValue, EventArgs.Empty);
            }
        }

        void TransformMargin(DependencyObject dObject, double fromX, double toX)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = fromX,
                To = toX,
                Duration = TimeSpan.FromSeconds(0.1)

            };
            widthAnimation.EnableDependentAnimation = true;
            Storyboard.SetTargetProperty(widthAnimation, "Grid.Width");
            Storyboard.SetTarget(widthAnimation, dObject);

            Storyboard s = new Storyboard();
            s.Children.Add(widthAnimation);
            s.Begin();
            s.Completed += S_Completed;
        }

        private void S_Completed(object sender, object e)
        {
            if (SliderValue) WhiteText.Visibility = Visibility;
        }
    }
    /* USAGE CS
    bool tapped;
        SliderControl SliderItem;
        private void SliderControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            tapped = true;
            SliderItem = sender as SliderControl;
            SliderItem.Pointer_Entered(e.GetCurrentPoint(SliderItem));
            ListViewRoot.IsHitTestVisible = false;
            FullDrugList.IsHitTestVisible = false;
        }

        private void SliderControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (tapped && SliderItem != null)
            {
                SliderItem.Pointer_Moved(e.GetCurrentPoint(SliderItem));
            }
        }

        private void Slider_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (tapped && SliderItem != null)
            {
                tapped = false;
                SliderItem.Pointer_Exited(e.GetCurrentPoint(SliderItem));
                ListViewRoot.IsHitTestVisible = true;
                FullDrugList.IsHitTestVisible = true;
            }
        }
    */
    /* USAGE XAML
    <Grid Grid.Row="1"
              Background="White"
              PointerExited="Slider_PointerExited"
              PointerMoved="SliderControl_PointerMoved">
            <uc:SliderControl Grid.ColumnSpan="3"
                              Margin="-50,5,5,5"
                              HorizontalAlignment="Right"
                              Background="White"
                              PointerEntered="SliderControl_PointerEntered" />
   </Grid<
    */
}
