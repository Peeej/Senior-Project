using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SeniorProjectWebsite.Inventory
{
    public partial class InventoryManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            SqlCommand cmd = new SqlCommand("Select inventoryId as 'Inventory ID', productName as 'Name', stockQuantity as 'Quantity', productSKU as 'SKU', price from inventory order by productName asc");
            gvInventory.DataSource = Classes.SQLHelper.ExecuteDataTable(cmd);
            gvInventory.DataBind();
        }

        protected void gvInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            invItem.Visible = true;
            hfInventoryId.Value = gvInventory.SelectedRow.Cells[1].Text;
            txtName.Text = gvInventory.SelectedRow.Cells[2].Text;
            txtQuantity.Text = gvInventory.SelectedRow.Cells[3].Text;
            txtSku.Text = gvInventory.SelectedRow.Cells[4].Text;
            txtprice.Text = gvInventory.SelectedRow.Cells[5].Text;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            invItem.Visible = false;
            if (hfInventoryId.Value == "-1")
            {
                SqlCommand cmd = new SqlCommand("Insert into inventory select @name, @quantity, @productSku, @price");
                cmd.Parameters.Add(new SqlParameter("@name", txtName.Text));
                cmd.Parameters.Add(new SqlParameter("@quantity", txtQuantity.Text));
                cmd.Parameters.Add(new SqlParameter("@productSku", txtSku.Text));
                cmd.Parameters.Add(new SqlParameter("@price", txtprice.Text));

                Classes.SQLHelper.ExecuteScalar(cmd);

                BindGrid();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update inventory set productName = @name, stockQuantity = @quantity, productSku = @productSku, price = @price where inventoryId = @id");
                cmd.Parameters.Add(new SqlParameter("@id", hfInventoryId.Value));
                cmd.Parameters.Add(new SqlParameter("@name", txtName.Text));
                cmd.Parameters.Add(new SqlParameter("@quantity", txtQuantity.Text));
                cmd.Parameters.Add(new SqlParameter("@productSku", txtSku.Text));
                cmd.Parameters.Add(new SqlParameter("@price", txtprice.Text));

                Classes.SQLHelper.ExecuteScalar(cmd);

                BindGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            invItem.Visible = true;
            hfInventoryId.Value = "-1";
            txtName.Text = "Enter New Item's Name";
            txtQuantity.Text = "Enter new Quantity";
            txtSku.Text = "Enter New SKU";
            txtprice.Text = "Enter Price";

        }
    }
}