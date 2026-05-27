CREATE PROCEDURE  spUpdateNotePriority
(
@NoteID INT,
@PriorityID INT
)
AS
BEGIN

IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE NoteID=@NoteID
)
BEGIN
SELECT 'NoteID Does Not Exist' AS Message
RETURN 
END



IF NOT EXISTS
(
SELECT 1 FROM tblNotePriorities
WHERE NotePriorityID=@PriorityID
)
BEGIN
SELECT 'Invalid Note PriorityID' AS Message
RETURN 
END

BEGIN TRY

UPDATE tblNote 
SET
    NotePriorityID=@PriorityID
WHERE NoteID=@NoteID

SELECT 'Note Priority Updated Successfully' AS Message
END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END