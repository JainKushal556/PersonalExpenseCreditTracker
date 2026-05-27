CREATE PROCEDURE spGetAllCreditsByID
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
        FROM tblCredit
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Credit Record Found' AS Message
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

    ORDER BY Credit.CreditAt DESC

END
GO