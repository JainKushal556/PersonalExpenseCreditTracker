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
        public static DataTable retrieveDataTableBySpNameAndUserId(string spName,int userId)
        {
            DataTable dataTable = new DataTable();
          
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(spName, sqlConnection))
                {
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@UserID", userId);

                    sqlDataAdapter.Fill(dataTable);

                    return dataTable;
                }
            }
            catch
            {
                throw;
            }
        }
        // function that retrive any single list  (one column) of data for combo boxes .
        // spName - StroeProcedure Name
        // colName - Column Name
        public static DataTable retriveDataTableBySpName(string spName)
        {
            SqlConnection sqlConnection = null;
            DataTable dataTable = null;
            try
            {
                    sqlConnection = new SqlConnection(connectionString);
                    // datatable stores retrived data from DB
                    dataTable = new DataTable();

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(spName, sqlConnection))
                    {
                        sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDataAdapter.Fill(dataTable);
                        return dataTable;
                    }
            }
            catch (Exception ex)
            {
                // return null assigned dataList if any error occur 
                return dataTable;         
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

        public static DataTable retriveFilteredDataByStatusAtDal(string spName, int userId, int statusID)
        {
            SqlConnection sqlConnection = null;
            DataTable dataTable = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                dataTable = new DataTable();

                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(spName, sqlConnection))
                {
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@StatusID", statusID);
                    sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                return dataTable;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
