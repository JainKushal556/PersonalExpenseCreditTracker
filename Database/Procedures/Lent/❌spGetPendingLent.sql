CREATE PROC spGetPendingLent
@UserID INT
AS
BEGIN
	SELECT Prns.PersonName, L.Amount, S.StatusName
	FROM tblLent L
	JOIN tblUsers U ON L.UserID = U.UserID
	JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	JOIN tblLentPersons Prns ON L.PersonID = Prns.PersonID
	WHERE U.UserID = @UserID AND S.StatusName = 'Pending';
END

-- user table er sathe join korar to proyojon ee nae direct to l.userid ke jae id ta ascxhe check kore dfilter korbi . 
-- status check id diye hbe naki status name diye or remaning amount 0 thakleo pending hbe to ota 
-- er only person name amount status select korle ki kore hbe ui te jokhon filter korbi all data ee to jbe . ae 3 te + deadline er jodi kichu thke 

