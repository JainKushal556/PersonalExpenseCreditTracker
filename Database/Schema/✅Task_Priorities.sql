CREATE TABLE Task_Priorities (
    Priority_ID INT PRIMARY KEY IDENTITY(1,1),
    Priority_Name VARCHAR(50) UNIQUE NOT NULL
);