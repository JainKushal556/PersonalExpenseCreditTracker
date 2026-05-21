CREATE TABLE Task (
    Task_ID INT PRIMARY KEY IDENTITY(1,1),
    User_ID INT NOT NULL,
    Priority_ID INT NOT NULL,
    Status_ID INT NOT NULL,
    Task_Title VARCHAR(150) NOT NULL,
    Deadline DATE NOT NULL,

    FOREIGN KEY (User_ID) REFERENCES Users(User_ID),
    FOREIGN KEY (Priority_ID) REFERENCES Task_Priorities(Priority_ID),
    FOREIGN KEY (Status_ID) REFERENCES Task_Status(Status_ID)
);