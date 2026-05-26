CREATE PROCEDURE spInsertCredit
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
                    FROM tblUsers
                    WHERE UserID = @UserID
               )
         BEGIN
                PRINT 'Invalid UserID'
         RETURN
         END

		 IF NOT EXISTS
		   (
		      SELECT 1
			  FROM tblCreditCategory
			  WHERE CategoryID=@CategoryID
		   )
		   BEGIN
                PRINT 'Invalid CategoryID'
          RETURN
          END

		    IF NOT EXISTS
			  (
			       SELECT 1
				   FROM tblCreditSubCategory
				   WHERE SubCategoryID=@SubCategoryID
			  )
			  BEGIN
			      PRINT 'Invalid SubCategoryID'
              RETURN
			  END

            IF @Amount <=0
			 BEGIN
			  PRINT 'Amount must be greater than zero'
			  RETURN
			END

			IF @Description IS NULL
			  OR @Description=''
			  BEGIN
			    PRINT 'Description cannot be empty'
			 RETURN

			 END

			IF @CreditAt >GETDATE()
			 BEGIN
             PRINT 'Future date is not allowed'
             RETURN
            END
             
              INSERT INTO tblCredit(UserID,CategoryID,SubCategoryID,Amount,Description,PaymentID,CreditAt)
               VALUES(@UserID,@CategoryID,@SubCategoryID,@Amount,@Description,@PaymentID,@CreditAt)
               PRINT 'Credit inserted successfully'
END
GO

--print er jaygay select use korte hbe
--active user validation lgbe 
-- catagory id r sathe subcatagory relation check hoche na 
--description ke trim kore insert korbi 
