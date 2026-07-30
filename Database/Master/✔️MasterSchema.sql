--Independent Tables

CREATE TABLE tblUsers (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UserName VARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE tblExpenseCategory (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    IsDefault BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,

    FOREIGN KEY (UserID) REFERENCES tblUsers(UserID)
);
GO

Create Table tblPaymentType(
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    PaymentName VARCHAR(50) UNIQUE NOT NULL 
);
GO

Create Table tblCreditCategory(
    CategoryID  INT PRIMARY KEY IDENTITY(1,1),
	UserID INT NULL,
	CategoryName VARCHAR(100) Not Null,
	IsDefault BIT NOT NULL DEFAULT 0,
	IsActive BIT NOT NULL DEFAULT 1,

	FOREIGN KEY (UserID) REFERENCES tblUsers(UserID)
);
GO

CREATE TABLE tblPersons(
	PersonID INT PRIMARY KEY IDENTITY(1,1),
	UserID INT NOT NULL,
	PersonName VARCHAR(100) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	Address VARCHAR(MAX),

	FOREIGN KEY (UserID) REFERENCES tblUsers(UserID)
);
GO

CREATE TABLE tblLentBorrowStatus(
	StatusID INT PRIMARY KEY IDENTITY(1,1),
	StatusName VARCHAR(50) NOT NULL
);
GO

CREATE TABLE tblTaskPriorities (
    PriorityID INT PRIMARY KEY IDENTITY(1,1),
    PriorityName VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE tblTaskStatus (
    TaskStatusID INT PRIMARY KEY IDENTITY(1,1),
    TaskStatusName VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE tblNotePriorities (
    NotePriorityID INT PRIMARY KEY IDENTITY(1,1),
    NotePriorityName VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE tblNoteColor (
    NoteColorID INT PRIMARY KEY IDENTITY(1,1),
    ColorName VARCHAR(50) NOT NULL UNIQUE,
    ColorHexCode VARCHAR(20) NULL
);
GO



--Dependent Tables

CREATE TABLE tblUserProfile (
    ProfileID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Name VARCHAR(MAX) NOT NULL,
    ProfilePhoto VARBINARY(MAX) NULL,

    FOREIGN KEY (UserID) REFERENCES tblUsers(UserID)
);
GO

CREATE TABLE tblUserContact (
    ContactID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber VARCHAR(15) NOT NULL UNIQUE,

    FOREIGN KEY (UserID) REFERENCES tblUsers(UserID)
);
GO

CREATE TABLE tblUserAuthentication (
    AuthID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Password VARCHAR(MAX) NOT NULL,
    Active BIT NOT NULL,

    FOREIGN KEY (UserID) REFERENCES tblUsers(UserID)
);
GO

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
GO

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
GO

CREATE TABLE tblExpense (
    ExpenseID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    CategoryID INT NOT NULL,
    SubCategoryID INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    Description VARCHAR(MAX) NOT NULL,
    PaymentID INT NOT NULL,
    ExpenseAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY(UserID)
    REFERENCES tblUsers(UserID),

    FOREIGN KEY (CategoryID)
    REFERENCES tblExpenseCategory(CategoryID),
    

    FOREIGN KEY (SubCategoryID)
    REFERENCES tblExpenseSubCategory(SubCategoryID),
    

    FOREIGN KEY (PaymentID)
    REFERENCES tblPaymentType(PaymentID)
    
);
GO

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
GO

CREATE TABLE tblLent(
	LentID INT PRIMARY KEY IDENTITY(1,1),
	UserID INT NOT NULL,
	PersonID INT NOT NULL,
	PaymentID INT NOT NULL,
	StatusID INT NOT NULL,
	Amount DECIMAL(10,2) NOT NULL,
	ReturnedAmount DECIMAL(10,2) NOT NULL,
	RemainingAmount DECIMAL(10,2) NOT NULL,
	LentAt DATETIME NOT NULL DEFAULT GETDATE(),
	DeadlineAt DATETIME,
	Description VARCHAR(MAX) NOT NULL,

	FOREIGN KEY (UserID) REFERENCES tblUsers(UserID),
	FOREIGN KEY (PersonID) REFERENCES tblPersons(PersonID),
	FOREIGN KEY (PaymentID) REFERENCES tblPaymentType(PaymentID),
	FOREIGN KEY (StatusID) REFERENCES tblLentBorrowStatus(StatusID)
);
GO

CREATE TABLE tblBorrow (
    BorrowID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    PersonID INT NOT NULL,
    PaymentID INT NOT NULL,
    StatusID INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaidAmount DECIMAL(10,2) NOT NULL,
    RemainingAmount DECIMAL(10,2) NOT NULL,
    BorrowAt DATETIME NOT NULL DEFAULT GETDATE(),
    DeadlineAt DATETIME,
    Description VARCHAR(MAX) NOT NULL,

	FOREIGN KEY (UserID) REFERENCES tblUsers(UserID),
	FOREIGN KEY (PersonID) REFERENCES tblPersons(PersonID),
	FOREIGN KEY (PaymentID) REFERENCES tblPaymentType(PaymentID),
	FOREIGN KEY (StatusID) REFERENCES tblLentBorrowStatus(StatusID)
);
GO

CREATE TABLE tblTask (
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    PriorityID INT NOT NULL,
    TaskStatusID INT NOT NULL,
    TaskTitle VARCHAR(150) NOT NULL,
    Deadline DATE NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (UserID) REFERENCES tblUsers(UserID),
    FOREIGN KEY (PriorityID) REFERENCES tblTaskPriorities(PriorityID),
    FOREIGN KEY (TaskStatusID) REFERENCES tblTaskStatus(TaskStatusID)
);
GO

CREATE TABLE tblNote
(
    NoteID INT NOT NULL PRIMARY KEY IDENTITY(1,1),

    UserID INT NOT NULL,
    NotePriorityID INT NOT NULL,
    NoteColorID INT NOT NULL DEFAULT 1,

    NoteTitle VARCHAR(MAX) NOT NULL,
    Description VARCHAR(MAX) NOT NULL,

    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (UserID)
    REFERENCES tblUsers(UserID),

    FOREIGN KEY (NotePriorityID)
    REFERENCES tblNotePriorities(NotePriorityID),

    FOREIGN KEY (NoteColorID)
    REFERENCES tblNoteColor(NoteColorID)
);
GO
