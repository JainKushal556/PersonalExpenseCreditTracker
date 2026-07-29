CREATE PROCEDURE spGetAllTaskPriorities
AS
BEGIN

    SET NOCOUNT OFF;

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTaskPriorities
    )
    BEGIN
        SELECT 'No Task Priority Found' AS Message;
        RETURN;
    END

    SELECT
        PriorityID,
        PriorityName
    FROM tblTaskPriorities
    ORDER BY PriorityName ASC;

END;
