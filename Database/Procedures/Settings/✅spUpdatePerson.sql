CREATE PROC spUpdatePerson
@UserID INT, @PersonID INT, @PersonName VARCHAR(100), @PhoneNumber VARCHAR(20), @Address VARCHAR(MAX)
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

		IF NOT EXISTS (SELECT 1 
						FROM tblLentPersons
						WHERE PersonID = @PersonID)
		BEGIN
			SELECT 'Invalid PersonID!!' AS Message
			RETURN
		END
	
		--Update Person on LentPerson Table
		UPDATE tblLentPersons
			SET PersonName = @PersonName,
			PhoneNumber = @PhoneNumber,
			Address = @Address
			WHERE PersonID = @PersonID;

	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END