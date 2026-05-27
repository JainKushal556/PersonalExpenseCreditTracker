CREATE PROCEDURE spInsertNewExpenseCategoryByUserID
(
   @UserID INT,
   @CategoryName VARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON
	   IF NOT EXISTS
	     (
		    SELECT 1
			FROM tblUserAuthentication UserAuthentication
			WHERE UserAuthentication.UserID=@UserID
			AND UserAuthentication.Active =1
		 )
		 BEGIN
		   SELECT 'Invalid or Inactive User' AS Message
		   RETURN
         END
		 SET @CategoryName = LTRIM(RTRIM(@CategoryName))
		 
		 IF @CategoryName IS NULL
		   OR @CategoryName =''
         BEGIN
		   SELECT 'Category Name cannot be empty' AS Message
		   RETURN
         END
		 IF EXISTS
		  (
		     SELECT 1
			 FROM tblExpenseCategory
			 WHERE CategoryName = @CategoryName
		  )
		  BEGIN
		    SELECT 'Category Already Exists' AS Message
		    RETURN
         END
		 
		 INSERT INTO tblExpenseCategory(CategoryName)
		  VALUES(@CategoryName)
     
	 SELECT 'Expense Category Inserted Successfully' AS Message

END
GO