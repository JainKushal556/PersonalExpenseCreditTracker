CREATE PROCEDURE spDeleteExpenseCategoryByUserID
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
			 FROM tblUserAuthentication UserAuthentication
			 WHERE UserAuthentication.UserID = @UserID
			 AND UserAuthentication.Active = 1
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
		   END

		   IF EXISTS
		     (
			   SELECT 1
			   FROM tblExpense
			   WHERE CategoryID = @CategoryID
			 )
			 BEGIN
			   SELECT 'Category Cannot Be Deleted Because It Is Used In Expense Records' AS MESSAGE
			   RETURN
			  END

			   IF EXISTS
                  (
                     SELECT 1
                     FROM tblExpenseSubCategory
                     WHERE CategoryID = @CategoryID
                  )
              BEGIN
                SELECT 'Category Cannot Be Deleted Because SubCategories Exist' AS Message
             RETURN
             END

			 DELETE FROM tblExpenseCategory
              WHERE CategoryID = @CategoryID

              SELECT 'Expense Category Deleted Successfully' AS Message
END
GO