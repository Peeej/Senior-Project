using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SeniorProjectWebsite.Classes
{
    public static class SQLHelper
    {
        
        public static object ExecuteScalar(SqlCommand cmd)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            cmd.Connection = sqlConnection;
            using (sqlConnection)
            {
                sqlConnection.Open();
                return cmd.ExecuteScalar();
            }
        }

        public static DataTable ExecuteDataTable(SqlCommand cmd)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            cmd.Connection = sqlConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (sqlConnection)
            {
                sqlConnection.Open();
                adapter.Fill(dt);
            }

            return dt;
        }
    }
}