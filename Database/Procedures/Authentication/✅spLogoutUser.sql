CREATE PROCEDURE spLogoutUser
    @UserID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
              AND Active = 1
    )
    BEGIN
        SELECT 'User Already Logout Or Invalid UserID' AS Message;
        RETURN;
    END

    UPDATE tblUserAuthentication
    SET Active = 0
    WHERE UserID = @UserID;

    SELECT 'Logout Successful' AS Message;

END;
