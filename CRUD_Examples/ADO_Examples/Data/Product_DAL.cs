using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ADO_Examples.Models;
using Microsoft.Data.SqlClient;
namespace ADO_Examples.Data
{
    public class Product_DAL
    {
        string conString = @"Data Source=DESKTOP-5BNTOH9\SQLEXPRESS;Initial Catalog=ADO_EXAMPLE;Integrated Security=SSPI;Encrypt=True;TrustServerCertificate=True";
        
        //Get All Products

        public List<Product> GetProducts()
        {
            List<Product> productsList = new List<Product>();
            
            using (SqlConnection connection = new SqlConnection(conString)) {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetProducts";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productsList.Add(new Product
                    {
                        //ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = Convert.ToString(dr["Remarks"])

                    
                    });

                    
                }

            }
            return productsList;
        }

        //Insert Product

        public bool InsertProduct(Product product)
        {
           int id = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);
                
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();

            }
            if(id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

    }
}
