CREATE or ALTER  PROCEDURE spInsertNewCreditSubCategoryByUserID  
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
  
  
        -- Trim Category Name  
        SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName));  
  
  
        -- Validate Category Name  
        IF @SubCategoryName IS NULL  
           OR @SubCategoryName = ''  
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
  
  
        -- Check Duplicate Category  
        IF EXISTS  
        (  
            SELECT 1  
            FROM tblCreditSubCategory  
            WHERE SubCategoryName = @SubCategoryName  
              AND UserID = @UserID  
              AND IsActive = 1  
        )  
        BEGIN  
            ROLLBACK TRANSACTION;  
  
            SELECT 'Category Already Exists for this user' AS Message;  
            RETURN;  
        END;  
  
  
        -- Insert Category  
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
   )  
  
  
        -- Commit Transaction  
        COMMIT TRANSACTION;  
  
  
        SELECT 'Credit Sub Category Inserted Successfully' AS Message;  
  
    END TRY  
  
    BEGIN CATCH  
  
        -- Rollback if transaction is active  
        IF @@TRANCOUNT > 0  
        BEGIN  
            ROLLBACK TRANSACTION;  
        END;  
  
  
        -- Return Error Information  
        SELECT  
            'Error occurred while inserting Expense Category' AS Message,  
            ERROR_MESSAGE() AS ErrorMessage,  
            ERROR_NUMBER() AS ErrorNumber,  
            ERROR_LINE() AS ErrorLine;  
  
    END CATCH  
  
END;
GO
