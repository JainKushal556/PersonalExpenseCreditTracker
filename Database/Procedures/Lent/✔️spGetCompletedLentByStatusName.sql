CREATE PROC spGetCompletedLentByStatusName
@UserID INT
AS
BEGIN
	SELECT Prsn.PersonName, L.Amount, L.ReturnedAmount, L.RemainingAmount, Pay.PaymentName, S.StatusName, L.LentAt, L.DeadlineAt, L.Description
	FROM tblLent L
	LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	LEFT JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
	LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID

	WHERE L.UserID = @UserID AND S.StatusName = 'Paid' ORDER BY L.LentAt DESC;
END
