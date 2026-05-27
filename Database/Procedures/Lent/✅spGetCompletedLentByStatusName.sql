CREATE PROC spGetCompletedLentByStatusName
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
	LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	LEFT JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
	LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID

	WHERE L.UserID = @UserID AND S.StatusName = 'Paid' ORDER BY L.LentAt ASC;
END


-- complete l;emnt ee asbe na bcz where ee only used id check hoche status noy . er status id diye check hbew or remaning amount dekhe 
-- usser tabl;e ee join korar proyojon ee nae 
-- order by use korbi date wise dekhabe latest record top e asbe.
-- same left join use korle valo hbe research korbi

-- All Mistaks are solved. After checked please these command lines are remove. These lines are for understanding only.