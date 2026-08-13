CREATE PROCEDURE spCheckDuplicateNoteTitle
    @UserID INT,
    @NoteID INT,
    @NoteTitle NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    
    
    IF EXISTS (
        SELECT 1 
        FROM tblNote 
        WHERE UserID = @UserID 
          AND LOWER(TRIM(NoteTitle)) = LOWER(TRIM(@NoteTitle))
          AND (@NoteID = -1 OR NoteID <> @NoteID)
    )
        SELECT 1; 
    ELSE
        SELECT 0; 
END
