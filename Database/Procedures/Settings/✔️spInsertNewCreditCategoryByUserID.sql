CREATE PROCEDURE spInsertNewCreditCategoryByUserID
(
   @UserID INT,
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
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END
    
    
    SET @CategoryName = LTRIM(RTRIM(@CategoryName))
    
    IF @CategoryName IS NULL
    OR @CategoryName = ''
    BEGIN
        SELECT 'Category Name cannot be empty' AS Message
        RETURN
    END
    
    
    IF EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryName = @CategoryName
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'Category Already Exists for this user' AS Message
        RETURN
    END
    
    
    INSERT INTO tblCreditCategory(UserID, CategoryName, IsDefault, IsActive)
    VALUES(@UserID, @CategoryName, 0, 1)
    
    SELECT 'Credit Category Inserted Successfully' AS Message

END
GO
