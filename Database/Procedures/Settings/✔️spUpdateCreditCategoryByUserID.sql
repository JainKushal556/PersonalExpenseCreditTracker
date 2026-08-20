CREATE OR ALTER PROCEDURE spUpdateCreditCategoryByUserID  
(  
    @UserID INT,  
    @CategoryID INT,  
    @ActiveStatus INT,  
    @CategoryName VARCHAR(MAX)  
)  
AS  
BEGIN  
    DECLARE @IsDefault INT;  
    DECLARE @IsActive INT;  
    DECLARE @ExistingCategoryName VARCHAR(100);  
    DECLARE @ExistingIsDefault BIT;  
    DECLARE @ExistingUserID INT;  
  
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
  
        -- Validate CategoryID and Fetch Existing Data  
        SELECT 
            @ExistingCategoryName = CategoryName,  
            @ExistingIsDefault = IsDefault,  
            @ExistingUserID = UserID  
        FROM tblCreditCategory  
        WHERE CategoryID = @CategoryID;  
  
        IF @ExistingCategoryName IS NULL  
        BEGIN  
            SELECT 'Invalid CategoryID' AS Message;  
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
  
        -- Cannot change name of default categories or categories owned by other users
        IF (@ExistingIsDefault = 1 OR (@ExistingUserID IS NOT NULL AND @ExistingUserID <> @UserID))  
           AND @CategoryName <> @ExistingCategoryName  
        BEGIN  
            SELECT 'Cannot update default categories or categories owned by other users' AS Message;  
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
  
        -- Check Duplicate Category Name if name is changed  
        IF @CategoryName <> @ExistingCategoryName  
        BEGIN  
            -- Check Duplicate Category for Default 
            IF EXISTS  
            (  
                SELECT 1  
                FROM tblCreditCategory  
                WHERE CategoryName = @CategoryName  
                  AND IsDefault = 1  
            )  
            BEGIN  
                ROLLBACK TRANSACTION;  
                SELECT 'Category Already Exists' AS Message;  
                RETURN;  
            END;  

            -- Check Duplicate Category for User (excluding current CategoryID)
            IF EXISTS  
            (  
                SELECT 1  
                FROM tblCreditCategory  
                WHERE CategoryName = @CategoryName  
                  AND CategoryID <> @CategoryID  
                  AND UserID = @UserID  
            )  
            BEGIN  
                ROLLBACK TRANSACTION;  

                IF EXISTS  
                (  
                    SELECT 1  
                    FROM tblCreditCategory  
                    WHERE CategoryName = @CategoryName  
                      AND CategoryID <> @CategoryID  
                      AND UserID = @UserID  
                      AND IsActive = 0  
                )  
                BEGIN  
                    SELECT 'Category Already Exists But It Is Inactive. Please Active It' AS Message;  
                    RETURN;  
                END;  

                SELECT 'Category Already Exists' AS Message;  
                RETURN;  
            END;  
        END;  
  
        -- Update Category  
        UPDATE tblCreditCategory  
        SET CategoryName = @CategoryName,  
            IsActive = @IsActive  
        WHERE CategoryID = @CategoryID;  
  
        -- Commit Transaction  
        COMMIT TRANSACTION;  
  
        SELECT 'Credit Category Updated Successfully' AS Message;  
  
    END TRY  
  
    BEGIN CATCH  
        IF @@TRANCOUNT > 0  
        BEGIN  
            ROLLBACK TRANSACTION;  
        END;  

        SELECT  
            'Error occurred while updating Credit Category' AS Message,  
            ERROR_MESSAGE() AS ErrorMessage,  
            ERROR_NUMBER() AS ErrorNumber,  
            ERROR_LINE() AS ErrorLine;  
    END CATCH  
END;  
GO
