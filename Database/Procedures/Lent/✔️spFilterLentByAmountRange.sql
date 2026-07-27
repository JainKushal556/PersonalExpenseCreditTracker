CREATE PROCEDURE spFilterLentByAmountRange
    @UserID INT,
    @MinAmount DECIMAL(10,2),
    @MaxAmount DECIMAL(10,2)
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
        SELECT 'Invalid or Inactive User' AS Message;
        RETURN;
    END;
    -- Validate Amount Range
    IF @MinAmount < 0 OR @MaxAmount < 0
    BEGIN
        SELECT 'Amount cannot be negative' AS Message;
        RETURN;
    END;
    IF @MinAmount > @MaxAmount
    BEGIN
        SELECT 'Minimum Amount cannot be greater than Maximum Amount' AS Message;
        RETURN;
    END;
    -- Filter Lent Records
    SELECT
        L.LentID,
        L.UserID,
        L.PersonID,
        PS.PersonName,
        L.PaymentID,
        PT.PaymentName,
        L.StatusID,
        S.StatusName,
        L.Amount,
        L.ReturnedAmount,
        L.RemainingAmount,
        L.LentAt,
        L.DeadlineAt,
        L.Description
    FROM tblLent L
        INNER JOIN tblPersons PS
            ON L.PersonID = PS.PersonID
        INNER JOIN tblPaymentType PT
            ON L.PaymentID = PT.PaymentID
        INNER JOIN tblLentBorrowStatus S
            ON L.StatusID = S.StatusID
    WHERE
        L.UserID = @UserID
        AND L.Amount BETWEEN @MinAmount AND @MaxAmount
    ORDER BY
        L.Amount DESC,
        L.LentAt DESC;
END;
GO
