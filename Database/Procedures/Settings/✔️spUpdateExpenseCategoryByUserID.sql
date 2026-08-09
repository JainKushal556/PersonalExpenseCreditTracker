CREATE OR ALTER PROCEDURE spUpdateExpenseCategoryByUserID
(
    @UserID INT,
    @CategoryID INT,
    @ActiveStatus INT,
    @CategoryName VARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;

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


        -- Validate CategoryID
        IF NOT EXISTS
        (
            SELECT 1
            FROM tblExpenseCategory
            WHERE CategoryID = @CategoryID
        )
        BEGIN
            SELECT 'Invalid CategoryID' AS Message;
            RETURN;
        END;


        -- Check Category Ownership and Default Status
        IF NOT EXISTS
        (
            SELECT 1
            FROM tblExpenseCategory
            WHERE CategoryID = @CategoryID
              AND UserID = @UserID
              AND IsDefault = 0
        )
        BEGIN
            SELECT 'Cannot update default categories or categories owned by other users' AS Message;
            RETURN;
        END;


        -- Trim Category Name
        SET @CategoryName = LTRIM(RTRIM(@CategoryName));


        -- Validate Category Name
        IF @CategoryName IS NULL
           OR @CategoryName = ''
        BEGIN
            SELECT 'Category Name Cannot Be Empty' AS Message;
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


        -- Check Duplicate Category Name
        IF EXISTS
        (
            SELECT 1
            FROM tblExpenseCategory
            WHERE CategoryName = @CategoryName
              AND CategoryID <> @CategoryID
              AND UserID = @UserID
              AND IsActive = 1
        )
        BEGIN
            ROLLBACK TRANSACTION;

            SELECT 'Category Name Already Exists for this user' AS Message;
            RETURN;
        END;


        -- Update Category
        UPDATE tblExpenseCategory
        SET
            CategoryName = @CategoryName,
            IsActive = @IsActive
        WHERE CategoryID = @CategoryID
          AND UserID = @UserID
          AND IsDefault = 0;


        -- Commit Transaction
        COMMIT TRANSACTION;


        SELECT 'Expense Category Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH

        -- Rollback Transaction if Active
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END;


        -- Return Error Details
        SELECT
            'Error occurred while updating Expense Category' AS Message,
            ERROR_MESSAGE() AS ErrorMessage,
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_LINE() AS ErrorLine;

    END CATCH

END;
GO
