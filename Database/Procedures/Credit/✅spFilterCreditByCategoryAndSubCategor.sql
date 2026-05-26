CREATE PROCEDURE spFilterCreditByCategory
 (
   @UserID INT,
   @CategoryID INT
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
			WHERE CategoryID = @CategoryID
		  )
       BEGIN
           PRINT 'Invalid CategoryID'
         RETURN
        END

    SELECT *FROM tblCredit
     WHERE UserID = @UserID AND CategoryID = @CategoryID
     ORDER BY CreditAt DESC
END
GO