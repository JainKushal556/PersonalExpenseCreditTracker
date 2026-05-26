CREATE PROC spGetAllLentPersons
@UserID INT
AS
BEGIN
	SELECT Prsn.PersonID, Prsn.PersonName AS LentPersonName, Prsn.PhoneNumber, Prsn.Address
	FROM tblLentPersons Prsn
	WHERE Prsn.UserID = @UserID;
END
