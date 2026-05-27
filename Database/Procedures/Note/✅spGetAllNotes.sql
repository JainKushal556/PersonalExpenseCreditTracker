CREATE PROCEDURE spGetAllNotes
AS
BEGIN

IF NOT EXISTS
(
SELECT 1 FROM tblNote
)
BEGIN
SELECT 'Notes Not Found' AS Message
RETURN
END

BEGIN TRY

SELECT
tblNote.NoteID,
tblNote.UserID,
tblNote.NotePriorityID,
tblNote.NoteTitle,
tblNote.Description,
tblNotePriorities.NotePriorityName,
tblNote.CreatedAt 

FROM tblNote
LEFT JOIN tblNotePriorities ON tblNote.NotePriorityID=tblNotePriorities.NotePriorityID

ORDER BY tblNote.CreatedAt DESC

END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END