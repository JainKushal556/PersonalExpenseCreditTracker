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

    DECLARE @CategoryID INT;
    DECLARE @SubCategoryID INT;

    -------------------------------------------------
    -- Trim Inputs
    -------------------------------------------------

    SET @PaymentName = LTRIM(RTRIM(@PaymentName));
    SET @StatusName = LTRIM(RTRIM(@StatusName));

    BEGIN TRY

        -------------------------------------------------
        -- Validation
        -------------------------------------------------

        IF @UserID IS NULL OR @UserID <= 0
        BEGIN
            SELECT 'Invalid UserID.' AS Message;
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
        -- User Added Person Validation
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
        -- Credit Category Validation
        -------------------------------------------------

        SELECT
            @CategoryID = CategoryID
        FROM tblCategory
        WHERE CategoryName = 'Credit';

        IF @CategoryID IS NULL
        BEGIN
            SELECT 'Credit Category Not Found.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Credit SubCategory Validation
        -------------------------------------------------

        SELECT
            @SubCategoryID = SubCategoryID
        FROM tblSubCategory
        WHERE SubCategoryName = 'Borrow Credit';

        IF @SubCategoryID IS NULL
        BEGIN
            SELECT 'Borrow Credit SubCategory Not Found.' AS Message;
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

        -------------------------------------------------
        -- Deadline Validation
        -------------------------------------------------

        IF @DeadlineAt IS NULL
        BEGIN
            SELECT 'Deadline date is required.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Transaction Start
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
        -- Insert Into Credit Automatically
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
            @CategoryID,
            @SubCategoryID,
            @PaymentID,
            @Amount,
            'Borrow Credit : ' + ISNULL(@Description,''),
            GETDATE()
        );

        -------------------------------------------------
        -- Commit Transaction
        -------------------------------------------------

        COMMIT TRANSACTION;

        SELECT 'Borrow transaction inserted successfully.' AS Message;

    END TRY

    BEGIN CATCH

        ROLLBACK TRANSACTION;

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END



--description ke trim kore insert korte hbeb
--deadline ta jeno ager date na diye day se mean 5 tarik ee diye ager maser 5 trik deadline dile to hbe na tai setaq check kor 

