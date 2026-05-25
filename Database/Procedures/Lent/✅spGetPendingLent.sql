CREATE PROC spGetPendingLent
@UserID INT
AS
BEGIN
	SELECT p.PersonName, l.Amount, s.StatusName
	FROM tblLent l
	INNER JOIN tblUsers u ON l.UserID = u.UserID
	INNER JOIN tblLentBorrowStatus s ON l.StatusID = s.StatusID
	INNER JOIN tblLentPersons p ON l.PersonID = p.PersonID
	WHERE u.UserID = @UserID AND s.StatusName = 'Pending';
END