CREATE PROCEDURE spFilterExpenseByAmountRange
(
    @UserID INT,
    @MinAmount DECIMAL(10,2),
    @MaxAmount DECIMAL(10,2)
)
AS
BEGIN
    SET NOCOUNT ON
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END
    
    IF @MinAmount < 0 OR @MaxAmount < 0
    BEGIN
        SELECT 'Amount cannot be negative' AS MESSAGE
        RETURN
    END
    
    IF @MinAmount > @MaxAmount
    BEGIN
        SELECT 'MinAmount cannot be greater than MaxAmount' AS MESSAGE
        RETURN
    END
    
    SELECT 
        E.ExpenseID,
        E.UserID,
        E.CategoryID,
        C.CategoryName,
        E.SubCategoryID,
        SC.SubCategoryName,
        E.PaymentID,
        P.PaymentName,
        E.Amount,
        E.Description,
        E.ExpenseAt
    FROM tblExpense E
    INNER JOIN tblExpenseCategory C ON E.CategoryID = C.CategoryID
    INNER JOIN tblExpenseSubCategory SC ON E.SubCategoryID = SC.SubCategoryID
    INNER JOIN tblPaymentType P ON E.PaymentID = P.PaymentID
    WHERE E.UserID = @UserID
    AND E.Amount >= @MinAmount
    AND E.Amount <= @MaxAmount
    ORDER BY E.Amount DESC, E.ExpenseAt DESC

END
GO
