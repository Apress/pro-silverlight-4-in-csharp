using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using System.Text;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class ProductAutoComplete
{
    [OperationContract]
    public string[] GetProductMatches(string inputText)
    {     
        // Get the products (for example, from a server-side database).
        Product[] products = GetProducts();

        // Create a collection of matches.
        List<string> productMatches = new List<string>();
        foreach (Product product in products)
        {
            // See if this is a match.
            if ((product.ProductName.StartsWith(inputText)) ||
                    (product.ProductCode.Contains(inputText)))
                productMatches.Add(product.ProductName);
        }

        return productMatches.ToArray();
    }

    private Product[] GetProducts()
    {
        return new[]{new Product("Peanut Butter Applicator", "C_PBA-01"),
                new Product("Pelvic Strengthener", "C_PVS-309")};            
    }
}

[DataContract]
public class Product
{
    [DataMember]
    public string ProductName { get; set; }
    [DataMember]
    public string ProductCode { get; set; }

    public Product(string productName, string productCode)
    {
        ProductName = productName;
        ProductCode = productCode;
    }

    public Product() { }

    public override string ToString()
    {
        return ProductName;
    }
}
