CREATE TABLE Person (
    Person_ID INT PRIMARY KEY IDENTITY(1,1),

    Person_Name VARCHAR(100) NOT NULL,

    Phone_Number VARCHAR(15) NOT NULL,

    Address VARCHAR(255) NULL
);