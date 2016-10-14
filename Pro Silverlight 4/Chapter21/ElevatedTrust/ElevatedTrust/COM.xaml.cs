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
using System.Runtime.InteropServices.Automation;

namespace ElevatedTrust
{
    public partial class COM : UserControl
    {
        public COM()
        {
            InitializeComponent();
        }

        private void cmdTextToSpeech_Click(object sender, RoutedEventArgs e)
        {
            if (TestForComSupport())
            {
                using (dynamic speech = AutomationFactory.CreateObject("Sapi.SpVoice"))
                {
                    speech.Volume = 100;
                    speech.Speak("This is a test");
                }
            }            
        }

        private void cmdRunWord_Click(object sender, RoutedEventArgs e)
        {
            if (TestForComSupport())
            {
                using (dynamic word = AutomationFactory.CreateObject("Word.Application"))
                {
                    dynamic document = word.Documents.Add();

                    dynamic paragraph = document.Content.Paragraphs.Add;
                    paragraph.Range.Text = "Heading 1";
                    paragraph.Range.Font.Bold = true;
                    paragraph.Format.SpaceAfter = 18;
                    paragraph.Range.InsertParagraphAfter();

                    paragraph = document.Content.Paragraphs.Add;
                    paragraph.Range.Font.Bold = false;
                    paragraph.Range.Text = "This is some more text";
                    
                    word.Visible = true;
                }
            }
            
        }

        private void cmdReadAndWriteAnywhere_Click(object sender, RoutedEventArgs e)
        {
            if (TestForComSupport())
            {
                using (dynamic shell = AutomationFactory.CreateObject("WScript.Shell"))
                {
                    string desktopPath = shell.RegRead(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders\Desktop");

                    using (dynamic fso = AutomationFactory.CreateObject("Scripting.FileSystemObject"))
                    {
                        string filePath = System.IO.Path.Combine(desktopPath, "TestFile.txt");
                        dynamic file = fso.CreateTextFile(filePath, true);
                        file.WriteLine("An elevated trust Silverlight application can write anywhere that doesn't require adminsitrative privileges.");
                        file.Close();

                        file = fso.OpenTextFile(filePath, 1, true);
                        MessageBox.Show(file.ReadAll());
                        file.Close();
                    }
                }
            }
        }

        private void cmdRunProcess_Click(object sender, RoutedEventArgs e)
        {
            if (TestForComSupport())
            {
                using (dynamic shell = AutomationFactory.CreateObject("WScript.Shell"))
                {
                    shell.Run("calc.exe");
                }
            }
        }

        private void cmdReadRegistry_Click(object sender, RoutedEventArgs e)
        {
            if (TestForComSupport())
            {
                using (dynamic shell = AutomationFactory.CreateObject("WScript.Shell"))
                {
                    string desktopPath = shell.RegRead(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders\Desktop");                    
                    MessageBox.Show("The desktop files on this machine are placed in: " + desktopPath);
                }
            }
        }

        private bool TestForComSupport()
        {
            if (App.Current.InstallState != InstallState.Installed)
            {
                MessageBox.Show("This feature is not available because the application is not installed.");
            }
            else if (!App.Current.IsRunningOutOfBrowser)
            {
                MessageBox.Show("This feature is not available because you are running in the browser.");
            }
            else if (!App.Current.HasElevatedPermissions)
            {
                MessageBox.Show("This feature is not available because the application does not have elevated trust.");
            }
            else if (!AutomationFactory.IsAvailable) 
            {
                MessageBox.Show("This feature is not available because the operating system does not appear to support COM.");
            }
            else
            {
                return true;
            }
            return false;
        }
    }
}
