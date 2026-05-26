CREATE PROCEDURE spUpdateProfilePhoto    
    
    @UserID INT,    
    @ProfilePhoto VARBINARY(MAX)    
    
AS    
BEGIN    

   
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
        FROM tblUserProfile    
        WHERE UserID = @UserID    
    )    
    BEGIN    

        UPDATE tblUserProfile    
        SET ProfilePhoto = @ProfilePhoto    
        WHERE UserID = @UserID;    
    
        SELECT 'Profile Photo Updated Successfully' AS Message;

    END    

    ELSE    
    BEGIN    
        SELECT 'User Not Found' AS Message;
    END    

END;