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
using System.Reflection;
using System.IO;
using System.Windows.Resources;

namespace Elements
{
    public partial class EmbeddedFont : UserControl
    {
        public EmbeddedFont()
        {
            InitializeComponent();

            //StreamResourceInfo sri = Application.GetResourceStream(
            //    new Uri("Elements;component/Bayern.ttf", UriKind.Relative));

            //lbl.FontSource = new FontSource(sri.Stream);
            //lbl.FontFamily = new FontFamily("Bayern");
        }

    }
}
