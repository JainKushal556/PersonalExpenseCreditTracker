CREATE PROCEDURE spUpdateNoteColor
(
@UserID INT,
@NoteID INT,
@NoteColorID INT
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
SELECT 1 FROM tblNoteColor
WHERE NoteColorID=@NoteColorID
)
BEGIN
SELECT 'Invalid Note ColorID' AS Message
RETURN 
END

BEGIN TRY

UPDATE tblNote 
SET
    NoteColorID=@NoteColorID
WHERE UserID=@UserID 
AND NoteID=@NoteID

SELECT 'Note Color Updated Successfully' AS Message
END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END
