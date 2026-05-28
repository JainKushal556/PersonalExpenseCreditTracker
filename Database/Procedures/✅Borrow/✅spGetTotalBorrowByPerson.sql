CREATE PROCEDURE spGetTotalBorrowByPerson
(
    @PersonID INT
)
AS
BEGIN

    -------------------------------------------------
    -- Validation
    -------------------------------------------------

    IF @PersonID IS NULL OR @PersonID <= 0
        RETURN 0;

    -------------------------------------------------
    -- Person Borrow Summary
    -------------------------------------------------

    SELECT
        p.PersonID,
        p.PersonName,

        ISNULL(SUM(b.Amount),0) AS TotalBorrowAmount,

        ISNULL(SUM(b.PaidAmount),0) AS TotalPaidAmount,

        ISNULL(SUM(b.RemainingAmount),0) AS TotalRemainingAmount

    FROM tblPersons p
    LEFT JOIN tblBorrow b
        ON p.PersonID = b.PersonID

    WHERE p.PersonID = @PersonID

    GROUP BY
        p.PersonID,
        p.PersonName;

    RETURN 1;

END;


