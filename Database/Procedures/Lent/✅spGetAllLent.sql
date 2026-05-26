CREATE PROC spGetAllLent
	@UserID INT
AS
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
	JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
	JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID
	WHERE L.UserID = @UserID ORDER BY L.LentAt ASC
END