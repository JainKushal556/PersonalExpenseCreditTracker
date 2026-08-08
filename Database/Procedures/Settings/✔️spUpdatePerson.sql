CREATE PROC spUpdatePerson
(
    @UserID INT,
    @PersonID INT,
    @PersonName VARCHAR(100),
    @PhoneNumber VARCHAR(20),
    @Address VARCHAR(MAX)
)
AS
BEGIN
    BEGIN TRY

        IF @UserID IS NULL OR @UserID <= 0
        BEGIN
            SELECT 'User ID is Null' AS Message;
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
            SELECT 'Invalid OR Inactive UserID!!' AS Message;
            RETURN;
        END

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE PersonID = @PersonID
            AND UserID = @UserID
        )
        BEGIN
            SELECT 'Invalid PersonID!!' AS Message;
            RETURN;
        END

        SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));
        SET @PersonName = LTRIM(RTRIM(@PersonName));
        SET @Address = LTRIM(RTRIM(@Address));


        IF @PersonName IS NULL
           OR @PersonName = ''
           OR UPPER(@PersonName) = 'NULL'
        BEGIN
            SELECT 'Person Name is Null' AS Message;
            RETURN;
        END

        IF @PhoneNumber IS NULL
           OR @PhoneNumber = ''
           OR UPPER(@PhoneNumber) = 'NULL'
        BEGIN
            SELECT 'Phone Number is Null' AS Message;
            RETURN;
        END

        IF EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE UserID = @UserID
            AND PhoneNumber = @PhoneNumber
            AND PersonID <> @PersonID
        )
        BEGIN
            --SELECT 'Phone Number Already Exists' AS Message;
            RETURN;
        END

        UPDATE tblPersons
        SET
            PersonName = @PersonName,
            PhoneNumber = @PhoneNumber,
            Address = @Address
        WHERE PersonID = @PersonID AND UserID = @UserID;

        --SELECT 'Person Details Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH
END
GO