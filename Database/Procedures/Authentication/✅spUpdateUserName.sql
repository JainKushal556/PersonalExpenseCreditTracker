

CREATE PROCEDURE spUpdateUserName
    @UserID INT,
    @Name VARCHAR(100)
AS
BEGIN
   
    IF EXISTS (SELECT 1 FROM tblUsers WHERE UserID = @UserID)
    BEGIN
      
        UPDATE tblUsers
        SET UserName = @Name
        WHERE UserID = @UserID;

    
        UPDATE tblUserProfile
        SET Name = @Name
        WHERE UserID = @UserID;

        PRINT 'User name updated successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Invalid UserID.';
    END
END;
