--Register a new user account into the system.
Create PROCEDURE spRegisterUser  
  
    @UserName VARCHAR(MAX),  
    @Email VARCHAR(100),  
    @PhoneNumber VARCHAR(15),  
    @Password VARCHAR(MAX)  
  
AS  
BEGIN  
      
  
    DECLARE @UserID INT;  
  

    INSERT INTO tblUsers (UserName)  
    VALUES (@UserName);  
  
  
    SET @UserID = SCOPE_IDENTITY();  
  
   
    INSERT INTO tblUserProfile (UserID, Name)  
    VALUES (@UserID, @UserName);  
  
  
    INSERT INTO tblUserContact (UserID, Email, PhoneNumber)  
    VALUES (@UserID, @Email, @PhoneNumber);  
  
  
    INSERT INTO tblUserAuthentication (UserID, Password, Active)  
    VALUES (@UserID, @Password, 1);  
  
    PRINT 'User Inserted Successfully';  
END;



