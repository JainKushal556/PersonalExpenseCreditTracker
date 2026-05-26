CREATE PROC spGetAllLent
	@UserID INT
AS
BEGIN
	SELECT * FROM tblLent WHERE UserID = @UserID;
END


-- order by nae last record ta dekhabe first e thkbe
