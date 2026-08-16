CREATE OR ALTER PROCEDURE spGetUpcomingLentReminders 
(
    @UserID INT
)
AS
BEGIN
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
        FROM tblLent
        WHERE UserID = @UserID
        AND RemainingAmount > 0
        AND
        (
           DeadlineAt < @Today
           OR DATEDIFF(DAY, @Today, CAST(DeadlineAt AS DATE)) BETWEEN 0 AND 7
        )
    )
    BEGIN
        SELECT 'No lent records found' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Reminder Query
    -------------------------------------------------

    SELECT
        L.LentID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        L.Amount,
        L.ReturnedAmount,
        L.RemainingAmount,
        L.LentAt,
        L.DeadlineAt,
        L.Description,

        DATEDIFF(DAY, @Today, CAST(L.DeadlineAt AS DATE)) AS DaysRemaining,

        CASE

            -------------------------------------------------
            -- OVERDUE
            -------------------------------------------------
            WHEN L.DeadlineAt < @Today THEN
                'Overdue collection. Please collect the payment as soon as possible.'

            -------------------------------------------------
            -- DUE TODAY
            -------------------------------------------------
            WHEN DATEDIFF(DAY, @Today, CAST(L.DeadlineAt AS DATE)) = 0 THEN
                'This collection is due today.'

            -------------------------------------------------
            -- BEFORE DEADLINE REMINDERS
            -------------------------------------------------
            WHEN DATEDIFF(DAY, @Today, CAST(L.DeadlineAt AS DATE)) = 1 THEN
                'Reminder: collection is due tomorrow.'

            WHEN DATEDIFF(DAY, @Today, CAST(L.DeadlineAt AS DATE)) = 3 THEN
                'Reminder: collection is due in 3 days.'

            WHEN DATEDIFF(DAY, @Today, CAST(L.DeadlineAt AS DATE)) = 7 THEN
                'Reminder: collection is due in 7 days.'

            ELSE
                'Upcoming collection.'
        END AS ReminderMessage

    FROM tblLent L

    LEFT JOIN tblPersons P
        ON L.PersonID = P.PersonID

    LEFT JOIN tblPaymentType PT
        ON L.PaymentID = PT.PaymentID

    LEFT JOIN tblLentBorrowStatus S
        ON L.StatusID = S.StatusID

    WHERE L.UserID = @UserID
      AND L.RemainingAmount > 0
      AND
      (
            L.DeadlineAt < @Today
            OR DATEDIFF(DAY, @Today, CAST(L.DeadlineAt AS DATE)) BETWEEN 0 AND 7
      )

    ORDER BY L.DeadlineAt ASC;

END
GO