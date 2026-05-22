CREATE TABLE Expense_Sub_Category (
    Sub_Category_Id INT PRIMARY KEY IDENTITY(1,1),
    Category_Id INT NOT NULL,
    Sub_Category_Name VARCHAR(100) UNIQUE NOT NULL,

    FOREIGN KEY (Category_Id)
    REFERENCES Expense_Category(Category_Id)
);