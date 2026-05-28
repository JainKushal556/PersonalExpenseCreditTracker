CREATE PROCEDURE spGetUserDashboard

    @UserID INT

AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
              AND Active = 1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS Message;
        RETURN;
    END;

    DECLARE @TotalExpense DECIMAL(18,2);
    DECLARE @TotalCredit DECIMAL(18,2);
    DECLARE @TotalLent DECIMAL(18,2);
    DECLARE @TotalBorrow DECIMAL(18,2);
    DECLARE @PendingTasks INT;
    DECLARE @NetBalance DECIMAL(18,2);


    SELECT @TotalExpense = ISNULL(SUM(Amount),0)
    FROM tblExpense
    WHERE UserID = @UserID;


    SELECT @TotalCredit = ISNULL(SUM(Amount),0)
    FROM tblCredit
    WHERE UserID = @UserID;


    SELECT @TotalLent = ISNULL(SUM(Amount),0)
    FROM tblLent
    WHERE UserID = @UserID;


    SELECT @TotalBorrow = ISNULL(SUM(Amount),0)
    FROM tblBorrow
    WHERE UserID = @UserID;


    SELECT @PendingTasks = COUNT(*)
    FROM tblTask T
    INNER JOIN tblTaskStatus TS
        ON T.TaskStatusID = TS.TaskStatusID
    WHERE T.UserID = @UserID
          AND TS.TaskStatusName = 'Pending';


    SET @NetBalance =
    (
        (@TotalCredit + @TotalBorrow)
        -
        (@TotalExpense + @TotalLent)
    );

    SELECT
        @TotalExpense AS TotalExpense,
        @TotalCredit AS TotalCredit,
        @TotalLent AS TotalLentAmount,
        @TotalBorrow AS TotalBorrowAmount,
        @NetBalance AS NetBalance,
        @PendingTasks AS PendingTaskCount;

END;
GO