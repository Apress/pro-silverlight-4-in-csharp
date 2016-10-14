using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace Media
{
    public class MemoryStreamAudioSink : AudioSink
    {
        private MemoryStream stream;
        protected override void OnCaptureStarted()
        {
            // Prepare a new in-memory stream to store the captured audio.            
            stream = new MemoryStream();
        }               

        protected override void OnCaptureStopped()
        {
            // Genereate the header.
            byte[] wavFileHeader = WavFileHelper.GetWavFileHeader(AudioData.Length,
              AudioFormat);

            // Write the header to a new stream.
            MemoryStream wavStream = new MemoryStream();
            wavStream.Write(wavFileHeader, 0, wavFileHeader.Length);
                       
            // Write the rest of the data, one chunk of 4096 bytes at a time.
            byte[] buffer = new byte[4096];
            int read = 0;
            stream.Seek(0, SeekOrigin.Begin);
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                wavStream.Write(buffer, 0, read);
            }

            // Replace the raw stream with the new stream.
            stream = wavStream;
            stream.Seek(0, SeekOrigin.Begin);
        }

        private AudioFormat audioFormat;
        public AudioFormat AudioFormat
        {
            get
            {
                return audioFormat;
            }
        }

        public MemoryStream AudioData
        {
            get
            {
                return stream;
            }
        }

        protected override void OnFormatChange(AudioFormat audioFormat)
        {
            if (this.audioFormat == null)
            {
                this.audioFormat = audioFormat;
            }
            else
            {
                // Don't allow changes that could affect an existing capture.
                throw new InvalidOperationException();
            }
        }
                
        protected override void OnSamples(long sampleTime, long sampleDuration, byte[] sampleData)
        {
            // Each time a sample is received, write it to the in-memory stream.
            // (A more complex implementation might stream it over the network.)
            stream.Write(sampleData, 0, sampleData.Length);
        }        
        
    }  

}
