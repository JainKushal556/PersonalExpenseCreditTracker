CREATE PROC spInsertLent
	@UserID INT,
	@PersonID INT,
	@PaymentID INT,
	@Amount DECIMAL(10,2),
	@DeadlineAT DATETIME,
	@Description VARCHAR(MAX)
AS
BEGIN
	
	-- Variable Declaration
	DECLARE @SubCategoryId INT;
	DECLARE @CategoryId INT;
	DECLARE @ReturnedAmount DECIMAL(10,2);
	DECLARE @RemainingAmount DECIMAL(10,2);
	DECLARE @StatusID INT;

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
						  WHERE PersonID = @PersonID AND UserID = @UserID)
			BEGIN
				SELECT 'Person Not Exist' AS Message
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


			SELECT @StatusID = StatusID
			FROM tblLentBorrowStatus
			WHERE StatusName = 'Pending'

			IF CAST(@DeadlineAT AS DATE) < CAST(GETDATE() AS DATE)
			BEGIN
				SELECT 'Deadline Date Cannot Be Earlier Than Today' AS Message
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

			SET @ReturnedAmount = 0;
			SET @RemainingAmount = @Amount;

			-- Get CategoryId & SubCategoryId From tblExpenseSubCategory
			SELECT @CategoryId = CategoryId,
			@SubCategoryId = SubCategoryId
			FROM tblExpenseSubCategory
			WHERE SubCategoryName = 'Lent Given';
            
			IF @SubCategoryId IS NULL
			BEGIN
				SELECT 'Lent Given SubCategory Not Found' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

			IF @StatusID IS NULL
			BEGIN
				SELECT 'Pending Status Not Found' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

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

		SELECT 'Lent Insert Successfully' AS Message
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END