CREATE PROC spGetAllCreditCategory
AS
BEGIN
    BEGIN TRY

        -- Get All Credit Categories
        SELECT
            CategoryID,
            CategoryName
        FROM tblCreditCategory
        ORDER BY CategoryName ASC;

    END TRY
    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH
END
GO
