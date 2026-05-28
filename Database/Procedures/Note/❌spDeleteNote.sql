CREATE PROCEDURE  spDeleteNote
(
@UserID INT,
@NoteID INT
)
AS
BEGIN


IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
)
BEGIN
SELECT 'UserID Does Not Exists' AS Message
RETURN
END


IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE NoteID=@NoteID
)
BEGIN
SELECT 'NoteID Does Not Exists' AS Message
RETURN
END

BEGIN TRY


DELETE FROM tblNote
WHERE UserID=@UserID
AND NoteID=@NoteID

SELECT 'Note Deleted Successfully' AS Message
END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END

--noteid er user id validation aksathe korte hbe akhon userid er noteid thakle ee delete hoye jbe but oi user er jeno note id ta hoy ota to nae ono user er note id thkle seo delete hoye jbe .