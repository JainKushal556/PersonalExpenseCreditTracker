CREATE PROC spGetAllPersons
@UserID INT
AS
BEGIN
	BEGIN TRY
		IF NOT EXISTS (SELECT 1 
						FROM tblUserAuthentication
						WHERE UserID = @UserID AND Active = 1)
		BEGIN
			SELECT 'Invalid OR Inactive UserID!!' AS Message
			RETURN
		END

		--Print Persons of Person Table
		SELECT  PersonName, PhoneNumber, Address
		FROM tblPersons
		WHERE UserID = @UserID;

	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END