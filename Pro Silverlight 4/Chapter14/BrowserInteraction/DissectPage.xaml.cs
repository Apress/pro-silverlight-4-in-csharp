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
using System.Windows.Browser;

namespace BrowserInteraction
{
    public partial class DissectPage : UserControl
    {
        public DissectPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Start processing the top-level <html> element.
            HtmlElement element = HtmlPage.Document.DocumentElement;
            ProcessElement(element, 0);
        }

        private void ProcessElement(HtmlElement element, int indent)
        {
            // Ignore comments.
            if (element.TagName == "!") return;
            
            // Indent the element to help show different levels of nesting.
            lblElementTree.Text += new String(' ', indent * 4);

            // Display the tag name.
            lblElementTree.Text += "<" + element.TagName;
            
            // Only show the id attribute if it's set.
            if (element.Id != "") lblElementTree.Text += " id=\"" + element.Id + "\"";
            lblElementTree.Text += ">\n";

            // Process all the elements nested inside the current element.
            foreach (HtmlElement childElement in element.Children)
            {
                ProcessElement(childElement, indent + 1);
            }
        }
    }
}
