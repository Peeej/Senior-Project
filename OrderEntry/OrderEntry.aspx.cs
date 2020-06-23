using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace SeniorProjectWebsite.OrderEntry
{
    public partial class OrderEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindgrid();
            }
        }

        private void bindgrid()
        {
            SqlCommand cmd = new SqlCommand("Select * from orders");
            orders.DataSource = Classes.SQLHelper.ExecuteDataTable(cmd);
            orders.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
        protected void grdOrderItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void grdOrderItems_DeleteItem(int id)
        {

        }
    }
}