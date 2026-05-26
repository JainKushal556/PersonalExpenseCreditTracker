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

--print use hbe na select use korte hbe
--select * kora jbe na required field gulo select korte hbe
--active check kora nae user active thkle ee hbe inactive user er khetre hbe na 
--invalid user id r pore return nae 
--no record found ae situation handle nae 
--foreign key link nae so catagory id asbe name asbe na 