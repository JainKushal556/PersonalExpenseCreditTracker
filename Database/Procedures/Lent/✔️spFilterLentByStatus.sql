CREATE PROCEDURE spFilterLentByStatus
    @UserID INT,
    @StatusID INT
AS
BEGIN
    SET NOCOUNT ON;
    
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

    -- Validate Status
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblLentBorrowStatus
        WHERE StatusID = @StatusID
    )
    BEGIN
        SELECT 'Invalid Status' AS MESSAGE;
        RETURN;
    END;

    -- Check Records
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblLent
        WHERE UserID = @UserID
          AND StatusID = @StatusID
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
      AND L.StatusID = @StatusID
    ORDER BY L.LentAt DESC;
END;
