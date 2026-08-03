CREATE PROCEDURE spReturnLentByReturnAmount
(
    @LentID INT,
    @PaymentID INT,
    @ReturnedAmount DECIMAL(10,2),
    @Description VARCHAR(MAX)
)
AS
BEGIN
    DECLARE @TotalAmount DECIMAL(10,2);
    DECLARE @RemainingAmount DECIMAL(10,2);
    DECLARE @NewRemainingAmount DECIMAL(10,2);
    DECLARE @NewReturnedAmount DECIMAL(10,2);
    DECLARE @OldReturnedAmount DECIMAL(10,2);
    DECLARE @StatusID INT;
    DECLARE @UserID INT;
    DECLARE @CategoryID INT;
    DECLARE @SubCategoryID INT;

    BEGIN TRY
        BEGIN TRANSACTION

        -------------------------------------------------
        -- Validation
        -------------------------------------------------

        IF NOT EXISTS (SELECT 1 FROM tblLent WHERE LentID=@LentID)
        BEGIN
            SELECT 'Invalid LentID' AS Message;
            ROLLBACK;
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentID=@PaymentID)
        BEGIN
            SELECT 'Invalid PaymentID' AS Message;
            ROLLBACK;
            RETURN;
        END

        IF @ReturnedAmount IS NULL OR @ReturnedAmount<=0
        BEGIN
            SELECT 'Returned Amount Must Be Greater Than 0' AS Message;
            ROLLBACK;
            RETURN;
        END

        IF @Description IS NULL
            SET @Description='';

        -------------------------------------------------
        -- Get Lent Details
        -------------------------------------------------

        SELECT
            @TotalAmount=Amount,
            @RemainingAmount=RemainingAmount,
            @OldReturnedAmount=ISNULL(ReturnedAmount,0),
            @UserID=UserID
        FROM tblLent
        WHERE LentID=@LentID;

        IF @TotalAmount IS NULL
        BEGIN
            SELECT 'Amount Not Found' AS Message;
            ROLLBACK;
            RETURN;
        END

        SET @RemainingAmount=ISNULL(@RemainingAmount,@TotalAmount);

        -------------------------------------------------
        -- Calculate
        -------------------------------------------------

        SET @NewRemainingAmount=@RemainingAmount-@ReturnedAmount;
        SET @NewReturnedAmount=@OldReturnedAmount+@ReturnedAmount;

        IF @NewRemainingAmount<0
        BEGIN
            RAISERROR('Returned amount exceeds remaining amount.',16,1);
        END

        -------------------------------------------------
        -- Category Lookup
        -------------------------------------------------

        SELECT
            @CategoryID=CategoryID,
            @SubCategoryID=SubCategoryID
        FROM tblCreditSubCategory
        WHERE SubCategoryName='Lent Returned';

        IF @CategoryID IS NULL OR @SubCategoryID IS NULL
        BEGIN
            SELECT 'Lent Returned Credit Category/SubCategory Not Found' AS Message;
            ROLLBACK;
            RETURN;
        END

        -------------------------------------------------
        -- Status
        -------------------------------------------------

        IF @NewRemainingAmount=0
        BEGIN
            SELECT @StatusID=StatusID
            FROM tblLentBorrowStatus
            WHERE StatusName='Paid';
        END
        ELSE
        BEGIN
            SELECT @StatusID=StatusID
            FROM tblLentBorrowStatus
            WHERE StatusName='Partially Paid';
        END

        -------------------------------------------------
        -- Update Lent
        -------------------------------------------------

        UPDATE tblLent
        SET
            RemainingAmount=@NewRemainingAmount,
            ReturnedAmount=@NewReturnedAmount,
            StatusID=@StatusID
        WHERE LentID=@LentID;

        IF @@ROWCOUNT=0
        BEGIN
            ROLLBACK;
            RETURN;
        END

        -------------------------------------------------
        -- Insert Credit
        -------------------------------------------------

        INSERT INTO tblCredit
        (
            UserID,
            CategoryID,
            SubCategoryID,
            PaymentID,
            Amount,
            Description
        )
        VALUES
        (
            @UserID,
            @CategoryID,
            @SubCategoryID,
            @PaymentID,
            @ReturnedAmount,
            @Description
        );

        IF @@ROWCOUNT=0
        BEGIN
            ROLLBACK;
            RETURN;
        END

        COMMIT;

        IF @NewRemainingAmount=0
            SELECT 'Lent Fully Returned Successfully' AS Message;
        ELSE
            SELECT 'Lent Partially Returned Successfully' AS Message;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT>0
            ROLLBACK;

        SELECT ERROR_MESSAGE() AS ErrorMessage;

    END CATCH
END
GO