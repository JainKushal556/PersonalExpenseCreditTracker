using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using DALayer.Task;
namespace BLLayer.Task
{
   public  class TaskBLL
    {
        public int taskId { get; set; }
        public int userId { get; set; }
        public int priorityId { get; set; }
        public int statusId { get; set; }
        public string taskTitle { get; set; }
        public DateTime deadline { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        private TaskDAL taskDal = new TaskDAL();
        // Stores the validation result
        CommonValidator.ValidationResult result;

        public CommonValidator.ValidationResult DateValidatorIntoTaskBll()
        {
            result = CommonValidator.ValidateDateRange(fromDate, toDate);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            return CommonValidator.ValidationResult.Success;
        }

        // Validate all input and save to database
        public CommonValidator.ValidationResult DataValidatorIntoTaskBll()
        {
            // Task Title Validation
            result = CommonValidator.ValidateTaskTitle(taskTitle);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }
            // Priority Validation
            result = CommonValidator.ValidatePriority(priorityId);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            // Deadline Validation
            result = CommonValidator.ValidateDeadline(deadline);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            // Pass data to DAL
            taskDal.userId = userId;
            taskDal.priorityId = priorityId;
            taskDal.taskTitle = taskTitle;
            taskDal.deadline = deadline;
            taskDal.statusId = statusId;

            // Save to Database
            if (taskDal.SaveTaskToDb())
            {
                return CommonValidator.ValidationResult.Success;
            }
            else
            {
                return CommonValidator.ValidationResult.StoreProcedureError;
            }

        }
      // Edit Task
        public CommonValidator.ValidationResult EditTaskValidator()
        {
            result = CommonValidator.ValidateTaskTitle(taskTitle);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            result = CommonValidator.ValidatePriority(priorityId);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            result = CommonValidator.ValidateStatus(statusId);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            result = CommonValidator.ValidateDeadline(deadline);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            taskDal.userId = userId;
            taskDal.taskId = taskId;
            taskDal.priorityId = priorityId;
            taskDal.statusId = statusId;
            taskDal.taskTitle = taskTitle;
            taskDal.deadline = deadline;

            if (taskDal.UpdateTaskToDb())
            {
                return CommonValidator.ValidationResult.Success;
            }
            else{

                 return CommonValidator.ValidationResult.StoreProcedureError;
            }
        }

        // Update Task Status
        public CommonValidator.ValidationResult UpdateTaskStatusValidator()
        {
            result = CommonValidator.ValidateStatus(statusId);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            taskDal.taskId = taskId;
            taskDal.statusId = statusId;

            string message = taskDal.UpdateTaskStatusToDb();

            if (message == "Task Status Updated Successfully") 
            {
                return CommonValidator.ValidationResult.Success;
            }
            else if (message == "Task Already Has This Status") 
            {
                return CommonValidator.ValidationResult.TaskAlreadyUpdated;
            }
            else
            {
                return CommonValidator.ValidationResult.StoreProcedureError;
            }

        }

        // Delete Task
        public CommonValidator.ValidationResult DeleteTaskValidator()
        {
            taskDal.taskId = taskId;

            if (taskDal.DeleteTaskToDb())
            {
                return CommonValidator.ValidationResult.Success;
            }
            else
            {
                return CommonValidator.ValidationResult.StoreProcedureError;
            }
        }


    }
}
