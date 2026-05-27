CREATE PROC spGetPendingLentByStatusName
@UserID INT
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
		LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
		LEFT JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
		LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID

		WHERE L.UserID = @UserID AND S.StatusName = 'Pending' ORDER BY L.LentAt DESC;
	END
	ELSE
	BEGIN
		PRINT 'User Not Active'
	END
END