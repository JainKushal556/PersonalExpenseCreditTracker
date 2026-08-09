CREATE PROCEDURE spGetUserCurrentPassword
    @UserID INT
AS
BEGIN
    SET NOCOUNT OFF;

    SELECT Password
    FROM tblUserAuthentication
    WHERE UserID = @UserID
      AND Active = 1;
END;
