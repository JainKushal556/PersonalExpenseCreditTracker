CREATE PROCEDURE spGetActiveUserDetails
(
    @UserID INT
)
AS
BEGIN
    SET NOCOUNT OFF;

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END;

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
    END;

    SELECT
        U.UserID,
        P.FullName,
        P.ProfilePhoto,
        C.Email,
        C.PhoneNumber,
        U.CreatedAt AS MemberSince,
        A.Active AS AccountStatus,
        P.DOB,
        G.GenderName AS Gender,
        P.Address
    FROM tblUsers U
    LEFT JOIN tblUserProfile P
        ON U.UserID = P.UserID
    LEFT JOIN tblUserContact C
        ON U.UserID = C.UserID
    LEFT JOIN tblUserAuthentication A
        ON U.UserID = A.UserID
    LEFT JOIN tblGender G
        ON P.GenderID = G.GenderID
    WHERE U.UserID = @UserID;
END;
GO
