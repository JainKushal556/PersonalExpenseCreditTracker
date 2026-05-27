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

	-- Get CategoryId & SubCategoryId From tblExpenseSubCategory
	SELECT @CategoryId = CategoryId,
	@SubCategoryId = SubCategoryId
	FROM tblExpenseSubCategory
	WHERE SubCategoryName = 'Lent';


	BEGIN TRY
		IF @Amount > 0 --Check Amount is Positive
		BEGIN
			--Insert Lent on Lent Table
			INSERT INTO tblLent
			(UserID, PersonID, PaymentID, StatusID, Amount, ReturnedAmount, RemainingAmount, DeadlineAt, Description)
			VALUES
			(@UserID,@PersonID,@PaymentID,@StatusID,@Amount,@ReturnedAmount,@RemainingAmount,@DeadlineAT,@Description);

			--Insert Lent on Expense Table
			INSERT INTO tblExpense
			(UserID, CategoryId, SubCategoryId, Amount, Description, PaymentID)
			VALUES
			(@UserID, @CategoryId, @SubCategoryId, @Amount, @Description, @PaymentID);
		END
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE()
	END CATCH
END

--transaction use korte hbe . commit rollback AE command gulo 
--print er jaygay select use kor
