CREATE PROCEDURE spUpdateUserPhoneNumber
    @UserID INT,
    @PhoneNumber VARCHAR(15)
AS
BEGIN

    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));


    IF @PhoneNumber IS NULL OR @PhoneNumber = ''
    BEGIN
        SELECT 'Phone Number Cannot Be Empty' AS Message;
        RETURN;
    END


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
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE PhoneNumber = @PhoneNumber
        AND UserID <> @UserID
    )
    BEGIN
        SELECT 'Phone Number Already Exists' AS Message;
        RETURN;
    END


    BEGIN TRY

        UPDATE tblUserContact
        SET PhoneNumber = @PhoneNumber
        WHERE UserID = @UserID;

        SELECT 'User Phone Number Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;
