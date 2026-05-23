CREATE TABLE tblExpenseCategory (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName VARCHAR(100) UNIQUE NOT NULL
);
