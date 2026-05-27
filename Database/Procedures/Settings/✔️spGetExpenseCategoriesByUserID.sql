CREATE PROCEDURE spGetExpenseCategoriesByUserID
(
    @UserID INT
)
AS
BEGIN
    SET NOCOUNT ON
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END
    
    
    SELECT 
        CategoryID,
        UserID,
        CategoryName,
        IsDefault,
        IsActive
    FROM tblExpenseCategory
    WHERE IsActive = 1
    AND (UserID IS NULL OR UserID = @UserID)
    ORDER BY IsDefault DESC, CategoryName ASC

END
GO
