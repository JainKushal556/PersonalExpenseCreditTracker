CREATE TABLE tblTask (
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    PriorityID INT NOT NULL,
    TaskStatusID INT NOT NULL,
    TaskTitle VARCHAR(150) NOT NULL,
    Deadline DATE NOT NULL,

    FOREIGN KEY (UserID) REFERENCES tblUsers(UserID),
    FOREIGN KEY (PriorityID) REFERENCES tblTaskPriorities(PriorityID),
    FOREIGN KEY (TaskStatusID) REFERENCES tblTaskStatus(TaskStatusID)
);