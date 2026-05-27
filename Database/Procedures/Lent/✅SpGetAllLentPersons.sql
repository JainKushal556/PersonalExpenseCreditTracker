CREATE PROC spGetAllLentPersons
@UserID INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 
					FROM tblUserAuthentication
					WHERE UserID = @UserID AND Active = 1)
	BEGIN
		SELECT 'Invalid OR Inactive UserID!!' AS Message
	END

	SELECT DISTINCT(Prsn.PhoneNumber), Prsn.PersonName AS LentPersonName, Prsn.Address
	FROM tblLentPersons Prsn
	LEFT JOIN tblLent L ON L.PersonId = Prsn.PersonID
	WHERE L.UserID = @UserID;
END