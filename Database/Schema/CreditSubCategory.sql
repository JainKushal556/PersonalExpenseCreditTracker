Create Table tblCreditSubCategory(
  SubCategoryID INT PRIMARY KEY IDENTITY(1,1),
  CategoryID INT NOT NULL,
  SubCategoryName VARCHAR(100) UNIQUE Not Null,

  FOREIGN KEY(CategoryID)
  REFERENCES tblCreditCategory(CategoryID)

);
