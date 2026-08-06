CREATE PROCEDURE spGetExpenseSubCategoryByCategoryID
(
    @CategoryID INT
)
AS
BEGIN
    BEGIN TRY

        -- Get Expense Sub Categories by CategoryID
        SELECT
            SubCategoryID,
            SubCategoryName
        FROM tblExpenseSubCategory
        WHERE CategoryID = @CategoryID
        ORDER BY SubCategoryName ASC;

    END TRY
    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH
END
GO
