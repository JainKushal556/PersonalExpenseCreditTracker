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
        // function that retrive datatables after filtering by any single parameter (e,g. status id , paymenttype id , person id)
        // spName - StroeProcedure Name
        // paramName - its is the parameter name that is used in store procedure in data base 
        //   |
        //   ---------------------------------------------------------
        //                                                           |
        //                                                          \ /
        //                                                           .
        // sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@StatusID", paramId);
        // paramId is the value of the parameter 
        public static DataTable retriveDataByUserIdAndFilterIdAtDal(string spName, int userId, string paramName, int paramId)
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
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName, paramId);
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
        // takes intiger as pararm value
        // function that retrive datatables after filtering by any double parameter (e,g. { Start Date , Ending Date } , { Min Amount , Max Amount } )
        // spName - StroeProcedure Name 
        // As We Have Two Parameters so we denoted as { paramName1 , paramId1 } , { paramName2 , paramId2 }
        // paramName - its is the parameter name that is used in store procedure in data base 
        //   |
        //   ---------------------------------------------------------
        //                                                           |
        //                                                          \ /
        //                                                           .
        // sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@StatusID", paramId);
        // paramId is the value of the parameter 
        public static DataTable retriveDataByUserIdAndFilterIdAtDal(string spName, int userId, string paramName1, Decimal paramId1, string paramName2, Decimal paramId2)
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
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName1, paramId1);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName2, paramId2);
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

        // takes intiger as pararm value
        // function that retrive datatables after filtering by any double parameter (e,g. { Start Date , Ending Date } , { Min Amount , Max Amount } )
        // spName - StroeProcedure Name 
        // As We Have Two Parameters so we denoted as { paramName1 , paramId1 } , { paramName2 , paramId2 }
        // paramName - its is the parameter name that is used in store procedure in data base 
        //   |
        //   ---------------------------------------------------------
        //                                                           |
        //                                                          \ /
        //                                                           .
        // sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@StatusID", paramId);
        // paramId is the value of the parameter 
        public static DataTable retriveDataByUserIdAndFilterIdAtDal(string spName, int userId, string paramName1, int paramId1, string paramName2, int paramId2)
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
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName1, paramId1);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName2, paramId2);
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

        // over loaded it take date time as paramvalue 
        // function that retrive datatables after filtering by any double parameter  (e,g. { Start Date , Ending Date } , { Min Amount , Max Amount } )
        // spName - StroeProcedure Name 
        // As We Have Two Parameters so we denoted as { paramName1 , paramId1 } , { paramName2 , paramId2 }
        // paramName - its is the parameter name that is used in store procedure in data base 
        //   |
        //   ---------------------------------------------------------
        //                                                           |
        //                                                          \ /
        //                                                           .
        // sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@StatusID", paramId);
        // paramId is the value of the parameter 
        public static DataTable retriveDataByUserIdAndFilterIdAtDal(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
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
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName1, paramId1);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName2, paramId2);
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


        // sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@StatusID", paramId);
        // paramId is the value of the parameter 
        public static DataTable retriveDataByAndFilterIdAtDal(string spName,string paramName, int paramId)
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
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName, paramId);
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


        public static DataTable GetErrorCategoryDataIntoCategoryDB(string spName, int userId, int paramId1, int paramId2, string paramName, string paramName1, string paramName2, string paramName3)
        {
            string connectionString = Common.SqlHelper.connectionString;
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(spName, sqlConnection))
                {
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName1, paramId1);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName2, paramId2);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(paramName3, paramName);
                    sqlDataAdapter.Fill(dataTable);

                    return dataTable;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
