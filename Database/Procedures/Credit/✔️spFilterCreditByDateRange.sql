CREATE PROCEDURE spFilterCreditByDateRange
(
  @UserID INT,
  @FromDate DATETIME,
  @ToDate DATETIME
)
AS
BEGIN
    SET NOCOUNT ON
	    IF NOT EXISTS
		  (
		     SELECT 1
			  FROM tblUserAuthentication UserAuthentication
			  WHERE UserAuthentication.UserID = @UserID
			  AND UserAuthentication.Active = 1
		  )
		  BEGIN
		    SELECT 'Invalid Or Inactive User' AS MESSAGE
			RETURN
		  END

		  IF @FromDate > @ToDate
		   BEGIN
		     SELECT 'FromDate Cannot Be Greater Than ToDate' AS MESSAGE
			 RETURN
			END

          IF NOT EXISTS
		    (
			   SELECT 1
			   FROM tblCredit
			    WHERE UserID = @UserID
				AND CAST(CreditAt AS DATE)
				BETWEEN @FromDate AND @ToDate
			)
			BEGIN
			  SELECT 'NO RECORD FOUND' AS MESSAGE
			  RETURN
			END

			SELECT
			   Credit.CreditID,
			   CreditCategory.CategoryName,
			   CreditSubCategory.SubCategoryName,
			   Credit.Amount,
			   LTRIM(RTRIM(Credit.Description)) AS Description,
			   PaymentType.PaymentName,
			   Credit.CreditAt
              
			  FROM tblCredit Credit

			  LEFT JOIN tblCreditCategory CreditCategory
			    ON Credit.CategoryID =CreditCategory.CategoryID

				LEFT JOIN tblCreditSubCategory CreditSubCategory
				 ON Credit.SubCategoryID = CreditSubCategory.SubCategoryID

				 LEFT JOIN tblPaymentType  PaymentType
				  ON Credit.PaymentID = PaymentType.PaymentID

				  WHERE Credit.UserID =@UserID
				  AND CAST(Credit.CreditAt AS DATE)
				  BETWEEN @FromDate AND @ToDate

                ORDER BY Credit.CreditAt DESC
END
GO