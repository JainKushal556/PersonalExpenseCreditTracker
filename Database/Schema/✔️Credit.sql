CREATE TABLE Credit(
  Credit_ID	INT	PRIMARY KEY IDENTITY(1,1),
  User_ID INT NOT NULL,
  Category_ID INT NOT NULL,
  Sub_Category_ID INT NOT NULL,
  Amount DECIMAL(10,2) NOT NULL,
  Description VARCHAR(255) NOT NULL,
  Payment_ID INT NOT NULL,
  Credit_at DATETIME DEFAULT GETDATE(),

  FOREIGN KEY (Category_ID)
  REFERENCES Credit_Category(Category_ID),

  FOREIGN KEY (Sub_Category_ID)
  REFERENCES Credit_Sub_Category(Sub_Category_ID),

  FOREIGN KEY (User_ID)
  REFERENCES Users(User_ID),
  
  FOREIGN KEY (Payment_ID)
  REFERENCES Payment_Type(Payment_ID)

);