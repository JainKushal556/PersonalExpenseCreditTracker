CREATE PROCEDURE spGetOverduedBorrow
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
    -- Get Only Overdue Borrow Records
    -------------------------------------------------

    SELECT
        b.BorrowID,
        b.UserID,
        b.PersonID,
        b.PaymentID,

        s.StatusName,

        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        b.Description

    FROM tblBorrow b

    INNER JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
        AND b.RemainingAmount > 0
        AND b.DeadlineAt < GETDATE()

    ORDER BY b.DeadlineAt ASC;

    RETURN 1;

END;
