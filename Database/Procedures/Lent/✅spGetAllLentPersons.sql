CREATE PROC spGetAllLentPersons
@UserID INT
AS
BEGIN
	SELECT Prsn.PersonName AS LentPersonName
	FROM tblLent L
	JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
	WHERE UserID = @UserID;
END
