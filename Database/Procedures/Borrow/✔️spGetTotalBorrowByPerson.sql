CREATE PROCEDURE spGetTotalBorrowByPerson
(
    @UserID INT,
    @PersonID INT
)
AS
BEGIN

    -------------------------------------------------
    -- User Validation
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID' AS Message;
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
        SELECT 'User not found or inactive' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Person Validation
    -------------------------------------------------

    IF @PersonID IS NULL OR @PersonID <= 0
    BEGIN
        SELECT 'Invalid PersonID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblPersons
        WHERE PersonID = @PersonID
        AND UserID = @UserID
    )
    BEGIN
        SELECT 'Person does not belong to this user' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Data Integrity Check (UPDATED AS REQUESTED)
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE PersonID = @PersonID
          AND UserID = @UserID
          AND Amount IS NOT NULL
    )
    BEGIN
        SELECT 'No borrow transactions found for this person' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Final Summary
    -------------------------------------------------

    SELECT
        p.PersonID,
        ISNULL(p.PersonName, 'Unknown Person') AS PersonName,

        ROUND(ISNULL(SUM(b.Amount), 0), 2) AS TotalBorrowAmount,
        ROUND(ISNULL(SUM(b.PaidAmount), 0), 2) AS TotalPaidAmount,
        ROUND(ISNULL(SUM(b.RemainingAmount), 0), 2) AS TotalRemainingAmount

    FROM tblPersons p

    LEFT JOIN tblBorrow b
        ON p.PersonID = b.PersonID
        AND b.UserID = @UserID
        AND b.Amount IS NOT NULL

    WHERE p.PersonID = @PersonID

    GROUP BY
        p.PersonID,
        p.PersonName;

END;