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
using System.ComponentModel;
using System.Text;

namespace Multithreading
{
    public partial class BackgroundWorkerTest : UserControl
    {
        private BackgroundWorker backgroundWorker = new BackgroundWorker();

        public BackgroundWorkerTest()
        {
            InitializeComponent();

            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
        }

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
            FindPrimesInput input = new FindPrimesInput(from, to);
            backgroundWorker.RunWorkerAsync(input);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the input values.
            FindPrimesInput input = (FindPrimesInput)e.Argument;

            // Start the search for primes and wait.
            int[] primes = FindPrimesWorker.FindPrimes(input.From, input.To, backgroundWorker);

            if (backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            // Return the result.
            e.Result = primes;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblStatus.Text = "Search cancelled.";
            }
            else if (e.Error != null)
            {
                // An error was thrown by the DoWork event handler.
                lblStatus.Text= e.Error.Message;
            }
            else
            {
                int[] primes = (int[])e.Result;
                lblStatus.Text = "Found " + primes.Length + " prime numbers.";
                lstPrimes.ItemsSource = primes;                
            }
            
            cmdFind.IsEnabled = true;
            cmdCancel.IsEnabled = false;
            progressBar.Width = 0;
        }

        private double maxWidth;
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            maxWidth = progressBarBackground.ActualWidth;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Width = (double)e.ProgressPercentage/100 * maxWidth;
            lblProgress.Text = ((double)e.ProgressPercentage/100).ToString("P0");
            
            if (e.UserState != null) lblStatus.Text = "Found prime: " + e.UserState.ToString() + "...";
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        
    }
}
