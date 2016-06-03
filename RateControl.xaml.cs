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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RejestracjaPacjenta.Controls
{
    public sealed partial class RateControl : UserControl
    {
        public RateControl()
        {
            this.InitializeComponent();
        }

        public int RateValue
        {
            get { return (int)GetValue(RateValueProperty); }
            set
            {
                int i = 0;
                SetValue(RateValueProperty, value);
                var childrens = FilledStarGrid.Children;
                                
                do
                {
                    if(i<value)
                        (childrens[i] as Grid).Visibility = Visibility.Visible;
                    else
                        (childrens[i] as Grid).Visibility = Visibility.Collapsed;
                    i++;
                }
                while (i<5);
            }
        }

        public static readonly DependencyProperty RateValueProperty =
            DependencyProperty.Register("RateValue", typeof(int), typeof(RateControl), new PropertyMetadata(0));
    }
}
