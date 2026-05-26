CREATE PROCEDURE spLoginUser    
 
    @Email VARCHAR(100),    
    @Password VARCHAR(MAX)    

AS    
BEGIN    
      
    DECLARE @UserID INT;  
  
    IF EXISTS    
    (    
        SELECT 1    
        FROM tblUserContact C    
        INNER JOIN tblUserAuthentication A    
            ON C.UserID = A.UserID    
        WHERE     
            C.Email = @Email    
            AND A.Password = @Password    
    )    
    BEGIN    
    
        SELECT @UserID = C.UserID  
        FROM tblUserContact C  
        INNER JOIN tblUserAuthentication A    
            ON C.UserID = A.UserID  
        WHERE     
            C.Email = @Email    
            AND A.Password = @Password;  
  
      
        UPDATE tblUserAuthentication  
        SET Active = 1  
        WHERE UserID = @UserID;  
  
        SELECT 
            'Login Successful' AS Message,
            @UserID AS UserID;  
  
    END    
      
    ELSE    
    BEGIN    
        SELECT 'Invalid Email Or Password' AS Message;    
    END    
  
END;