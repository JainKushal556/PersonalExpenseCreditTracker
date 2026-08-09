CREATE OR ALTER PROCEDURE spGetCreditSubCategoriesByUserID
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
    
    -- Fetch all Credit SubCategories for the User (both Active and Inactive for Settings UI)
    SELECT 
        SubCategoryID,
        CategoryID,
        UserID,
        SubCategoryName,
        IsDefault,
        IsActive
    FROM tblCreditSubCategory
    WHERE (UserID IS NULL OR UserID = @UserID)
    ORDER BY IsDefault DESC, SubCategoryName ASC;

END;
GO
