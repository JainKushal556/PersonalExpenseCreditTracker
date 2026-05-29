CREATE PROCEDURE spGetPendingBorrow
(
    @UserID INT,
    @PersonName VARCHAR(100) = NULL,
    @PaymentName VARCHAR(100) = NULL
)
AS
BEGIN

    DECLARE @PersonID INT;
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
    -- Resolve PersonName → PersonID (optional filter)
    -------------------------------------------------

    IF @PersonName IS NOT NULL AND LTRIM(RTRIM(@PersonName)) <> ''
    BEGIN
        SELECT @PersonID = PersonID
        FROM tblPersons
        WHERE UserID = @UserID
          AND LTRIM(RTRIM(PersonName)) = LTRIM(RTRIM(@PersonName));

        IF @PersonID IS NULL
        BEGIN
            SELECT 'Person not found for this user!' AS Message;
            RETURN;
        END
    END

    -------------------------------------------------
    -- Resolve PaymentName → PaymentID (optional filter)
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
        FROM tblBorrow b
        WHERE b.UserID = @UserID
          AND b.RemainingAmount > 0
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
      AND s.StatusName IN ('Pending', 'Partially Paid', 'Overdue')

      -- optional filters
      AND (@PersonName IS NULL OR b.PersonID = @PersonID)
      AND (@PaymentName IS NULL OR b.PaymentID = @PaymentID)

    ORDER BY b.DeadlineAt ASC;

END;
