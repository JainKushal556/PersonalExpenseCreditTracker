CREATE PROCEDURE spFilterCreditByAmountRange
(
    @UserID INT,
    @MinAmount DECIMAL(10,2),
    @MaxAmount DECIMAL(10,2)
)
AS
BEGIN
    SET NOCOUNT OFF
    
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

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCredit
        WHERE UserID = @UserID
        AND Amount >= @MinAmount
        AND Amount <= @MaxAmount
    )
    BEGIN
        SELECT 'No Record Found' AS MESSAGE
        RETURN
    END
    
    SELECT 
        CR.CreditID,
        CR.UserID,
        CR.CategoryID,
        C.CategoryName,
        CR.SubCategoryID,
        SC.SubCategoryName,
        CR.PaymentID,
        P.PaymentName,
        CR.Amount,
        CR.Description,
        CR.CreditAt
    FROM tblCredit CR
    INNER JOIN tblCreditCategory C ON CR.CategoryID = C.CategoryID
    INNER JOIN tblCreditSubCategory SC ON CR.SubCategoryID = SC.SubCategoryID
    INNER JOIN tblPaymentType P ON CR.PaymentID = P.PaymentID
    WHERE CR.UserID = @UserID
    AND CR.Amount >= @MinAmount
    AND CR.Amount <= @MaxAmount
    ORDER BY CR.Amount DESC, CR.CreditAt DESC

END
GO
