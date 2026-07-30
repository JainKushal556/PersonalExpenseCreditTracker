CREATE PROCEDURE spGetNotesBetweenDates

@UserID INT,
@FromDate DATE,
@ToDate DATE

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

IF @FromDate>@ToDate
BEGIN
SELECT 'Start Date Cannot Be Greater Than End Date' AS Message
RETURN
END


IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
AND CAST(tblNote.CreatedAt AS DATE)
BETWEEN @FromDate AND @ToDate
)
BEGIN
SELECT 'No Notes Found Between These Dates' AS Message
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
AND CAST(tblNote.CreatedAt AS DATE)
BETWEEN @FromDate AND @ToDate
ORDER BY tblNote.CreatedAt DESC

END TRY
BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END