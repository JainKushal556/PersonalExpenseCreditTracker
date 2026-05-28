CREATE PROCEDURE spGetBorrowPersonHistory
(
    @PersonID INT
)
AS
BEGIN

    -------------------------------------------------
    -- Validation
    -------------------------------------------------

    IF @PersonID IS NULL OR @PersonID <= 0
    BEGIN
        RETURN 0;
    END

    -------------------------------------------------
    -- Get Borrow History Of Person
    -------------------------------------------------

    SELECT
        b.BorrowID,
        p.PersonName,
        p.PhoneNumber,
        p.Address,
        b.UserID,
        b.PaymentID,
        b.StatusID,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        b.Description
    FROM tblBorrow b
    INNER JOIN tblPersons p
        ON b.PersonID = p.PersonID
    WHERE b.PersonID = @PersonID
    ORDER BY b.BorrowAt DESC;

END;
