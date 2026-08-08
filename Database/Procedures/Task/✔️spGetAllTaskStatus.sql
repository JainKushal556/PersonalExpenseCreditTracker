CREATE PROCEDURE spGetAllTaskStatus
AS
BEGIN
    SET NOCOUNT OFF;

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTaskStatus
    )
    BEGIN
        SELECT 'No Task Status Found' AS Message;
        RETURN;
    END

    SELECT
        TaskStatusID,
        TaskStatusName
    FROM tblTaskStatus
    ORDER BY TaskStatusName ASC;
END;
GO
