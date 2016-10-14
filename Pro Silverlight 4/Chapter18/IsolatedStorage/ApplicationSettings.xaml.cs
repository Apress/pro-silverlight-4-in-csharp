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

namespace IsolatedStorage
{
    public partial class ApplicationSettings : UserControl
    {
        public ApplicationSettings()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings["LastRunDate"] = DateTime.Now;
            lblData.Text = "Saved.";
        }

        private void cmdRead_Click(object sender, RoutedEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LastRunDate"))
            {
                DateTime date = (DateTime)IsolatedStorageSettings.ApplicationSettings["LastRunDate"];
                lblData.Text = date.ToShortTimeString();
            }
        }
    }
}
