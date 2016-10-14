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

namespace Styles
{
    public partial class DynamicStyles : UserControl
    {
        public DynamicStyles()
        {
            InitializeComponent();
        }

        private void chkAlternate_Click(object sender, RoutedEventArgs e)
        {
            if (chkAlternate.IsChecked == true)
            {
                ResourceDictionary resourceDictionary = new ResourceDictionary();
                resourceDictionary.Source = new Uri("/Styles/AlternateStyles.xaml",
                  UriKind.Relative);
                Style newStyle = (Style)resourceDictionary["BigButtonStyle"];
                cmd1.Style = newStyle;
            }
            else
            {
                cmd1.Style = (Style)this.Resources["BigButtonStyle"];
            }
            cmd2.Style = cmd1.Style;
        }
    }
}
