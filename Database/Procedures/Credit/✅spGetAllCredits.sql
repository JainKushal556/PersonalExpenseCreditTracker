CREATE PROCEDURE spGetAllCredits
 (
  @UserID INT
 )
 AS
 BEGIN
      SET NOCOUNT ON

	     IF NOT EXISTS
		 (
		   SELECT 1
		   FROM tblUsers
		   WHERE UserID= @UserID
		 )
		 BEGIN
		   PRINT 'INVALID UserID'
         END
        
		SELECT
		  Credit.CreditID,
		  CreditCategory.CategoryName,
		  CreditSubCategory.SubCategoryName,
		  Credit.Amount,
		  Credit.Description,
		  PaymentType.PaymentName,
		  Credit.CreditAt
		  FROM tblCredit Credit
		    INNER JOIN tblCreditCategory CreditCategory
			  ON Credit.CategoryID = CreditCategory.CategoryID
            INNER JOIN tblCreditSubCategory CreditSubCategory
			    ON Credit.SubCategoryID = CreditSubCategory.SubCategoryID 
             INNER JOIN tblPaymentType PaymentType
			    ON Credit.PaymentID = PaymentType.PaymentName
            WHERE Credit.UserID=@UserID
			ORDER BY Credit.CreditAt DESC

 END
 GO