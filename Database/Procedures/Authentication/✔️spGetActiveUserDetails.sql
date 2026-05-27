ALTER PROCEDURE spGetActiveUserDetails
(
    @UserID INT
)
AS
BEGIN

    -- User Exists Check
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END


    -- Active User Check
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Is Not Active' AS Message;
        RETURN;
    END


    -- Active User Details
    SELECT
        U.UserID,
        U.UserName,
        P.ProfilePhoto,
        C.Email,
        C.PhoneNumber,
        U.CreatedAt
    FROM tblUsers U

    LEFT JOIN tblUserProfile P
        ON U.UserID = P.UserID

    LEFT JOIN tblUserContact C
        ON U.UserID = C.UserID

    WHERE U.UserID = @UserID;

END;

