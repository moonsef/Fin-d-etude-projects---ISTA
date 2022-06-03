using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd
{
    class DataAccess
    {
        static string connectionString = "Data source=DESKTOP-MMR96DC;Initial Catalog=db;Integrated Security=True;";

        public static DataTable getData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            cmd.Connection = con;
            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }

        public static void setData(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(connectionString);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
