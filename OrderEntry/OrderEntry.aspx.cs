using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace SeniorProjectWebsite.OrderEntry
{
    public partial class OrderEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindgrid();
                loadDropdown();
                ddlItem.SelectedValue = "-1";
                txtQuantity.Text = "0";
            }
        }

        private void bindgrid()
        {
            SqlCommand cmd = new SqlCommand("Select * from orders where price <> -1");
            orders.DataSource = Classes.SQLHelper.ExecuteDataTable(cmd);
            orders.DataBind();
        }
        private decimal getOrderPrice(int orid)
        {
            SqlCommand cmd = new SqlCommand("Select oi.quantity, i.price from ordersProducts oi inner join inventory i on oi.ProductId = i.inventoryId where oi.orderid = " + orid);
            System.Data.DataTable dt = Classes.SQLHelper.ExecuteDataTable(cmd);

            decimal price = 0;

            foreach(System.Data.DataRow dr in dt.Rows)
            {
                price += decimal.Parse(dr["quantity"].ToString()) * decimal.Parse(dr["price"].ToString());
            }
            lblPrice.Text = price.ToString();
            return price;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update orders set customername = @cn, deliveryAddress = @da, orderDate = @od, price = @price, orderActive = @oactive where orderid = @orderid");
            cmd.Parameters.Add(new SqlParameter("@cn", txtCustomer.Text));
            cmd.Parameters.Add(new SqlParameter("@da", txtAddress.Text));
            cmd.Parameters.Add(new SqlParameter("@od", System.DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@oactive", chkDelivered.Checked));
            cmd.Parameters.Add(new SqlParameter("@orderid", int.Parse(hforderid.Value.ToString())));
            decimal price = getOrderPrice(int.Parse(hforderid.Value.ToString()));
            cmd.Parameters.Add(new SqlParameter("@price", price));
            Classes.SQLHelper.ExecuteScalar(cmd);
            bindgrid();
            ctlOrder.Visible = false;
        }
        

        protected void newOrder_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from orders where price = -1 Insert into orders Select 'Enter Customer', 'Enter Address', GETDATE(), -1, 0,'Enter Tracking Number' Select Scope_Identity()");
            int orderId = int.Parse(SeniorProjectWebsite.Classes.SQLHelper.ExecuteScalar(cmd).ToString());
            hforderid.Value = orderId.ToString();
            ctlOrder.Visible = true;
        }
        protected void grdOrderItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select (Select productName from inventory where inventoryId = orderProductId), quantity from ordersItems where orderId = " + int.Parse(hforderid.Value.ToString()));
            grdOrderItems.DataSource = Classes.SQLHelper.ExecuteDataTable(cmd);
            grdOrderItems.DataBind();
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
            if (ddlItem.SelectedValue != "-1" | int.Parse(txtQuantity.Text) <= int.Parse(ddlItem.SelectedItem.Text.Split(':')[1].ToString()))
            {
                SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
                SqlCommand cmd = new SqlCommand("Insert into ordersproducts Select @orderId, @productId, @quantity Select Scope_Identity() update inventory set StockQuantity = StockQuantity - @quantity where inventoryId = @productId");
                cmd.Parameters.Add(new SqlParameter("@orderId", hforderid.Value.ToString()));
                cmd.Parameters.Add(new SqlParameter("@productId", ddlItem.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@quantity", txtQuantity.Text));

                Classes.SQLHelper.ExecuteScalar(cmd);

                cmd = new SqlCommand("Select * from ordersProducts where orderId = " + hforderid.Value.ToString());
                grdOrderItems.DataSource = Classes.SQLHelper.ExecuteDataTable(cmd);
                grdOrderItems.DataBind();
            }
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            saveItem();
            loadDropdown();
        }

        protected void orders_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from orders where price <> -1 and orderid = " + orders.SelectedDataKey.ToString());
            DataTable dt = Classes.SQLHelper.ExecuteDataTable(cmd);
            hforderid.Value = orders.SelectedDataKey.ToString();
            txtCustomer.Text = dt.Rows[0]["customername"].ToString();
            txtAddress.Text = dt.Rows[0]["deliveryAddress"].ToString();
            txtTrackNum.Text = dt.Rows[0]["trackingNumber"].ToString();
            chkDelivered.Checked = bool.Parse(dt.Rows[0]["orderActive"].ToString());


        }
    }
}