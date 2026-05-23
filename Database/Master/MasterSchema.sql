--Independent Tables

CREATE TABLE tblUsers (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UserName VARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME NOT NULL
);
GO

--tblExpenseCategory

Create Table tblPaymentType(
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    PaymentName VARCHAR(50) UNIQUE NOT NULL 
);
GO

--tblCreditCategory

CREATE TABLE tblLentPersons(
	PersonID INT PRIMARY KEY IDENTITY(1,1),
	PersonName VARCHAR(100) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	Address VARCHAR(MAX)
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

--tblNoteStatus


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

-- tblExpense

-- tblExpenseSubCategory

-- tblCredit

-- tblCreditSubCategory

-- tblLent

-- tblBorrow

-- tblBorrowPersons

CREATE TABLE tblTask (
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    PriorityID INT NOT NULL,
    TaskStatusID INT NOT NULL,
    TaskTitle VARCHAR(150) NOT NULL,
    Deadline DATE NOT NULL,

    FOREIGN KEY (UserID) REFERENCES tblUsers(UserID),
    FOREIGN KEY (PriorityID) REFERENCES tblTaskPriorities(PriorityID),
    FOREIGN KEY (TaskStatusID) REFERENCES tblTaskStatus(TaskStatusID)
);
GO

-- tblNote

