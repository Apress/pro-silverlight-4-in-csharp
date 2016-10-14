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
using StoreDbDataClasses;

namespace DataControls
{
    public partial class Validation : UserControl
    {
        public Validation()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Product product = new Product("AEFS100", "Portable Defibrillator", 77.99,
                "Analyzes the electrical activity of a person's heart and applies an electric shock if necessary.");
            gridProductDetails.DataContext = product;            
        }
    }
}
