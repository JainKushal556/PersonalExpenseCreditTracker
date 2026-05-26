CREATE PROCEDURE spUpdateUserProfile
    @UserID INT,
    @Name VARCHAR(100),
    @Email VARCHAR(150),
    @PhoneNumber VARCHAR(15),
    @ProfilePhoto VARBINARY(MAX)
AS
BEGIN

    SET @Name = LTRIM(RTRIM(@Name));
    SET @Email = LTRIM(RTRIM(@Email));
    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));


    IF @Name IS NULL OR @Name = ''
    BEGIN
        SELECT 'Name Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @Email IS NULL OR @Email = ''
    BEGIN
        SELECT 'Email Cannot Be Empty' AS Message;
        RETURN;
    END

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
    FROM tblUsers
    WHERE UserName = @Name
)
BEGIN
    SELECT 'User Name Already Exists' AS Message;
    RETURN;
END


IF EXISTS
(
    SELECT 1
    FROM tblUserContact
    WHERE Email = @Email
)
BEGIN
    SELECT 'Email Already Exists' AS Message;
    RETURN;
END


IF EXISTS
(
    SELECT 1
    FROM tblUserContact
    WHERE PhoneNumber = @PhoneNumber
)
BEGIN
    SELECT 'Phone Number Already Exists' AS Message;
    RETURN;
END


    BEGIN TRY


        BEGIN TRANSACTION;


        UPDATE tblUsers
        SET UserName = @Name
        WHERE UserID = @UserID;

        UPDATE tblUserProfile
        SET Name = @Name,
            ProfilePhoto = @ProfilePhoto
        WHERE UserID = @UserID;

        UPDATE tblUserContact
        SET Email = @Email,
            PhoneNumber = @PhoneNumber
        WHERE UserID = @UserID;

        COMMIT TRANSACTION;


        SELECT 'User Profile Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;


        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;
