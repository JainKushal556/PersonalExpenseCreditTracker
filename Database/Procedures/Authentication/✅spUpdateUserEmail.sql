--UpdateUserEmail
CREATE PROCEDURE spUpdateUserEmail
    @UserID INT,
    @Email VARCHAR(150)
AS
BEGIN

    IF EXISTS (SELECT 1 FROM tblUserContact WHERE UserID = @UserID)
    BEGIN
     
        UPDATE tblUserContact
        SET Email = @Email
        WHERE UserID = @UserID;

        PRINT 'User email updated successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Invalid UserID.';
    END
END;
