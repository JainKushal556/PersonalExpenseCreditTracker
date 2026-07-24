CREATE PROC spGetAllLentBorrowStatus
AS
BEGIN
	BEGIN TRY
		
		--Print Status of LentBorrowStatus Table
		SELECT  StatusID ,StatusName 
		FROM tblLentBorrowStatus
		ORDER BY StatusName ASC;

	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END
