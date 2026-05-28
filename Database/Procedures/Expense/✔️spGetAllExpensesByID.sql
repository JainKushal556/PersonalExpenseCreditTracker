CREATE PROCEDURE spGetAllExpensesByID
(
    @UserID INT
)
AS
BEGIN

    SET NOCOUNT ON

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
        FROM tblExpense
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Expense Record Found' AS Message
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

    ORDER BY Expense.ExpenseAt DESC

END
GO