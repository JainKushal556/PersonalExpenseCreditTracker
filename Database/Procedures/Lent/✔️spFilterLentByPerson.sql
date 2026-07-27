CREATE PROCEDURE spFilterLentByPerson
    @UserID INT,
    @PersonID INT
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
    -- Validate Person
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblPersons
        WHERE PersonID = @PersonID
          AND UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid Person' AS MESSAGE;
        RETURN;
    END;
    -- Check Records
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblLent
        WHERE UserID = @UserID
          AND PersonID = @PersonID
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
      AND L.PersonID = @PersonID
    ORDER BY L.LentAt DESC;
END;
GO
