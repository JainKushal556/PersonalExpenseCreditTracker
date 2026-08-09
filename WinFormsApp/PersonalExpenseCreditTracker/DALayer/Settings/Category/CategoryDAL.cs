using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DALayer.Settings.Category
{
    public class CategoryDAL
    {
        public int UserId { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategory { get; set; }
        public int status;

        private Boolean ReturnBoolean(int value)
        {
            if (value > 0) return true;
            else return false;
        }

        public Boolean AddExpenseCategoryDataIntoCategoryDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spInsertNewExpenseCategoryByUserID", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlCommand.Parameters.AddWithValue("@AvtiveStatus", this.status);
                    sqlCommand.Parameters.AddWithValue("@CategoryName", this.CategoryName);
                    sqlConnection.Open();
                    rowEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowEffected);
                }
            }
            catch (Exception ex)
            {
                return ReturnBoolean(rowEffected);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Boolean AddExpenseSubCategoryDataIntoCategoryDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spInsertNewExpenseSubCategoryByUserID", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlCommand.Parameters.AddWithValue("@CategoryID", this.CategoryID);
                    sqlCommand.Parameters.AddWithValue("@AvtiveStatus", this.status);
                    sqlCommand.Parameters.AddWithValue("@SubCategoryName", this.SubCategory);
                    sqlConnection.Open();
                    rowEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowEffected);
                }
            }
            catch (Exception ex)
            {
                return ReturnBoolean(rowEffected);
            }
            finally
            {
                sqlConnection.Close();
            }
        }



        public Boolean AddCreditCategoryDataIntoCategoryDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spInsertNewCreditCategoryByUserID", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlCommand.Parameters.AddWithValue("@AvtiveStatus", this.status);
                    sqlCommand.Parameters.AddWithValue("@CategoryName", this.CategoryName);
                    sqlConnection.Open();
                    rowEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowEffected);
                }
            }
            catch (Exception ex)
            {
                return ReturnBoolean(rowEffected);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Boolean AddCreditSubCategoryDataIntoCategoryDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spInsertNewCreditSubCategoryByUserID", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlCommand.Parameters.AddWithValue("@CategoryID", this.CategoryID);
                    sqlCommand.Parameters.AddWithValue("@AvtiveStatus", this.status);
                    sqlCommand.Parameters.AddWithValue("@SubCategoryName", this.SubCategory);
                    sqlConnection.Open();
                    rowEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowEffected);
                }
            }
            catch (Exception ex)
            {
                return ReturnBoolean(rowEffected);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Boolean UpdateExpenseCategoryDataIntoCategoryDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spUpdateExpenseCategoryByUserID", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlCommand.Parameters.AddWithValue("@CategoryID", this.CategoryID);
                    sqlCommand.Parameters.AddWithValue("@AvtiveStatus", this.status);
                    sqlCommand.Parameters.AddWithValue("@CategoryName", this.CategoryName);
                    sqlConnection.Open();
                    rowEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowEffected);
                }
            }
            catch (Exception ex)
            {
                return ReturnBoolean(rowEffected);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Boolean UpdateCreditCategoryDataIntoCategoryDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spUpdateCreditCategoryByUserID", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlCommand.Parameters.AddWithValue("@CategoryID", this.CategoryID);
                    sqlCommand.Parameters.AddWithValue("@AvtiveStatus", this.status);
                    sqlCommand.Parameters.AddWithValue("@CategoryName", this.CategoryName);
                    sqlConnection.Open();
                    rowEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowEffected);
                }
            }
            catch (Exception ex)
            {
                return ReturnBoolean(rowEffected);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Boolean UpdateCreditSubCategoryDataIntoCategoryDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spUpdateCreditSubCategoryByUserID", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlCommand.Parameters.AddWithValue("@SubCategoryID", this.CategoryID);
                    sqlCommand.Parameters.AddWithValue("@AvtiveStatus", this.status);
                    sqlCommand.Parameters.AddWithValue("@SubCategoryName", this.CategoryName);
                    sqlConnection.Open();
                    rowEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowEffected);
                }
            }
            catch (Exception ex)
            {
                return ReturnBoolean(rowEffected);
            }
            finally
            {
                sqlConnection.Close();
            }
        }


        public Boolean UpdateExpenseSubCategoryDataIntoCategoryDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spUpdateExpenseSubCategoryByUserID", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlCommand.Parameters.AddWithValue("@SubCategoryID", this.CategoryID);
                    sqlCommand.Parameters.AddWithValue("@AvtiveStatus", this.status);
                    sqlCommand.Parameters.AddWithValue("@SubCategoryName", this.CategoryName);
                    sqlConnection.Open();
                    rowEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowEffected);
                }
            }
            catch (Exception ex)
            {
                return ReturnBoolean(rowEffected);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
