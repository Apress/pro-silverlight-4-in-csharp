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

namespace Media
{
    public partial class MultipleSounds : UserControl
    {
        public MultipleSounds()
        {
            InitializeComponent();
        }

        private void cmdPlay_Click(object sender, RoutedEventArgs e)
        {
            MediaElement media = new MediaElement();
            media.Source = new Uri("test.mp3", UriKind.Relative);
            media.MediaEnded += new RoutedEventHandler(media_MediaEnded);
            MediaContainer.Children.Add(media);
            lblStatus.Text = MediaContainer.Children.Count + " media files playing.";
        }

        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaContainer.Children.Remove((MediaElement)sender);
            lblStatus.Text = MediaContainer.Children.Count + " media files playing.";
        }
    }
}
