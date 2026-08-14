using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DALayer.Common;

namespace DALayer.Task
{
    public class TaskDAL
    {
        public int taskId { get; set; }
        public int userId { get; set; }
        public int priorityId { get; set; }
        public int statusId { get; set; }
        public string taskTitle { get; set; }
        public DateTime deadline { get; set; }

        private Boolean ReturnBoolean(int value)
        {
            if (value > 0)
                return true;
            else
                return false;
        }
        public Boolean SaveTaskToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowsEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);

                using (SqlCommand sqlCommand = new SqlCommand("spInsertTask", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserID", this.userId);               
                    sqlCommand.Parameters.AddWithValue("@TaskTitle", this.taskTitle);
                    sqlCommand.Parameters.AddWithValue("@PriorityID", this.priorityId);
                    sqlCommand.Parameters.AddWithValue("@Deadline", this.deadline);

                    sqlConnection.Open();

                    object result = sqlCommand.ExecuteScalar();


                    if (result != null)
                    {
                        return true;
                    }

                    return false;
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

        //Edit task
        public Boolean UpdateTaskToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowsEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);

                using (SqlCommand sqlCommand = new SqlCommand("spUpdateTask", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserID", this.userId);
                    sqlCommand.Parameters.AddWithValue("@TaskID", this.taskId);
                    sqlCommand.Parameters.AddWithValue("@PriorityID", this.priorityId);
                    sqlCommand.Parameters.AddWithValue("@TaskStatusID", this.statusId);
                    sqlCommand.Parameters.AddWithValue("@TaskTitle", this.taskTitle);
                    sqlCommand.Parameters.AddWithValue("@Deadline", this.deadline);

                    sqlConnection.Open();

                    object result = sqlCommand.ExecuteScalar();

                    if (result != null)
                    {
                        return true;
                    }

                    return false;
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


        // Update Task Status
        public string UpdateTaskStatusToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(connectionString);

                using (SqlCommand sqlCommand = new SqlCommand("spUpdateTaskStatus", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@TaskID", this.taskId);
                    sqlCommand.Parameters.AddWithValue("@TaskStatusID", this.statusId);

                    sqlConnection.Open();

                    object result = sqlCommand.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();   
                    }

                    return "No message returned.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }


        // Delete Task
        public Boolean DeleteTaskToDb()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            int rowsEffected = 0;

            try
            {
                sqlConnection = new SqlConnection(connectionString);

                using (SqlCommand sqlCommand = new SqlCommand("spDeleteTask", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@TaskID", this.taskId);

                    sqlConnection.Open();

                    object result = sqlCommand.ExecuteScalar();

                    if (result != null)
                    {
                        return true;
                    }

                    return false;
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


        public Boolean FindDuplicateTaskTitleIntoDB()
        {
            string connectionString = Common.SqlHelper.connectionString;
            SqlConnection sqlConnection = null;
            bool isDuplicate = false;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                using (SqlCommand sqlCommand = new SqlCommand("spCheckDuplicateTaskTitle", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserID", this.userId);
                    sqlCommand.Parameters.AddWithValue("@TaskID", this.taskId);
                    sqlCommand.Parameters.AddWithValue("@TaskTitle", this.taskTitle);

                    sqlConnection.Open();
                    isDuplicate = Convert.ToBoolean(sqlCommand.ExecuteScalar());
                    return isDuplicate;
                }
            }
            catch (Exception)
            {
                return isDuplicate;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

    }
}
