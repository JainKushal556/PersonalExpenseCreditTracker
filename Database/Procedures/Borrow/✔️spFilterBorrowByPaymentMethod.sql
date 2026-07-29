CREATE PROCEDURE spFilterBorrowByPaymentMethod
    @UserID INT,
    @PaymentID INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblUserAuthentication
        WHERE UserID=@UserID
        AND Active=1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblPaymentType
        WHERE PaymentID=@PaymentID
    )
    BEGIN
        SELECT 'Invalid Payment Method' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblBorrow
        WHERE UserID=@UserID
        AND PaymentID=@PaymentID
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    SELECT
        B.BorrowID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        B.Amount,
        B.PaidAmount,
        B.RemainingAmount,
        B.DeadlineAt,
        LTRIM(RTRIM(B.Description)) AS Description,
        B.BorrowAt
    FROM tblBorrow B
    LEFT JOIN tblPersons P ON B.PersonID=P.PersonID
    LEFT JOIN tblPaymentType PT ON B.PaymentID=PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S ON B.StatusID=S.StatusID
    WHERE B.UserID=@UserID
    AND B.PaymentID=@PaymentID
    ORDER BY B.BorrowAt DESC;
END;
GO
