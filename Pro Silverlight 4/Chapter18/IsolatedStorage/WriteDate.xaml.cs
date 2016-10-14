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
using System.IO;

namespace IsolatedStorage
{
    public partial class WriteDate : UserControl
    {
        public WriteDate()
        {
            InitializeComponent();
        }

        private void cmdWrite_Click(object sender, RoutedEventArgs e)
        {
            // Write to isolated storage.
            try
            {
                IsolatedStorageFile store =
                  IsolatedStorageFile.GetUserStoreForApplication();
                
                using (IsolatedStorageFileStream fs = store.CreateFile("date.txt"))
                {
                    StreamWriter w = new StreamWriter(fs);
                    w.Write(DateTime.Now);
                    w.Close();
                }
                lblData.Text = "Data written to date.txt";
            }
            catch (Exception err)
            {
                lblData.Text = err.Message;
            }
        }

        private void cmdRead_Click(object sender, RoutedEventArgs e)
        {
            // Read from isolated storage.
            try
            {
                IsolatedStorageFile store =
                  IsolatedStorageFile.GetUserStoreForApplication();

                using (IsolatedStorageFileStream fs = store.OpenFile("date.txt", FileMode.Open))
                {
                    StreamReader r = new StreamReader(fs);
                    lblData.Text = r.ReadLine();
                    r.Close();
                }
            }
            catch (Exception err)
            {
                // An exception will occur if you attempt to open a file that doesn't exist.
                lblData.Text = err.Message;
            }
        }

        private void cmdTryIncrease_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile store =
                  IsolatedStorageFile.GetUserStoreForApplication();
            lblData.Text = "Current quota: " + store.Quota.ToString();
            if (store.IncreaseQuotaTo(store.Quota + 10))
            {
                lblData.Text += "\nIncreased to: " + store.Quota.ToString();
            }
            
        }

      
    }

}
