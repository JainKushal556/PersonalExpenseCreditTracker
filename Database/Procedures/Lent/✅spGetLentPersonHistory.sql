CREATE PROC spGetLentPersonHistory
@PersonID INT
AS
BEGIN
	SELECT Prsn.PersonName, L.Amount, Pay.PaymentName, S.StatusName, L.LentAt, L.DeadlineAt, L.Description
	FROM tblLent L
	JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
	JOIN tblPaymentType Pay ON L.PaymentID = Pay.PaymentID
	JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	WHERE Prsn.PersonID = @PersonID;
END