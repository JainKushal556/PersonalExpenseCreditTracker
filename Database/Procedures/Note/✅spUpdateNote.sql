CREATE PROCEDURE spUpdateNote
(
@NoteID INT,
@PriorityID INT,
@NoteTitle VARCHAR(MAX),
@Description VARCHAR(MAX)
)
AS
BEGIN

SET @NoteTitle=LTRIM(RTRIM(@NoteTitle))
SET @Description=LTRIM(RTRIM(@Description))

IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE NoteID=@NoteID
)
BEGIN
SELECT 'NoteID Does Not Exist' AS Message
RETURN 
END

IF @NoteTitle IS NULL OR @NoteTitle= ''
BEGIN
SELECT 'Note Title Cannot be Empty' AS Message
RETURN
END

IF @Description IS NULL OR @Description= ''
BEGIN
SELECT 'Description Cannot be Empty' AS Message
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
    NotePriorityID=@PriorityID,
    NoteTitle=@NoteTitle,
    Description=@Description
WHERE NoteID=@NoteID
SELECT 'Note Updated Successfully' AS Message


END TRY
BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END