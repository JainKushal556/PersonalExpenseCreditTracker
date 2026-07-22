CREATE PROCEDURE spDeleteUserProfilePhotoByUserId
    @UserID INT
AS
BEGIN

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

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserProfile
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Profile Not Found' AS Message;
        RETURN;
    END

    BEGIN TRY

        UPDATE tblUserProfile
        SET ProfilePhoto = NULL
        WHERE UserID = @UserID;

        SELECT 'Profile Photo Deleted Successfully' AS Message;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;




