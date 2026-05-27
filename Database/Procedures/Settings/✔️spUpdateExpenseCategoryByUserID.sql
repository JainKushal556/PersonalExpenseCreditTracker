CREATE PROCEDURE spUpdateExpenseCategoryByUserID
(
  @UserID INT,
  @CategoryID INT,
  @CategoryName VARCHAR(MAX)
)
AS
BEGIN
    
    SET NOCOUNT ON
    
    
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
        FROM tblExpenseCategory 
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS MESSAGE
        RETURN 
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot update default categories or categories owned by other users' AS MESSAGE
        RETURN
    END

    
    SET @CategoryName = LTRIM(RTRIM(@CategoryName))

    IF @CategoryName IS NULL
    OR @CategoryName = ''
    BEGIN
        SELECT 'Category Name Cannot Be Empty' AS MESSAGE
        RETURN
    END

    
    IF EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryName = @CategoryName
        AND CategoryID != @CategoryID
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'Category Name Already Exists for this user' AS MESSAGE
        RETURN
    END

    
    UPDATE tblExpenseCategory
    SET CategoryName = @CategoryName
    WHERE CategoryID = @CategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Expense Category Updated Successfully' AS MESSAGE

END
GO
