CREATE PROCEDURE spFilterCreditByCategoryAndSubCategory
(
    @UserID INT,
    @CategoryID INT,
    @SubCategoryID INT
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
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'SubCategory does not belong to selected Category' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCredit
        WHERE UserID = @UserID
        AND CategoryID = @CategoryID
        AND SubCategoryID = @SubCategoryID
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
        Credit.Description,
        PaymentType.PaymentName,
        Credit.CreditAt
    FROM tblCredit Credit
    INNER JOIN tblCreditCategory CreditCategory
        ON Credit.CategoryID = CreditCategory.CategoryID
    INNER JOIN tblCreditSubCategory CreditSubCategory
        ON Credit.SubCategoryID = CreditSubCategory.SubCategoryID
    INNER JOIN tblPaymentType PaymentType
        ON Credit.PaymentID = PaymentType.PaymentID
    WHERE Credit.UserID = @UserID
    AND Credit.CategoryID = @CategoryID
    AND Credit.SubCategoryID = @SubCategoryID
    ORDER BY Credit.CreditAt DESC

END
GO