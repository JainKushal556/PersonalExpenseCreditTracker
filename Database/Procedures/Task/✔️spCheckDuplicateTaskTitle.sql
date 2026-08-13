CREATE PROCEDURE spCheckDuplicateTaskTitle
    @UserID INT,
    @TaskID INT,
    @TaskTitle NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1 
        FROM tblTask 
        WHERE UserID = @UserID 
          AND LOWER(TRIM(TaskTitle)) = LOWER(TRIM(@TaskTitle))
          AND (@TaskID = -1 OR TaskID <> @TaskID)
    )
        SELECT 1; 
    ELSE
        SELECT 0; 
END
