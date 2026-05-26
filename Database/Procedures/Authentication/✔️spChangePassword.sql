CREATE PROCEDURE spChangePassword  
  
    @UserID INT,  
    @OldPassword VARCHAR(MAX),  
    @NewPassword VARCHAR(MAX)  

AS  
BEGIN  
  

    IF @NewPassword IS NULL OR LTRIM(RTRIM(@NewPassword)) = ''
    BEGIN
        SELECT 'New Password Cannot Be Empty' AS Message;
        RETURN;
    END


    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE
            UserID = @UserID
            AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

 
    IF EXISTS  
    (  
        SELECT 1  
        FROM tblUserAuthentication  
        WHERE  
            UserID = @UserID  
            AND Password = @OldPassword  
    )  
    BEGIN  

     
        IF @OldPassword = @NewPassword
        BEGIN
            SELECT 'New Password Cannot Be Same As Old Password' AS Message;
        END

        ELSE
        BEGIN
  
            UPDATE tblUserAuthentication  
            SET Password = @NewPassword  
            WHERE UserID = @UserID;  
  
            SELECT 'Password Changed Successfully' AS Message;

        END
  
    END  
  
    ELSE  
    BEGIN  
        SELECT 'Invalid Old Password' AS Message;
    END  
  
END;