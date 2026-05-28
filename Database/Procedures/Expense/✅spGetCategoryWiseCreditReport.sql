CREATE PROCEDURE spGetCategoryWiseCreditReport
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
        FROM tblCredit
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Credit Record Found' AS Message
        RETURN
    END

    SELECT 
        ISNULL(CreditCategory.CategoryName, 'Category Deleted') AS CategoryName,
        SUM(Credit.Amount) AS TotalCredit
    FROM tblCredit Credit
    LEFT JOIN tblCreditCategory CreditCategory
        ON Credit.CategoryID = CreditCategory.CategoryID
    WHERE Credit.UserID = @UserID
    GROUP BY CreditCategory.CategoryName
    ORDER BY TotalCredit DESC;

END
GO
