CREATE PROCEDURE spUpdateOverdueStatus
AS
BEGIN

    SET NOCOUNT OFF;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @OverdueStatusID INT;

    -------------------------------------------------
    -- Get Overdue Status ID
    -------------------------------------------------

    SELECT @OverdueStatusID = StatusID
    FROM tblLentBorrowStatus
    WHERE StatusName = 'Overdue';

	-------------------------------------------------
    -- Check OverdueStatusID is NULL
    -------------------------------------------------

	IF @OverdueStatusID IS NULL
	BEGIN
		SELECT 'Overdue Status Not Found!' AS Message;
		RETURN
	END
    -------------------------------------------------
    -- Update Overdue Records
    -------------------------------------------------

    UPDATE tblBorrow
    SET StatusID = @OverdueStatusID
    WHERE RemainingAmount > 0
      AND CAST(DeadlineAt AS DATE) < @Today
      AND StatusID <> @OverdueStatusID;

END
 
