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

namespace ElevatedTrust
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser)
            {
                // Check for updates.
                Application.Current.CheckAndDownloadUpdateCompleted += Application_CheckAndDownloadUpdateCompleted;
                Application.Current.CheckAndDownloadUpdateAsync();
            }

            this.RootVisual = new MenuPage();

            // Use this line to show the custom window.
            // Make sure you first change the window state to "No Border" or "Borderless Round Corners" in the Out-of-Browser Settings window.
            //this.RootVisual = new CustomWindow();
        }
        private void Application_CheckAndDownloadUpdateCompleted(object sender, CheckAndDownloadUpdateCompletedEventArgs e)
        {
            if (e.UpdateAvailable)
            {
                MessageBox.Show("A new version has been installed. Please restart the application.");
                Application.Current.MainWindow.Close();                
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
                
        private void Application_Exit(object sender, EventArgs e)
        {

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
