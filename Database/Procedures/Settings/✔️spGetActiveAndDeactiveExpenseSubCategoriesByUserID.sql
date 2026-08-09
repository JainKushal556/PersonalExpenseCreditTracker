CREATE OR ALTER PROCEDURE spGetActiveAndDeactiveExpenseSubCategoriesByUserID
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
    
    -- Fetch both Active and Inactive Expense SubCategories for Settings UI
    SELECT 
        SubCategoryID,
        CategoryID,
        UserID,
        SubCategoryName,
        IsDefault,
        IsActive
    FROM tblExpenseSubCategory
    WHERE (UserID IS NULL OR UserID = @UserID)
    ORDER BY IsDefault DESC, SubCategoryName ASC;

END;
GO
