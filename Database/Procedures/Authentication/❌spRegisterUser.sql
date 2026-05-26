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
            SELECT 'Email Already Exists' AS Message;
            ROLLBACK TRANSACTION;  
            RETURN;  
        END  
  

        IF EXISTS (  
            SELECT 1  
            FROM tblUserContact  
            WHERE PhoneNumber = @PhoneNumber  
        )  
        BEGIN  
            SELECT 'Phone Number Already Exists' AS Message;
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
  
        SELECT 'User Inserted Successfully' AS Message;
  
    END TRY  
  
    BEGIN CATCH  
  
        ROLLBACK TRANSACTION;  
  
        SELECT 'Error Occurred' AS Message;
  
    END CATCH  
  
END;


--usewr id return korte hbe to je create holo jkron then tar data gulo niye ui te kaj hbe . 
--email er phone number duplicate check korar time ee roll back kore labh nae to data to akhon oo insert hoy ni data insert hole sekhan theke roll back kor . 
--null check nae to jodio buissnes logic layer ee hbe but tao kore dewwa jaye ekhaneo
