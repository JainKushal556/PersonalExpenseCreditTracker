using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Note;

namespace PersonalExpenseCreditTracker.Modules.Note
{
    class NoteUI
    {
        // Properties
        public int userId { get; set; }
        public int noteId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int priorityId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        private NoteBLL noteBll = new NoteBLL();

        public bool InsertDataToNoteUi(NoteUI noteUi)
        {

            noteBll.userId = noteUi.userId;
            noteBll.noteId = noteUi.noteId;
            noteBll.title = noteUi.title;
            noteBll.content = noteUi.content;
            noteBll.priorityId = noteUi.priorityId;
            noteBll.startDate = noteUi.startDate;
            noteBll.endDate = noteUi.endDate;
            return noteBll.InsertDataToNoteBll();
        }
    }
}
