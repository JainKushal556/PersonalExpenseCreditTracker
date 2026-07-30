CREATE PROCEDURE spFilterExpenseByCategory
(
    @UserID INT,
    @CategoryID INT
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
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpense
        WHERE UserID = @UserID
        AND CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'No Record Found' AS Message
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
        ON Expense.CategoryID = ExpenseCategory.CategoryID

    LEFT JOIN tblExpenseSubCategory ExpenseSubCategory
        ON Expense.SubCategoryID = ExpenseSubCategory.SubCategoryID

    LEFT JOIN tblPaymentType PaymentType
        ON Expense.PaymentID = PaymentType.PaymentID

    WHERE Expense.UserID = @UserID
    AND Expense.CategoryID = @CategoryID

    ORDER BY Expense.ExpenseAt DESC

END
GO