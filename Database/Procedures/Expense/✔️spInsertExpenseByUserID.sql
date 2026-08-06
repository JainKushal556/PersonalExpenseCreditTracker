CREATE PROCEDURE spInsertExpenseByUserID
(
    @UserID INT,
    @CategoryID INT,
    @SubCategoryID INT,
    @Amount DECIMAL(10,2),
    @Description VARCHAR(MAX),
    @PaymentID INT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check User
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication UA
        INNER JOIN tblUsers U
            ON UA.UserID = U.UserID
        WHERE U.UserID = @UserID
          AND UA.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message;
        RETURN;
    END

    -- Check Category
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message;
        RETURN;
    END

    -- Check SubCategory
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryID = @SubCategoryID
          AND CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'SubCategory does not belong to selected Category' AS Message;
        RETURN;
    END

    -- Check Payment
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblPaymentType
        WHERE PaymentID = @PaymentID
    )
    BEGIN
        SELECT 'Invalid PaymentID' AS Message;
        RETURN;
    END

    -- Check Amount
    IF @Amount <= 0
    BEGIN
        SELECT 'Amount must be greater than zero' AS Message;
        RETURN;
    END

    -- Check Description
    SET @Description = LTRIM(RTRIM(@Description));

    IF @Description IS NULL OR @Description = ''
    BEGIN
        SELECT 'Description cannot be empty' AS Message;
        RETURN;
    END

    -- Insert
    INSERT INTO tblExpense
    (
        UserID,
        CategoryID,
        SubCategoryID,
        Amount,
        Description,
        PaymentID
    )
    VALUES
    (
        @UserID,
        @CategoryID,
        @SubCategoryID,
        @Amount,
        @Description,
        @PaymentID
    );

    SELECT 'Expense inserted successfully' AS Message;
END
GO