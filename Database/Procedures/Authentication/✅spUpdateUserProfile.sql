
CREATE PROCEDURE spUpdateUserProfile
    @UserID INT,
    @Name VARCHAR(100),
    @Email VARCHAR(150),
    @PhoneNumber VARCHAR(15),
    @ProfilePhoto VARBINARY(MAX)
AS
BEGIN
 
    IF EXISTS (SELECT 1 FROM tblUsers WHERE UserID = @UserID)
    BEGIN
   
        UPDATE tblUsers
        SET UserName = @Name
        WHERE UserID = @UserID;

    
        UPDATE tblUserProfile
        SET Name = @Name,
            ProfilePhoto = @ProfilePhoto
        WHERE UserID = @UserID;

       
        UPDATE tblUserContact
        SET Email = @Email,
            PhoneNumber = @PhoneNumber
        WHERE UserID = @UserID;

        PRINT 'User profile updated successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Invalid UserID.';
    END
END;
