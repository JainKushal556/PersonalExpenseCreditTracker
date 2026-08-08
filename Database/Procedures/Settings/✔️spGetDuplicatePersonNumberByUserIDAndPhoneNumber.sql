CREATE PROC spGetDuplicatePersonNumberByUserIDAndPhoneNumber
(
    @UserID INT,
    @PersonID INT,
    @PhoneNumber VARCHAR(20)
)
AS
BEGIN
    SET NOCOUNT OFF;

    BEGIN TRY

        -- UserID Validation
        IF @UserID IS NULL OR @UserID <= 0
        BEGIN
            SELECT
                CAST(0 AS BIT) AS IsDuplicate,
                'User ID is Invalid.' AS Message;
            RETURN;
        END

        SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));

        IF @PhoneNumber = '' OR @PhoneNumber IS NULL
        BEGIN
            SELECT
                CAST(0 AS BIT) AS IsDuplicate,
                'Phone Number is Null' AS Message;
            RETURN;
        END

        -------------------------------------------------------
        -- PersonID Validation (Only for Edit)
        -------------------------------------------------------
        IF @PersonID <> -1
        BEGIN
            IF @PersonID <= 0
            BEGIN
                SELECT
                    CAST(0 AS BIT) AS IsDuplicate,
                    'Invalid Person ID.' AS Message;
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
                SELECT
                    CAST(0 AS BIT) AS IsDuplicate,
                    'Person Not Found.' AS Message;
                RETURN;
            END
        END

        -------------------------------------------------------
        -- Duplicate Phone Number Check
        -------------------------------------------------------
        IF EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE UserID = @UserID
              AND PhoneNumber = @PhoneNumber
              AND
              (
                    @PersonID = -1
                    OR PersonID <> @PersonID
              )
        )
        BEGIN
            SELECT
                CAST(1 AS BIT) AS IsDuplicate,
                'Phone Number Already Exists.' AS Message;
        END
        ELSE
        BEGIN
            SELECT
                CAST(0 AS BIT) AS IsDuplicate,
                'Phone Number Available.' AS Message;
        END

    END TRY

    BEGIN CATCH

        SELECT
            CAST(0 AS BIT) AS IsDuplicate,
            ERROR_MESSAGE() AS Message;

    END CATCH
END
GO
