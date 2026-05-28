CREATE PROCEDURE spGetCompletedBorrow
(
    @UserID INT
)
AS
BEGIN

    -------------------------------------------------
    -- Get Completed Borrow Records
    -------------------------------------------------

    SELECT
        b.BorrowID,
        p.PersonName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        s.StatusName,
        b.Description
    FROM tblBorrow b
    INNER JOIN tblPersons p
        ON b.PersonID = p.PersonID
    INNER JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID
    WHERE b.UserID = @UserID
        AND s.StatusName = 'Paid'
    ORDER BY b.BorrowAt DESC;

END;


