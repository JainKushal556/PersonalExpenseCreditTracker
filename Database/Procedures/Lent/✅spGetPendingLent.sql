CREATE PROC spGetPendingLent
@UserID INT
AS
BEGIN
	SELECT Prns.PersonName, L.Amount, S.StatusName
	FROM tblLent L
	INNER JOIN tblUsers U ON L.UserID = U.UserID
	INNER JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	INNER JOIN tblLentPersons Prns ON L.PersonID = Prns.PersonID
	WHERE U.UserID = @UserID AND S.StatusName = 'Pending';
END