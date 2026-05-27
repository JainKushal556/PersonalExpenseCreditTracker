CREATE PROC spGetLentPersonHistory
@PersonID INT, @UserID INT
AS
BEGIN
	IF EXISTS (SELECT 1 UserID FROM tblUserAuthentication WHERE UserID = @UserID AND Active = 1)
	BEGIN
		SELECT Prsn.PersonName,
			   L.Amount,
			   L.ReturnedAmount,
			   L.RemainingAmount,
			   Pay.PaymentName,
			   S.StatusName,
			   L.LentAt,
			   L.DeadlineAt,
			   L.Description
		FROM tblLent L
		LEFT JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
		LEFT JOIN tblPaymentType Pay ON L.PaymentID = Pay.PaymentID
		LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
		WHERE Prsn.PersonID = @PersonID AND L.UserID = @UserID
		ORDER BY L.LentAt DESC;
	END
	ELSE
	BEGIN
		PRINT 'User Not Active'
	END
END