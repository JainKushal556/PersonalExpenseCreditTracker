CREATE OR ALTER PROCEDURE spInsertNewExpenseSubCategoryByUserID    
(    
    @UserID INT,    
    @CategoryID INT,    
    @ActiveStatus INT,    
    @SubCategoryName VARCHAR(MAX)    
)    
AS    
BEGIN    
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
    
        -- Trim SubCategory Name    
        SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName));    
    
        -- Validate SubCategory Name    
        IF @SubCategoryName IS NULL    
           OR @SubCategoryName = ''    
        BEGIN    
            SELECT 'Sub Category Name cannot be empty' AS Message;    
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
    
        -- Check Duplicate SubCategory for Default under this CategoryID
        IF EXISTS  
        (  
            SELECT 1  
            FROM tblExpenseSubCategory  
            WHERE SubCategoryName = @SubCategoryName  
              AND CategoryID = @CategoryID  
              AND IsDefault = 1  
        )  
        BEGIN  
            ROLLBACK TRANSACTION;  
            SELECT 'Sub Category Already Exists' AS Message;  
            RETURN;  
        END;  

        -- Check Duplicate SubCategory for User under this CategoryID
        IF EXISTS  
        (  
            SELECT 1  
            FROM tblExpenseSubCategory  
            WHERE SubCategoryName = @SubCategoryName  
              AND CategoryID = @CategoryID  
              AND UserID = @UserID  
        )  
        BEGIN  
            ROLLBACK TRANSACTION;  

            IF EXISTS  
            (  
                SELECT 1  
                FROM tblExpenseSubCategory  
                WHERE SubCategoryName = @SubCategoryName  
                  AND CategoryID = @CategoryID  
                  AND UserID = @UserID  
                  AND IsActive = 0  
            )  
            BEGIN  
                SELECT 'Sub Category Already Exists But It Is Inactive. Please Active It' AS Message;  
                RETURN;  
            END;  

            SELECT 'Sub Category Already Exists' AS Message;  
            RETURN;  
        END;  
    
        -- Insert SubCategory    
        INSERT INTO tblExpenseSubCategory    
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
    
        SELECT 'Expense Sub Category Inserted Successfully' AS Message;    
    
    END TRY    
    
    BEGIN CATCH    
        IF @@TRANCOUNT > 0    
        BEGIN    
            ROLLBACK TRANSACTION;    
        END;    
    
        SELECT    
            'Error occurred while inserting Expense Sub Category' AS Message,    
            ERROR_MESSAGE() AS ErrorMessage,    
            ERROR_NUMBER() AS ErrorNumber,    
            ERROR_LINE() AS ErrorLine;    
    
    END CATCH    
END;  
GO
