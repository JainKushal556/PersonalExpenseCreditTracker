CREATE PROCEDURE spGetCompletedTasks
    @UserID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
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
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE UserID = @UserID
        AND TaskStatusID = 2
    )
    BEGIN
        SELECT 'No Completed Tasks Found' AS Message;
        RETURN;
    END


    SELECT
        Task.TaskID,
        Task.TaskTitle,
        TaskPriorities.PriorityName,
        TaskStatus.TaskStatusName,
        Task.Deadline
    FROM tblTask Task

    INNER JOIN tblTaskPriorities TaskPriorities
        ON Task.PriorityID = TaskPriorities.PriorityID

    INNER JOIN tblTaskStatus TaskStatus
        ON Task.TaskStatusID = TaskStatus.TaskStatusID

    WHERE Task.UserID = @UserID
    AND Task.TaskStatusID = 2

    ORDER BY Task.Deadline ASC;

END;
