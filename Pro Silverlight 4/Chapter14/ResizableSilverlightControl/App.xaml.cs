﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ResizableSilverlightControl
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

        private Grid rootVisual = new Grid();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Load the main control
            this.RootVisual = rootVisual;
            rootVisual.Children.Add(new Page());
        }

        public static void Navigate(UserControl newPage)
        {
            App currentApp = (App)Application.Current;
                        
            // Change the currently displayed page.
            currentApp.rootVisual.Children.Clear();
            currentApp.rootVisual.Children.Add(newPage);
        }


        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {

        }
    }
}
