CREATE TABLE tblExpenseSubCategory (
    SubCategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryID INT NOT NULL,
    SubCategoryName VARCHAR(100) UNIQUE NOT NULL,

    FOREIGN KEY (CategoryID)
    REFERENCES tblExpenseCategory(CategoryID)
);