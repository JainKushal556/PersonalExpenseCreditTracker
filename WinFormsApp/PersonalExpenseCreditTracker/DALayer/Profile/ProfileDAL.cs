using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DALayer.Common;

namespace DALayer.Profile
{
    public class ProfileDAL
    {
        public int userId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int genderId { get; set; }
        public byte[] photoData { get; set; }

        private Boolean ReturnBoolean(int value)
        {
            if (value > 0) return true;
            else return false;
        }
        public bool UpdateProfilePhotoToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateProfilePhoto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@ProfilePhoto", photoData);

                    con.Open();

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


        public Boolean UpdateUserProfileToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(connectionString);

                using (SqlCommand sqlCommand = new SqlCommand("spUpdateUserProfile", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserID", this.userId);
                    sqlCommand.Parameters.AddWithValue("@FullName", this.fullName);
                    sqlCommand.Parameters.AddWithValue("@Email", this.email);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", this.phoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Address", this.address);
                    sqlCommand.Parameters.AddWithValue("@DOB", this.dateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@GenderID", this.genderId);

                    sqlConnection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    

                    if (reader.Read())
                    {
                        if (reader["Message"].ToString() == "User Profile Updated Successfully")
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
            catch (Exception)
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
