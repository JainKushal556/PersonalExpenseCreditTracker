CREATE PROCEDURE spFilterExpenseByDateRange
(
  @UserID INT,
  @FromDate DATETIME,
  @ToDate DATETIME
)
AS
BEGIN
    SET NOCOUNT OFF
	    IF NOT EXISTS
		  (
		     SELECT 1
			  FROM tblUserAuthentication UserAuthentication
			  WHERE UserAuthentication.UserID = @UserID
			  AND UserAuthentication.Active = 1
		  )
		  BEGIN
		    SELECT 'Invalid Or Inactive User' AS MESSAGE
			RETURN
		  END

		  IF @FromDate > @ToDate
		   BEGIN
		     SELECT 'FromDate Cannot Be Greater Than ToDate' AS MESSAGE
			 RETURN
			END

          IF NOT EXISTS
		    (
			   SELECT 1
			   FROM tblExpense
			    WHERE UserID = @UserID
				AND CAST(ExpenseAt AS DATE)
				BETWEEN @FromDate AND @ToDate
			)
			BEGIN
			  SELECT 'NO RECORD FOUND' AS MESSAGE
			  RETURN
			END

			SELECT
			   Expense.ExpenseID,
			   ExpenseCategory.CategoryName,
               ExpenseSubCategory.SubCategoryName,
			   Expense.Amount,
			   LTRIM(RTRIM(Expense.Description)) AS Description,
			   PaymentType.PaymentName,
			   Expense.ExpenseAt

			  FROM tblExpense Expense

			  LEFT JOIN tblExpenseCategory ExpenseCategory
			    ON Expense.CategoryID =ExpenseCategory.CategoryID

				LEFT JOIN tblExpenseSubCategory ExpenseSubCategory
				 ON Expense.SubCategoryID = ExpenseSubCategory.SubCategoryID

				 LEFT JOIN tblPaymentType  PaymentType
				  ON Expense.PaymentID = PaymentType.PaymentID

				  WHERE Expense.UserID =@UserID
				  AND CAST(Expense.ExpenseAt AS DATE)
				  BETWEEN @FromDate AND @ToDate

                ORDER BY Expense.ExpenseAt DESC
END
GO