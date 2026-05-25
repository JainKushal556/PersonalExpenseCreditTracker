--UpdateUserProfilePhoto
CREATE PROCEDURE spUpdateProfilePhoto  
(  
    @UserID INT,  
    @ProfilePhoto VARBINARY(MAX)  
)  
AS  
BEGIN  
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
  
        PRINT 'Profile Photo Updated Successfully';  
    END  
    ELSE  
    BEGIN  
        PRINT 'User Not Found';  
    END  
END;