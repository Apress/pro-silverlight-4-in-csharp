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

namespace Elements
{
    public partial class RichTextEditor : UserControl
    {
        public RichTextEditor()
        {
            InitializeComponent();
        }

        private void cmdBold_Click(object sender, RoutedEventArgs e)
        {            
            TextSelection selection = richTextBox.Selection;

            // You could check for no selection, but here the code simply applies the bold formatting
            // to the empty selection (the insertion point), so that when the user starts typing the
            // new text will be bold.

            // GetPropertyValue() returns null if the selection has mixed font bolding.
            // It's up to you what to do in this case, but here the application simply bolds
            // the entire selection.
            FontWeight currentState = FontWeights.Normal;
            if (selection.GetPropertyValue(Run.FontWeightProperty) != DependencyProperty.UnsetValue)
                currentState = (FontWeight)selection.GetPropertyValue(Run.FontWeightProperty);

            if (currentState == FontWeights.Normal)
            {
                selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
            }

            // A nice detail is to bring the focus back to the text box, so the user can resume typing.
            richTextBox.Focus();
        }

        private void cmdItalic_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox.Selection;
            
            FontStyle currentState = FontStyles.Normal;
            if (selection.GetPropertyValue(Run.FontStyleProperty) != DependencyProperty.UnsetValue)
                currentState = (FontStyle)selection.GetPropertyValue(Run.FontStyleProperty);

            if (currentState == FontStyles.Italic)
            {
                selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
            }
                        
            richTextBox.Focus();
        }

        private void cmdUnderline_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox.Selection;
            
            TextDecorationCollection currentState = null;
            if (selection.GetPropertyValue(Run.TextDecorationsProperty) != DependencyProperty.UnsetValue)
                currentState = (TextDecorationCollection)selection.GetPropertyValue(Run.TextDecorationsProperty);

            if (currentState != TextDecorations.Underline)
            {
                selection.ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                selection.ApplyPropertyValue(Run.TextDecorationsProperty, null);
            }

            richTextBox.Focus();
        }

        private void cmdShowXAML_Click(object sender, RoutedEventArgs e)
        {     
            txtFlowDocumentMarkup.Text = richTextBox.Xaml;            
        }
        
        private void cmdOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XAML Files (*.xaml)|*.xaml";

            string content = "";
            if (openFile.ShowDialog() == true)
            {
                using (StreamReader reader = openFile.File.OpenText())
                {
                    content = reader.ReadToEnd();
                }

                richTextBox.Xaml = content;
                txtFlowDocumentMarkup.Text = "";
            }            
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XAML Files (*.xaml)|*.xaml";

            if (saveFile.ShowDialog() == true)
            {
                using (FileStream fs = (FileStream)saveFile.OpenFile())
                {
                    System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                    byte[] buffer = enc.GetBytes(richTextBox.Xaml);
                    fs.Write(buffer, 0, buffer.Length);                    
                }
            }
        }

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Blocks.Clear();
            txtFlowDocumentMarkup.Text = "";
        }
        
    }
}
