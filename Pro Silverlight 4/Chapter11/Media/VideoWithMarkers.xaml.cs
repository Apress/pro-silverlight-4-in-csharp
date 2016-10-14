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
    public partial class VideoWithMarkers : UserControl
    {
        public VideoWithMarkers()
        {
            InitializeComponent();
        }

        private void media_MarkerReached(object sender, TimelineMarkerRoutedEventArgs e)
        {
            lblMarker.Text = e.Marker.Text + " at " + e.Marker.Time.TotalSeconds + " seconds";
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            // Use data binding.
            // The MarkerInfo wrapper is needed because you can't bind TimelineMarker objects directly.
            lstMarkers.DisplayMemberPath = "DisplayText";            
            foreach (TimelineMarker marker in media.Markers)
            {
                lstMarkers.Items.Add(new MarkerInfo(marker));
            }            
        }

        private void lstMarkers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MarkerInfo markerInfo = (MarkerInfo)lstMarkers.SelectedItem;      
            media.Position = markerInfo.Marker.Time;
            media.Play();
        }
    }

    public class MarkerInfo
    {
        public TimelineMarker Marker
        {
            get;
            set;
        }

        public string DisplayText
        {
            get
            {
                return Marker.Text + " (" + Marker.Time.Minutes + ":" + Marker.Time.Seconds + ":" + 
                    Marker.Time.Milliseconds + ")";
            }
        }

        public MarkerInfo(TimelineMarker marker)
        {
            Marker = marker;
        }
    }
}
