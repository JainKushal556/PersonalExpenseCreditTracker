CREATE TABLE Credit_Sub_Category(
  Sub_Category_ID	INT	PRIMARY KEY IDENTITY(1,1),
  Category_ID	INT NOT NULL,
  Sub_Category_Name	VARCHAR(100) UNIQUE NOT NULL,
  FOREIGN KEY (Category_ID)
  REFERENCES Credit_Category(Category_ID)
);