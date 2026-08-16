CREATE OR ALTER PROCEDURE spLoginUser
(
    @Email VARCHAR(100),
    @Password VARCHAR(MAX)
)
AS
BEGIN

    SET @Email = LTRIM(RTRIM(@Email));
    SET @Password = LTRIM(RTRIM(@Password));

    DECLARE @UserID INT;

    SELECT @UserID = C.UserID
	FROM tblUserContact C
	INNER JOIN tblUserAuthentication A
		ON C.UserID = A.UserID
	WHERE C.Email = @Email
	AND A.Password COLLATE SQL_Latin1_General_CP1_CS_AS = @Password;

    IF @UserID IS NULL
    BEGIN
        SELECT 'Invalid Email Or Password' AS Message;
        RETURN;
    END

    IF EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE Active = 1
              AND UserID <> @UserID
    )
    BEGIN
        SELECT 'Another User Is Already Logged In' AS Message;
        RETURN;
    END

	IF EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE Active = 1
              AND UserID = @UserID
    )
    BEGIN
        SELECT 'Your are Already Logged In' AS Message;
        RETURN;
    END

    UPDATE tblUserAuthentication
    SET Active = 1
    WHERE UserID = @UserID;

    SELECT
        'Login Successful' AS Message,
        @UserID AS UserID;

END;
GO
