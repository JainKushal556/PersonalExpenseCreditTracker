Create Table tblCreditCategory(
    CategoryID  INT PRIMARY KEY IDENTITY(1,1),
	UserID INT NULL,
	CategoryName VARCHAR(100) Not Null,
	IsDefault BIT NOT NULL DEFAULT 0,
	IsActive BIT NOT NULL DEFAULT 1,

	FOREIGN KEY (UserID) REFERENCES tblUsers(UserID)
);
