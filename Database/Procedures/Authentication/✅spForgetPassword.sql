CREATE PROCEDURE spForgetPassword  
    @Email VARCHAR(100),  
    @PhoneNumber VARCHAR(15),  
    @NewPassword VARCHAR(MAX)  
AS  
BEGIN  
  
    IF EXISTS  
    (  
        SELECT 1  
        FROM tblUserContact  
        WHERE   
            Email = @Email  
            AND PhoneNumber = @PhoneNumber  
    )  
    BEGIN  
  
        UPDATE A  
        SET A.Password = @NewPassword  
        FROM tblUserAuthentication A  
        INNER JOIN tblUserContact C  
            ON A.UserID = C.UserID  
        WHERE   
            C.Email = @Email  
            AND C.PhoneNumber = @PhoneNumber;  
  
        PRINT 'Password Reset Successfully';  
  
    END  
  
    ELSE  
    BEGIN  
        PRINT 'Invalid Email Or Phone Number';  
    END  
  
END;