using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using System.Data;
using DALayer.Common;
using DALayer.Note;

namespace BLLayer.Note
{
    public class NoteBLL
    {
        public int userId { get; set; }
        public int noteId { get; set; }
        public string noteTitle { get; set; }
        public string description { get; set; }
        public int priorityId { get; set; }
        public int colorId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        private NoteDAL noteDAL = new NoteDAL();

        // Stores the validation result
        CommonValidator.ValidationResult result;

        public CommonValidator.ValidationResult DateValidatorIntoNoteBll()
        {
            result = CommonValidator.ValidateDateRange(fromDate, toDate);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            return CommonValidator.ValidationResult.Success;
        }

        // Validates all user input before saving the data
        public CommonValidator.ValidationResult DataValidatorIntoNoteBll()
        {
            //Note Title Validation
            result = CommonValidator.ValidateNoteTitle(noteTitle);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            //description Validation
            result = CommonValidator.ValidateDescription(description);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            //Priority Validation
            result = CommonValidator.ValidatePriority(priorityId);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            ////color Validation
            //result = CommonValidator.ValidateColor(colorId);
            //if (result != CommonValidator.ValidationResult.Success)
            //{
            //    return result;
            //}


            noteDAL.userId = userId;
            noteDAL.noteId = noteId;
            noteDAL.noteTitle = noteTitle;
            noteDAL.description = description;
            noteDAL.priorityId = priorityId;
            noteDAL.colorId = colorId;
            //noteDAL.colorHexCode = colorHexCode;


            if (noteDAL.SaveNoteToDb())
            {
                return CommonValidator.ValidationResult.Success;
            }
            else
            {
                return CommonValidator.ValidationResult.StoreProcedureError;
            }
        }

        public CommonValidator.ValidationResult UpdateDataIntoNoteBll()
        {
            result = CommonValidator.ValidateNoteTitle(noteTitle);
            if (result != CommonValidator.ValidationResult.Success) return result;

            result = CommonValidator.ValidateDescription(description);
            if (result != CommonValidator.ValidationResult.Success) return result;

            result = CommonValidator.ValidatePriority(priorityId);
            if (result != CommonValidator.ValidationResult.Success) return result;

            //result = CommonValidator.ValidateColor(colorId);
            //if (result != CommonValidator.ValidationResult.Success) return result;

            noteDAL.userId = userId;
            noteDAL.noteId = noteId;
            noteDAL.noteTitle = noteTitle;
            noteDAL.description = description;
            noteDAL.priorityId = priorityId;
            noteDAL.colorId = colorId;

            if (noteDAL.UpdateNoteToDb())
            {
                return CommonValidator.ValidationResult.Success;
            }
            else
            {
                return CommonValidator.ValidationResult.StoreProcedureError;
            }
        }

        public CommonValidator.ValidationResult DeleteNoteIntoNoteBll()
        {
            noteDAL.userId = userId;
            noteDAL.noteId = noteId;

            if (noteDAL.DeleteNoteToDb())
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