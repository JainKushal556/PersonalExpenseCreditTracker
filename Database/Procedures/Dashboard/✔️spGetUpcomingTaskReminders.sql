CREATE PROCEDURE spGetUpcomingTaskReminders

    @UserID INT

AS
BEGIN


    DECLARE @Today DATE = CAST(GETDATE() AS DATE);

    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUsers
            WHERE UserID = @UserID
        )
        BEGIN
            SELECT 'UserID Does Not Exist' AS Message;
            RETURN;
        END

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblTask
            WHERE UserID = @UserID
            AND Deadline >= @Today
            AND TaskStatusID = 1
        )
        BEGIN
            SELECT 'No Pending Upcoming Tasks Found' AS Message;
            RETURN;
        END

        SELECT
            tblTask.TaskID,
            tblTask.TaskTitle,
            tblTask.Deadline,
            tblTaskStatus.TaskStatusName,
            tblTaskPriorities.PriorityName,
            DATEDIFF(DAY, @Today, tblTask.Deadline) AS RemainingDays,
            tblTask.CreatedAt

        FROM tblTask

        INNER JOIN tblTaskStatus
            ON tblTask.TaskStatusID = tblTaskStatus.TaskStatusID

        INNER JOIN tblTaskPriorities
            ON tblTask.PriorityID = tblTaskPriorities.PriorityID

        WHERE tblTask.UserID = @UserID
        AND tblTask.Deadline >= @Today
        AND tblTask.TaskStatusID = 1

        ORDER BY tblTask.Deadline ASC;

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH

END
