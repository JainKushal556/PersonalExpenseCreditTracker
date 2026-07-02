CREATE PROC spReturnLentByReturnAmount
@LentID INT, @PaymentID INT, @ReturnedAmount DECIMAL(10,2), @Description VARCHAR(MAX)
AS
BEGIN
	DECLARE @TotalAmount DECIMAL(10,2);
	DECLARE @RemainingAmount DECIMAL(10,2);
	DECLARE @NewRemainingAmount DECIMAL(10,2);
	DECLARE @NewReturnedAmount DECIMAL(10,2);
	DECLARE @OldReturnedAmount DECIMAL(10,2);
	DECLARE @StatusID INT;
	DECLARE @UserID INT;
	DECLARE @CategoryID INT;
	DECLARE @SubCategoryID INT;

	BEGIN TRY
		BEGIN TRANSACTION
			----------------------------All Validation-----------------------------------------
			IF NOT EXISTS (SELECT 1 FROM tblLent WHERE LentID = @LentID)
			BEGIN
				SELECT 'Invalid LentID!!' AS MESSAGE
				ROLLBACK TRANSACTION
				RETURN
			END

			IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentID = @PaymentID)
			BEGIN
				SELECT 'Invalid PaymentID!!' AS MESSAGE
				ROLLBACK TRANSACTION
				RETURN
			END

			IF @ReturnedAmount <= 0
			BEGIN
				SELECT 'Returned Amount Must Be Greater Than 0!' AS MESSAGE
				ROLLBACK TRANSACTION
				RETURN
			END

			----------------------------All Validation-----------------------------------------
			

			--Get Amount & RemainingAmount
			SELECT @TotalAmount = Amount,
			@RemainingAmount = RemainingAmount,
			@OldReturnedAmount = ReturnedAmount,
			@UserID = UserID
			FROM tblLent
			WHERE LentID = @LentID;

			--IF RemainingAmount is NULL THEN @RemainingAmount = @TotalAmount
			IF @RemainingAmount is NULL
			BEGIN
				SET @RemainingAmount = @TotalAmount;
			END


			--Calculating  New RemainingAmount
			SET @NewRemainingAmount = @RemainingAmount - @ReturnedAmount;

			--Calculate Total Returned Amount
			SET @NewReturnedAmount = @ReturnedAmount + @OldReturnedAmount;

			SELECT @SubCategoryID = SubCategoryID,
			@CategoryID = CategoryID
			FROM tblCreditSubCategory
			WHERE SubCategoryName = 'Lent Returned';
             
            IF @CategoryID IS NULL OR @SubCategoryID IS NULL
            BEGIN
            SELECT 'Lent Returned Credit Category/SubCategory Not Found' AS Message
            ROLLBACK TRANSACTION
            RETURN
            END

			IF @NewRemainingAmount = 0
			BEGIN
				--Get 'Complete' StatusName ID
				SELECT @StatusID = StatusID FROM tblLentBorrowStatus
				WHERE StatusName = 'Paid';

				--Update Lent Table Data
				UPDATE tblLent
				SET RemainingAmount = 0,
				ReturnedAmount = @NewReturnedAmount,
				StatusID = @StatusID
				WHERE LentID = @LentID;

			END
			ELSE IF @NewRemainingAmount > 0
			BEGIN
				--Get 'Pending' StatusName ID
				SELECT @StatusID = StatusID FROM tblLentBorrowStatus
				WHERE StatusName = 'Pending';

				--Update Lent Table Data
				UPDATE tblLent
				SET RemainingAmount = @NewRemainingAmount,
				ReturnedAmount = @NewReturnedAmount,
				StatusID = @StatusID
				WHERE LentID = @LentID;

			END
			ELSE
			BEGIN
				RAISERROR('Returned amount exceeds remaining amount.',16,1);
			END

			--Data Insert On Credit Table
				INSERT INTO tblCredit(
					UserID,
					CategoryID,
					SubCategoryID,
					PaymentID,
					Amount,
					Description
					)
				VALUES(
					@UserID, 
					@CategoryID,
					@SubCategoryID,
					@PaymentID,
					@ReturnedAmount,
					@Description
					);

			COMMIT TRANSACTION
			SELECT 'Lent Returned Successfully' AS MESSAGE
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			SELECT
				ERROR_MESSAGE() AS ErrorMessage
		END CATCH
END


-- eta ektu check korbi mne thik oo ache but problem oo ache null validatiopn nae kichu jaygay total ta dekhbi . partialy paid dekhache na kono khetre setao dekhbi 