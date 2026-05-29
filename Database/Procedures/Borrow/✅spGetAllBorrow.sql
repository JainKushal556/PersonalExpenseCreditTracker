alter PROCEDURE spGetAllBorrow
(
    @UserID INT
)
AS
BEGIN

    -------------------------------------------------
    -- Validate User
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid OR Inactive UserID!!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get All Borrow Details
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

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID

    ORDER BY b.BorrowAt DESC;

END;
