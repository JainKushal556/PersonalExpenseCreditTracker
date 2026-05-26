CREATE PROC spReturnLentByReturnAmount
@LentID INT, @PaymentName VARCHAR(50), @ReturnedAmount DECIMAL(10,2), @Description VARCHAR(MAX),
@CreditSubCategoryID INT, @CreditCategoryID INT
AS
BEGIN
	DECLARE @TotalAmount DECIMAL(10,2);
	DECLARE @RemainingAmount DECIMAL(10,2);
	DECLARE @NewRemainingAmount DECIMAL(10,2);
	DECLARE @NewReturnedAmount DECIMAL(10,2);
	DECLARE @OldReturnedAmount DECIMAL(10,2);
	DECLARE @StatusID INT;
	DECLARE @PaymentID INT;
	DECLARE @UserID INT;

	--Get Amount & RemainingAmount
	SELECT @TotalAmount = Amount,
	@RemainingAmount = RemainingAmount,
	@OldReturnedAmount = ReturnedAmount,
	@UserID = UserID
	FROM tblLent
	WHERE LentID = @LentID;


	--Get PaymentID Using Payment Type name
	SELECT @PaymentID = PaymentID FROM tblPaymentType
	WHERE PaymentName = @PaymentName;


	--IF RemainingAmount is NULL THEN @RemainingAmount = @TotalAmount
	IF @RemainingAmount is NULL
	BEGIN
		SET @RemainingAmount = @TotalAmount;
	END


	--Calculating  New RemainingAmount
	SET @NewRemainingAmount = @RemainingAmount - @ReturnedAmount;

	--Calculate Total Returned Amount
	SET @NewReturnedAmount = @ReturnedAmount + @OldReturnedAmount;


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
			@CreditCategoryID,
			@CreditSubCategoryID,
			@PaymentID,
			@ReturnedAmount,
			@Description
			);


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
			@CreditCategoryID,
			@CreditSubCategoryID,
			@PaymentID,
			@ReturnedAmount,
			@Description
			);
	END
	ELSE
	BEGIN
		RAISERROR('Returned amount exceeds remaining amount.',16,1);
	END
END