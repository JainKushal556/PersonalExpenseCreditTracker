CREATE PROCEDURE spRegisterUser    

    @UserName VARCHAR(100),    
    @Email VARCHAR(100),    
    @PhoneNumber VARCHAR(15),    
    @Password VARCHAR(100)    

AS    
BEGIN    

    SET XACT_ABORT ON;

    DECLARE @UserID INT;    


    SET @UserName = LTRIM(RTRIM(@UserName));
    SET @Email = LTRIM(RTRIM(@Email));
    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));
    SET @Password = LTRIM(RTRIM(@Password));

	--empty and null checked
    IF @UserName IS NULL OR @UserName = ''
    BEGIN
        SELECT 0 AS Success, 'User Name Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @Email IS NULL OR @Email = ''
    BEGIN
        SELECT 0 AS Success, 'Email Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @PhoneNumber IS NULL OR @PhoneNumber = ''
    BEGIN
        SELECT 0 AS Success, 'Phone Number Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @Password IS NULL OR @Password = ''
    BEGIN
        SELECT 0 AS Success, 'Password Cannot Be Empty' AS Message;
        RETURN;
    END


    -- Duplicate check
    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE Email = @Email
    )
    BEGIN
        SELECT 0 AS Success, 'Email Already Exists' AS Message;
        RETURN;
    END


    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE PhoneNumber = @PhoneNumber
    )
    BEGIN
        SELECT 0 AS Success, 'Phone Number Already Exists' AS Message;
        RETURN;
    END


    BEGIN TRY

        BEGIN TRANSACTION;


        INSERT INTO tblUsers (UserName)
        VALUES (@UserName);

        SET @UserID = SCOPE_IDENTITY();

        INSERT INTO tblUserProfile
        (
            UserID,
            Name
        )
        VALUES
        (
            @UserID,
            @UserName
        );


        INSERT INTO tblUserContact
        (
            UserID,
            Email,
            PhoneNumber
        )
        VALUES
        (
            @UserID,
            @Email,
            @PhoneNumber
        );

        INSERT INTO tblUserAuthentication
        (
            UserID,
            Password,
            Active
        )
        VALUES
        (
            @UserID,
            @Password,
            0
        );


        COMMIT TRANSACTION;


        SELECT 
            1 AS Success,
            @UserID AS UserID,
            'User Inserted Successfully' AS Message;

    END TRY  

    BEGIN CATCH 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;


        SELECT 0 AS Success, ERROR_MESSAGE() AS Message;

    END CATCH  

END;



