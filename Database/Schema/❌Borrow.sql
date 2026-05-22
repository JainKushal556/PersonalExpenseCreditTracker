
CREATE TABLE Borrow_Details (
    Borrow_ID INT PRIMARY KEY IDENTITY(1,1),

    User_ID INT NOT NULL,
    Person_ID INT NOT NULL,
    Payment_ID INT NOT NULL,
    Status_ID INT NOT NULL,

    Amount DECIMAL(10,2) NOT NULL,

    Borrow_at DATETIME NOT NULL,
    Return_at DATETIME NOT NULL,

    Description VARCHAR(255) NOT NULL,

    FOREIGN KEY (User_ID) REFERENCES Users(User_ID),

    FOREIGN KEY (Person_ID) REFERENCES Person(Person_ID),

);