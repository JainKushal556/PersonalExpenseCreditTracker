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


--userid check korte hbe exist ache ki na ++ active thkle okay na hole hbe na ?
--left join korte hbe jodi catagory or payment type delete hoye jay taholeo lent er data show hbe na.
--orderby date wise korte hbe lent at er upor DESC order e.
--descrioption insert er age trim korte hbe 
