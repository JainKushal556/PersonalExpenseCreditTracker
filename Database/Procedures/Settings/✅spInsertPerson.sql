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

		IF @UserID IS NULL
		BEGIN
			SELECT 'User ID is Null' AS Message
			RETURN
		END

		IF TRIM(@PersonName) = ''
		BEGIN
			SELECT 'Person Name is Null' AS Message
			RETURN
		END

		IF TRIM(@PhoneNumber) = ''
		BEGIN
			SELECT 'Phone Number is Null' AS Message
			RETURN
		END

		--Insert Person on Person Table
		INSERT INTO tblPersons
		(
			UserID,
			PersonName,
			PhoneNumber,
			Address
		)
		VALUES
		(
			@UserID,
			@PersonName,
			@PhoneNumber,
			@Address
		);
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END