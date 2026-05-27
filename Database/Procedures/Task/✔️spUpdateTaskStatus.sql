CREATE PROCEDURE spUpdateTaskStatus
    @TaskID INT,
    @TaskStatusID INT
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

			IF NOT EXISTS
			(
				SELECT 1
				FROM tblTaskStatus
				WHERE TaskStatusID = @TaskStatusID
			)
			BEGIN
				SELECT 'Invalid TaskStatusID' AS Message;
				RETURN;
			END

			IF EXISTS
			(
				SELECT 1
				FROM tblTask
				WHERE TaskID = @TaskID
				AND TaskStatusID = @TaskStatusID
			)
		BEGIN
			SELECT 'Task Already Has This Status' AS Message;
			RETURN;
		END

    BEGIN TRY

        UPDATE tblTask
        SET TaskStatusID = @TaskStatusID
        WHERE TaskID = @TaskID;

        SELECT 'Task Status Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;

