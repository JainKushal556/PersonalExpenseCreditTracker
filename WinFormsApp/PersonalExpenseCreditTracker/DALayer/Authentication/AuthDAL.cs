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
    }
}
