CREATE PROCEDURE spGetTodayCredit
(
    @UserID INT
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
        SELECT 'Invalid Or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCredit
        WHERE UserID = @UserID
        AND CAST(CreditAt AS DATE) = CAST(GETDATE() AS DATE)
    )
    BEGIN
        SELECT 'No Record Found' AS Message
        RETURN
    END

    SELECT
        Credit.CreditID,
        CreditCategory.CategoryName,
        CreditSubCategory.SubCategoryName,
        Credit.Amount,
        LTRIM(RTRIM(Credit.Description)) AS Description,
        PaymentType.PaymentName,
        Credit.CreditAt

         FROM tblCredit Credit

          LEFT JOIN tblCreditCategory CreditCategory
               ON Credit.CategoryID = CreditCategory.CategoryID

          LEFT JOIN tblCreditSubCategory CreditSubCategory
                ON Credit.SubCategoryID = CreditSubCategory.SubCategoryID

             LEFT JOIN tblPaymentType PaymentType
               ON Credit.PaymentID = PaymentType.PaymentID

             WHERE Credit.UserID = @UserID
                 AND CAST(Credit.CreditAt AS DATE) = CAST(GETDATE() AS DATE)

              ORDER BY Credit.CreditAt DESC

END
GO