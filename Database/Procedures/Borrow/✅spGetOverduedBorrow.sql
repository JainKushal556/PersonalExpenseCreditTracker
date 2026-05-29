CREATE PROCEDURE spGetOverduedBorrow
(
    @UserID INT
)
AS
BEGIN

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
    -- Overdue existence check
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID = @UserID
        AND RemainingAmount > 0
        AND CAST(DeadlineAt AS DATE) < CAST(GETDATE() AS DATE)
    )
    BEGIN
        SELECT 'No overdue borrow records found!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Overdue Borrow Records (Name-based output)
    -------------------------------------------------

    SELECT
        b.BorrowID,

        ISNULL(p.PersonName, 'Unknown') AS PersonName,
        ISNULL(pt.PaymentName, 'Unknown') AS PaymentName,
        ISNULL(s.StatusName, 'Unknown') AS StatusName,

        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        b.Description,

        DATEDIFF(DAY, b.DeadlineAt, GETDATE()) AS OverdueDays

    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.RemainingAmount > 0
      AND CAST(b.DeadlineAt AS DATE) < CAST(GETDATE() AS DATE)

    ORDER BY b.DeadlineAt ASC;

END;