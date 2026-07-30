CREATE PROC spGetCreditSubCategoryByCategoryID
(
    @CategoryID INT
)
AS
BEGIN
    BEGIN TRY

        -- Get Sub Categories by CategoryID
        SELECT
            SubCategoryID,
            SubCategoryName
        FROM tblCreditSubCategory
        WHERE CategoryID = @CategoryID
        ORDER BY SubCategoryName ASC;

    END TRY
    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH
END
GO
