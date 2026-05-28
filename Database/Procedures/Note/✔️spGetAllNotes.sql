CREATE PROCEDURE spGetAllNotes
(
@UserID INT
)
AS
BEGIN

IF NOT EXISTS
(
SELECT 1 FROM tblUsers
WHERE UserID=@UserID
)
BEGIN
SELECT 'UserID Does Not Exists' AS Message
RETURN
END

IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
)
BEGIN
SELECT 'No Notes Found For This User' AS Message
RETURN
END
BEGIN TRY

SELECT
tblNote.NoteID,
tblNote.NotePriorityID,
tblNote.NoteTitle,
tblNote.Description,
tblNotePriorities.NotePriorityName,
tblNote.CreatedAt 

FROM tblNote
LEFT JOIN tblNotePriorities ON tblNote.NotePriorityID=tblNotePriorities.NotePriorityID
WHERE tblNote.UserID=@UserID
ORDER BY tblNote.CreatedAt DESC

END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END