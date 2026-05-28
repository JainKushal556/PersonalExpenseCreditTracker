CREATE PROC spInsertPerson
@UserID INT, @PersonName VARCHAR(100), @PhoneNumber VARCHAR(20), @Address VARCHAR(MAX)
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
	
		--Insert Person on Lent Table
		INSERT INTO tblLentPersons
		(
			PersonName,
			PhoneNumber,
			Address
		)
		VALUES
		(
			@PersonName,
			@PhoneNumber,
			@Address
		);
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END