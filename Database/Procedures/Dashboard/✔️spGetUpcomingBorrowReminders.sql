CREATE PROCEDURE spGetUpcomingBorrowReminders
(
    @UserID INT
)
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);

    -------------------------------------------------
    -- User Validation
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
    -- Data Check
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID = @UserID
        AND RemainingAmount > 0
        AND
        (
           DeadlineAt < @Today
           OR DATEDIFF(DAY,@Today,CAST(DeadlineAt AS DATE)) IN (0,1,3,7)
        )
    )
    BEGIN
        SELECT 'No borrow records found' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Reminder Query
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

        DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) AS DaysRemaining,

        CASE

            -------------------------------------------------
            -- OVERDUE
            -------------------------------------------------
            WHEN b.DeadlineAt < @Today THEN
                'Overdue payment. Please clear it as soon as possible.'

            -------------------------------------------------
            -- DUE TODAY
            -------------------------------------------------
            WHEN DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) = 0 THEN
                'This payment is due today.'

            -------------------------------------------------
            -- BEFORE DEADLINE REMINDERS
            -------------------------------------------------
            WHEN DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) = 1 THEN
                'Reminder: payment is due tomorrow.'

            WHEN DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) = 3 THEN
                'Reminder: payment is due in 3 days.'

            WHEN DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) = 7 THEN
                'Reminder: payment is due in 7 days.'

            ELSE
                'Upcoming payment.'

        END AS ReminderMessage

    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.RemainingAmount > 0
      AND (
            b.DeadlineAt < @Today
            OR DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) IN (7, 3, 1, 0)
          )

    ORDER BY b.DeadlineAt ASC;

END