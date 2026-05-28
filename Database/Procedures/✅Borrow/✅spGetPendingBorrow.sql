CREATE PROCEDURE spGetPendingBorrow
(
    @UserID INT
)
AS
BEGIN

    SELECT
        b.BorrowID,
        p.PersonName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.DeadlineAt,
        s.StatusName,
        b.Description
    FROM tblBorrow b
    INNER JOIN tblPersons p
        ON b.PersonID = p.PersonID
    INNER JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID
    WHERE b.UserID = @UserID
        AND s.StatusName IN ('Pending', 'Partially Paid', 'Overdue')
    ORDER BY b.DeadlineAt ASC;

END;
