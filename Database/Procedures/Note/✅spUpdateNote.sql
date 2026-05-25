CREATE PROCEDURE spUpdateNote
@NoteID INT,
@UserID INT,
@PriorityID INT,
@NoteTitle VARCHAR(MAX),
@Description VARCHAR(MAX)
AS
BEGIN
UPDATE tblNote 
SET UserID=@UserID,
    NotePriorityID=@PriorityID,
    NoteTitle=@NoteTitle,
    Description=@Description
WHERE NoteID=@NoteID
END