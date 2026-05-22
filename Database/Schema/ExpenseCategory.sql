CREATE TABLE Expense_Category (
    Category_Id INT PRIMARY KEY IDENTITY(1,1),
    Category_Name VARCHAR(100) UNIQUE NOT NULL
);