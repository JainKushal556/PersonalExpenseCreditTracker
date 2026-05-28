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

--user id validation nae 
--user id diye check korte hbe je oi user ta ki tar ee mean user id ki same oi person er record ee.
--return 0 korte hbe na message print korbi select use kore print noy 
--inner join hbe na jodi person delete hoye jaye pawa ee jbe na data 
