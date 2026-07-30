CREATE PROCEDURE spGetTasksBetweenCreatedDates
(
    @UserID INT,
    @FromDate DATE,
    @ToDate DATE
)
AS
BEGIN
    SET NOCOUNT OFF;

    BEGIN TRY

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
            SELECT 'Inactive User Cannot View Tasks' AS Message;
            RETURN;
        END

        IF @FromDate IS NULL OR @ToDate IS NULL
        BEGIN
            SELECT 'Date Cannot Be NULL' AS Message;
            RETURN;
        END

        IF @FromDate > @ToDate
        BEGIN
            SELECT 'FromDate Cannot Be Greater Than ToDate' AS Message;
            RETURN;
        END

        SELECT
            T.TaskID,
            T.TaskTitle,
            P.PriorityName,
            S.TaskStatusName,
            T.Deadline,
            T.CreatedAt
        FROM tblTask T
        INNER JOIN tblTaskPriorities P
            ON T.PriorityID = P.PriorityID
        INNER JOIN tblTaskStatus S
            ON T.TaskStatusID = S.TaskStatusID
        WHERE
            T.UserID = @UserID
            AND CAST(T.CreatedAt AS DATE) BETWEEN @FromDate AND @ToDate
        ORDER BY T.CreatedAt ASC;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;
