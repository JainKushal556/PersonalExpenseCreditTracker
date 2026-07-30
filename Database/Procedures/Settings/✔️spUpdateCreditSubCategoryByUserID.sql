CREATE PROCEDURE spUpdateCreditSubCategoryByUserID
(
  @UserID INT,
  @SubCategoryID INT,
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
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END

    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditSubCategory 
        WHERE SubCategoryID = @SubCategoryID
    )
    BEGIN
        SELECT 'Invalid SubCategoryID' AS MESSAGE
        RETURN 
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot update default subcategories or subcategories owned by other users' AS MESSAGE
        RETURN
    END

    
    SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName))

    IF @SubCategoryName IS NULL
    OR @SubCategoryName = ''
    BEGIN
        SELECT 'SubCategory Name Cannot Be Empty' AS MESSAGE
        RETURN
    END

    
    IF EXISTS
    (
        SELECT 1
        FROM tblCreditSubCategory
        WHERE SubCategoryName = @SubCategoryName
        AND SubCategoryID != @SubCategoryID
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'SubCategory Name Already Exists for this user' AS MESSAGE
        RETURN
    END

    
    UPDATE tblCreditSubCategory
    SET SubCategoryName = @SubCategoryName
    WHERE SubCategoryID = @SubCategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Credit SubCategory Updated Successfully' AS MESSAGE

END
GO
