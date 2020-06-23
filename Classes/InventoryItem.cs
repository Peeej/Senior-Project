using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SeniorProjectWebsite.Classes
{
    public class InventoryItem
    {
        public string name;
        public int inventoryId;
        public int inventoryQuantity;
        public string productSku;

        public InventoryItem(string name, int invId, int invQuantity, string sku)
        {
            this.name = name;
            inventoryId = invId;
            inventoryQuantity = invQuantity;
            productSku = sku;

        }

        public void updateItem()
        {
            string sql = "Update inventory set productName = @prodname, sotckQuantity = @invQuantity, productSKU = @prodSku where inventoryId = @inventoryid";

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.Add(new SqlParameter("@inventoryid", inventoryId));
            cmd.Parameters.Add(new SqlParameter("@prodName", name));
            cmd.Parameters.Add(new SqlParameter("@invQuantity", inventoryQuantity));
            cmd.Parameters.Add(new SqlParameter("@prodSku", productSku));

            using (sqlConnection)
            {
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private int saveInventoryItem()
        {
            string sql = "Insert into inventory Select @prodName, @invQuantity, @prodSku Select Scope_Identity()";

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.Add(new SqlParameter("@prodName", name));
            cmd.Parameters.Add(new SqlParameter("@invQuantity", inventoryQuantity));
            cmd.Parameters.Add(new SqlParameter("@prodSku", productSku));

            int newId;
            using (sqlConnection)
            {
                sqlConnection.Open();
                newId = int.Parse(cmd.ExecuteScalar().ToString());
            }

            inventoryId = newId;
            return newId;
        }
    }
}