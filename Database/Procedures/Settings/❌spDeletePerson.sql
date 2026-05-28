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

			IF EXISTS (SELECT 1 
							FROM tblBorrow
							WHERE PersonID = @PersonID)
			BEGIN
				SELECT 'You Still Have a Borrow Amount Left!! First Clear It.' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

			IF NOT EXISTS (SELECT 1 
							FROM tblPersons
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
			DELETE FROM tblPersons
			WHERE PersonID = @PersonID;

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
		ROLLBACK TRANSACTION
	END CATCH
END


-- taka paid kora baki ache chek korchis okay but okhne to mainly taka tai dekhis ne check korechis je person d tblborrow te thklae hbe je taka paid kora baki ache . so dekh rem,anin amount koto jodi 0 r bes hoy then eta okay er user id diye validate kor person id oi id ta oi user er ki na 
-- er person delete korle user 