CREATE PROCEDURE  spUpdateNotePriority
@NoteID INT,
@PriorityID INT

AS
BEGIN
UPDATE tblNote 
SET
    NotePriorityID=@PriorityID
WHERE NoteID=@NoteID
END