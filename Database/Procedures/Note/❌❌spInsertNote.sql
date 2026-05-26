CREATE PROCEDURE spInsertNote
@UserID INT,
@PriorityID INT,
@NoteTitle VARCHAR(MAX),
@Description VARCHAR(MAX)
AS
BEGIN
INSERT INTO tblNote (UserID, NotePriorityID, NoteTitle, Description,CreatedAt)
VALUES
(@UserID,@PriorityID,@NoteTitle,@Description,GETDATE())
END


--validation nae jodi kau title na day empty thake tokhon oo insert hoye jbe . 
--user id o check kora nae adeo oi user id ta tblusers e ache kina jodi na thake tokhon oo insert hoye jbe .
--only active user ee note add korte parbe inactive thkle hbe na setao cehck nae 
--priority id o check nae .
--success message nae 