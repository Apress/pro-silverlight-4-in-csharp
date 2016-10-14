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

namespace DynamicPositioningSilverlightControl
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
        }

        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HtmlElement target = HtmlPage.Document.GetElementById("target");
            target.AttachEvent("onmouseover", element_MouseOver);
        }

        private void element_MouseOver(object sender, HtmlEventArgs e)
        {
            HtmlElement target = HtmlPage.Document.GetElementById("target");
            double targetLeft = Convert.ToDouble(target.GetProperty("offsetLeft")) - 20;
            double targetTop = Convert.ToDouble(target.GetProperty("offsetTop")) - 20;

            HtmlElement silverlightControl = HtmlPage.Document.GetElementById("silverlightControlHost");            
            silverlightControl.SetStyleAttribute("left", targetLeft.ToString() + "px");
            silverlightControl.SetStyleAttribute("top", targetTop.ToString() + "px");
            silverlightControl.SetStyleAttribute("width", this.Width + "px");
            silverlightControl.SetStyleAttribute("height", this.Height + "px");

            fadeUp.Begin();

        }
                
        private void Page_MouseLeave(object sender, MouseEventArgs e)
        {
            HtmlElement silverlightControl = HtmlPage.Document.GetElementById("silverlightControlHost");
            silverlightControl.SetStyleAttribute("width", "0px");
            silverlightControl.SetStyleAttribute("height", "0px");
        }

    }
}
