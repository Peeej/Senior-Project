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

        }

        private void saveInventoryItem()
        {
            string sql = "Update inventory set productName = @prodname, sotckQuantity = @invQuantity, productSKU = @prodSku";

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.Add(new SqlParameter("@prodName", name));
            cmd.Parameters.Add(new SqlParameter("@invQuantity", inventoryQuantity));
            cmd.Parameters.Add(new SqlParameter("@prodSku", productSku));

            using (sqlConnection)
            {
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }


        }
    }
}