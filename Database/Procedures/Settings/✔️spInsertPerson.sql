CREATE PROC spInsertPerson
(
    @UserID INT,
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

        SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));
        SET @PersonName = LTRIM(RTRIM(@PersonName));
        SET @Address = LTRIM(RTRIM(@Address));

        IF @PersonName = '' OR @PersonName = NULL
        BEGIN
            SELECT 'Person Name is Null' AS Message;
            RETURN;
        END

        IF @PhoneNumber = '' OR @PhoneNumber = NULL
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
        )
        BEGIN
            SELECT 'Phone Number Already Taken' AS Message;
            RETURN;
        END


        INSERT INTO tblPersons
        (
            UserID,
            PersonName,
            PhoneNumber,
            Address
        )
        VALUES
        (
            @UserID,
            @PersonName,
            @PhoneNumber,
            @Address
        );

     SELECT 'Person Details Inserted Successfully' AS Message;

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH
END