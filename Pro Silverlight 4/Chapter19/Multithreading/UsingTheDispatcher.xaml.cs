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
    public partial class UsingTheDispatcher : UserControl
    {
        public UsingTheDispatcher()
        {
            InitializeComponent();
        }
        private void cmdBreakRules_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(UpdateTextWrong);
            thread.Start();
        }

        private void UpdateTextWrong()
        {
            // Simulate some work taking place with a five-second delay.
            Thread.Sleep(TimeSpan.FromSeconds(5));

            txt.Text = "Here is some new text.";
        }

        private void cmdFollowRules_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(UpdateTextRight);
            thread.Start();
        }

        private void UpdateTextRight()
        {
            // Simulate some work taking place with a five-second delay.
            Thread.Sleep(TimeSpan.FromSeconds(5));

            // Get the dispatcher from the current page, and use it to invoke
            // the update code.
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {
                txt.Text = "Here is some new text.";
            }
              );
        }


    }
}
