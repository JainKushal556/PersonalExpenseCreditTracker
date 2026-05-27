CREATE PROCEDURE spUpdateExpenseCategoryByUserID
(
  @UserID INT,
  @CategoryID INT,
  @CategoryName VARCHAR(MAX)
)
AS
BEGIN
    
	 SET NOCOUNT ON
	   IF NOT EXISTS
	     (
		    SELECT 1
			FROM tblUserAuthentication UserAuthentication
			WHERE UserAuthentication.UserID = @UserID
			AND UserAuthentication.Active =1
		 )
		 BEGIN
		    SELECT 'Invalid or Inactive User' AS MESSAGE
			RETURN
		  END

		  IF NOT EXISTS
		   (
		       SELECT 1
			   FROM tblExpenseCategory 
			   WHERE CategoryID = @CategoryID
		   )
		   BEGIN
		     SELECT 'Invalid CategoryID' AS MESSAGE
			 RETURN 
			END

			SET @CategoryName = LTRIM(RTRIM(@CategoryName))

			IF @CategoryName IS NULL
			  OR @CategoryName = ''
			  BEGIN
			     SELECT 'Category Name Cannot Be Empty' AS MESSAGE
			     RETURN
			  END

			  IF EXISTS
			   (
			     SELECT 1
				 FROM tblExpenseCategory
				 WHERE @CategoryName = @CategoryName
				 AND @CategoryID = @CategoryID
			   )
			   BEGIN
			     SELECT 'Category Name Already Exists'
				 RETURN
			   END

              UPDATE tblExpenseCategory
			  SET CategoryName = @CategoryName
			  WHERE CategoryID = @CategoryID

			  SELECT 'Expense Category Updated Successfully' AS MESSAGE

END
GO