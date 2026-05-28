CREATE PROCEDURE  spUpdateNotePriority
(
@UserID INT,
@NoteID INT,
@PriorityID INT
)
AS
BEGIN

IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
AND NoteID=@NoteID
)
BEGIN
SELECT 'Invalid UserID Or NoteID' AS Message
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
WHERE UserID=@UserID 
AND NoteID=@NoteID

SELECT 'Note Priority Updated Successfully' AS Message
END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END