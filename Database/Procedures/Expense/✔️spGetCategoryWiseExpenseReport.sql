CREATE PROCEDURE spGetCategoryWiseExpenseReport
(
    @UserID INT
)
AS
BEGIN

    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication UserAuthentication
        INNER JOIN tblUsers Users
            ON UserAuthentication.UserID = Users.UserID
        WHERE Users.UserID = @UserID
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
        ISNULL(ExpenseCategory.CategoryName, 'Category Deleted') AS CategoryName,
        SUM(Expense.Amount) AS TotalExpense
    FROM tblExpense Expense
    LEFT JOIN tblExpenseCategory ExpenseCategory
        ON Expense.CategoryID = ExpenseCategory.CategoryID
    WHERE Expense.UserID = @UserID
    GROUP BY 
        ISNULL(ExpenseCategory.CategoryName, 'Category Deleted')
    ORDER BY TotalExpense DESC;

END
GO