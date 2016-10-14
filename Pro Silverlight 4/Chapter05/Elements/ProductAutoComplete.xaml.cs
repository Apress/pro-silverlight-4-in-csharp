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
    public partial class ProductAutoComplete : UserControl
    {
        public ProductAutoComplete()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Product[] products = new []{new Product("Peanut Butter Applicator", "C_PBA-01"),
                new Product("Pelvic Strengthener", "C_PVS-309")};            
            acbProduct.ItemsSource = products;

            acbProduct.ItemFilter = ProductItemFilter;           
        }

        public bool ProductItemFilter(string text, object item)
        {
            Product product = (Product)item;

            // Call it a match if the typed-in text appears in the product code
            // or at the beginning of the product name.
            return ((product.ProductName.StartsWith(text)) ||
                    (product.ProductCode.Contains(text)));
        }

    }

    public class Product
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }

        public Product(string productName, string productCode)
        {
            ProductName = productName;
            ProductCode = productCode;
        }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
