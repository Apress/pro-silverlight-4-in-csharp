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
    public partial class RotatingButtons : UserControl
    {
        public RotatingButtons()
        {
            InitializeComponent();
        }

        private void cmd_MouseEnter(object sender, MouseEventArgs e)
        {
            rotateStoryboard.Stop();
            Storyboard.SetTarget(rotateStoryboard, ((Button)sender).RenderTransform);
            rotateStoryboard.Begin();            
        }

        private void cmd_MouseLeave(object sender, MouseEventArgs e)
        {
            unrotateStoryboard.Stop();
            Storyboard.SetTarget(unrotateStoryboard, ((Button)sender).RenderTransform);
            unrotateStoryboard.Begin();
        }
    }
}
