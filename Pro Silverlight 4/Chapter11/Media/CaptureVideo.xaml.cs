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

namespace Media
{
    public partial class CaptureVideo : UserControl
    {
        public CaptureVideo()
        {
            InitializeComponent();
        }

        private CaptureSource capture = new CaptureSource();

        private void cmdStartCapture_Click(object sender, RoutedEventArgs e)
        {
            if (CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess())
            {
                // Permission has been granted.
                                
                // It's always safe to call this Stop(), even if no capture is running.
                // However, calling Start() while a capture is already running causes an error.
                capture.Stop();

                // Get the default webcam.                
                capture.VideoCaptureDevice = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
                if (capture.VideoCaptureDevice == null)
                {
                    MessageBox.Show("Your computer does not have a video capture device.");
                }
                else
                {                    
                    // Start a new capture.
                    capture.Start();

                    // Map the live video to a VideoBrush.
                    VideoBrush videoBrush = new VideoBrush();
                    videoBrush.Stretch = Stretch.Uniform;
                    videoBrush.SetSource(capture);

                    // Use the VideoBrush to paint the fill of a Rectangle.
                    rectWebcamDisplay.Fill = videoBrush;                    
                }
            }
        }

        private void cmdStopCapture_Click(object sender, RoutedEventArgs e)
        {
            capture.Stop();
        }

        private void cmdSnapshot_Click(object sender, RoutedEventArgs e)
        {
            if (capture.State == CaptureState.Started)
            {
                capture.CaptureImageCompleted += capture_CaptureImageCompleted;
                capture.CaptureImageAsync();
            }
        }

        private void capture_CaptureImageCompleted(object sender, CaptureImageCompletedEventArgs e)
        {
            imgSnapshot.Source = e.Result;
        }
    }
}
