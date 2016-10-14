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
    public partial class Notifications : UserControl
    {
        public Notifications()
        {
            InitializeComponent();
        }

        private NotificationWindow window = new NotificationWindow();
        private void cmdNotify_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser)
            {                
                CustomNotification notification = new CustomNotification();
                notification.Message = "You have just been notified. The time is " + DateTime.Now.ToLongTimeString() + ".";                                
                window.Content = notification;

                window.Close();
                window.Show(5000);
            }
            else
            {
                MessageBox.Show("This feature is not available while you are running in the browser.");
                // (Implement a different notification strategy here.)
            }
        }
    }
}
