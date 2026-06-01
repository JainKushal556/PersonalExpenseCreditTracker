---------------------------------------------------------
/* An index is a database object that helps SQL Server find rows faster without scanning the entire table.

The index is sorted like:

UserID	DeadlineAt
1	2025-01-01
1	2025-02-01
1	2025-03-01
2	2025-01-10
2	2025-02-15

So SQL Server can:

Jump directly to UserID = @UserID
Find matching DeadlineAt values
Return results

instead of checking every row.*/
---------------------------------------------------------

CREATE NONCLUSTERED INDEX IX_tblBorrow_UserID_DeadlineAt
ON tblBorrow(UserID, DeadlineAt)
INCLUDE (RemainingAmount, PersonID, PaymentID, StatusID);


CREATE PROCEDURE spGetOverduedBorrow
(
    @UserID INT
)
AS
BEGIN

    SET NOCOUNT ON;

    -------------------------------------------------
    -- User Validation
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID!' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'User does not exist or inactive!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Store Today's Date Once
    -------------------------------------------------

    DECLARE @Today DATE = GETDATE();

    -------------------------------------------------
    -- Overdue Existence Check
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID = @UserID
          AND RemainingAmount > 0
          AND DeadlineAt < @Today
    )
    BEGIN
        SELECT 'No overdue borrow records found!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Overdue Borrow Records
    -------------------------------------------------

    SELECT
        b.BorrowID,
        ISNULL(p.PersonName, 'Unknown') AS PersonName,
        ISNULL(pt.PaymentName, 'Unknown') AS PaymentName,
        ISNULL(s.StatusName, 'Unknown') AS StatusName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        b.Description,

        DATEDIFF
        (
            DAY,
            CAST(b.DeadlineAt AS DATE),
            @Today
        ) AS OverdueDays

    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.RemainingAmount > 0
      AND b.DeadlineAt < @Today

    ORDER BY b.DeadlineAt ASC;

END