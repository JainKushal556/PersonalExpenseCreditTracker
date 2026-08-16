CREATE OR ALTER PROCEDURE spForgetPassword
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

        IF EXISTS
        (
            SELECT 1
            FROM tblUserAuthentication A
            INNER JOIN tblUserContact C
                ON A.UserID = C.UserID
            WHERE
                C.Email = @Email
                AND C.PhoneNumber = @PhoneNumber
                AND A.Password = @NewPassword
        )
        BEGIN
            SELECT 'New Password Same As Old Password' AS Message;
        END

        ELSE
        BEGIN
  
            UPDATE A  
            SET A.Password = @NewPassword  
            FROM tblUserAuthentication A  
            INNER JOIN tblUserContact C  
                ON A.UserID = C.UserID  
            WHERE   
                C.Email = @Email  
                AND C.PhoneNumber = @PhoneNumber;  
  
            SELECT 'Password Reset Successfully' AS Message;

        END
  
    END  
  
    ELSE  
    BEGIN  
        SELECT 'Invalid Email Or Phone Number' AS Message;
    END  
  
END;
GO