using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Task;
using BLLayer.Common;
namespace PersonalExpenseCreditTracker.Modules.Task
{
    public class TaskUI
    {
        public int taskId { get; set; }
        public int userId { get; set; }
        public int priorityId { get; set; }
        public int statusId { get; set; }
        public string taskTitle { get; set; }
        public DateTime deadline { get; set; }

        // Create an object of the Business Logic Layer
        private TaskBLL taskBLL = new TaskBLL();

        // Pass the data from the UI layer to the Business Logic Layer
        public CommonValidator.ValidationResult InsertDataIntoTaskUi()
        {
            taskBLL.userId = userId;
            taskBLL.taskTitle = taskTitle;
            taskBLL.priorityId = priorityId;
            taskBLL.statusId = statusId;
            taskBLL.deadline = deadline;

            // Call the BLL method for validation
            return taskBLL.DataValidatorIntoTaskBll();
        }
        public CommonValidator.ValidationResult UpdateDataIntoTaskUi()
        {
            taskBLL.userId = userId;
            taskBLL.taskId = taskId;
            taskBLL.taskTitle = taskTitle;
            taskBLL.priorityId = priorityId;
            taskBLL.statusId = statusId;
            taskBLL.deadline = deadline;

            return taskBLL.EditTaskValidator();
        }
        public CommonValidator.ValidationResult UpdateStatusIntoTaskUi()
        {
            taskBLL.taskId = taskId;
            taskBLL.statusId = statusId;

            return taskBLL.UpdateTaskStatusValidator();
        }

        // Delete Task
        public CommonValidator.ValidationResult DeleteTaskIntoTaskUi()
        {
            taskBLL.taskId = taskId;

            return taskBLL.DeleteTaskValidator();
        }
    }
}
