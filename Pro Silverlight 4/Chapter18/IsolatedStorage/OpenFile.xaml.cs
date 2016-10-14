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
using System.IO;

namespace IsolatedStorage
{
    public partial class OpenFile : UserControl
    {
        public OpenFile()
        {
            InitializeComponent();
        }

        private void cmdOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text Files (*.txt)|*.txt";
            if (openDialog.ShowDialog() == true)
            {                
                using (StreamReader reader = openDialog.File.OpenText())
                {
                    lblData.Text = reader.ReadToEnd();
                }
            }
        }
    }
}
