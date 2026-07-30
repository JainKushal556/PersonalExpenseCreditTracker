CREATE PROCEDURE spDeleteExpenseSubCategoryByUserID
(
 @UserID INT,
 @SubCategoryID INT
)
AS
BEGIN
    SET NOCOUNT OFF
    
    
    IF NOT EXISTS 
    (
        SELECT 1
        FROM tblUserAuthentication UserAuthentication
        WHERE UserAuthentication.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END

    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryID = @SubCategoryID
    )
    BEGIN
        SELECT 'Invalid SubCategoryID' AS MESSAGE
        RETURN
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot delete default subcategories or subcategories owned by other users' AS MESSAGE
        RETURN
    END

    
    
    UPDATE tblExpenseSubCategory
    SET IsActive = 0
    WHERE SubCategoryID = @SubCategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Expense SubCategory Deleted Successfully' AS Message
END
GO
