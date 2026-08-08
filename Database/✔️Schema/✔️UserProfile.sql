CREATE TABLE tblUserProfile
(
    ProfileID INT PRIMARY KEY IDENTITY(1,1),

    UserID INT NOT NULL,

    FullName VARCHAR(100) NOT NULL,

    ProfilePhoto VARBINARY(MAX) NULL,

    DOB DATE NULL,

    GenderID INT NULL,

    Address VARCHAR(500) NULL,

    CONSTRAINT FK_tblUserProfile_tblUsers
        FOREIGN KEY (UserID)
        REFERENCES tblUsers(UserID),

    CONSTRAINT FK_tblUserProfile_tblGender
        FOREIGN KEY (GenderID)
        REFERENCES tblGender(GenderID)
);