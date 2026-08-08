using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Note;
using BLLayer.Common;
using System.Data;

namespace PersonalExpenseCreditTracker.Modules.Note
{
    public class NoteUI
    {
        public int userId { get; set; }
        public int noteId { get; set; }
        public string noteTitle { get; set; }
        public string description { get; set; }
        public int priorityId { get; set; }
        public int colorId { get; set; }
        // public string colorHexCode { get; set; }

        // Create an object of the Business Logic Layer
        private NoteBLL noteBll = new NoteBLL();

        // Pass the data from the UI layer to the Business Logic Layer
        public CommonValidator.ValidationResult InsertDataIntoNoteUi()
        {
            noteBll.userId = userId;
            noteBll.noteId = noteId;
            noteBll.noteTitle = noteTitle;
            noteBll.priorityId = priorityId;
            noteBll.colorId = colorId;
            //noteBll.colorHexCode = colorHexCode;
            noteBll.description = description;

            // Call the BLL method for validation
            return noteBll.DataValidatorIntoNoteBll();
        }
    }
}
