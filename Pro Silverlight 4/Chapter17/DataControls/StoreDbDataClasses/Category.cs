using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace StoreDbDataClasses
{
    [DataContract(Name = "Category",
        Namespace = "http://www.prosetech.com/StoreDb/Category")]
    public class Category : INotifyPropertyChanged
    {
        private string categoryName;
        [DataMember()]
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CategoryName"));
            }
        }

        private List<Product> products;
        [DataMember()]
        public List<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Products"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public Category(string categoryName, List<Product> products)
        {
            CategoryName = categoryName;
            Products = products;
        }

        public Category() { }
    }

}
