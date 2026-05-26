CREATE PROC spGetLentPersonHistory
@PersonID INT, @UserID INT
AS
BEGIN
	SELECT Prsn.PersonName, L.Amount, L.ReturnedAmount, L.RemainingAmount, Pay.PaymentName, S.StatusName, L.LentAt, L.DeadlineAt, L.Description
	FROM tblLent L
	JOIN tblLentPersons Prsn ON L.PersonID = Prsn.PersonID
	JOIN tblPaymentType Pay ON L.PaymentID = Pay.PaymentID
	JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	WHERE Prsn.PersonID = @PersonID AND L.UserID = @UserID
	ORDER BY L.LentAt ASC;
END

-- user table er sathe join korar to proyojon ee nae direct to l.userid ke jae id ta ascxhe check kore dfilter korbi .
-- person er sathe tar total ReturnedAmount and RemainingAmount ae 2to oo dekhabe 
-- etay order by hbe latest record mean date wise.
-- er ektu dekhbi research kore inner join korle data nao aste pare jodi if payment/status/person delete kora hoy.

-- All Problem are solved. After check the code please remove these lines.
