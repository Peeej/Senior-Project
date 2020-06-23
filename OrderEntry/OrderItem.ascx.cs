using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace SeniorProjectWebsite.OrderEntry
{
    public partial class OrderItem : System.Web.UI.UserControl
    {
        public int orderItemId = -1;
        public int orderId = -1;
        public int inventoryItemId = -1;
        public int quantity = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { 
            loadDropdown();
            ddlItem.SelectedValue = inventoryItemId.ToString();
            txtQuantity.Text = quantity.ToString();
            }
        }

        private void loadDropdown()
        {
            SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);

            SqlCommand cmd = new SqlCommand("Select Concat(productName, ':', stockQuantity) as 'Name', inventoryId as 'InventoryID' from inventory where stockQuantity > 0 order by productName asc");
            cmd.Connection = sqlConnection;
            DataTable dt = Classes.SQLHelper.ExecuteDataTable(cmd);
            ddlItem.DataSource = dt;
            ddlItem.DataTextField = "Name";
            ddlItem.DataValueField = "InventoryID";
            ddlItem.DataBind();
            ddlItem.Items.Add(new ListItem("Select Item", "-1"));
            ddlItem.SelectedValue = "-1";

        }

        private void saveItem()
        {
            if (ddlItem.SelectedIndex > -1 | int.Parse(txtQuantity.Text) <= int.Parse(ddlItem.SelectedItem.Text.Split(':')[1].ToString())) { 
            SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand cmd = new SqlCommand("Delete from ordersProducts where orderProductId = @orderProductId Insert into ordersproducts Select @orderId, @productId, @quantity Select Scope_Identity() update inventory set quantity = qantity - @quantity where inventoryId = @productId");
            cmd.Connection = sqlConnection;
            cmd.Parameters.Add(new SqlParameter("@orderProductId", orderItemId));
            cmd.Parameters.Add(new SqlParameter("@orderId", orderId));
            cmd.Parameters.Add(new SqlParameter("@productId", inventoryItemId));
            cmd.Parameters.Add(new SqlParameter("@quantity", quantity));
                cmd.Connection.Open();
            orderItemId = int.Parse(cmd.ExecuteScalar().ToString());
                cmd.Connection.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveItem();
        }
    }
}