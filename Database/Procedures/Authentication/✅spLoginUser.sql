CREATE PROCEDURE spLoginUser  
(  
    @Email VARCHAR(100),  
    @Password VARCHAR(MAX)  
)  
AS  
BEGIN  
      
  
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
        PRINT 'User Exists';  
    END  
    ELSE  
    BEGIN  
        PRINT 'Invalid Email Or Password';  
    END  
END;

