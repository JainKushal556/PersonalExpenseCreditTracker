using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DALayer.Common;
namespace DALayer.Lent
{
    public class LentDAL
    {
        public int userId { get; set; }
        public int lentId { get; set; }
        public int personId { get; set; }
        public int paymentId { get; set; }
        public int statusId { get; set; }
        public string amount { get; set; }
        public DateTime deadlineAt { get; set; }
        public string description { get; set; }
        private Boolean ReturnBoolean(int value)
        {
            if (value > 0) return true;
            else return false;
        }
        public Boolean SaveLentToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowsEffected = 0;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spInsertLent", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.userId);
                    sqlCommand.Parameters.AddWithValue("@PersonID", this.personId);
                    sqlCommand.Parameters.AddWithValue("@PaymentID", this.paymentId);
                    sqlCommand.Parameters.AddWithValue("@Amount",Convert.ToDecimal(this.amount));
                    sqlCommand.Parameters.AddWithValue("@DeadlineAT",this.deadlineAt);
                    sqlCommand.Parameters.AddWithValue("@Description",this.description);
                    sqlConnection.Open();
                    rowsEffected = sqlCommand.ExecuteNonQuery();
                    return ReturnBoolean(rowsEffected); 
                }
            }
            catch (Exception ex)
            {
                return ReturnBoolean(rowsEffected);
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
