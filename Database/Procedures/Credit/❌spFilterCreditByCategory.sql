CREATE PROCEDURE spFilterCreditByCategoryAndSubCategory
(
    @UserID INT,
    @CategoryID INT,
    @SubCategoryID INT
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

      IF NOT EXISTS
      (
        SELECT 1
        FROM tblCreditSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND CategoryID = @CategoryID
     )
     BEGIN
      PRINT 'Invalid SubCategoryID'
     RETURN
    END

    SELECT *FROM tblCredit
    WHERE UserID = @UserID
   	AND CategoryID = @CategoryID
    AND SubCategoryID = @SubCategoryID
    ORDER BY CreditAt DESC
END
GO

--print er jaygay select use korte hbe 
--select * kora jbe na required field gulo select korte hbe
--active check kora nae user active thkle ee hbe inactive user er khetre hbe na eta 
--no record found ae ta nae 