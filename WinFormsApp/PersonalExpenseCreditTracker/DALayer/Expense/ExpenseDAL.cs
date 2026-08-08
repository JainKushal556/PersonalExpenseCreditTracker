using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DALayer.Common;

namespace DALayer.Expense
{
    public class ExpenseDAL
    {
        public int userId { get; set; }
        public int expenseId { get; set; }
        public int categoryId { get; set; }
        public int subCategoryId { get; set; }
        public int paymentId { get; set; }
        public string amount { get; set; }
        public string description { get; set; }

        private Boolean ReturnBoolean(int value)
        {
            if (value > 0) return true;
            else return false;
        }

        public Boolean SaveExpenseToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowsEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);

                using (SqlCommand sqlCommand = new SqlCommand("spInsertExpenseByUserID", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserID", this.userId);
                    sqlCommand.Parameters.AddWithValue("@CategoryID", this.categoryId);
                    sqlCommand.Parameters.AddWithValue("@SubCategoryID", this.subCategoryId);
                    sqlCommand.Parameters.AddWithValue("@Amount", Convert.ToDecimal(this.amount));
                    sqlCommand.Parameters.AddWithValue("@Description", this.description);
                    sqlCommand.Parameters.AddWithValue("@PaymentID", this.paymentId);

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
