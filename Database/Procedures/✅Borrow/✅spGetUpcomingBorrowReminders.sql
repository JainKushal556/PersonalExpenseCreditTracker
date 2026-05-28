CREATE PROCEDURE spGetUpcomingBorrowReminders
(
    @UserID INT
)
AS
BEGIN

    -------------------------------------------------
    -- Validation
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
        RETURN 0;

    -------------------------------------------------
    -- Get Reminder Records
    -------------------------------------------------

    SELECT
        BorrowID,
        UserID,
        PersonID,
        PaymentID,
        StatusID,
        Amount,
        PaidAmount,
        RemainingAmount,
        BorrowAt,
        DeadlineAt,
        Description,

        DATEDIFF(DAY, GETDATE(), DeadlineAt) AS DaysRemaining

    FROM tblBorrow

    WHERE UserID = @UserID
        AND RemainingAmount > 0
        AND DATEDIFF(DAY, GETDATE(), DeadlineAt) IN (7,3,1)

    ORDER BY DeadlineAt ASC;

    RETURN 1;

END;

