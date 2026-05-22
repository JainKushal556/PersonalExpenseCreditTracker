CREATE TABLE tblTaskPriorities (
    PriorityID INT PRIMARY KEY IDENTITY(1,1),
    PriorityName VARCHAR(50) NOT NULL UNIQUE
);