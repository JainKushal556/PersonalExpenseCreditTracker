CREATE PROCEDURE spInsertBorrow
(
    @UserID INT,
    @PersonID INT,
    @PaymentName VARCHAR(100),
    @StatusName VARCHAR(100),
    @Amount DECIMAL(10,2),
    @DeadlineAt DATETIME,
    @Description VARCHAR(MAX)
)
AS
BEGIN

    DECLARE @PaymentID INT;
    DECLARE @StatusID INT;
    DECLARE @CreditCategoryID INT;
    DECLARE @CreditSubCategoryID INT;

    -------------------------------------------------
    -- Trim Inputs
    -------------------------------------------------

    SET @PaymentName = LTRIM(RTRIM(@PaymentName));
    SET @StatusName = LTRIM(RTRIM(@StatusName));
    SET @Description = LTRIM(RTRIM(@Description));

    BEGIN TRY

        -------------------------------------------------
        -- UserID Validation
        -------------------------------------------------

        IF @UserID IS NULL OR @UserID <= 0
        BEGIN
            SELECT 'Invalid UserID.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- PersonID Validation
        -------------------------------------------------

        IF @PersonID IS NULL OR @PersonID <= 0
        BEGIN
            SELECT 'Invalid PersonID.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- User Exists + Active Check
        -------------------------------------------------

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUsers U
            INNER JOIN tblUserAuthentication UA
                ON U.UserID = UA.UserID
            WHERE U.UserID = @UserID
            AND UA.Active = 1
        )
        BEGIN
            SELECT 'User does not exist or inactive.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Person Belongs To User Validation
        -------------------------------------------------

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE PersonID = @PersonID
            AND UserID = @UserID
        )
        BEGIN
            SELECT 'Person does not belong to this user.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Payment Name Validation
        -------------------------------------------------

        SELECT
            @PaymentID = PaymentID
        FROM tblPaymentType
        WHERE LTRIM(RTRIM(PaymentName)) = @PaymentName;

        IF @PaymentID IS NULL
        BEGIN
            SELECT 'Invalid Payment Type.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Status Name Validation
        -------------------------------------------------

        SELECT
            @StatusID = StatusID
        FROM tblLentBorrowStatus
        WHERE LTRIM(RTRIM(StatusName)) = @StatusName;

        IF @StatusID IS NULL
        BEGIN
            SELECT 'Invalid Status.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- New Borrow Must Be Pending
        -------------------------------------------------

        IF @StatusName <> 'Pending'
        BEGIN
            SELECT 'New borrow must have Pending status.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Amount Validation
        -------------------------------------------------

        IF @Amount IS NULL OR @Amount <= 0
        BEGIN
            SELECT 'Amount must be greater than zero.' AS Message;
            RETURN;
        END

        IF @Amount > 10000000
        BEGIN
            SELECT 'Amount exceeds maximum limit.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Description Validation
        -------------------------------------------------

        IF @Description IS NULL OR @Description = ''
        BEGIN
            SELECT 'Description is required.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Deadline Validation
        -------------------------------------------------

        IF @DeadlineAt IS NULL
        BEGIN
            SELECT 'Deadline date is required.' AS Message;
            RETURN;
        END

        IF CAST(@DeadlineAt AS DATE) < CAST(GETDATE() AS DATE)
        BEGIN
            SELECT 'Deadline cannot be in the past!' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Duplicate Borrow Prevention
        -------------------------------------------------

        IF EXISTS
        (
            SELECT 1
            FROM tblBorrow
            WHERE UserID = @UserID
            AND PersonID = @PersonID
            AND Amount = @Amount
            AND CAST(BorrowAt AS DATE) = CAST(GETDATE() AS DATE)
        )
        BEGIN
            SELECT 'Similar borrow entry already exists today.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Get Credit Category ID
        -------------------------------------------------

        SELECT
            @CreditCategoryID = CategoryID
        FROM tblCategory
        WHERE CategoryName = 'Credit';

        IF @CreditCategoryID IS NULL
        BEGIN
            SELECT 'Credit Category Not Found.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Get Credit SubCategory ID
        -------------------------------------------------

        SELECT
            @CreditSubCategoryID = SubCategoryID
        FROM tblSubCategory
        WHERE SubCategoryName = 'Borrow'
        AND CategoryID = @CreditCategoryID;

        IF @CreditSubCategoryID IS NULL
        BEGIN
            SELECT 'Borrow Credit SubCategory Not Found.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Start Transaction
        -------------------------------------------------

        BEGIN TRANSACTION;

        -------------------------------------------------
        -- Insert Into Borrow
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
        -- Insert Into Credit
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
            @CreditCategoryID,
            @CreditSubCategoryID,
            @PaymentID,
            @Amount,
            'Borrow Amount Credited : ' + @Description,
            GETDATE()
        );

        -------------------------------------------------
        -- Commit Transaction
        -------------------------------------------------

        COMMIT TRANSACTION;

        SELECT 'Borrow transaction inserted successfully.' AS Message;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END