using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace E_CommerceApi.Helpers
{
    public class DataAccess
    {
        // 
        public static string connectionString = "Data source=.;Initial Catalog=db;Integrated Security=True;";


        public static void SetData(SqlCommand cmd, out string message)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                message = string.Empty;

            }catch(Exception ex)
            {
                message = ex.Message;
            }
        }

        public static DataTable GetData(SqlCommand cmd, out string message)
        {
            try
            {
                var dt = new DataTable();
                SqlConnection con = new SqlConnection(connectionString);
                cmd.Connection = con;
                con.Open();
                dt.Load(cmd.ExecuteReader());
                con.Close();

                message = string.Empty;
                return dt;
            }
            catch(Exception ex)
            {
                message = ex.Message; return null;
            }
        }
    }
}
