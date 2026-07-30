CREATE PROCEDURE spFilterLentByDateRange
    @UserID INT,
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT OFF;
    -- Validate User
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    -- Validate Date Range
    IF @FromDate > @ToDate
    BEGIN
        SELECT 'FromDate Cannot Be Greater Than ToDate' AS MESSAGE;
        RETURN;
    END;
    -- Check Records
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblLent
        WHERE UserID = @UserID
          AND CAST(LentAt AS DATE) BETWEEN CAST(@FromDate AS DATE) AND CAST(@ToDate AS DATE)
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    -- Fetch Records
    SELECT
        L.LentID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        L.Amount,
        L.ReturnedAmount,
        L.RemainingAmount,
		L.LentAt,
        L.DeadlineAt,
        LTRIM(RTRIM(L.Description)) AS Description,
        L.LentAt
    FROM tblLent L
    LEFT JOIN tblPersons P
        ON L.PersonID = P.PersonID
    LEFT JOIN tblPaymentType PT
        ON L.PaymentID = PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S
        ON L.StatusID = S.StatusID
    WHERE L.UserID = @UserID
      AND CAST(L.LentAt AS DATE)
          BETWEEN CAST(@FromDate AS DATE) AND CAST(@ToDate AS DATE)
    ORDER BY L.LentAt DESC;
END;
GO
