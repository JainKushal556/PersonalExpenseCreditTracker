CREATE PROCEDURE spFilterBorrowByDateRange
    @UserID INT,
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT OFF;
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
    IF @FromDate>@ToDate
    BEGIN
        SELECT 'FromDate Cannot Be Greater Than ToDate' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID=@UserID
        AND CAST(BorrowAt AS DATE)
        BETWEEN @FromDate AND @ToDate
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
    AND CAST(B.BorrowAt AS DATE)
    BETWEEN @FromDate AND @ToDate
    ORDER BY B.BorrowAt DESC;
END;
GO
