CREATE PROCEDURE spGetMonthlyExpenseSummary
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
        YEAR(ExpenseAt) AS [Year],
        MONTH(ExpenseAt) AS [Month],
        SUM(Amount) AS TotalExpense
    FROM tblExpense
    WHERE UserID = @UserID
    GROUP BY 
        YEAR(ExpenseAt),
        MONTH(ExpenseAt)
    ORDER BY 
        [Year] DESC,
        [Month] DESC;

END
GO