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
    public partial class SaveFile : UserControl
    {
        public SaveFile()
        {
            InitializeComponent();            
        }

        private void cmdSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text Files (*.txt)|*.txt";
            saveDialog.DefaultExt = "txt";

            if (saveDialog.ShowDialog() == true)
            {
                using (Stream stream = saveDialog.OpenFile())
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(txtData.Text);
                    }
                }
                lblFileName.Text = "Saved " + saveDialog.SafeFileName;
            }
        }
    }
}
