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

namespace Layout
{
    public partial class FullScreen : UserControl
    {
        public FullScreen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            Application.Current.Host.Content.IsFullScreen = true;
        }

        private void LayoutRoot_KeyDown(object sender, KeyEventArgs e)
        {
            lbl.Text = "Pressed " + e.Key + " at " + DateTime.Now.ToLongTimeString();
        }
    }
}
