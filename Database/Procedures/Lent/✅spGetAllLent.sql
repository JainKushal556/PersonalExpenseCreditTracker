CREATE PROC spGetAllLent
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

	SELECT L.LentID,
			Prsn.PersonName,
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
	WHERE L.UserID = @UserID ORDER BY L.LentAt DESC

END