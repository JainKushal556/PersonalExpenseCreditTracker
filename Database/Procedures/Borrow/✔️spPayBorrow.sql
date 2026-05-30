CREATE PROCEDURE spPayBorrow
(
    @BorrowID INT,
    @PaidAmount DECIMAL(10,2),
    @PaymentName VARCHAR(100)
)
AS
BEGIN

    BEGIN TRY

        DECLARE @UserID INT;
        DECLARE @RemainingAmount DECIMAL(10,2);
        DECLARE @NewRemainingAmount DECIMAL(10,2);

        DECLARE @PaymentID INT;
        DECLARE @StatusID INT;
        DECLARE @CategoryID INT;
        DECLARE @SubCategoryID INT;

        -------------------------------------------------
        -- Validation
        -------------------------------------------------

        IF @BorrowID IS NULL OR @BorrowID <= 0
        BEGIN
            SELECT 'Invalid BorrowID' AS Message;
            RETURN;
        END

        IF @PaidAmount IS NULL OR @PaidAmount <= 0
        BEGIN
            SELECT 'Invalid Paid Amount' AS Message;
            RETURN;
        END

        IF @PaymentName IS NULL OR LTRIM(RTRIM(@PaymentName)) = ''
        BEGIN
            SELECT 'Payment Name required' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Borrow Exists Check
        -------------------------------------------------

        IF NOT EXISTS
        (
            SELECT 1 FROM tblBorrow WHERE BorrowID = @BorrowID
        )
        BEGIN
            SELECT 'Borrow record not found' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Get Borrow Details
        -------------------------------------------------

        SELECT
            @UserID = UserID,
            @RemainingAmount = RemainingAmount
        FROM tblBorrow
        WHERE BorrowID = @BorrowID;

        -------------------------------------------------
        -- Over Payment Check
        -------------------------------------------------

        IF @PaidAmount > @RemainingAmount
        BEGIN
            SELECT 'Paid amount exceeds remaining balance' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Payment Lookup
        -------------------------------------------------

        SELECT @PaymentID = PaymentID
        FROM tblPaymentType
        WHERE LTRIM(RTRIM(PaymentName)) = LTRIM(RTRIM(@PaymentName));

        IF @PaymentID IS NULL
        BEGIN
            SELECT 'Invalid Payment Name' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Status Lookup
        -------------------------------------------------

        SELECT @StatusID = StatusID
        FROM tblLentBorrowStatus
        WHERE StatusName =
            CASE 
                WHEN (@RemainingAmount - @PaidAmount) = 0 THEN 'Paid'
                ELSE 'Partially Paid'
            END;

        IF @StatusID IS NULL
        BEGIN
            SELECT 'Status not found' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Expense Category Lookup
        -------------------------------------------------

        SELECT @CategoryID = CategoryID
        FROM tblExpenseCategory
        WHERE CategoryName = 'Borrow';

        SELECT @SubCategoryID = SubCategoryID
        FROM tblExpenseSubCategory
        WHERE SubCategoryName = 'Borrow Returned'
          AND CategoryID = @CategoryID;

        -------------------------------------------------
        -- Transaction Start
        -------------------------------------------------

        BEGIN TRANSACTION;

        -------------------------------------------------
        -- Update Borrow
        -------------------------------------------------

        SET @NewRemainingAmount = @RemainingAmount - @PaidAmount;

        UPDATE tblBorrow
        SET
            PaidAmount = PaidAmount + @PaidAmount,
            RemainingAmount = @NewRemainingAmount,
            StatusID = @StatusID
        WHERE BorrowID = @BorrowID;

        -------------------------------------------------
        -- Insert Expense
        -------------------------------------------------

        INSERT INTO tblExpense
        (
            UserID,
            CategoryID,
            SubCategoryID,
            PaymentID,
            Amount,
            Description,
            ExpenseAt
        )
        VALUES
        (
            @UserID,
            @CategoryID,
            @SubCategoryID,
            @PaymentID,
            @PaidAmount,
            'Borrow repayment payment',
            GETDATE()
        );

        -------------------------------------------------
        -- Commit
        -------------------------------------------------

        COMMIT TRANSACTION;

        -------------------------------------------------
        -- Result Output (no RETURN)
        -------------------------------------------------

        IF @NewRemainingAmount = 0
            SELECT 'Fully Paid' AS Message, 1 AS Result;
        ELSE
            SELECT 'Partially Paid' AS Message, 2 AS Result;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT ERROR_MESSAGE() AS Message, 0 AS Result;

    END CATCH

END;
