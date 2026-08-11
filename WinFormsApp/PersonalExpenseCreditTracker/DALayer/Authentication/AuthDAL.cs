using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DALayer.Common;
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
        public string message { get; set; }
        //private Boolean ReturnBoolean(int value)
        //{
        //    if (value > 0) return true;
        //    else return false;
        //}
        public Boolean SaveRegisterToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(connectionString);

                using (SqlCommand sqlCommand =
                    new SqlCommand("spRegisterUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserName", this.userName);
                    sqlCommand.Parameters.AddWithValue("@Email", this.email);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", this.phoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Password", this.password);

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        //this.message = reader["Message"].ToString();
                        
                        if (reader.Read())
                        {
                            bool hasMessage = false;
                            bool hasUserId = false;
                            bool hasSuccess = false;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string colName = reader.GetName(i);
                                if (colName.Equals("Message", StringComparison.OrdinalIgnoreCase)) hasMessage = true;
                                if (colName.Equals("UserID", StringComparison.OrdinalIgnoreCase)) hasUserId = true;
                                if (colName.Equals("Success", StringComparison.OrdinalIgnoreCase)) hasSuccess = true;
                            }

                            if (hasMessage && reader["Message"] != DBNull.Value)
                            {
                                this.message = reader["Message"].ToString();
                            }

                            if (hasUserId && reader["UserID"] != DBNull.Value)
                            {
                                this.userId = Convert.ToInt32(reader["UserID"]);
                            }

                            if (hasSuccess && reader["Success"] != DBNull.Value)
                            {
                                return Convert.ToInt32(reader["Success"]) == 1;
                            }

                            if (this.userId > 0 || (this.message != null && this.message.Equals("User Inserted Successfully", StringComparison.OrdinalIgnoreCase)))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                return false;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }

            return false;
        }
        //Login page
        public Boolean LoginUserToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spLoginUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Email", this.email);
                    sqlCommand.Parameters.AddWithValue("@Password", this.password);
                    sqlConnection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.message = reader["Message"].ToString();

                            bool hasUserId = false;
                            bool hasSuccess = false;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string colName = reader.GetName(i);

                                if (colName.Equals("UserID", StringComparison.OrdinalIgnoreCase))
                                    hasUserId = true;

                                if (colName.Equals("Success", StringComparison.OrdinalIgnoreCase))
                                    hasSuccess = true;
                            }

                            if (hasUserId && reader["UserID"] != DBNull.Value)
                            {
                                this.userId = Convert.ToInt32(reader["UserID"]);
                            }

                            if (hasSuccess && reader["Success"] != DBNull.Value)
                            {
                                return Convert.ToInt32(reader["Success"]) == 1;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                this.message = ex.ToString();
                return false;
                
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
            return false;
        }

    }
}
