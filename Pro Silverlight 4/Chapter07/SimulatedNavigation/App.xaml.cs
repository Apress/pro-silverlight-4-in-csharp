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
using System.Reflection;

namespace SimulatedNavigation
{
    public enum Pages
    {
        Page, Page2
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

        // This Grid will host your pages.
        private Grid rootVisual = new Grid();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Load the first page.
            this.RootVisual = rootVisual;
            Navigate(Pages.Page);
        }

        public static void Navigate(Pages newPage)
        {
            // Get the current application object and cast it to
            // an instance of the custom (derived) App class.
            App currentApp = (App)Application.Current;

            // Check if the page has been created before.
            if (!pageCache.ContainsKey(newPage))
            {
                // Create the first instance of the page,
                // and cache it for future use.
                Type type = currentApp.GetType();
                Assembly assembly = type.Assembly;
                pageCache[newPage] = (UserControl)assembly.CreateInstance(
                    type.Namespace + "." + newPage.ToString());
            }
            
            // Change the currently displayed page.
            currentApp.rootVisual.Children.Clear();
            currentApp.rootVisual.Children.Add(pageCache[newPage]);
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {

        }

        private static Dictionary<Pages, UserControl> pageCache = new Dictionary<Pages,UserControl>();
    }
}
