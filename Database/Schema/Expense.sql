CREATE TABLE Expense (
    Expense_Id INT PRIMARY KEY IDENTITY(1,1),
    User_Id INT NOT NULL,
    Category_Id INT NOT NULL,
    Sub_Category_Id INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    Payment_Id INT NOT NULL,
    Expense_At DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (User_Id)
    REFERENCES Users(User_Id),

    FOREIGN KEY (Category_Id)
    REFERENCES Expense_Category(Category_Id),
    

    FOREIGN KEY (Sub_Category_Id)
    REFERENCES Expense_Sub_Category(Sub_Category_Id),
    

    FOREIGN KEY (Payment_Id)
    REFERENCES Payment_Type(Payment_Id)
    
);