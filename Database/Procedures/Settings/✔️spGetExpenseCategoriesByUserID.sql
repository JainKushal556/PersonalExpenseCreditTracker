CREATE OR ALTER PROCEDURE spGetExpenseCategoriesByUserID
(
    @UserID INT
)
AS
BEGIN
    SET NOCOUNT OFF;
    
    -- Validate User
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message;
        RETURN;
    END;
    
    -- Fetch all Expense Categories for the User (both Active and Inactive for Settings UI)
    SELECT 
        CategoryID,
        CategoryName,
        IsDefault,
        IsActive
    FROM tblExpenseCategory
    WHERE (UserID IS NULL OR UserID = @UserID)
    ORDER BY IsDefault DESC, CategoryName ASC;

END;
GO
