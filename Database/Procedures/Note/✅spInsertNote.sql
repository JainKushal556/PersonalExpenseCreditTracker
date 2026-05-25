CREATE PROCEDURE spInsertNote
@UserID INT,
@PriorityID INT,
@NoteTitle VARCHAR(MAX),
@Description VARCHAR(MAX)
AS
BEGIN
INSERT INTO tblNote (UserID, NotePriorityID, NoteTitle, Description,CreatedAt)
VALUES
(@UserID,@PriorityID,@NoteTitle,@Description,GETDATE())
END