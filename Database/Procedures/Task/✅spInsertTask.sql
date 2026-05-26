CREATE PROCEDURE spInsertTask  
    @UserID INT,  
    @PriorityID INT,  
    @TaskStatusID INT,  
    @TaskTitle VARCHAR(150),  
    @Deadline DATE  
AS  
BEGIN  
  
    -- Trim Task Title  
    SET @TaskTitle = LTRIM(RTRIM(@TaskTitle));  
  
  
    -- Empty Task Title Check  
    IF @TaskTitle IS NULL OR @TaskTitle = ''  
    BEGIN  
        SELECT 'Task Title Cannot Be Empty' AS Message;  
        RETURN;  
    END  
  
  
    -- Empty Deadline Check  
    IF @Deadline IS NULL  
    BEGIN  
        SELECT 'Deadline Cannot Be Empty' AS Message;  
        RETURN;  
    END  
  
  
    -- Deadline Validation  
    IF @Deadline < CAST(GETDATE() AS DATE)  
    BEGIN  
        SELECT 'Invalid Deadline Date' AS Message;  
        RETURN;  
    END  
  
  
    -- User Exists Check  
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
  
  
    -- Active User Check  
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
  
  
    -- Priority Exists Check  
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
  
  
    -- Task Status Exists Check  
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
  
  
    BEGIN TRY  
  
        INSERT INTO tblTask  
        (  
            UserID,  
            PriorityID,  
            TaskStatusID,  
            TaskTitle,  
            Deadline  
        )  
        VALUES  
        (  
            @UserID,  
            @PriorityID,  
            @TaskStatusID,  
            @TaskTitle,  
            @Deadline  
        );  
  
        SELECT 'Task Inserted Successfully' AS Message;  
  
    END TRY  
  
    BEGIN CATCH  
  
        SELECT ERROR_MESSAGE() AS Message;  
  
    END CATCH  
  
END;