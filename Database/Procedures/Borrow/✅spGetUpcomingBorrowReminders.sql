CREATE PROCEDURE spGetUpcomingBorrowReminders
(
    @UserID INT
)
AS
BEGIN

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);

    -------------------------------------------------
    -- Validation: User ID
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID' AS Message;
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
        SELECT 'User does not exist or inactive' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Check Data Exists
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID = @UserID
          AND RemainingAmount > 0
    )
    BEGIN
        SELECT 'No upcoming borrow reminders found' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Reminder Records
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
        b.Description,

        DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) AS DaysRemaining

    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.RemainingAmount > 0
      AND DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) IN (7, 3, 1)

    ORDER BY b.DeadlineAt ASC;

END;


