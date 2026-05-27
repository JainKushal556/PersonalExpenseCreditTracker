CREATE PROC spDeletePerson
@UserID INT, @PersonID INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			IF NOT EXISTS (SELECT 1 
							FROM tblUserAuthentication
							WHERE UserID = @UserID AND Active = 1)
			BEGIN
				SELECT 'Invalid OR Inactive UserID!!' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

			IF NOT EXISTS (SELECT 1 
							FROM tblLentPersons
							WHERE PersonID = @PersonID)
			BEGIN
				SELECT 'Invalid PersonID!!' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END
			
			--Delete Person on Lent Table
			DELETE FROM tblLent
			WHERE PersonID = @PersonID;

			--Delete Person on Person Table
			DELETE FROM tblLentPersons
			WHERE PersonID = @PersonID;

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
		ROLLBACK TRANSACTION
	END CATCH
END