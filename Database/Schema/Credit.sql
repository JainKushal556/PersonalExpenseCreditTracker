Create Table tblCredit(
  CreditID INT PRIMARY KEY IDENTITY(1,1),
  UserID  INT NOT NULL,
  CategoryID INT NOT NULL,
  SubCategoryID INT NOT NULL,
  Amount DECIMAL(10,2) NOT NULL,
  Description VARCHAR(MAX) NOT NULL,
  PaymentID	INT NOT NULL,
  CreditAt DATETIME DEFAULT GETDATE(),

  FOREIGN KEY(CategoryID)
  REFERENCES tblCreditCategory(CategoryID),

  FOREIGN KEY(SubCategoryID)
  REFERENCES tblCreditSubCategory(SubCategoryID),

  FOREIGN KEY(UserID)
  REFERENCES tblUsers(UserID),

  FOREIGN KEY(PaymentID)
  REFERENCES tblPaymentType(PaymentID)
);