CREATE TABLE tblExpenseSubCategory (
    SubCategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryID INT NOT NULL,
    UserID INT NULL,
    SubCategoryName VARCHAR(100) NOT NULL,
    IsDefault BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,

    FOREIGN KEY (CategoryID)
    REFERENCES tblExpenseCategory(CategoryID),

    FOREIGN KEY (UserID)
    REFERENCES tblUsers(UserID)
);
