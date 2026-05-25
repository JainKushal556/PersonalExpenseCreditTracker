CREATE PROC spGetLentPersonHistory
@PersonID INT
AS
BEGIN
	SELECT Prsn.PersonName, L.Amount, Pay.PaymentName, S.StatusName, L.LentAt, L.DeadlineAt, L.Description
	FROM tblLent L
	INNER JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
	INNER JOIN tblPaymentType Pay ON L.PaymentID = Pay.PaymentID
	INNER JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	WHERE Prsn.PersonID = @PersonID;
END