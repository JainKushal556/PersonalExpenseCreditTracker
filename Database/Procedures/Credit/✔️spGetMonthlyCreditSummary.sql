CREATE PROCEDURE spGetMonthlyCreditSummary
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
        YEAR(CreditAt) AS [Year],
        MONTH(CreditAt) AS [Month],
        SUM(Amount) AS TotalCredit
    FROM tblCredit
    WHERE UserID = @UserID
    GROUP BY 
        YEAR(CreditAt),
        MONTH(CreditAt)
    ORDER BY 
        [Year] DESC,
        [Month] DESC;

END
GO