CREATE TABLE tblUserAuthentication (
    AuthID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Password VARCHAR(MAX) NOT NULL,
    Active BIT NOT NULL,

    FOREIGN KEY (UserID) REFERENCES tblUsers(UserID)
);