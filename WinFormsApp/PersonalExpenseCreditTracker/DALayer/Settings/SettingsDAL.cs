using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALayer.Common;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DALayer.Settings
{
    public class SettingsDAL
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        private Boolean ReturnBoolean(int value)
        {
            if (value > 0) return true;
            else return false;
        }

        public Boolean ChangePasswordDataIntoSettingsDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spChangePassword", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlCommand.Parameters.AddWithValue("@OldPassword", this.CurrentPassword);
                    sqlCommand.Parameters.AddWithValue("@NewPassword ", this.NewPassword);
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

        public Boolean LogoutUserFromDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spLogoutUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.UserId);
                    sqlConnection.Open();
                    rowEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowEffected);
                }
            }
            catch (Exception ex)
            {
                return false;
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
