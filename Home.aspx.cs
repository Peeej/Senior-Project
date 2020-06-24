using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace SeniorProjectWebsite
{
    public partial class _Default : Page
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
            SqlCommand cmd = new SqlCommand("Select * from orders where price <> -1");
            grdOrders.DataSource = Classes.SQLHelper.ExecuteDataTable(cmd);
            grdOrders.DataBind();
        }
    }
}