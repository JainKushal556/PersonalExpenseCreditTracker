using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace DALayer.Common
{
    public class SqlHelper
    {
        // Universal ConnectionString for All Store Procedures
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        // function that retrive any single list  (one column) of data for combo boxes .
        // spName - StroeProcedure Name
        // colName - Column Name
        public static List<string> retriveListForComboBoxAtDal(string spName, string colName , int userId)
        {
            SqlConnection sqlConnection = null;
            List<string> dataList = null;
            try
            {
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    // dataList stores the list of strings retrived from DB
                    dataList = new List<string>();
                    SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", userId);
                    // Opening Connection
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    // reads the column data one by one append into dataList
                    while (sqlDataReader.Read())
                    {
                        dataList.Add(sqlDataReader[colName].ToString());
                    }
                    // return dataLIst after getting all strings or column data from DB
                    return dataList;
                }
            }
            catch (Exception EX)
            {
                // return null assigned dataList if any error occur 
                return dataList;
            }
            finally
            {
                // close the connection string if error occur's or not 
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
