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
    public partial class ControlAnimation : UserControl
    {
        public ControlAnimation()
        {
            InitializeComponent();
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            fadeStoryboard.Begin();
        }

        private void cmdPause_Click(object sender, RoutedEventArgs e)
        {
            fadeStoryboard.Pause();
        }

        private void cmdResume_Click(object sender, RoutedEventArgs e)
        {
            fadeStoryboard.Resume();
        }

        private void cmdStop_Click(object sender, RoutedEventArgs e)
        {
            fadeStoryboard.Stop();            
        }

        private void cmdMiddle_Click(object sender, RoutedEventArgs e)
        {
            fadeStoryboard.Begin();
            fadeStoryboard.Seek(TimeSpan.FromSeconds(fadeAnimation.Duration.TimeSpan.TotalSeconds/2));
        }

        private void sldSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Avoid problems when page is first parsed.
            if (sldSpeed == null) return;

            // This also restarts the animation if it's currently underway.
            fadeStoryboard.SpeedRatio = sldSpeed.Value;
            lblSpeed.Text = sldSpeed.Value.ToString("0.0");
        }
    }
}
