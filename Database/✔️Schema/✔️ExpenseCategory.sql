CREATE TABLE tblExpenseCategory (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    IsDefault BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,

    FOREIGN KEY (UserID) REFERENCES tblUsers(UserID)
);
