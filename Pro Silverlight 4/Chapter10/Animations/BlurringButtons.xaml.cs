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
    public partial class BlurringButtons : UserControl
    {
        public BlurringButtons()
        {
            InitializeComponent();
        }

        private void cmd_MouseEnter(object sender, MouseEventArgs e)
        {
            blurStoryboard.Stop();
            Storyboard.SetTarget(blurStoryboard, ((Button)sender).Effect);
            blurStoryboard.Begin();            
        }

        private void cmd_MouseLeave(object sender, MouseEventArgs e)
        {
            unblurStoryboard.Stop();
            Storyboard.SetTarget(unblurStoryboard, ((Button)sender).Effect);
            unblurStoryboard.Begin();
        }
    }
}
