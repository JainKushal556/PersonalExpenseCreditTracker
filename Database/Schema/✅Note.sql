CREATE TABLE Note
(
    Note_ID INT PRIMARY KEY IDENTITY(1,1),

    User_ID INT NOT NULL,
    Status_ID INT NOT NULL,

    Note_Title VARCHAR(150) NOT NULL,
    Description VARCHAR(500) NOT NULL,
    Created_at DATETIME NOT NULL,

    
        FOREIGN KEY (User_ID)
        REFERENCES Users(User_ID),

        FOREIGN KEY (Status_ID)
        REFERENCES Note_Status(Status_ID)
);

