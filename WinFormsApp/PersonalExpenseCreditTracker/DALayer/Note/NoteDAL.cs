using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DALayer.Common;

namespace DALayer.Note
{
    public class NoteDAL
    {
        public int userId { get; set; }
        public int noteId { get; set; }
        public string noteTitle { get; set; }
        public string description { get; set; }
        public int priorityId { get; set; }
        public int colorId { get; set; }
        //public string colorHexCode { get; set; }

        private Boolean ReturnBoolean(int value)
        {
            if (value > 0) return true;
            else return false;
        }
        public Boolean SaveNoteToDb()
        {
            string connectionstring = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowsEffected = 0;
            try
            {
                sqlConnection = new SqlConnection(connectionstring);
                using (SqlCommand sqlCommand = new SqlCommand("spInsertNote", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", userId);
                    sqlCommand.Parameters.AddWithValue("@PriorityID", priorityId);
                    sqlCommand.Parameters.AddWithValue("@NoteColorID", colorId);
                    sqlCommand.Parameters.AddWithValue("@NoteTitle", noteTitle);
                    sqlCommand.Parameters.AddWithValue("@Description", description);

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

        public Boolean UpdateNoteToDb()
        {
            string connectionstring = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowsEffected = 0;
            try
            {
                sqlConnection = new SqlConnection(connectionstring);
                using (SqlCommand sqlCommand = new SqlCommand("spUpdateNote", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", userId);
                    sqlCommand.Parameters.AddWithValue("@NoteID", noteId);
                    sqlCommand.Parameters.AddWithValue("@PriorityID", priorityId);
                    sqlCommand.Parameters.AddWithValue("@NoteColorID", colorId);
                    sqlCommand.Parameters.AddWithValue("@NoteTitle", noteTitle);
                    sqlCommand.Parameters.AddWithValue("@Description", description);

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


        public Boolean DeleteNoteToDb()
        {
            string connectionstring = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowsEffected = 0;
            try
            {
                sqlConnection = new SqlConnection(connectionstring);
                using (SqlCommand sqlCommand = new SqlCommand("spDeleteNote", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", userId);
                    sqlCommand.Parameters.AddWithValue("@NoteID", noteId);

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
