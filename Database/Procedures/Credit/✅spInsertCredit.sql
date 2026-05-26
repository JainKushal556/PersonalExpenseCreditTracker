CREATE PROCEDURE spInsertCreditByUserID
(
    @UserID INT,
    @CategoryID INT,
    @SubCategoryID INT,
    @Amount DECIMAL(10,2),
    @Description VARCHAR(MAX),
    @PaymentID INT,
    @CreditAt DATETIME
)
AS
BEGIN

    SET NOCOUNT ON

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
        FROM tblPaymentType
        WHERE PaymentID = @PaymentID
    )
    BEGIN
        SELECT 'Invalid PaymentID' AS Message
        RETURN
    END

    IF @Amount <= 0
    BEGIN
        SELECT 'Amount must be greater than zero' AS Message
        RETURN
    END

    SET @Description = LTRIM(RTRIM(@Description))

    IF @Description IS NULL
       OR @Description = ''
    BEGIN
        SELECT 'Description cannot be empty' AS Message
        RETURN
    END

    IF @CreditAt > GETDATE()
    BEGIN
        SELECT 'Future date is not allowed' AS Message
        RETURN
    END

    INSERT INTO tblCredit
    (
        UserID,
        CategoryID,
        SubCategoryID,
        Amount,
        Description,
        PaymentID,
        CreditAt
    )
    VALUES
    (
        @UserID,
        @CategoryID,
        @SubCategoryID,
        @Amount,
        @Description,
        @PaymentID,
        @CreditAt
    )

    SELECT 'Credit inserted successfully' AS Message

END
GO