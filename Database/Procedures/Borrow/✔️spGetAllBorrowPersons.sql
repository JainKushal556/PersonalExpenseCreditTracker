CREATE PROC spGetAllBorrowPersons
@UserID INT
AS
BEGIN
	SELECT Prsn.PersonID, Prsn.PersonName AS BorrowPersonName, Prsn.PhoneNumber, Prsn.Address
	FROM tblBorrowPersons Prsn
	WHERE Prsn.UserID = @UserID;
END
