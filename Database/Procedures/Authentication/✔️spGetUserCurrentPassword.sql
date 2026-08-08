CREATE PROCEDURE spGetUserCurrentPassword
(
   @UserID INT
)
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication UserAuthentication
        WHERE UserAuthentication.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message;
        RETURN;
    END

    SELECT Password 
    FROM tblUserAuthentication
    WHERE UserID = @UserID;
END;
