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

namespace Resources
{
    public partial class DownloadAssembly : UserControl
    {
        public DownloadAssembly()
        {
            InitializeComponent();
        }

        private void cmdDownload_Click(object sender, RoutedEventArgs e)
        {
            string uri = Application.Current.Host.Source.AbsoluteUri;
            int index = uri.IndexOf("/ClientBin");
            // In this example, the URI includes the /ClientBin portion, because we've
            // decided to place the DLL in the ClientBin folder.
            uri = uri.Substring(0, index) + "/ClientBin/ResourceClassLibrary2.dll";

            // Begin the download.
            WebClient webClient = new WebClient();
            webClient.OpenReadCompleted += webClient_OpenReadCompleted;
            webClient.OpenReadAsync(new Uri(uri));
        }

        private void webClient_OpenReadCompleted(object sender,
  OpenReadCompletedEventArgs e)
        {
            AssemblyPart assemblypart = new AssemblyPart();
            assemblypart.Load(e.Result);
            txt.Text = "Assembly downloaded. You can now use it.";
        }

        private void cmdUseAssembly_Click(object sender, RoutedEventArgs e)
        {
            // You can't catch errors here.
            // If the assembly was not loaded, look for a FileNotFoundException in Application.UnhandledException event.

                ResourceClassLibrary2.ClassLibraryUtil util = new ResourceClassLibrary2.ClassLibraryUtil();
                txt.Text = util.DoSomething();
        }

    }
}
