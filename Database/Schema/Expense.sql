CREATE TABLE tblExpense (
    ExpenseID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    CategoryID INT NOT NULL,
    SubCategoryID INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    Description VARCHAR(MAX) NOT NULL,
    PaymentID INT NOT NULL,
    ExpenseAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserID)
    REFERENCES tblUsers(UserID),

    FOREIGN KEY (CategoryID)
    REFERENCES tblExpenseCategory(CategoryID),
    

    FOREIGN KEY (SubCategoryID)
    REFERENCES tblExpenseSubCategory(SubCategoryID),
    

    FOREIGN KEY (PaymentID)
    REFERENCES tblPaymentType(PaymentID)
    
);