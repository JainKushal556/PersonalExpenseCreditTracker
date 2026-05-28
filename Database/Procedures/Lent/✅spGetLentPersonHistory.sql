CREATE PROC spGetLentPersonHistory
@PersonID INT, @UserID INT
AS
BEGIN

	IF NOT EXISTS (SELECT 1 
				   FROM tblLent L
					JOIN tblPersons LP 
						ON L.PersonID = LP.PersonID
					WHERE L.UserID = @UserID
					AND L.PersonID = @PersonID)
	BEGIN
		SELECT 'Invalid PersonID OR No Lent History Found!' AS Message
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
	LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
	LEFT JOIN tblPaymentType Pay ON L.PaymentID = Pay.PaymentID
	LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	WHERE L.PersonID = @PersonID AND L.UserID = @UserID
	ORDER BY L.LentAt DESC;

END