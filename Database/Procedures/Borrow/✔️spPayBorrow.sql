CREATE PROCEDURE spPayBorrow
(
    @BorrowID INT,
    @PaymentID INT,
    @PaidAmount DECIMAL(10,2),
    @Description VARCHAR(MAX)
)
AS
BEGIN
    DECLARE @UserID INT;
    DECLARE @TotalAmount DECIMAL(10,2);
    DECLARE @RemainingAmount DECIMAL(10,2);
    DECLARE @OldPaidAmount DECIMAL(10,2);
    DECLARE @NewPaidAmount DECIMAL(10,2);
    DECLARE @NewRemainingAmount DECIMAL(10,2);
    DECLARE @StatusID INT;
    DECLARE @CategoryID INT;
    DECLARE @SubCategoryID INT;

    BEGIN TRY
        BEGIN TRANSACTION

        -------------------------------------------------
        -- Validation
        -------------------------------------------------

        IF NOT EXISTS (SELECT 1 FROM tblBorrow WHERE BorrowID = @BorrowID)
        BEGIN
            SELECT 'Invalid BorrowID!!' AS Message;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentID = @PaymentID)
        BEGIN
            SELECT 'Invalid PaymentID!!' AS Message;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @PaidAmount <= 0
        BEGIN
            SELECT 'Paid Amount Must Be Greater Than 0!' AS Message;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -------------------------------------------------
        -- Get Borrow Details
        -------------------------------------------------

        SELECT
            @UserID = UserID,
            @TotalAmount = Amount,
            @RemainingAmount = RemainingAmount,
            @OldPaidAmount = ISNULL(PaidAmount,0)
        FROM tblBorrow
        WHERE BorrowID = @BorrowID;

        SET @RemainingAmount = ISNULL(@RemainingAmount, @TotalAmount);

        -------------------------------------------------
        -- Calculate Amount
        -------------------------------------------------

        SET @NewRemainingAmount = @RemainingAmount - @PaidAmount;
        SET @NewPaidAmount = @OldPaidAmount + @PaidAmount;

        IF @NewRemainingAmount < 0
        BEGIN
            RAISERROR('Paid amount exceeds remaining amount.',16,1);
        END

        -------------------------------------------------
        -- Category & SubCategory
        -------------------------------------------------

        SELECT
            @CategoryID = CategoryID,
            @SubCategoryID = SubCategoryID
        FROM tblExpenseSubCategory
        WHERE SubCategoryName = 'Borrow Returned';

        IF @CategoryID IS NULL OR @SubCategoryID IS NULL
        BEGIN
            SELECT 'Borrow Returned Expense Category/SubCategory Not Found' AS Message;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -------------------------------------------------
        -- Status Update
        -------------------------------------------------

        IF @NewRemainingAmount = @TotalAmount
        BEGIN
            SELECT @StatusID = StatusID
            FROM tblLentBorrowStatus
            WHERE StatusName = 'Pending';
        END
        ELSE IF @NewRemainingAmount = 0
        BEGIN
            SELECT @StatusID = StatusID
            FROM tblLentBorrowStatus
            WHERE StatusName = 'Paid';
        END
        ELSE
        BEGIN
            SELECT @StatusID = StatusID
            FROM tblLentBorrowStatus
            WHERE StatusName = 'Partially Paid';
        END

        -------------------------------------------------
        -- Update Borrow
        -------------------------------------------------

        UPDATE tblBorrow
        SET
            PaidAmount = @NewPaidAmount,
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
            @Description,
            GETDATE()
        );

        COMMIT TRANSACTION;

        SELECT 'Borrow Paid Successfully' AS Message;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT ERROR_MESSAGE() AS ErrorMessage;

    END CATCH
END
GO
