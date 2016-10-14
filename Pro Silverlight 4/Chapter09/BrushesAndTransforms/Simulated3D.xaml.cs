using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BrushesAndTransforms
{
    public partial class Simulated3D : UserControl
    {
        public Simulated3D()
        {
            InitializeComponent();
        }

        private void plusGlobalX_Click(object sender, RoutedEventArgs e)
        {
            projection.GlobalOffsetX++;
        }

        private void minusGlobalX_Click(object sender, RoutedEventArgs e)
        {
            projection.GlobalOffsetX--;
        }
        
        private void plusLocalX_Click(object sender, RoutedEventArgs e)
        {
            projection.LocalOffsetX++;
        }

        private void minusLocalX_Click(object sender, RoutedEventArgs e)
        {
            projection.LocalOffsetX--;
        }
    }
}
