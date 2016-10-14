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

//[ServiceContract(Namespace = "")]
//[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
//public class StoreDb
//{
//    private string connectionString = WebConfigurationManager.AppSettings["storeDbConnectionString"];

//    [OperationContract()]
//    public Product GetProduct(int ID)
//    {
//        SqlConnection con = new SqlConnection(connectionString);
//        SqlCommand cmd = new SqlCommand("GetProductByID", con);
//        cmd.CommandType = CommandType.StoredProcedure;
//        cmd.Parameters.AddWithValue("@ProductID", ID);

//        try
//        {
//            con.Open();
//            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
//            if (reader.Read())
//            {
//                // Create a Product object that wraps the 
//                // current record.
//                Product product = new Product((string)reader["ModelNumber"],
//                    (string)reader["ModelName"], Convert.ToDouble(reader["UnitCost"]),
//                    (string)reader["Description"],
//                    (string)reader["ProductImage"]);
//                return product;
//            }
//            else
//            {
//                return null;
//            }
//        }
//        finally
//        {
//            con.Close();
//        }
//    }

//    [OperationContract()]
//    public List<Product> GetProducts()
//    {
//        SqlConnection con = new SqlConnection(connectionString);
//        SqlCommand cmd = new SqlCommand("GetProducts", con);
//        cmd.CommandType = CommandType.StoredProcedure;

//        List<Product> products = new List<Product>();
//        try
//        {
//            con.Open();
//            SqlDataReader reader = cmd.ExecuteReader();
//            while (reader.Read())
//            {
//                // Create a Product object that wraps the
//                // current record.
//                Product product = new Product((string)reader["ModelNumber"],
//                  (string)reader["ModelName"], Convert.ToDouble(reader["UnitCost"]),
//                  (string)reader["Description"], (string)reader["CategoryName"],
//                  (string)reader["ProductImage"]);

//                // Add to collection
//                products.Add(product);
//            }
//        }
//        finally
//        {
//            con.Close();
//        }
//        return products;
//    }

//    [OperationContract()]
//    public List<Category> GetCategoriesWithProducts()
//    {
//        SqlConnection con = new SqlConnection(connectionString);
//        SqlCommand cmd = new SqlCommand("GetProducts", con);
//        cmd.CommandType = CommandType.StoredProcedure;
//        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

//        DataSet ds = new DataSet();
//        adapter.Fill(ds, "Products");
//        cmd.CommandText = "GetCategories";
//        adapter.Fill(ds, "Categories");

//        // Set up a relation between these tables (optional).
//        DataRelation relCategoryProduct = new DataRelation("CategoryProduct",
//          ds.Tables["Categories"].Columns["CategoryID"],
//          ds.Tables["Products"].Columns["CategoryID"]);
//        ds.Relations.Add(relCategoryProduct);

//        List<Category> categories = new List<Category>();
//        foreach (DataRow categoryRow in ds.Tables["Categories"].Rows)
//        {
//            List<Product> products = new List<Product>();
//            foreach (DataRow productRow in categoryRow.GetChildRows(relCategoryProduct))
//            {
//                products.Add(new Product(productRow["ModelNumber"].ToString(),
//                    productRow["ModelName"].ToString(), Convert.ToDouble(productRow["UnitCost"]),
//                    productRow["Description"].ToString()));
//            }
//            categories.Add(new Category(categoryRow["CategoryName"].ToString(), products));
//        }
//        return categories;
//    }
//}
