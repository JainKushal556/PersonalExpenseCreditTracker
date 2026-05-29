CREATE PROCEDURE spGetCompletedBorrow
(
    @UserID INT
)
AS
BEGIN

    DECLARE @PaidStatusID INT;

    -------------------------------------------------
    -- User Validation (Exist + Active)
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID!' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers U
        INNER JOIN tblUserAuthentication UA
            ON U.UserID = UA.UserID
        WHERE U.UserID = @UserID
        AND UA.Active = 1
    )
    BEGIN
        SELECT 'User does not exist or inactive!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get StatusID from StatusName (NO typo risk in logic)
    -------------------------------------------------

    SELECT @PaidStatusID = StatusID
    FROM tblLentBorrowStatus
    WHERE LTRIM(RTRIM(StatusName)) = 'Paid';

    IF @PaidStatusID IS NULL
    BEGIN
        SELECT 'Paid status not found in system!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- No Record Check
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow b
        WHERE b.UserID = @UserID
        AND b.StatusID = @PaidStatusID
        AND b.RemainingAmount = 0
    )
    BEGIN
        SELECT 'No completed borrow records found!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Completed Borrow Records
    -------------------------------------------------

    SELECT
        b.BorrowID,
        ISNULL(p.PersonName,'Unknown') AS PersonName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        ISNULL(s.StatusName,'Unknown') AS StatusName,
        b.Description
    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.StatusID = @PaidStatusID
      AND b.RemainingAmount = 0

    ORDER BY b.BorrowAt DESC;

END;
