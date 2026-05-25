CREATE PROC spGetCompletedLent
@UserID INT
AS
BEGIN
	SELECT Prsn.PersonName, L.Amount, L.ReturnedAmount, L.RemainingAmount, Pay.PaymentName, S.StatusName, L.LentAt, L.DeadlineAt, L.Description
	FROM tblLent L
	INNER JOIN tblUsers U ON L.UserID = U.UserID
	INNER JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	INNER JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
	INNER JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID

	WHERE U.UserID = @UserID;
END
