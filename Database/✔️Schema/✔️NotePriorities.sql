CREATE TABLE tblNotePriorities (
    NotePriorityID INT PRIMARY KEY IDENTITY(1,1),
    NotePriorityName VARCHAR(50) NOT NULL UNIQUE
);