CREATE TABLE tblBorrowPersons (
    PersonID INT PRIMARY KEY IDENTITY(1,1),
    PersonName VARCHAR(100) NOT NULL,
    PhoneNumber VARCHAR(15) NOT NULL,
    Address VARCHAR(MAX) NULL
);