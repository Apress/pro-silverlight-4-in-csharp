using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Collections.Generic;

/// <summary>
/// Summary description for Category
/// </summary>
[DataContract()]
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
