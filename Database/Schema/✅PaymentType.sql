
Create Table tblPaymentType(
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    PaymentName VARCHAR(50) UNIQUE NOT NULL 
);