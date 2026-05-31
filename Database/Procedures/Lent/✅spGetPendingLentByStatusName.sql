CREATE PROC spGetPendingLentByStatusName 1
@UserID INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 
					FROM tblUserAuthentication
					WHERE UserID = @UserID AND Active = 1)
	BEGIN
		SELECT 'Invalid OR Inactive UserID!!' AS Message
		RETURN
	END

	IF NOT EXISTS (SELECT 1 FROM tblLent L
	LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	WHERE L.UserID = @UserID AND S.StatusName IN ('Pending', 'Overdue', 'Partially Paid'))
	BEGIN
		SELECT 'Not Pending Record Found' AS Message
	END

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
	LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
	LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID

	WHERE L.UserID = @UserID AND S.StatusName IN ('Pending', 'Overdue', 'Partially Paid')
	ORDER BY L.LentAt DESC;
END