CREATE PROCEDURE spGetPendingBorrow
(
    @UserID INT,
    @PersonID INT = NULL,
    @PaymentName VARCHAR(100) = NULL
)
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @PaymentID INT;

    -------------------------------------------------
    -- User Validation
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID!' AS Message;
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
        SELECT 'User does not exist or inactive!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Validate PersonID (if provided)
    -------------------------------------------------

    IF @PersonID IS NOT NULL AND @PersonID > 0
    BEGIN
        IF NOT EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE PersonID = @PersonID
              AND UserID = @UserID
        )
        BEGIN
            SELECT 'Person does not belong to this user!' AS Message;
            RETURN;
        END
    END

    -------------------------------------------------
    -- Resolve PaymentName → PaymentID (optional)
    -------------------------------------------------

    IF @PaymentName IS NOT NULL AND LTRIM(RTRIM(@PaymentName)) <> ''
    BEGIN
        SELECT @PaymentID = PaymentID
        FROM tblPaymentType
        WHERE LTRIM(RTRIM(PaymentName)) = LTRIM(RTRIM(@PaymentName));

        IF @PaymentID IS NULL
        BEGIN
            SELECT 'Invalid Payment Name!' AS Message;
            RETURN;
        END
    END

    -------------------------------------------------
    -- Check data exists
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID = @UserID
        AND RemainingAmount > 0 
        AND (@PersonID IS NULL OR PersonID = @PersonID)
        AND (@PaymentID IS NULL OR PaymentID = @PaymentID)
    )
    BEGIN
        SELECT 'No pending borrow records found!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Pending Borrow Records
    -------------------------------------------------

    SELECT
        b.BorrowID,
        p.PersonName,
        pt.PaymentName,
        s.StatusName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        b.Description
    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.RemainingAmount > 0

      -- filter by PersonID if provided
      AND (@PersonID IS NULL OR b.PersonID = @PersonID)

      -- filter by PaymentID if provided
      AND (@PaymentName IS NULL OR b.PaymentID = @PaymentID)

      AND s.StatusName IN ('Pending', 'Partially Paid', 'Overdue')

    ORDER BY b.DeadlineAt ASC;

END;