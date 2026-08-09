CREATE OR ALTER PROCEDURE spUpdateCreditSubCategoryByUserID
(
    @UserID INT,
    @ActiveStatus INT,
    @SubCategoryID INT,
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


        -- Validate SubCategoryID
        IF NOT EXISTS
        (
            SELECT 1
            FROM tblCreditSubCategory
            WHERE SubCategoryID = @SubCategoryID
        )
        BEGIN
            SELECT 'Invalid SubCategoryID' AS Message;
            RETURN;
        END;


        -- Check SubCategory Ownership and Default Status
        IF NOT EXISTS
        (
            SELECT 1
            FROM tblCreditSubCategory
            WHERE SubCategoryID = @SubCategoryID
              AND UserID = @UserID
              AND IsDefault = 0
        )
        BEGIN
            SELECT 'Cannot update default categories or categories owned by other users' AS Message;
            RETURN;
        END;


        -- Trim SubCategory Name
        SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName));


        -- Validate SubCategory Name
        IF @SubCategoryName IS NULL
           OR @SubCategoryName = ''
        BEGIN
            SELECT 'SubCategory Name Cannot Be Empty' AS Message;
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


        -- Start Transaction
        BEGIN TRANSACTION;


        -- Check Duplicate SubCategory Name
        IF EXISTS
        (
            SELECT 1
            FROM tblCreditSubCategory
            WHERE SubCategoryName = @SubCategoryName
              AND SubCategoryID <> @SubCategoryID
              AND UserID = @UserID
              AND IsActive = 1
        )
        BEGIN
            ROLLBACK TRANSACTION;

            SELECT 'SubCategory Name Already Exists for this user' AS Message;
            RETURN;
        END;


        -- Update SubCategory
        UPDATE tblCreditSubCategory
        SET
            SubCategoryName = @SubCategoryName,
            IsActive = @IsActive
        WHERE SubCategoryID = @SubCategoryID
          AND UserID = @UserID
          AND IsDefault = 0;


        -- Commit Transaction
        COMMIT TRANSACTION;


        SELECT 'Credit SubCategory Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH

        -- Rollback Transaction if Active
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END;


        -- Return Error Details
        SELECT
            'Error occurred while updating Credit SubCategory' AS Message,
            ERROR_MESSAGE() AS ErrorMessage,
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_LINE() AS ErrorLine;

    END CATCH

END;
GO
