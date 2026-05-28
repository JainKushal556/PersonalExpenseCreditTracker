CREATE PROCEDURE spGetAllPaymentTypes
AS
BEGIN

    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblPaymentType
    )
    BEGIN
        SELECT 'No Payment Type Found' AS Message
        RETURN
    END

    SELECT
        PaymentID,
        PaymentName
    FROM tblPaymentType
    ORDER BY PaymentName ASC;

END
