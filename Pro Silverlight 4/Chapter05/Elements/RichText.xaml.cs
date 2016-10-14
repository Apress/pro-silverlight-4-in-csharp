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

namespace Elements
{
    public partial class RichText : UserControl
    {
        public RichText()
        {
            InitializeComponent();
        }

        private void cmdGenerate_Click(object sender, RoutedEventArgs e)
        {
            // Create the first part of the sentence.
            Run runFirst = new Run();
            runFirst.Text = "Hello world of ";

            // Create bolded text.
            Bold bold = new Bold();
            Run runBold = new Run();
            runBold.Text = "dynamically generated";
            bold.Inlines.Add(runBold);

            // Create last part of sentence.
            Run runLast = new Run();
            runLast.Text = " documents";

            // Add three parts of sentence to a paragraph, in order.
            Paragraph paragraph = new Paragraph();            
            paragraph.Inlines.Add(runFirst);
            paragraph.Inlines.Add(bold);
            paragraph.Inlines.Add(runLast);

            // Add this paragraph to the RichTextBox.
            richText.Blocks.Clear();
            richText.Blocks.Add(paragraph);
        }
                
        private void chkReadOnly_Click(object sender, RoutedEventArgs e)
        {
            richText.IsReadOnly = chkReadOnly.IsChecked.Value;
        }
    }
}
