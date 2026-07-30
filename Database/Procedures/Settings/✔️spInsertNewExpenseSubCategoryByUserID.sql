CREATE PROCEDURE spInsertNewExpenseSubCategoryByUserID
(
   @UserID INT,
   @CategoryID INT,
   @SubCategoryName VARCHAR(MAX)
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
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
        AND IsActive = 1
        AND (UserID IS NULL OR UserID = @UserID)
    )
    BEGIN
        SELECT 'Invalid or inactive category' AS Message
        RETURN
    END
    
    
    SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName))
    
    IF @SubCategoryName IS NULL
    OR @SubCategoryName = ''
    BEGIN
        SELECT 'SubCategory Name cannot be empty' AS Message
        RETURN
    END
    
    
    IF EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryName = @SubCategoryName
        AND CategoryID = @CategoryID
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'SubCategory Already Exists for this user in this category' AS Message
        RETURN
    END
    
    
    INSERT INTO tblExpenseSubCategory(CategoryID, UserID, SubCategoryName, IsDefault, IsActive)
    VALUES(@CategoryID, @UserID, @SubCategoryName, 0, 1)
    
    SELECT 'Expense SubCategory Inserted Successfully' AS Message

END
GO
