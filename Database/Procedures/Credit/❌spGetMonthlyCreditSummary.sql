CREATE PROCEDURE spGetMonthlyCreditSummary
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        YEAR(CreditAt) AS [Year],
        MONTH(CreditAt) AS [Month],
        SUM(Amount) AS TotalCredit
    FROM tblCredit
    GROUP BY 
        YEAR(CreditAt),
        MONTH(CreditAt)
    ORDER BY 
        [Year] DESC,
        [Month] DESC;
END;
GO

--etay user_id depend kore report asbe . akhon all user er amount total kore diye diche 
--active user validation ta nae
