CREATE OR ALTER PROCEDURE spInsertNewCreditSubCategoryByUserID
(
    @UserID INT,
    @CategoryID INT,
    @ActiveStatus INT,
    @SubCategoryName VARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT OFF;

    DECLARE @IsDefault INT;
    DECLARE @IsActive INT;

    BEGIN TRY

        -- Validate User
        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUserAuthentication AS UserAuthentication
            WHERE UserAuthentication.UserID = @UserID
              AND UserAuthentication.Active = 1
        )
        BEGIN
            SELECT 'Invalid or Inactive User' AS Message;
            RETURN;
        END;


        -- Validate Credit Category
        IF NOT EXISTS
        (
            SELECT 1
            FROM tblCreditCategory
            WHERE CategoryID = @CategoryID
              AND IsActive = 1
              AND (UserID IS NULL OR UserID = @UserID)
        )
        BEGIN
            SELECT 'Invalid or Inactive Category' AS Message;
            RETURN;
        END;


        -- Trim SubCategory Name
        SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName));


        -- Validate SubCategory Name
        IF @SubCategoryName IS NULL
           OR @SubCategoryName = ''
        BEGIN
            SELECT 'SubCategory Name cannot be empty' AS Message;
            RETURN;
        END;


        -- Validate Active Status
        IF @ActiveStatus = 1
        BEGIN
            SET @IsActive = 1;
            SET @IsDefault = 0;
        END
        ELSE IF @ActiveStatus = 0
        BEGIN
            SET @IsActive = 0;
            SET @IsDefault = 0;
        END
        ELSE
        BEGIN
            SELECT 'Please Select Valid Input' AS Message;
            RETURN;
        END;


        -- Transaction Starts
        BEGIN TRANSACTION;


        -- Check Duplicate SubCategory
        IF EXISTS
        (
            SELECT 1
            FROM tblCreditSubCategory
            WHERE SubCategoryName = @SubCategoryName
              AND CategoryID = @CategoryID
              AND UserID = @UserID
              AND IsActive = 1
        )
        BEGIN
            ROLLBACK TRANSACTION;

            SELECT 'SubCategory Already Exists for this user in this category' AS Message;
            RETURN;
        END;


        -- Insert SubCategory
        INSERT INTO tblCreditSubCategory
        (
            CategoryID,
            UserID,
            SubCategoryName,
            IsDefault,
            IsActive
        )
        VALUES
        (
            @CategoryID,
            @UserID,
            @SubCategoryName,
            @IsDefault,
            @IsActive
        );


        -- Commit Transaction
        COMMIT TRANSACTION;


        SELECT 'Credit SubCategory Inserted Successfully' AS Message;

    END TRY

    BEGIN CATCH

        -- Rollback if transaction is active
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END;


        -- Return Error Information
        SELECT
            'Error occurred while inserting Credit SubCategory' AS Message,
            ERROR_MESSAGE() AS ErrorMessage,
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_LINE() AS ErrorLine;

    END CATCH

END;
GO
