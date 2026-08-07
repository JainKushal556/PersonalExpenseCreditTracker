CREATE PROCEDURE spUpdateUserProfile
    @UserID INT,
    @FullName VARCHAR(100),
    @Email VARCHAR(150),
    @PhoneNumber VARCHAR(15),
    @Address VARCHAR(500),
    @DOB DATE,
    @GenderID INT
AS
BEGIN
    SET NOCOUNT OFF;

    SET @FullName = LTRIM(RTRIM(@FullName));
    SET @Email = LTRIM(RTRIM(@Email));
    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));
    SET @Address = LTRIM(RTRIM(@Address));

    IF @FullName IS NULL OR @FullName = ''
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

    -- Check if Email already belongs to another user
    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE Email = @Email
        AND UserID != @UserID
    )
    BEGIN
        SELECT 'Email Already Exists' AS Message;
        RETURN;
    END

    -- Check if PhoneNumber already belongs to another user
    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE PhoneNumber = @PhoneNumber
        AND UserID != @UserID
    )
    BEGIN
        SELECT 'Phone Number Already Exists' AS Message;
        RETURN;
    END

    -- Check GenderID if provided
    IF @GenderID IS NOT NULL AND @GenderID > 0 AND NOT EXISTS
    (
        SELECT 1
        FROM tblGender
        WHERE GenderID = @GenderID
    )
    BEGIN
        SELECT 'Invalid Gender' AS Message;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE tblUsers
        SET UserName = @FullName
        WHERE UserID = @UserID;

        IF EXISTS (SELECT 1 FROM tblUserProfile WHERE UserID = @UserID)
        BEGIN
            UPDATE tblUserProfile
            SET FullName = @FullName,
                DOB = @DOB,
                GenderID = CASE WHEN @GenderID > 0 THEN @GenderID ELSE GenderID END,
                Address = @Address
            WHERE UserID = @UserID;
        END
        ELSE
        BEGIN
            INSERT INTO tblUserProfile (UserID, FullName, DOB, GenderID, Address)
            VALUES (@UserID, @FullName, @DOB, CASE WHEN @GenderID > 0 THEN @GenderID ELSE NULL END, @Address);
        END

        IF EXISTS (SELECT 1 FROM tblUserContact WHERE UserID = @UserID)
        BEGIN
            UPDATE tblUserContact
            SET Email = @Email,
                PhoneNumber = @PhoneNumber
            WHERE UserID = @UserID;
        END
        ELSE
        BEGIN
            INSERT INTO tblUserContact (UserID, Email, PhoneNumber)
            VALUES (@UserID, @Email, @PhoneNumber);
        END

        COMMIT TRANSACTION;

        SELECT 'User Profile Updated Successfully' AS Message;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO
