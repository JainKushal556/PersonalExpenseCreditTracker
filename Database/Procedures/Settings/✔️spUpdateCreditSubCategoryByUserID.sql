CREATE or ALTER	 PROCEDURE [dbo].[spUpdateCreditSubCategoryByUserID]  
(  
    @UserID INT,  
    @SubCategoryID INT,  
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
        FROM tblCreditSubCategory   
        WHERE SubCategoryID = @SubCategoryID  
        )  
        BEGIN  
            SELECT 'Invalid CategoryID' AS Message;  
            RETURN;  
        END;  
  
  
        -- Check Category Ownership and Default Status  
        IF NOT EXISTS  
        (  
            SELECT 1  
        FROM tblCreditSubCategory  
        WHERE SubCategoryID = @SubCategoryID  
        AND UserID = @UserID  
        AND IsDefault = 0  
        )  
        BEGIN  
            SELECT 'Cannot update default sub categories or categories owned by other users' AS Message;  
            RETURN;  
        END;  
  
  
        -- Trim Category Name  
        SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName))  
  
    IF @SubCategoryName IS NULL  
    OR @SubCategoryName = ''  
    BEGIN  
        SELECT 'SubCategory Name Cannot Be Empty' AS MESSAGE  
        RETURN  
 END  
  
  
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
        FROM tblCreditSubCategory  
        WHERE SubCategoryName = @SubCategoryName  
        AND SubCategoryID != @SubCategoryID  
        AND UserID = @UserID  
        AND IsActive = 1  
        )  
        BEGIN  
            ROLLBACK TRANSACTION;  
  
            SELECT 'Sub Category Name Already Exists for this user' AS Message;  
            RETURN;  
        END;  
  
  
        -- Update Category  
       UPDATE tblCreditSubCategory  
    SET SubCategoryName = @SubCategoryName  
    WHERE SubCategoryID = @SubCategoryID  
    AND UserID = @UserID  
    AND IsDefault = 0  
  
  
        -- Commit Transaction  
        COMMIT TRANSACTION;  
  
  
        SELECT 'Credit Sub Category Updated Successfully' AS Message;  
  
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
