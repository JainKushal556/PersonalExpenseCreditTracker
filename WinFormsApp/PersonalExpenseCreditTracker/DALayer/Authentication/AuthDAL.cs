using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALayer.Common;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace DALayer.Authentication
{
    public class AuthDAL
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string ErrorMsg;

        private bool ReturnBoolean(int value)
        {
            if (value > 0) return true;
            else return false;
        }
        
        public int GetUserIdFromDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            userId = 0;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spGetActiveUserId", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();
                    object result = sqlCommand.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                    return userId;
                }
            }
            catch (Exception ex)
            {
                return userId;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool RegistrationFormDataIntoAuthDAL()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spRegisterUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Password", this.newPassword);
                    sqlCommand.Parameters.AddWithValue("@UserName", this.userName);
                    sqlCommand.Parameters.AddWithValue("@Email", this.email);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", this.phoneNumber);

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

        public string GetErrorMsgForRegistrationForm()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spRegisterUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Password", this.newPassword);
                    sqlCommand.Parameters.AddWithValue("@UserName", this.userName);
                    sqlCommand.Parameters.AddWithValue("@Email", this.email);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", this.phoneNumber);

                    sqlConnection.Open();
                    ErrorMsg = sqlCommand.ExecuteScalar().ToString();
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                return ErrorMsg;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool LoginDataIntoAuthDAL()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spLoginUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Password", this.password);
                    sqlCommand.Parameters.AddWithValue("@Email", this.email);

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

        public string GetErrorMsgForLogin()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;


            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spLoginUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Password", this.password);
                    sqlCommand.Parameters.AddWithValue("@Email", this.email);

                    sqlConnection.Open();
                    ErrorMsg = sqlCommand.ExecuteScalar().ToString();
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                return ErrorMsg;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool ForgotPasswordDataIntoAuthDAL()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spForgetPassword", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@NewPassword", this.newPassword);
                    sqlCommand.Parameters.AddWithValue("@Email", this.email);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", this.phoneNumber);

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

        public string GetErrorMsgForForgotPassword()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;


            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spForgetPassword", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@NewPassword", this.newPassword);
                    sqlCommand.Parameters.AddWithValue("@Email", this.email);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", this.phoneNumber);

                    sqlConnection.Open();
                    ErrorMsg = sqlCommand.ExecuteScalar().ToString();
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                return ErrorMsg;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
