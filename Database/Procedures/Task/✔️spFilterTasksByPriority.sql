CREATE PROCEDURE spFilterTasksByPriority
    @UserID INT,
    @PriorityID INT
AS
BEGIN
    SET NOCOUNT OFF;

    -- Check User Exists
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

    -- Check User Active
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

    -- Check Priority Exists
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTaskPriorities
        WHERE PriorityID = @PriorityID
    )
    BEGIN
        SELECT 'Invalid PriorityID' AS Message;
        RETURN;
    END

    -- Check Any Task Exists For This Priority
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE UserID = @UserID
          AND PriorityID = @PriorityID
    )
    BEGIN
        SELECT 'No Tasks Found' AS Message;
        RETURN;
    END

    -- Return Filtered Tasks
    SELECT
        Task.TaskID,
        Task.TaskTitle,
        TaskPriorities.PriorityName,
        TaskStatus.TaskStatusName,
        Task.Deadline,
        Task.CreatedAt  
    FROM tblTask AS Task

    INNER JOIN tblTaskPriorities AS TaskPriorities
        ON Task.PriorityID = TaskPriorities.PriorityID

    INNER JOIN tblTaskStatus AS TaskStatus
        ON Task.TaskStatusID = TaskStatus.TaskStatusID

    WHERE Task.UserID = @UserID
      AND Task.PriorityID = @PriorityID

    ORDER BY Task.Deadline ASC;

END;
