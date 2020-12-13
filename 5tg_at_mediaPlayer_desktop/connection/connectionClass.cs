using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5tg_at_mediaPlayer_desktop.connection
{
    public class ConnectionClass
    {
        public SqlConnection con;
        public SqlCommand cmd;
        public DataSet ds;
        public SqlDataAdapter sda;
        int count = 0;

        public ConnectionClass()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            con = new SqlConnection(connectionString);
        }

        public void checkConnection()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            else if (con.State == ConnectionState.Closed)
                con.Open();
        }

        public int insertData(String quary)
        {
            count = 0;
            try
            {
                cmd = new SqlCommand(quary, con);
                checkConnection();
                count = cmd.ExecuteNonQuery();
                checkConnection();
            }
            catch (Exception ex)
            {
                Global_Log.EXC_WriteIn_LOGfile(ex.StackTrace);
            }
            return count;
        }

        public DataTable retriveData(string query, string tableName)
        {
            ds = new DataSet();
            sda = new SqlDataAdapter(query, con);
            sda.Fill(ds, tableName);
            return (ds.Tables[tableName]);
        }
    }
}
