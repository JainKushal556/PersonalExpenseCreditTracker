CREATE PROCEDURE spDeleteCreditCategoryByUserID
(
 @UserID INT,
 @CategoryID INT
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
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS MESSAGE
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot delete default categories or categories owned by other users' AS MESSAGE
        RETURN
    END

    UPDATE tblCreditCategory
    SET IsActive = 0
    WHERE CategoryID = @CategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Credit Category Deleted Successfully' AS Message
END
GO
