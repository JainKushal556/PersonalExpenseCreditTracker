CREATE PROCEDURE spRegisterUser  
  
    @UserName VARCHAR(MAX),  
    @Email VARCHAR(100),  
    @PhoneNumber VARCHAR(15),  
    @Password VARCHAR(MAX)  
  
AS  
BEGIN  
      
    DECLARE @UserID INT;  

    BEGIN TRANSACTION;

    BEGIN TRY

      
        IF EXISTS (
            SELECT 1
            FROM tblUserContact
            WHERE Email = @Email
        )
        BEGIN
            PRINT 'Email Already Exists';
            ROLLBACK TRANSACTION;
            RETURN;
        END

      
        IF EXISTS (
            SELECT 1
            FROM tblUserContact
            WHERE PhoneNumber = @PhoneNumber
        )
        BEGIN
            PRINT 'Phone Number Already Exists';
            ROLLBACK TRANSACTION;
            RETURN;
        END

       
        INSERT INTO tblUsers (UserName)  
        VALUES (@UserName);  
  
        SET @UserID = SCOPE_IDENTITY();  
  
        INSERT INTO tblUserProfile (UserID, Name)  
        VALUES (@UserID, @UserName);  
  
     
        INSERT INTO tblUserContact (UserID, Email, PhoneNumber)  
        VALUES (@UserID, @Email, @PhoneNumber);  
  
       
        INSERT INTO tblUserAuthentication (UserID, Password, Active)  
        VALUES (@UserID, @Password, 0);  
  
        COMMIT TRANSACTION;

        PRINT 'User Inserted Successfully';

    END TRY

    BEGIN CATCH

        ROLLBACK TRANSACTION;

        PRINT 'Error Occurred';

    END CATCH

END;

