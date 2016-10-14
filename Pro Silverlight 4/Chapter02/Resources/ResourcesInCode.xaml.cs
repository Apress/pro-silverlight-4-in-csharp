using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Resources
{
    public partial class ResourcesInCode : UserControl
    {
        public ResourcesInCode()
        {
            InitializeComponent();
        }

        private void cmdChange_Click(object sender, RoutedEventArgs e)
        {
            LinearGradientBrush brush = (LinearGradientBrush)this.Resources["ButtonFace"];

            // Swap the color order.
            Color color = brush.GradientStops[0].Color;
            brush.GradientStops[0].Color = brush.GradientStops[2].Color;
            brush.GradientStops[2].Color = color;
        }

        private void cmdReplace_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.Yellow);
            this.Resources["ButtonFace"] = brush;
        }
    }
}
