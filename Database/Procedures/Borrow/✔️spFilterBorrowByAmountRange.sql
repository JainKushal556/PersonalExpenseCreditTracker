CREATE PROCEDURE spFilterBorrowByAmountRange
    @UserID INT,
    @MinAmount DECIMAL(10,2),
    @MaxAmount DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID=@UserID
        AND Active=1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    IF @MinAmount<0 OR @MaxAmount<0
    BEGIN
        SELECT 'Amount Cannot Be Negative' AS MESSAGE;
        RETURN;
    END;
    IF @MinAmount>@MaxAmount
    BEGIN
        SELECT 'Minimum Amount Cannot Be Greater Than Maximum Amount' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID=@UserID
        AND Amount BETWEEN @MinAmount AND @MaxAmount
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
    AND B.Amount BETWEEN @MinAmount AND @MaxAmount
    ORDER BY B.Amount DESC,B.BorrowAt DESC;
END;
GO
