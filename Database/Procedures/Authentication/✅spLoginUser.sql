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

        PRINT 'Login Successful';

        SELECT @UserID AS UserID;
    END  
    
    ELSE  
    BEGIN  
        PRINT 'Invalid Email Or Password';  
    END  

END;

