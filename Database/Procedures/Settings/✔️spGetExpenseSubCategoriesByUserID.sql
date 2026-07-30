CREATE PROCEDURE spGetExpenseSubCategoriesByUserID
(
    @UserID INT
)
AS
BEGIN
    SET NOCOUNT OFF
    
    
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
        SubCategoryID,
        CategoryID,
        UserID,
        SubCategoryName,
        IsDefault,
        IsActive
    FROM tblExpenseSubCategory
    WHERE IsActive = 1
    AND (UserID IS NULL OR UserID = @UserID)
    ORDER BY IsDefault DESC, SubCategoryName ASC

END
GO
