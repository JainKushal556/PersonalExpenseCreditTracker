--UpdateUserPhoneNumber
CREATE PROCEDURE spUpdateUserPhoneNumber
    @UserID INT,
    @PhoneNumber VARCHAR(15)
AS
BEGIN
   
    IF EXISTS (SELECT 1 FROM tblUserContact WHERE UserID = @UserID)
    BEGIN
     
        UPDATE tblUserContact
        SET PhoneNumber = @PhoneNumber
        WHERE UserID = @UserID;

        PRINT 'User phone number updated successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Invalid UserID.';
    END
END;
