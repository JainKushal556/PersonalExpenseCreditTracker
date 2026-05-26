CREATE PROCEDURE spUpdateUserEmail  
    @UserID INT,  
    @Email VARCHAR(150)  
AS
BEGIN      

  
    IF @Email IS NULL OR LTRIM(RTRIM(@Email)) = ''
    BEGIN
        SELECT 'Email Cannot Be Empty' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

   
    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE Email = @Email
        AND UserID <> @UserID
    )
    BEGIN
        SELECT 'Email Already Exists' AS Message;
        RETURN;
    END


    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE UserID = @UserID
    )    
    BEGIN               

        UPDATE tblUserContact        
        SET Email = @Email         
        WHERE UserID = @UserID;         

        SELECT 'User Email Updated Successfully' AS Message;

    END    

    ELSE     
    BEGIN         
        SELECT 'Invalid UserID' AS Message;
    END 

END;