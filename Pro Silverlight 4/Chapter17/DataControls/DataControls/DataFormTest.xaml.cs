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
using System.ComponentModel.DataAnnotations;

namespace DataControls
{
    public partial class DataFormTest : UserControl
    {
        public DataFormTest()
        {
            InitializeComponent();
        }

        Product product = new Product("AEFS100", "Portable Defibrillator", 77.99,
                "Analyzes the electrical activity of a person's heart and applies an electric shock if necessary.");
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            List<Product> products = new List<Product>();
            products.Add(product);
            products.Add(product);            
            //formProductDetails.CurrentItem = product;
            formProductDetails.ItemsSource = products;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ValidationContext context =  new ValidationContext(product,null,null);
            ICollection<ValidationResult> r = new List<ValidationResult>();
            Validator.TryValidateObject(product, context, r);
        }
    }
}
