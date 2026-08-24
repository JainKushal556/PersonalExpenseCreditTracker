CREATE PROCEDURE spUpdateUserName  
    @UserID INT,  
    @Name VARCHAR(100)  
AS  
BEGIN  
  

    SET @Name = LTRIM(RTRIM(@Name));  
  

    IF @Name IS NULL OR @Name = ''  
    BEGIN  
        SELECT 'Name Cannot Be Empty' AS Message;  
        RETURN;  
    END  
  

    IF NOT EXISTS  
    (  
        SELECT 1  
        FROM tblUsers  
        WHERE UserID = @UserID  
    )  
    BEGIN  
        SELECT 'Invalid UserID' AS Message;  
        RETURN;  
    END  
  

    IF NOT EXISTS  
    (  
        SELECT 1  
        FROM tblUserAuthentication  
        WHERE UserID = @UserID  
        AND Active = 1  
    )  
    BEGIN  
        SELECT 'User Account Is Not Active' AS Message;  
        RETURN;  
    END  
  

	  IF EXISTS
	(
		SELECT 1
		FROM tblUsers
		WHERE UserName = @Name
		AND UserID <> @UserID 
	)
	BEGIN
		SELECT 'User Name Already Exists' AS Message;
		RETURN;
	END
  
    BEGIN TRY  

        BEGIN TRANSACTION;  
  
  

        UPDATE tblUsers  
        SET UserName = @Name  
        WHERE UserID = @UserID;  
  
  

        UPDATE tblUserProfile  
        SET FullName = @Name  
        WHERE UserID = @UserID;  
  

        COMMIT TRANSACTION;  
  
        SELECT 'User Name Updated Successfully' AS Message;  
  
    END TRY  
  
    BEGIN CATCH  
  

        IF @@TRANCOUNT > 0  
            ROLLBACK TRANSACTION;  
  
  
        SELECT ERROR_MESSAGE() AS Message;  
  
    END CATCH  
  
END;
GO
