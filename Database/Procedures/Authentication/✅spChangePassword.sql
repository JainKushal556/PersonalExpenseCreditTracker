CREATE PROCEDURE spChangePassword  
(  
    @UserID INT,  
    @OldPassword VARCHAR(MAX),  
    @NewPassword VARCHAR(MAX)  
)  
AS  
BEGIN  
  

    IF EXISTS  
    (  
        SELECT 1  
        FROM tblUserAuthentication  
        WHERE  
            UserID = @UserID  
            AND Password = @OldPassword  
    )  
    BEGIN  
  
      
        UPDATE tblUserAuthentication  
        SET Password = @NewPassword  
        WHERE UserID = @UserID;  
  
        PRINT 'Password Changed Successfully';  
  
    END  
  
    ELSE  
    BEGIN  
        PRINT 'Invalid Old Password';  
    END  
  
END;
