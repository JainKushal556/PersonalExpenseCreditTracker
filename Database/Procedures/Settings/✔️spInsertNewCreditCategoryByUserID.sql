CREATE OR ALTER PROCEDURE spInsertNewCreditCategoryByUserID  
(  
    @UserID INT,  
    @ActiveStatus INT,  
    @CategoryName VARCHAR(MAX)  
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
  
        -- Trim Category Name  
        SET @CategoryName = LTRIM(RTRIM(@CategoryName));  
  
        -- Validate Category Name  
        IF @CategoryName IS NULL  
           OR @CategoryName = ''  
        BEGIN  
            SELECT 'Category Name cannot be empty' AS Message;  
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

        -- Check Duplicate Category for User
        IF EXISTS  
        (  
            SELECT 1  
            FROM tblCreditCategory  
            WHERE CategoryName = @CategoryName  
              AND UserID = @UserID  
        )  
        BEGIN  
            ROLLBACK TRANSACTION;  

            IF EXISTS  
            (  
                SELECT 1  
                FROM tblCreditCategory  
                WHERE CategoryName = @CategoryName  
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

        -- Insert Category  
        INSERT INTO tblCreditCategory  
        (  
            UserID,  
            CategoryName,  
            IsDefault,  
            IsActive  
        )  
        VALUES  
        (  
            @UserID,  
            @CategoryName,  
            @IsDefault,  
            @IsActive  
        );  

        -- Commit Transaction  
        COMMIT TRANSACTION;  

        SELECT 'Credit Category Inserted Successfully' AS Message;  
  
    END TRY  
  
    BEGIN CATCH  

        IF @@TRANCOUNT > 0  
        BEGIN  
            ROLLBACK TRANSACTION;  
        END;  

        SELECT  
            'Error occurred while inserting Credit Category' AS Message,  
            ERROR_MESSAGE() AS ErrorMessage,  
            ERROR_NUMBER() AS ErrorNumber,  
            ERROR_LINE() AS ErrorLine;  
  
    END CATCH  
  
END;  
GO
