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
using System.IO;

namespace ElevatedTrust
{
    public partial class FileAccess : UserControl
    {
        public FileAccess()
        {
            InitializeComponent();
        }

        private void cmdWrite_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.HasElevatedPermissions)
            {
                string documentPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments);
                string fileName = System.IO.Path.Combine(documentPath, "TestFile.txt");
                
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    StreamWriter writer = new StreamWriter(fs);
                    writer.Write("This is a test with FileStream.");
                    writer.Close();
                }
                //Or:
                //File.WriteAllText(file, "This is a test.");
                lblResults.Text = "";
                MessageBox.Show("Finished successfully.");
            }
            else
            {
                MessageBox.Show("Not allowed.");
            }
        }

        private void cmdRead_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.HasElevatedPermissions)
            {
                string documentPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments);
                string fileName = System.IO.Path.Combine(documentPath, "TestFile.txt");
                
                if (File.Exists(fileName))
                {
                    string contents = File.ReadAllText(fileName);
                    lblResults.Text = contents;
                }
                else
                {
                    MessageBox.Show("No file.");
                }
            }
            else
            {
                MessageBox.Show("Not allowed.");
            }
        }
    }
}
