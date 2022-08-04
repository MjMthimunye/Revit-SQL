using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SQLDoors
{
    public class SQLDBConnect
    {
        public static SqlConnection connect;

        public void ConnectDB()
        {
            //Connect to SQL database
            connect = new SqlConnection("SQL connection string");
            connect.Open();
        }

        /// <summary>
        /// Function to handle SQL command queries
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public SqlCommand Query(string sqlQuery)
        {
            SqlCommand command = new SqlCommand(sqlQuery, connect);
            return command;
        }


    }
}
