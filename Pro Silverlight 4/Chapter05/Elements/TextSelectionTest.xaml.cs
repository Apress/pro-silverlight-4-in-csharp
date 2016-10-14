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

namespace Elements
{
    public partial class TextSelectionTest : UserControl
    {
        public TextSelectionTest()
        {
            InitializeComponent();
        }

        private void txt_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (txtSelection == null) return;
            txtSelection.Text = String.Format(
                "Selection from {0} to {1} is \"{2}\"",
                txt.SelectionStart, txt.SelectionLength, txt.SelectedText);
        }
    }
}
