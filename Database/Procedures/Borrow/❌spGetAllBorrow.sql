CREATE PROCEDURE spGetAllBorrow
(
    @UserID INT
)
AS
BEGIN

    -------------------------------------------------
    -- Get All Borrow Details of Logged In User
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
    INNER JOIN tblPersons p
        ON b.PersonID = p.PersonID
    INNER JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID
    INNER JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID
    WHERE b.UserID = @UserID
    ORDER BY b.BorrowAt DESC;

END;

-- user ache na nae active ki na 
--inner jon hbe na left join hbe jodi payment id person name kichu delte thke to data asbe na so left join korte hbe