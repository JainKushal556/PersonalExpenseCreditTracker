CREATE PROCEDURE spPayBorrow
(
    @BorrowID INT,
    @PaidAmount DECIMAL(10,2),
    @PaymentID INT
)
AS
BEGIN

    BEGIN TRY

        DECLARE @UserID INT;
        DECLARE @RemainingAmount DECIMAL(10,2);
        DECLARE @NewRemainingAmount DECIMAL(10,2);
        DECLARE @StatusID INT;

        -------------------------------------------------
        -- Validation
        -------------------------------------------------

        IF @BorrowID IS NULL OR @BorrowID <= 0
            RETURN 0;

        IF @PaidAmount IS NULL OR @PaidAmount <= 0
            RETURN 0;

        IF @PaymentID IS NULL OR @PaymentID <= 0
            RETURN 0;

        IF NOT EXISTS (SELECT 1 FROM tblBorrow WHERE BorrowID = @BorrowID)
            RETURN 0;

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
            RETURN 0;

        -------------------------------------------------
        -- Remaining Amount Calculation
        -------------------------------------------------

        SET @NewRemainingAmount = @RemainingAmount - @PaidAmount;

        -------------------------------------------------
        -- Status Decide
        -------------------------------------------------

        IF @NewRemainingAmount = 0
            SET @StatusID = 2;
        ELSE
            SET @StatusID = 5;

        -------------------------------------------------
        -- Update Borrow
        -------------------------------------------------

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
            PaymentID,
            Amount,
            Description
        )
        VALUES
        (
            @UserID,
            @PaymentID,
            @PaidAmount,
            'Borrow repayment payment'
        );

        -------------------------------------------------
        -- Return Result
        -------------------------------------------------

        IF @NewRemainingAmount = 0
            RETURN 1; -- Fully Paid
        ELSE
            RETURN 2; -- Partially Paid

    END TRY

    BEGIN CATCH

        RETURN 0;

    END CATCH

END;


