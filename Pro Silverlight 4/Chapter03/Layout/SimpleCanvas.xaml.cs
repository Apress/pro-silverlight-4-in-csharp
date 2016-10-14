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

namespace Layout
{
    public partial class SimpleCanvas : UserControl
    {
        public SimpleCanvas()
        {
            InitializeComponent();            
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            ((SolidColorBrush)((Canvas)sender).Background).Color = Colors.Yellow;
        }
    }
}
