CREATE TABLE tblNote
(
    NoteID INT NOT NULL PRIMARY KEY IDENTITY(1,1),

    UserID INT NOT NULL ,
    NoteStatusID INT NOT NULL,

    NoteTitle VARCHAR(150) NOT NULL,
    Description VARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME NOT NULL,

    
        FOREIGN KEY (UserID)
        REFERENCES tblUsers(UserID),

        FOREIGN KEY (NoteStatusID)
        REFERENCES tblNoteStatus(NoteStatusID)
);
