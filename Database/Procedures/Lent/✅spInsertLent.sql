CREATE PROC spInsertLent
	@UserID INT,
	@PersonID INT,
	@PaymentID INT,
	@StatusID INT,
	@Amount DECIMAL(10,2),
	@ReturnedAmount DECIMAL(10,2),
	@RemainingAmount DECIMAL(10,2),
	@DeadlineAT DATETIME,
	@Description VARCHAR(MAX)
AS
BEGIN
	
	-- Variable Declaration
	DECLARE @SubCategoryId INT;
	DECLARE @CategoryId INT;

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

        
			-- Check PersonID
			IF NOT EXISTS(SELECT 1
						  FROM tblPersons
						  WHERE PersonID = @PersonID)
			BEGIN
				SELECT 'Invalid PersonID!!' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

        
			-- Check PaymentID
			IF NOT EXISTS(SELECT 1
						  FROM tblPaymentType
						  WHERE PaymentID = @PaymentID)
			BEGIN
				SELECT 'Invalid PaymentID!!' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END


			-- Check StatusID
			IF NOT EXISTS(SELECT 1
						  FROM tblLentBorrowStatus
						  WHERE StatusID = @StatusID)
			BEGIN
				SELECT 'Invalid StatusID!!' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

			-- Check Amount
			IF @Amount <= 0
			BEGIN
				SELECT 'Amount Must Be Greater Than 0!!' AS Message
				ROLLBACK TRANSACTION
				RETURN

			END

			-- Get CategoryId & SubCategoryId From tblExpenseSubCategory
			SELECT @CategoryId = CategoryId,
			@SubCategoryId = SubCategoryId
			FROM tblExpenseSubCategory
			WHERE SubCategoryName = 'Lent';

			--Insert Lent on Lent Table
			INSERT INTO tblLent
			(
				UserID,
				PersonID,
				PaymentID,
				StatusID,
				Amount,
				ReturnedAmount,
				RemainingAmount,
				DeadlineAt,
				Description
			)
			VALUES
			(
				@UserID,
				@PersonID,
				@PaymentID,
				@StatusID,
				@Amount,
				@ReturnedAmount,
				@RemainingAmount,
				@DeadlineAT,
				@Description
			);
			--Insert Lent on Expense Table
			INSERT INTO tblExpense
			(
				UserID,
				CategoryId,
				SubCategoryId,
				Amount,
				Description,
				PaymentID
			)
			VALUES
			(
				@UserID,
				@CategoryId,
				@SubCategoryId,
				@Amount,
				@Description,
				@PaymentID
			);

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END