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

namespace ApplicationParameters
{
    public enum ViewMode
    {
        Customer, Employee
    }

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
            // Take the view mode setting, and store in an application property.
            if (e.InitParams.ContainsKey("viewMode"))
            {
                string view = e.InitParams["viewMode"];
                try
                {
                    this.viewMode = (ViewMode)Enum.Parse(typeof(ViewMode), view, true);
                }
                catch { }
            }

            // Create the root page.
            this.RootVisual = new Page();
        }

        private void Application_Exit(object sender, EventArgs e)
        {
            
        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            
        }

        private ViewMode viewMode = ViewMode.Customer;
        public ViewMode ViewMode
        {
            get { return viewMode; }            
        }
        
    }
}
