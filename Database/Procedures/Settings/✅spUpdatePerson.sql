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
						FROM tblPersons
						WHERE PersonID = @PersonID AND UserID = @UserID)
		BEGIN
			SELECT 'Invalid PersonID!!' AS Message
			RETURN
		END
		
		IF TRIM(@PersonName) = '' OR @PersonName = 'NULL'
		BEGIN
			SELECT 'Person Name is Null' AS Message
			RETURN
		END

		IF TRIM(@PhoneNumber) = '' OR @PersonName = 'NULL'
		BEGIN
			SELECT 'Phone Number is Null' AS Message
			RETURN
		END

		--PhoneNumber and PersonName Space Check
		SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));
		SET @PersonName = LTRIM(RTRIM(@PersonName));

		--Update Person on Person Table
		UPDATE tblPersons
			SET PersonName = @PersonName,
			PhoneNumber = @PhoneNumber,
			Address = @Address
			WHERE PersonID = @PersonID;
		SELECT 'Person Detailes Updated' AS Message
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END