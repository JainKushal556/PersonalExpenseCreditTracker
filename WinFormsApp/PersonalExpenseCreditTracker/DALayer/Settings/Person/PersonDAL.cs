using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALayer.Common;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DALayer.Settings.Person
{
    public class PersonDAL
    {
        public int userId { get; set; }
        public int personId { get; set; }
        public string personName { get; set; }
        public string personNumber { get; set; }
        public string address { get; set; }

        private Boolean ReturnBoolean(int value)
        {
            if (value > 0) return true;
            else return false;
        }

        public Boolean SavePersonToDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spInsertPerson", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.userId);
                    sqlCommand.Parameters.AddWithValue("@PersonName", this.personName);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", this.personNumber);
                    sqlCommand.Parameters.AddWithValue("@Address", this.address);
                    sqlConnection.Open();
                    object result = sqlCommand.ExecuteScalar();
                    string msg = result != null ? result.ToString() : "";
                    return msg == "Person Details Inserted Successfully";
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        public Boolean FindDuplicatePhoneNumberIntoDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            bool isDuplicate = false;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spGetDuplicatePersonNumberByUserIDAndPhoneNumber", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.userId);
                    sqlCommand.Parameters.AddWithValue("@PersonID", this.personId);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", this.personNumber);
                    sqlConnection.Open();
                    isDuplicate = Convert.ToBoolean(sqlCommand.ExecuteScalar());
                    return isDuplicate;
                }
            }
            catch (Exception ex)
            {
                return isDuplicate;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Boolean UpdatePersonToDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spUpdatePerson", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.userId);
                    sqlCommand.Parameters.AddWithValue("@PersonID", this.personId);
                    sqlCommand.Parameters.AddWithValue("@PersonName", this.personName);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", this.personNumber);
                    sqlCommand.Parameters.AddWithValue("@Address", this.address);
                    sqlConnection.Open();
                    object result = sqlCommand.ExecuteScalar();
                    string msg = result != null ? result.ToString() : "";
                    return msg == "Person Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }
    }
}
