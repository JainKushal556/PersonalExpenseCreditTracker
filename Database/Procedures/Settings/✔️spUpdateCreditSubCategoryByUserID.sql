CREATE OR ALTER PROCEDURE spUpdateCreditSubCategoryByUserID  
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
    DECLARE @CategoryID INT;  
    DECLARE @ExistingSubCategoryName VARCHAR(100);  
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
  
        -- Validate SubCategoryID and Fetch Existing Data  
        SELECT 
            @CategoryID = CategoryID,  
            @ExistingSubCategoryName = SubCategoryName,  
            @ExistingIsDefault = IsDefault,  
            @ExistingUserID = UserID  
        FROM tblCreditSubCategory   
        WHERE SubCategoryID = @SubCategoryID;  

        IF @ExistingSubCategoryName IS NULL  
        BEGIN  
            SELECT 'Invalid SubCategoryID' AS Message;  
            RETURN;  
        END;  
  
        -- Trim SubCategory Name  
        SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName));  
  
        IF @SubCategoryName IS NULL  
           OR @SubCategoryName = ''  
        BEGIN  
            SELECT 'SubCategory Name Cannot Be Empty' AS Message;  
            RETURN;  
        END;  
  
        -- Cannot change name of default subcategories or subcategories owned by other users
        IF (@ExistingIsDefault = 1 OR (@ExistingUserID IS NOT NULL AND @ExistingUserID <> @UserID))  
           AND @SubCategoryName <> @ExistingSubCategoryName  
        BEGIN  
            SELECT 'Cannot update default sub categories or subcategories owned by other users' AS Message;  
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
  
        -- Check Duplicate SubCategory Name if name is changed  
        IF @SubCategoryName <> @ExistingSubCategoryName  
        BEGIN  
            -- Check Duplicate SubCategory for Default under this CategoryID 
            IF EXISTS  
            (  
                SELECT 1  
                FROM tblCreditSubCategory  
                WHERE SubCategoryName = @SubCategoryName  
                  AND CategoryID = @CategoryID  
                  AND IsDefault = 1  
            )  
            BEGIN  
                ROLLBACK TRANSACTION;  
                SELECT 'Sub Category Already Exists' AS Message;  
                RETURN;  
            END;  

            -- Check Duplicate SubCategory for User under this CategoryID (excluding current SubCategoryID)
            IF EXISTS  
            (  
                SELECT 1  
                FROM tblCreditSubCategory  
                WHERE SubCategoryName = @SubCategoryName  
                  AND SubCategoryID <> @SubCategoryID  
                  AND CategoryID = @CategoryID  
                  AND UserID = @UserID  
            )  
            BEGIN  
                ROLLBACK TRANSACTION;  

                IF EXISTS  
                (  
                    SELECT 1  
                    FROM tblCreditSubCategory  
                    WHERE SubCategoryName = @SubCategoryName  
                      AND SubCategoryID <> @SubCategoryID  
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
        END;  
  
        -- Update SubCategory  
        UPDATE tblCreditSubCategory  
        SET SubCategoryName = @SubCategoryName,  
            IsActive = @IsActive  
        WHERE SubCategoryID = @SubCategoryID;  
  
        -- Commit Transaction  
        COMMIT TRANSACTION;  
  
        SELECT 'Credit Sub Category Updated Successfully' AS Message;  
  
    END TRY  
  
    BEGIN CATCH  
        IF @@TRANCOUNT > 0  
        BEGIN  
            ROLLBACK TRANSACTION;  
        END;  

        SELECT  
            'Error occurred while updating Credit Sub Category' AS Message,  
            ERROR_MESSAGE() AS ErrorMessage,  
            ERROR_NUMBER() AS ErrorNumber,  
            ERROR_LINE() AS ErrorLine;  
    END CATCH  
END;  
GO
