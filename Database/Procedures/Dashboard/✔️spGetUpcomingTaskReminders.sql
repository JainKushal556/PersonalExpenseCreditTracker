CREATE OR ALTER PROCEDURE spGetUpcomingTaskReminders
(
    @UserID INT
)
AS
BEGIN

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @OverdueStatusID INT;
    DECLARE @PendingStatusID INT;

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
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'User does not exist' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Fetch Status IDs
    -------------------------------------------------
    SELECT @OverdueStatusID = TaskStatusID
    FROM tblTaskStatus
    WHERE TaskStatusName = 'Overdue';

    SELECT @PendingStatusID = TaskStatusID
    FROM tblTaskStatus
    WHERE TaskStatusName = 'Pending';

    -------------------------------------------------
    -- Data Check
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE UserID = @UserID
          AND TaskStatusID IN (@PendingStatusID, @OverdueStatusID)
          AND
          (
                Deadline < @Today
                OR DATEDIFF(DAY, @Today, CAST(Deadline AS DATE)) BETWEEN 0 AND 7
          )
    )
    BEGIN
        SELECT 'No task reminders found' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Reminder Query
    -------------------------------------------------

    SELECT
        T.TaskID,
        T.TaskTitle,
        T.Deadline,
        TS.TaskStatusName,
        TP.PriorityName,
        T.CreatedAt,

        DATEDIFF(DAY, @Today, CAST(T.Deadline AS DATE)) AS RemainingDays,

        CASE

            -------------------------------------------------
            -- OVERDUE
            -------------------------------------------------
            WHEN T.Deadline < @Today THEN
                'Task deadline has passed. Please complete it as soon as possible.'

            -------------------------------------------------
            -- DUE TODAY
            -------------------------------------------------
            WHEN DATEDIFF(DAY, @Today, CAST(T.Deadline AS DATE)) = 0 THEN
                'This task is due today.'

            -------------------------------------------------
            -- BEFORE DEADLINE REMINDERS
            -------------------------------------------------
            WHEN DATEDIFF(DAY, @Today, CAST(T.Deadline AS DATE)) = 1 THEN
                'Reminder: task is due tomorrow.'

            WHEN DATEDIFF(DAY, @Today, CAST(T.Deadline AS DATE)) = 3 THEN
                'Reminder: task is due in 3 days.'

            WHEN DATEDIFF(DAY, @Today, CAST(T.Deadline AS DATE)) = 7 THEN
                'Reminder: task is due in 7 days.'

            ELSE
                'Upcoming task.'
        END AS ReminderMessage

    FROM tblTask T

    INNER JOIN tblTaskStatus TS
        ON T.TaskStatusID = TS.TaskStatusID

    INNER JOIN tblTaskPriorities TP
        ON T.PriorityID = TP.PriorityID

    WHERE T.UserID = @UserID
      AND T.TaskStatusID IN (@PendingStatusID, @OverdueStatusID)
      AND
      (
            T.Deadline < @Today
            OR DATEDIFF(DAY, @Today, CAST(T.Deadline AS DATE)) BETWEEN 0 AND 7
      )

    ORDER BY T.Deadline ASC;

END
GO
