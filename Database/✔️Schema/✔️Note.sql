CREATE TABLE tblNote
(
    NoteID INT NOT NULL PRIMARY KEY IDENTITY(1,1),

    UserID INT NOT NULL,
    NotePriorityID INT NOT NULL,
    NoteColorID INT NOT NULL DEFAULT 1,

    NoteTitle VARCHAR(MAX) NOT NULL,
    Description VARCHAR(MAX) NOT NULL,

    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (UserID)
    REFERENCES tblUsers(UserID),

    FOREIGN KEY (NotePriorityID)
    REFERENCES tblNotePriorities(NotePriorityID),

    FOREIGN KEY (NoteColorID)
    REFERENCES tblNoteColor(NoteColorID)
);
