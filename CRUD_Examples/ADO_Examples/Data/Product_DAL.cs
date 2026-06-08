using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ADO_Examples.Models;

namespace ADO_Examples.Data
{
    public class Product_DAL
    {
        string conString = @"Data Source=DESKTOP-5BNTOH9\SQLEXPRESS;Initial Catalog=ADO_EXAMPLE;Integrated Security=SSPI;Encrypt=True;TrustServerCertificate=True";

        // =========================
        // GET ALL PRODUCTS
        // =========================
        public List<Product> GetProducts()
        {
            List<Product> list = new List<Product>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }

            return list;
        }

        // =========================
        // GET BY ID
        // =========================
        public List<Product> GetProductByID(int id)
        {
            List<Product> list = new List<Product>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetProductByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductID", id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }

            return list;
        }

        // =========================
        // CRUD EXECUTOR
        // =========================
        public bool ExecuteProduct(Product product, string action)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("sp_ProductCRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", action);
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Qty", product.Qty);
                cmd.Parameters.AddWithValue("@Remarks", product.Remarks ?? (object)DBNull.Value);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                return i > 0;
            }
        }
    }
}