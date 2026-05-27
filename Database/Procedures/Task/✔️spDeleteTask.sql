CREATE PROCEDURE spDeleteTask
    @TaskID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE TaskID = @TaskID
    )
    BEGIN
        SELECT 'Invalid TaskID' AS Message;
        RETURN;
    END


    BEGIN TRY

        DELETE FROM tblTask
        WHERE TaskID = @TaskID;

        SELECT 'Task Deleted Successfully' AS Message;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;
