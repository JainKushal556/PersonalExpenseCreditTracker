CREATE PROCEDURE spFilterNotesByPriority

@UserID INT,
@PriorityID INT

AS
BEGIN

IF NOT EXISTS
(
SELECT 1 FROM tblUsers
WHERE UserID=@UserID
)
BEGIN
SELECT 'UserID Does Not Exist' AS Message
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

IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
AND NotePriorityID=@PriorityID
)
BEGIN
SELECT 'No Notes Found' AS Message
RETURN
END

BEGIN TRY
SELECT
tblNote.NoteID,
tblNote.NotePriorityID,
tblNote.NoteColorID,
tblNoteColor.ColorName,
tblNoteColor.ColorHexCode,
tblNote.NoteTitle,
tblNote.Description,
tblNotePriorities.NotePriorityName,
tblNote.CreatedAt
FROM tblNote
LEFT JOIN tblNotePriorities ON tblNote.NotePriorityID=tblNotePriorities.NotePriorityID
LEFT JOIN tblNoteColor ON tblNote.NoteColorID=tblNoteColor.NoteColorID
WHERE tblNote.UserID=@UserID
AND tblNote.NotePriorityID=@PriorityID

ORDER BY tblNote.CreatedAt DESC

END TRY
BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END