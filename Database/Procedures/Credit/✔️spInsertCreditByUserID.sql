CREATE PROCEDURE spInsertCreditByUserID
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

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditSubCategory
        WHERE SubCategoryID = @SubCategoryID
          AND CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'SubCategory does not belong to selected Category' AS Message;
        RETURN;
    END

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

    IF @Amount <= 0
    BEGIN
        SELECT 'Amount must be greater than zero' AS Message;
        RETURN;
    END

    SET @Description = LTRIM(RTRIM(@Description));

    IF @Description IS NULL OR @Description = ''
    BEGIN
        SELECT 'Description cannot be empty' AS Message;
        RETURN;
    END

    INSERT INTO tblCredit
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

    SELECT 'Credit inserted successfully' AS Message;

END
GO