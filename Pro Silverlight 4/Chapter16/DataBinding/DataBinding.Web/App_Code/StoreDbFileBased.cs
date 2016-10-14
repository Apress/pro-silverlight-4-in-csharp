using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class StoreDb
{
    private DataSet ds = new DataSet();

    public StoreDb()
    {        
        ds.ReadXmlSchema(HttpContext.Current.Server.MapPath("store.xsd"));
        ds.ReadXml(HttpContext.Current.Server.MapPath("store.xml"));     
    }

    [OperationContract()]
    public Product GetProduct(int ID)
    {
        DataRow productRow = ds.Tables["Products"].Select("ProductID = " + ID.ToString())[0];
        Product product = new Product((string)productRow["ModelNumber"],
                (string)productRow["ModelName"], Convert.ToDouble(productRow["UnitCost"]),
                (string)productRow["Description"], (string)productRow["CategoryName"],
                (string)productRow["ProductImage"]);
        return product;
    }

    [OperationContract()]
    public List<Product> GetProducts()
    {
        List<Product> products = new List<Product>();
        foreach (DataRow productRow in ds.Tables["Products"].Rows)
        {
            products.Add(new Product((string)productRow["ModelNumber"],
                (string)productRow["ModelName"], Convert.ToDouble(productRow["UnitCost"]),
                (string)productRow["Description"], (string)productRow["CategoryName"],
                (string)productRow["ProductImage"]));
        }
        return products;
    }

    [OperationContract()]
    public List<Category> GetCategoriesWithProducts()
    {        
        DataRelation relCategoryProduct = ds.Relations[0];

        List<Category> categories = new List<Category>();
        foreach (DataRow categoryRow in ds.Tables["Categories"].Rows)
        {
            List<Product> products = new List<Product>();
            foreach (DataRow productRow in categoryRow.GetChildRows(relCategoryProduct))
            {
                products.Add(new Product(productRow["ModelNumber"].ToString(),
                    productRow["ModelName"].ToString(), Convert.ToDouble(productRow["UnitCost"]),
                    productRow["Description"].ToString()));
            }
            categories.Add(new Category(categoryRow["CategoryName"].ToString(), products));
        }
        return categories;
    }
}
