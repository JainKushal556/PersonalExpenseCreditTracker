CREATE PROCEDURE spInsertNote

@UserID INT,
@PriorityID INT,
@NoteTitle VARCHAR(MAX),
@Description VARCHAR(MAX)

AS
BEGIN

SET @NoteTitle=LTRIM(RTRIM(@NoteTitle))
SET @Description=LTRIM(RTRIM(@Description))

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
SELECT 1 FROM tblUsers
WHERE UserID=@UserID
)
BEGIN
SELECT 'UserID Does Not Exist' AS Message 
RETURN 
END

IF NOT EXISTS
(
SELECT 1 FROM tblUserAuthentication
WHERE UserID=@UserID
AND Active=1
)
BEGIN
SELECT 'Inactive User Cannot Add Notes' AS Message 
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

INSERT INTO tblNote (UserID, NotePriorityID, NoteTitle, Description)
VALUES
(@UserID,@PriorityID,@NoteTitle,@Description)

SELECT 'Note Inserted Successfully' AS Message

END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END