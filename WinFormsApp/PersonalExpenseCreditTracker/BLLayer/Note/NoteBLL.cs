using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLLayer.Note
{
    public class NoteBLL
    {
        // Properties
        public int userId { get; set; }
        public int noteId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int priorityId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }


        // ---------------- Insert Note ----------------
        public bool ValidetInsertNoteDeatils(int userId)
        {
            // User Validation
            if (userId <= 0)
                return false;

            // Title Validation
             if (title != null)
                {
                    title = title.Trim();
                }
                else
                {
                    title = null;
                }
            if (string.IsNullOrWhiteSpace(title))
                return false;

            if (title.Length < 3 || title.Length > 150)
                return false;

            // Content Validation
            if (content != null)
            {
                content = content.Trim();
            }
            else
            {
                content = null;
            }
            if (string.IsNullOrWhiteSpace(content))
                return false;

            if (content.Length > 5000)
                return false;

            // Priority Validation
            if (priorityId <= 0)
                return false;

            return true;
        }


        // ---------------- Update Note ----------------
        public bool ValidateUpdateNoteDetails(int noteId)
        {
            if (noteId <= 0)
                return false;

            if (userId <= 0)
                return false;

            if (title != null)
                {
                    title = title.Trim();
                }
                else
                {
                    title = null;
                }
            if (string.IsNullOrWhiteSpace(title))
                return false;

            if (title.Length < 3 ||title.Length > 150)
                return false;

            if (content != null)
                {
                    content = content.Trim();
                }
                else
                {
                    content = null;
                }
            if (string.IsNullOrWhiteSpace(content))
                return false;

            if (content.Length > 5000)
                return false;

            if (priorityId <= 0)
                return false;

            return true;
        }


        // ---------------- Delete Note ----------------
        public bool ValidateDeleteNoteDetails(int noteId)
        {
            if (noteId <= 0)
                return false;

            if (userId <= 0)
                return false;

            return true;
        }


        // ---------------- Update Note Priority ----------------
        public bool ValidatePriorityUpdateNoteDetails(int noteId)
        {
            if (noteId <= 0)
                return false;

            if (priorityId <= 0)
                return false;

            return true;
        }

        // ---------------- Get All Notes ----------------
        public bool ValidateGetAllNote(int userId)
        {
            return userId > 0;
        }


        // ---------------- Filter By Priority ----------------
        public bool ValidateFilterByPriority(int priorityId)
        {
            if (userId <= 0)
                return false;

            if (priorityId <= 0)
                return false;

            return true;
        }


        // ---------------- Date Filter ----------------
        public bool ValidateDateRange(DateTime startDate, DateTime endDate)
        {
            if (userId <= 0)
                return false;

            if (startDate > endDate)
                return false;

            // Optional: Restrict future dates
            if (startDate > DateTime.Today || endDate > DateTime.Today)
                return false;

            return true;
        }


        // ---------------- Call All Validate Function ----------------
        public bool InsertDataToNoteBll()
        {
            if (ValidetInsertNoteDeatils(userId))
            {
                return false;
            }

            if (ValidateUpdateNoteDetails(noteId))
            {
                return false;
            }

            if (ValidateDeleteNoteDetails(noteId))
                return false;

            if (ValidatePriorityUpdateNoteDetails(noteId))
                return false;

            if (ValidateGetAllNote(userId))
                return false;

            if (ValidateFilterByPriority(priorityId))
                return false;

            if (ValidateDateRange(startDate, endDate))
                return false;

            return true;
        }
    }
}