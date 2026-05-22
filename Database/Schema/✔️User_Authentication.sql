CREATE TABLE User_Authentication (
    Auth_ID INT PRIMARY KEY IDENTITY(1,1),
    User_ID INT NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Active BIT NOT NULL,

    FOREIGN KEY (User_ID) REFERENCES Users(User_ID)
);