CREATE PROCEDURE spUpdateOverdueStatus
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @OverdueStatusID INT;

    -------------------------------------------------
    -- Get Overdue Status ID
    -------------------------------------------------

    SELECT @OverdueStatusID = StatusID
    FROM tblLentBorrowStatus
    WHERE StatusName = 'Overdue';

    -------------------------------------------------
    -- Update Overdue Records
    -------------------------------------------------

    UPDATE tblBorrow
    SET StatusID = @OverdueStatusID
    WHERE RemainingAmount > 0
      AND DeadlineAt < @Today
      AND StatusID <> @OverdueStatusID;

END