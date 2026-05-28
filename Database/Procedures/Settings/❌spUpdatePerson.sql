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
						WHERE PersonID = @PersonID)
		BEGIN
			SELECT 'Invalid PersonID!!' AS Message
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

		--Update Person on Person Table
		UPDATE tblPersons
			SET PersonName = @PersonName,
			PhoneNumber = @PhoneNumber,
			Address = @Address
			WHERE PersonID = @PersonID;

	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END



-- PersonID validation e ownership check nei, onno user er PersonID update korte parbe.
-- TRIM(@PersonName) = '' → NULL handle korbe na.
-- TRIM(@PhoneNumber) = '' → NULL handle korbe na.
-- Input trim kore update korche na, extra spaces DB te store hobe.
-- Success message nei.
-- SET NOCOUNT ON missing.