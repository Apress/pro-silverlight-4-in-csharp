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
using System.Threading;

namespace Multithreading
{
    public partial class ThreadTest : UserControl
    {
        public ThreadTest()
        {
            InitializeComponent();
        }

        private FindPrimesThreadWrapper threadWrapper;
        private void cmdFind_Click(object sender, RoutedEventArgs e)
        {
            // Disable the button and clear previous results.
            cmdFind.IsEnabled = false;
            cmdCancel.IsEnabled = true;
            lstPrimes.ItemsSource = null;
            lblStatus.Text = "";

            // Get the search range.
            int from, to;
            if (!Int32.TryParse(txtFrom.Text, out from))
            {
                lblStatus.Text = "Invalid From value.";
                return;
            }
            if (!Int32.TryParse(txtTo.Text, out to))
            {
                lblStatus.Text = "Invalid To value.";
                return;
            }

            // Start the search for primes on another thread.
            threadWrapper = new FindPrimesThreadWrapper(from, to);
            threadWrapper.Completed += threadWrapper_Completed;
            threadWrapper.Cancelled +=threadWrapper_Cancelled;
            threadWrapper.Start();

            lblStatus.Text = "The search is in progress...";            
        }

        private void threadWrapper_Cancelled(object sender, EventArgs e)
        {
             this.Dispatcher.BeginInvoke(delegate() {
                lblStatus.Text = "Search cancelled.";
                cmdFind.IsEnabled = true;
                cmdCancel.IsEnabled = false;
             });
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            threadWrapper.RequestCancel();
        }

        private void threadWrapper_Completed(object sender, FindPrimesCompletedEventArgs e)
        {
            FindPrimesThreadWrapper thread = (FindPrimesThreadWrapper)sender;
            
            this.Dispatcher.BeginInvoke(delegate() {
                if (thread.Status == StatusState.Completed)
                {
                    int[] primes = e.PrimeList;
                    lblStatus.Text = "Found " + primes.Length + " prime numbers.";
                    lstPrimes.ItemsSource = primes;
                }

                cmdFind.IsEnabled = true;
                cmdCancel.IsEnabled = false;

                    }
            );
        }
                

      
    }

}

