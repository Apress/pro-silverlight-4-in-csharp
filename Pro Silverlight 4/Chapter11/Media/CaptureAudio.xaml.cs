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
using System.IO;

namespace Media
{
    public partial class CaptureAudio : UserControl
    {
        public CaptureAudio()
        {
            InitializeComponent();
        }

        private MemoryStreamAudioSink audioSink;

        // You must hold a separate reference to the CaptureSource to prevent
        // exceptions when calling CaptureSource.Stop() on long clips.
        private CaptureSource capture;

        private void cmdStartRecord_Click(object sender, RoutedEventArgs e)
        {
            if (CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess())
            {
                if (audioSink == null)
                {
                    capture = new CaptureSource();
                    capture.AudioCaptureDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();

                    audioSink = new MemoryStreamAudioSink();
                    audioSink.CaptureSource = capture;
                }
                else
                {
                    audioSink.CaptureSource.Stop();
                }

                audioSink.CaptureSource.Start();
                cmdStartRecord.IsEnabled = false;

                // Add a delay to make sure the recording is initialized.
                // (Otherwise, a user may cause an error by attempting to stop it immediately.)
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                cmdStopRecord.IsEnabled = true;
                
                lblStatus.Text = "Now recording ...";                
            }
        }

        private void cmdStopRecord_Click(object sender, RoutedEventArgs args)
        {
            audioSink.CaptureSource.Stop();                
            
            cmdPlayClip.IsEnabled = true;
            cmdStopRecord.IsEnabled = false;
            cmdStartRecord.IsEnabled = true;
            lblStatus.Text = "Finished recording. A clip is available to play.";
        }               

        private void cmdPlayClip_Click(object sender, RoutedEventArgs args)
        {                      
            // Play the audio.
            WaveMSS.WaveMediaStreamSource wavMss = new WaveMSS.WaveMediaStreamSource(audioSink.AudioData);
            media.SetSource(wavMss);
        }

        
    }
}
