CREATE PROCEDURE spGetBorrowPersonHistory
(
    @PersonID INT,
    @UserID INT
)
AS
BEGIN

    -------------------------------------------------
    -- Check Person Belongs To User + Borrow History Exists
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow B
        JOIN tblPersons P
            ON B.PersonID = P.PersonID
        WHERE B.UserID = @UserID
        AND B.PersonID = @PersonID
    )
    BEGIN
        SELECT 'Invalid PersonID OR No Borrow History Found!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Borrow History Of Person
    -------------------------------------------------

    SELECT
        B.BorrowID,
        P.PersonName,
        P.PhoneNumber,
        P.Address,
        Pay.PaymentName,
        S.StatusName,
        B.Amount,
        B.PaidAmount,
        B.RemainingAmount,
        B.BorrowAt,
        B.DeadlineAt,
        B.Description

    FROM tblBorrow B

    LEFT JOIN tblPersons P
        ON B.PersonID = P.PersonID

    LEFT JOIN tblPaymentType Pay
        ON B.PaymentID = Pay.PaymentID

    LEFT JOIN tblLentBorrowStatus S
        ON B.StatusID = S.StatusID

    WHERE B.PersonID = @PersonID
    AND B.UserID = @UserID

    ORDER BY B.BorrowAt DESC;

END