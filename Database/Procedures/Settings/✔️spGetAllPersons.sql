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

		--Check Person Exist
		IF NOT EXISTS (SELECT 1 FROM tblPersons
		WHERE UserID = @UserID)
		BEGIN
			SELECT 'No Persons Found' AS Message
			RETURN
		END

		--Print Persons of Person Table
		SELECT  PersonID,PersonName, PhoneNumber, Address
		FROM tblPersons
		WHERE UserID = @UserID  ORDER BY PersonName ASC;

	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END