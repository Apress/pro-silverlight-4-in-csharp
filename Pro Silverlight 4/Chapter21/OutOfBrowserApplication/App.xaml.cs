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
using System.IO.IsolatedStorage;
using System.IO;

namespace OutOfBrowserApplication
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            this.InstallStateChanged += this.Application_InstallStateChanged;
            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser)
            {
                // Check for updates.
                Application.Current.CheckAndDownloadUpdateCompleted += Application_CheckAndDownloadUpdateCompleted;
                Application.Current.CheckAndDownloadUpdateAsync();

                // Restore the window state.
                try
                {
                    IsolatedStorageFile store =
                      IsolatedStorageFile.GetUserStoreForApplication();

                    if (store.FileExists("window.Settings"))
                    {
                        using (IsolatedStorageFileStream fs = store.OpenFile("window.Settings", FileMode.Open))
                        {
                            BinaryReader r = new BinaryReader(fs);
                            Application.Current.MainWindow.Top = r.ReadDouble();
                            Application.Current.MainWindow.Left = r.ReadDouble();
                            Application.Current.MainWindow.Width = r.ReadDouble();
                            Application.Current.MainWindow.Height = r.ReadDouble();
                            r.Close();
                        }
                    }
                }
                catch (Exception err)
                {
                    // Can't restore the window details. No need to report the error.
                }

                // Show the full user interface.
                this.RootVisual = new MainPage();
            }            
            else
            {
                // Show a window with an installation message and an Install button.
                this.RootVisual = new InstallPage();
            }
        }

        private void Application_CheckAndDownloadUpdateCompleted(object sender, CheckAndDownloadUpdateCompletedEventArgs e)
        {
            if (e.UpdateAvailable)
            {
                MessageBox.Show("A new version has been installed. Please restart the application.");
                // (You could add code here to call a custom method in MainPage
                // to disable the user interface.)                
            }
            else if (e.Error != null)
            {
                if (e.Error is PlatformNotSupportedException)
                {
                    MessageBox.Show("An application update is available, " +
                      "but it requires a new version of Silverlight. " +
                      "Visit http://silverlight.net to upgrade.");
                }
                else
                {
                    MessageBox.Show("An application update is available, " +
                      "but it cannot be installed. Please remove the current version " +
                      "before installing the new version.");
                }
            } 
        }

        private void Application_InstallStateChanged(object sender, EventArgs e)
        {
            InstallPage page = this.RootVisual as InstallPage;
            if (page != null)
            {
                // Tell the root visual to show a message by calling a method
                // in InstallPage that updates the display.
                switch (this.InstallState)
                {
                    case InstallState.InstallFailed:
                        page.DisplayFailed();
                        break;
                    case InstallState.Installed:
                        page.DisplayInstalled();
                        break;
                }
            }
        }


        private void Application_Exit(object sender, EventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser)
            {
                // Store window state.            
                try
                {
                    IsolatedStorageFile store =
                      IsolatedStorageFile.GetUserStoreForApplication();

                    using (IsolatedStorageFileStream fs = store.CreateFile("window.Settings"))
                    {
                        BinaryWriter w = new BinaryWriter(fs);
                        w.Write(Application.Current.MainWindow.Top);
                        w.Write(Application.Current.MainWindow.Left);
                        w.Write(Application.Current.MainWindow.Width);
                        w.Write(Application.Current.MainWindow.Height);
                        w.Close();
                    }
                }
                catch (Exception err)
                {
                    // Can't save the window details. No need to report the error.
                }
            }
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
