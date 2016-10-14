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

namespace Animations
{
    public partial class Easing : UserControl
    {
        public Easing()
        {
            InitializeComponent();
        }

        private void cmdGrow_MouseEnter(object sender, MouseEventArgs e)
        {                 
            growStoryboard.Begin();            
        }

        private void cmdGrow_MouseLeave(object sender, MouseEventArgs e)
        {
            revertStoryboard.Begin();
        }
    }
}
