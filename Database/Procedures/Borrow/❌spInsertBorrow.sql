CREATE PROCEDURE spInsertBorrow
(
    @UserID INT,
    @PersonID INT,
    @PaymentID INT,
    @StatusID INT,
    @Amount DECIMAL(10,2),
    @DeadlineAt DATETIME,
    @Description VARCHAR(MAX)
)
AS
BEGIN

    BEGIN TRY

        -- Validation
        IF @UserID IS NULL OR @UserID <= 0
        BEGIN
            RAISERROR('Invalid UserID.',16,1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM tblUsers WHERE UserID = @UserID)
        BEGIN
            RAISERROR('User does not exist.',16,1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM tblPersons WHERE PersonID = @PersonID)
        BEGIN
            RAISERROR('Person does not exist.',16,1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentID = @PaymentID)
        BEGIN
            RAISERROR('Payment type does not exist.',16,1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusID = @StatusID)
        BEGIN
            RAISERROR('Status does not exist.',16,1);
            RETURN;
        END

        IF @Amount IS NULL OR @Amount <= 0
        BEGIN
            RAISERROR('Amount must be greater than zero.',16,1);
            RETURN;
        END

        IF @DeadlineAt IS NULL
        BEGIN
            RAISERROR('Deadline date is required.',16,1);
            RETURN;
        END

        -------------------------------------------------
        -- Insert into Borrow Table
        -------------------------------------------------

        INSERT INTO tblBorrow
        (
            UserID,
            PersonID,
            PaymentID,
            StatusID,
            Amount,
            PaidAmount,
            RemainingAmount,
            BorrowAt,
            DeadlineAt,
            Description
        )
        VALUES
        (
            @UserID,
            @PersonID,
            @PaymentID,
            @StatusID,
            @Amount,
            0,
            @Amount,
            GETDATE(),
            @DeadlineAt,
            @Description
        );

        -------------------------------------------------
        -- Insert into Credit Table Automatically
        -------------------------------------------------

        INSERT INTO tblCredit
        (
            UserID,
            CategoryID,
            SubCategoryID,
            PaymentID,
            Amount,
            Description,
            CreditAt
        )
        VALUES
        (
            @UserID,
            4,
            4,
            @PaymentID,
            @Amount,
            'Borrow Amount Credited : ' + ISNULL(@Description,''),
            GETDATE()
        );

        PRINT 'Borrow transaction inserted successfully.';
        PRINT 'Credit transaction inserted successfully.';

    END TRY

    BEGIN CATCH

        PRINT ERROR_MESSAGE();

    END CATCH
END;




--transaction use korte hbe commit rollback jatye 2to table ae insert hoy properly
--user active and exist both validate korte hbe
--user tar add kora person ee use korbe tai ote user id ache table ee so seta dekh 
--directly id na pass koriye name diye id search kor then ota de 
--print er jaygay select use korte hbe 
