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

namespace ResizableSilverlightControl
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Navigate(new Page2());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HtmlElement element = HtmlPage.Document.GetElementById("silverlightControl");
            element.SetStyleAttribute("width", this.Width + "px");
            element.SetStyleAttribute("height", this.Height + "px");
        }
    }
}
