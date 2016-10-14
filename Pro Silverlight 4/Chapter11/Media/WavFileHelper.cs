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
    // From Mike Taulty:
    // http://mtaulty.com/CommunityServer/blogs/mike_taultys_blog/archive/2009/11/18/silverlight-4-rough-notes-camera-and-microphone-support.aspx

    public static class WavFileHelper
    {
        public static byte[] GetWavFileHeader(long audioLength,
          AudioFormat audioFormat)
        {
            // This code could use some constants...   
            MemoryStream stream = new MemoryStream(44);

            // "RIFF"   
            stream.Write(new byte[] { 0x52, 0x49, 0x46, 0x46 }, 0, 4);

            // Data length + 44 byte header length - 8 bytes occupied by first 2 fields   
            stream.Write(BitConverter.GetBytes((UInt32)(audioLength + 44 - 8)), 0, 4);

            // "WAVE"   
            stream.Write(new byte[] { 0x57, 0x41, 0x56, 0x45 }, 0, 4);

            // "fmt "   
            stream.Write(new byte[] { 0x66, 0x6D, 0x74, 0x20 }, 0, 4);

            // Magic # of PCM file - not sure about that one   
            stream.Write(BitConverter.GetBytes((UInt32)16), 0, 4);

            // 1 == Uncompressed   
            stream.Write(BitConverter.GetBytes((UInt16)1), 0, 2);

            // Channel count   
            stream.Write(BitConverter.GetBytes((UInt16)audioFormat.Channels), 0, 2);

            // Sample rate   
            stream.Write(BitConverter.GetBytes((UInt32)audioFormat.SamplesPerSecond), 0, 4);

            // Byte rate   
            stream.Write(BitConverter.GetBytes((UInt32)
                ((audioFormat.SamplesPerSecond *
                audioFormat.Channels * audioFormat.BitsPerSample) / 8)), 0, 4);

            // Block alignment   
            stream.Write(BitConverter.GetBytes((UInt16)
                ((audioFormat.Channels * audioFormat.BitsPerSample) / 8)), 0, 2);

            // Bits per sample   
            stream.Write(BitConverter.GetBytes((UInt16)audioFormat.BitsPerSample), 0, 2);

            // "data"   
            stream.Write(new byte[] { 0x64, 0x61, 0x74, 0x61 }, 0, 4);

            // Length of the rest of the file   
            stream.Write(BitConverter.GetBytes((UInt32)audioLength), 0, 4);

            return (stream.GetBuffer());
        }
    }  

}
