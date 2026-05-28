Create Table tblCreditSubCategory(
  SubCategoryID INT PRIMARY KEY IDENTITY(1,1),
  CategoryID INT NOT NULL,
  UserID INT NULL,
  SubCategoryName VARCHAR(100) Not Null,
  IsDefault BIT NOT NULL DEFAULT 0,
  IsActive BIT NOT NULL DEFAULT 1,

  FOREIGN KEY(CategoryID)
  REFERENCES tblCreditCategory(CategoryID),

  FOREIGN KEY(UserID)
  REFERENCES tblUsers(UserID)

);
