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

--return 0 1 esob hbe na message print korbi select use kore "SELECT 'Message' AS Message"
--ekhane user id nuull ki na check to korechis but same exist and active ae duto check nae 
--er person id er payment id keno select korchis oder name select hbe to join kore 
--left join hbe jodi kichu delte hoye tao jeno data show kore 