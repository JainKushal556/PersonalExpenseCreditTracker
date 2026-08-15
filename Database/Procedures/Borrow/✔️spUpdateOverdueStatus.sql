CREATE OR ALTER PROCEDURE spUpdateOverdueStatus
AS
BEGIN

    SET NOCOUNT OFF;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @LentBorrowOverdueID INT;
    DECLARE @TaskOverdueID INT;

    -- 1. Get Overdue Status ID for Lent & Borrow
    SELECT @LentBorrowOverdueID = StatusID
    FROM tblLentBorrowStatus
    WHERE StatusName = 'Overdue';

    -- 2. Get Overdue Status ID for Tasks
    SELECT @TaskOverdueID = TaskStatusID
    FROM tblTaskStatus
    WHERE TaskStatusName = 'Overdue';

    -- 3. Update Lent & Borrow Overdue Records
    IF @LentBorrowOverdueID IS NOT NULL
    BEGIN
        UPDATE tblLent
        SET StatusID = @LentBorrowOverdueID
        WHERE RemainingAmount > 0
          AND CAST(DeadlineAt AS DATE) < @Today
          AND StatusID <> @LentBorrowOverdueID;

        UPDATE tblBorrow
        SET StatusID = @LentBorrowOverdueID
        WHERE RemainingAmount > 0
          AND CAST(DeadlineAt AS DATE) < @Today
          AND StatusID <> @LentBorrowOverdueID;
    END

    -- 4. Update Task Overdue Records
    IF @TaskOverdueID IS NOT NULL
    BEGIN
        UPDATE tblTask
        SET TaskStatusID = @TaskOverdueID
        WHERE CAST(Deadline AS DATE) < @Today
          AND TaskStatusID <> @TaskOverdueID;
    END
END
GO
