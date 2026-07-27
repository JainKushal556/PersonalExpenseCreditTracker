-- =========================================================================
-- MASTER STORED PROCEDURES SCRIPT
-- =========================================================================

-- =========================================================================
-- SPs that NEED FIXES or are MISSING (Not included in this script):
-- =========================================================================
-- None

-- =========================================================================
-- ALL OKAY SPs (Included below):
-- =========================================================================


-- ==========================================================

-- SP: ✔️spChangePassword.sql

-- ==========================================================

CREATE PROCEDURE spChangePassword  
  
    @UserID INT,  
    @OldPassword VARCHAR(MAX),  
    @NewPassword VARCHAR(MAX)  

AS  
BEGIN  
  

    IF @NewPassword IS NULL OR LTRIM(RTRIM(@NewPassword)) = ''
    BEGIN
        SELECT 'New Password Cannot Be Empty' AS Message;
        RETURN;
    END


    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE
            UserID = @UserID
            AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

 
    IF EXISTS  
    (  
        SELECT 1  
        FROM tblUserAuthentication  
        WHERE  
            UserID = @UserID  
            AND Password = @OldPassword  
    )  
    BEGIN  

     
        IF @OldPassword = @NewPassword
        BEGIN
            SELECT 'New Password Cannot Be Same As Old Password' AS Message;
        END

        ELSE
        BEGIN
  
            UPDATE tblUserAuthentication  
            SET Password = @NewPassword  
            WHERE UserID = @UserID;  
  
            SELECT 'Password Changed Successfully' AS Message;

        END
  
    END  
  
    ELSE  
    BEGIN  
        SELECT 'Invalid Old Password' AS Message;
    END  
  
END;

GO


-- ==========================================================

-- SP: ✔️spDeleteUserProfilePhotoByUserId.sql

-- ==========================================================

CREATE PROCEDURE spDeleteUserProfilePhotoByUserId
    @UserID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserProfile
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Profile Not Found' AS Message;
        RETURN;
    END

    BEGIN TRY

        UPDATE tblUserProfile
        SET ProfilePhoto = NULL
        WHERE UserID = @UserID;

        SELECT 'Profile Photo Deleted Successfully' AS Message;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;






GO


-- ==========================================================

-- SP: ✔️spForgetPassword.sql

-- ==========================================================

CREATE PROCEDURE spForgetPassword  
    @Email VARCHAR(100),  
    @PhoneNumber VARCHAR(15),  
    @NewPassword VARCHAR(MAX)  
AS  
BEGIN  
  
    IF EXISTS  
    (  
        SELECT 1  
        FROM tblUserContact  
        WHERE   
            Email = @Email  
            AND PhoneNumber = @PhoneNumber  
    )  
    BEGIN  

        IF EXISTS
        (
            SELECT 1
            FROM tblUserAuthentication A
            INNER JOIN tblUserContact C
                ON A.UserID = C.UserID
            WHERE
                C.Email = @Email
                AND C.PhoneNumber = @PhoneNumber
                AND A.Password = @NewPassword
        )
        BEGIN
            SELECT 'New Password Cannot Be Same As Old Password' AS Message;
        END

        ELSE
        BEGIN
  
            UPDATE A  
            SET A.Password = @NewPassword  
            FROM tblUserAuthentication A  
            INNER JOIN tblUserContact C  
                ON A.UserID = C.UserID  
            WHERE   
                C.Email = @Email  
                AND C.PhoneNumber = @PhoneNumber;  
  
            SELECT 'Password Reset Successfully' AS Message;

        END
  
    END  
  
    ELSE  
    BEGIN  
        SELECT 'Invalid Email Or Phone Number' AS Message;
    END  
  
END;

GO


-- ==========================================================

-- SP: ✔️spGetActiveUserDetails.sql

-- ==========================================================

CREATE PROCEDURE spGetActiveUserDetails
(
    @UserID INT
)
AS
BEGIN

    -- User Exists Check
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END


    -- Active User Check
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Is Not Active' AS Message;
        RETURN;
    END


    -- Active User Details
    SELECT
        U.UserID,
        U.UserName,
        P.ProfilePhoto,
        C.Email,
        C.PhoneNumber,
        U.CreatedAt
    FROM tblUsers U

    LEFT JOIN tblUserProfile P
        ON U.UserID = P.UserID

    LEFT JOIN tblUserContact C
        ON U.UserID = C.UserID

    WHERE U.UserID = @UserID;

END;



GO


-- ==========================================================

-- SP: ✔️spLoginUser.sql

-- ==========================================================

CREATE PROCEDURE spLoginUser    
 
    @Email VARCHAR(100),    
    @Password VARCHAR(MAX)    

AS    
BEGIN    
      
    DECLARE @UserID INT;  
  
    IF EXISTS    
    (    
        SELECT 1    
        FROM tblUserContact C    
        INNER JOIN tblUserAuthentication A    
            ON C.UserID = A.UserID    
        WHERE     
            C.Email = @Email    
            AND A.Password = @Password    
    )    
    BEGIN    
    
        SELECT @UserID = C.UserID  
        FROM tblUserContact C  
        INNER JOIN tblUserAuthentication A    
            ON C.UserID = A.UserID  
        WHERE     
            C.Email = @Email    
            AND A.Password = @Password;  
  
      
        UPDATE tblUserAuthentication  
        SET Active = 1  
        WHERE UserID = @UserID;  
  
        SELECT 
            'Login Successful' AS Message,
            @UserID AS UserID;  
  
    END    
      
    ELSE    
    BEGIN    
        SELECT 'Invalid Email Or Password' AS Message;    
    END    
  
END;

GO


-- ==========================================================

-- SP: ✔️spLogoutUser.sql

-- ==========================================================

CREATE PROCEDURE spLogoutUser
    @UserID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
              AND Active = 1
    )
    BEGIN
        SELECT 'User Already Logged Out Or Invalid UserID' AS Message;
        RETURN;
    END

    UPDATE tblUserAuthentication
    SET Active = 0
    WHERE UserID = @UserID;

    SELECT 'Logout Successful' AS Message;

END;


GO


-- ==========================================================

-- SP: ✔️spRegisterUser.sql

-- ==========================================================

CREATE PROCEDURE spRegisterUser    

    @UserName VARCHAR(100),    
    @Email VARCHAR(100),    
    @PhoneNumber VARCHAR(15),    
    @Password VARCHAR(100)    

AS    
BEGIN    

    SET XACT_ABORT ON;

    DECLARE @UserID INT;    


    SET @UserName = LTRIM(RTRIM(@UserName));
    SET @Email = LTRIM(RTRIM(@Email));
    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));
    SET @Password = LTRIM(RTRIM(@Password));

	--empty and null checked
    IF @UserName IS NULL OR @UserName = ''
    BEGIN
        SELECT 'User Name Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @Email IS NULL OR @Email = ''
    BEGIN
        SELECT 'Email Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @PhoneNumber IS NULL OR @PhoneNumber = ''
    BEGIN
        SELECT 'Phone Number Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @Password IS NULL OR @Password = ''
    BEGIN
        SELECT 'Password Cannot Be Empty' AS Message;
        RETURN;
    END


    -- Duplicate check
    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE Email = @Email
    )
    BEGIN
        SELECT 'Email Already Exists' AS Message;
        RETURN;
    END


    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE PhoneNumber = @PhoneNumber
    )
    BEGIN
        SELECT 'Phone Number Already Exists' AS Message;
        RETURN;
    END


    BEGIN TRY

        BEGIN TRANSACTION;


        INSERT INTO tblUsers (UserName)
        VALUES (@UserName);

        SET @UserID = SCOPE_IDENTITY();

        INSERT INTO tblUserProfile
        (
            UserID,
            Name
        )
        VALUES
        (
            @UserID,
            @UserName
        );


        INSERT INTO tblUserContact
        (
            UserID,
            Email,
            PhoneNumber
        )
        VALUES
        (
            @UserID,
            @Email,
            @PhoneNumber
        );

        INSERT INTO tblUserAuthentication
        (
            UserID,
            Password,
            Active
        )
        VALUES
        (
            @UserID,
            @Password,
            0
        );


        COMMIT TRANSACTION;


        SELECT 
            @UserID AS UserID,
            'User Inserted Successfully' AS Message;

    END TRY  

    BEGIN CATCH 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;


        SELECT ERROR_MESSAGE() AS Message;

    END CATCH  

END;





GO


-- ==========================================================

-- SP: ✔️spUpdateProfilePhoto.sql

-- ==========================================================

CREATE PROCEDURE spUpdateProfilePhoto    
    
    @UserID INT,    
    @ProfilePhoto VARBINARY(MAX)    
    
AS    
BEGIN    

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE
            UserID = @UserID
            AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

    IF EXISTS    
    (    
        SELECT 1    
        FROM tblUserProfile    
        WHERE UserID = @UserID    
    )    
    BEGIN    

        UPDATE tblUserProfile    
        SET ProfilePhoto = @ProfilePhoto    
        WHERE UserID = @UserID;    
    
        SELECT 'Profile Photo Updated Successfully' AS Message;

    END    

    ELSE    
    BEGIN    
        SELECT 'User Not Found' AS Message;
    END    

END;



GO


-- ==========================================================

-- SP: ✔️spUpdateUserEmail.sql

-- ==========================================================

CREATE PROCEDURE spUpdateUserEmail  
    @UserID INT,  
    @Email VARCHAR(150)  
AS
BEGIN      

  
    IF @Email IS NULL OR LTRIM(RTRIM(@Email)) = ''
    BEGIN
        SELECT 'Email Cannot Be Empty' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

   
    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE Email = @Email
        AND UserID <> @UserID
    )
    BEGIN
        SELECT 'Email Already Exists' AS Message;
        RETURN;
    END


    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE UserID = @UserID
    )    
    BEGIN               

        UPDATE tblUserContact        
        SET Email = @Email         
        WHERE UserID = @UserID;         

        SELECT 'User Email Updated Successfully' AS Message;

    END    

    ELSE     
    BEGIN         
        SELECT 'Invalid UserID' AS Message;
    END 

END;




GO


-- ==========================================================

-- SP: ✔️spUpdateUserName.sql

-- ==========================================================


CREATE PROCEDURE spUpdateUserName  
    @UserID INT,  
    @Name VARCHAR(100)  
AS  
BEGIN  
  

    SET @Name = LTRIM(RTRIM(@Name));  
  

    IF @Name IS NULL OR @Name = ''  
    BEGIN  
        SELECT 'Name Cannot Be Empty' AS Message;  
        RETURN;  
    END  
  

    IF NOT EXISTS  
    (  
        SELECT 1  
        FROM tblUsers  
        WHERE UserID = @UserID  
    )  
    BEGIN  
        SELECT 'Invalid UserID' AS Message;  
        RETURN;  
    END  
  

    IF NOT EXISTS  
    (  
        SELECT 1  
        FROM tblUserAuthentication  
        WHERE UserID = @UserID  
        AND Active = 1  
    )  
    BEGIN  
        SELECT 'User Account Is Not Active' AS Message;  
        RETURN;  
    END  
  

	  IF EXISTS
	(
		SELECT 1
		FROM tblUsers
		WHERE UserName = @Name
		AND UserID <> @UserID 
	)
	BEGIN
		SELECT 'User Name Already Exists' AS Message;
		RETURN;
	END
  
    BEGIN TRY  

        BEGIN TRANSACTION;  
  
  

        UPDATE tblUsers  
        SET UserName = @Name  
        WHERE UserID = @UserID;  
  
  

        UPDATE tblUserProfile  
        SET Name = @Name  
        WHERE UserID = @UserID;  
  

        COMMIT TRANSACTION;  
  
        SELECT 'User Name Updated Successfully' AS Message;  
  
    END TRY  
  
    BEGIN CATCH  
  

        IF @@TRANCOUNT > 0  
            ROLLBACK TRANSACTION;  
  
  
        SELECT ERROR_MESSAGE() AS Message;  
  
    END CATCH  
  
END;





GO


-- ==========================================================

-- SP: ✔️spUpdateUserPhoneNumber.sql

-- ==========================================================

CREATE PROCEDURE spUpdateUserPhoneNumber
    @UserID INT,
    @PhoneNumber VARCHAR(15)
AS
BEGIN

    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));


    IF @PhoneNumber IS NULL OR @PhoneNumber = ''
    BEGIN
        SELECT 'Phone Number Cannot Be Empty' AS Message;
        RETURN;
    END


    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END


    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

    IF EXISTS
    (
        SELECT 1
        FROM tblUserContact
        WHERE PhoneNumber = @PhoneNumber
        AND UserID <> @UserID
    )
    BEGIN
        SELECT 'Phone Number Already Exists' AS Message;
        RETURN;
    END


    BEGIN TRY

        UPDATE tblUserContact
        SET PhoneNumber = @PhoneNumber
        WHERE UserID = @UserID;

        SELECT 'User Phone Number Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;


GO


-- ==========================================================

-- SP: ✔️spUpdateUserProfile.sql

-- ==========================================================

CREATE PROCEDURE spUpdateUserProfile
    @UserID INT,
    @Name VARCHAR(100),
    @Email VARCHAR(150),
    @PhoneNumber VARCHAR(15),
    @ProfilePhoto VARBINARY(MAX)
AS
BEGIN

    SET @Name = LTRIM(RTRIM(@Name));
    SET @Email = LTRIM(RTRIM(@Email));
    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));


    IF @Name IS NULL OR @Name = ''
    BEGIN
        SELECT 'Name Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @Email IS NULL OR @Email = ''
    BEGIN
        SELECT 'Email Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @PhoneNumber IS NULL OR @PhoneNumber = ''
    BEGIN
        SELECT 'Phone Number Cannot Be Empty' AS Message;
        RETURN;
    END


    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END


    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END


IF EXISTS
(
    SELECT 1
    FROM tblUsers
    WHERE UserName = @Name
	AND UserID = @UserID
)
BEGIN
    SELECT 'User Name Already Exists' AS Message;
    RETURN;
END


IF EXISTS
(
    SELECT 1
    FROM tblUserContact
    WHERE Email = @Email
)
BEGIN
    SELECT 'Email Already Exists' AS Message;
    RETURN;
END


IF EXISTS
(
    SELECT 1
    FROM tblUserContact
    WHERE PhoneNumber = @PhoneNumber
)
BEGIN
    SELECT 'Phone Number Already Exists' AS Message;
    RETURN;
END


    BEGIN TRY


        BEGIN TRANSACTION;


        UPDATE tblUsers
        SET UserName = @Name
        WHERE UserID = @UserID;

        UPDATE tblUserProfile
        SET Name = @Name,
            ProfilePhoto = @ProfilePhoto
        WHERE UserID = @UserID;

        UPDATE tblUserContact
        SET Email = @Email,
            PhoneNumber = @PhoneNumber
        WHERE UserID = @UserID;

        COMMIT TRANSACTION;


        SELECT 'User Profile Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;


        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;


GO


-- ==========================================================

-- SP: ✔️spFilterCreditByAmountRange.sql

-- ==========================================================

CREATE PROCEDURE spFilterCreditByAmountRange
(
    @UserID INT,
    @MinAmount DECIMAL(10,2),
    @MaxAmount DECIMAL(10,2)
)
AS
BEGIN
    SET NOCOUNT ON
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END
    
    IF @MinAmount < 0 OR @MaxAmount < 0
    BEGIN
        SELECT 'Amount cannot be negative' AS MESSAGE
        RETURN
    END
    
    IF @MinAmount > @MaxAmount
    BEGIN
        SELECT 'MinAmount cannot be greater than MaxAmount' AS MESSAGE
        RETURN
    END
    
    SELECT 
        CR.CreditID,
        CR.UserID,
        CR.CategoryID,
        C.CategoryName,
        CR.SubCategoryID,
        SC.SubCategoryName,
        CR.PaymentID,
        P.PaymentName,
        CR.Amount,
        LTRIM(RTRIM(CR.Description)) AS Description,
        CR.CreditAt
    FROM tblCredit CR
    INNER JOIN tblCreditCategory C ON CR.CategoryID = C.CategoryID
    INNER JOIN tblCreditSubCategory SC ON CR.SubCategoryID = SC.SubCategoryID
    INNER JOIN tblPaymentType P ON CR.PaymentID = P.PaymentID
    WHERE CR.UserID = @UserID
    AND CR.Amount >= @MinAmount
    AND CR.Amount <= @MaxAmount
    ORDER BY CR.Amount DESC, CR.CreditAt DESC

END
GO


GO


-- ==========================================================

-- SP: ✔️spFilterCreditByCategory.sql

-- ==========================================================

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
        FROM tblUserAuthentication UserAuthentication
        WHERE UserAuthentication.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCredit
        WHERE UserID = @UserID
        AND CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'No Record Found' AS Message
        RETURN
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

    LEFT JOIN tblCreditCategory CreditCategory
        ON Credit.CategoryID = CreditCategory.CategoryID

    LEFT JOIN tblCreditSubCategory CreditSubCategory
        ON Credit.SubCategoryID = CreditSubCategory.SubCategoryID

    LEFT JOIN tblPaymentType PaymentType
        ON Credit.PaymentID = PaymentType.PaymentID

    WHERE Credit.UserID = @UserID
    AND Credit.CategoryID = @CategoryID

    ORDER BY Credit.CreditAt DESC

END
GO

GO


-- ==========================================================

-- SP: ✔️spFilterCreditByCategoryAndSubCategory.sql

-- ==========================================================

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
        FROM tblUserAuthentication UserAuthentication
        WHERE UserAuthentication.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message
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
        SELECT 'SubCategory does not belong to selected Category' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCredit
        WHERE UserID = @UserID
        AND CategoryID = @CategoryID
        AND SubCategoryID = @SubCategoryID
    )
    BEGIN
        SELECT 'No Record Found' AS Message
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
        ON Credit.CategoryID = CreditCategory.CategoryID

    LEFT JOIN tblCreditSubCategory CreditSubCategory
        ON Credit.SubCategoryID = CreditSubCategory.SubCategoryID

    LEFT JOIN tblPaymentType PaymentType
        ON Credit.PaymentID = PaymentType.PaymentID

    WHERE Credit.UserID = @UserID
    AND Credit.CategoryID = @CategoryID
    AND Credit.SubCategoryID = @SubCategoryID

    ORDER BY Credit.CreditAt DESC

END
GO

GO


-- ==========================================================

-- SP: ✔️spFilterCreditByDateRange.sql

-- ==========================================================

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

GO


-- ==========================================================

-- SP: ✔️spGetAllCreditsByID.sql

-- ==========================================================

CREATE PROCEDURE spGetAllCreditsByID
(
    @UserID INT
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
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCredit
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Credit Record Found' AS Message
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
        ON Credit.CategoryID = CreditCategory.CategoryID

    LEFT JOIN tblCreditSubCategory CreditSubCategory
        ON Credit.SubCategoryID = CreditSubCategory.SubCategoryID

    LEFT JOIN tblPaymentType PaymentType
        ON Credit.PaymentID = PaymentType.PaymentID

    WHERE Credit.UserID = @UserID

    ORDER BY Credit.CreditAt DESC

END
GO

GO


-- ==========================================================

-- SP: ✔️spGetCategoryWiseCreditReport.sql

-- ==========================================================

CREATE PROCEDURE spGetCategoryWiseCreditReport
(
    @UserID INT
)
AS
BEGIN

    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication UserAuthentication
        INNER JOIN tblUsers Users
            ON UserAuthentication.UserID = Users.UserID
        WHERE Users.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCredit
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Credit Record Found' AS Message
        RETURN
    END

    SELECT 
        ISNULL(CreditCategory.CategoryName, 'Category Deleted') AS CategoryName,
        SUM(Credit.Amount) AS TotalCredit
    FROM tblCredit Credit
    LEFT JOIN tblCreditCategory CreditCategory
        ON Credit.CategoryID = CreditCategory.CategoryID
    WHERE Credit.UserID = @UserID
    GROUP BY CreditCategory.CategoryName
    ORDER BY TotalCredit DESC;

END
GO

GO


-- ==========================================================

-- SP: ✔️spGetMonthlyCreditSummary.sql

-- ==========================================================

CREATE PROCEDURE spGetMonthlyCreditSummary
(
    @UserID INT
)
AS
BEGIN

    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication UserAuthentication
        INNER JOIN tblUsers Users
            ON UserAuthentication.UserID = Users.UserID
        WHERE Users.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCredit
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Credit Record Found' AS Message
        RETURN
    END

    SELECT 
        YEAR(CreditAt) AS [Year],
        MONTH(CreditAt) AS [Month],
        SUM(Amount) AS TotalCredit
    FROM tblCredit
    WHERE UserID = @UserID
    GROUP BY 
        YEAR(CreditAt),
        MONTH(CreditAt)
    ORDER BY 
        [Year] DESC,
        [Month] DESC;

END
GO

GO


-- ==========================================================

-- SP: ✔️spGetTodayCredit.sql

-- ==========================================================

CREATE PROCEDURE spGetTodayCredit
(
    @UserID INT
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
        SELECT 'Invalid Or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCredit
        WHERE UserID = @UserID
        AND CAST(CreditAt AS DATE) = CAST(GETDATE() AS DATE)
    )
    BEGIN
        SELECT 'No Record Found' AS Message
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
               ON Credit.CategoryID = CreditCategory.CategoryID

          LEFT JOIN tblCreditSubCategory CreditSubCategory
                ON Credit.SubCategoryID = CreditSubCategory.SubCategoryID

             LEFT JOIN tblPaymentType PaymentType
               ON Credit.PaymentID = PaymentType.PaymentID

             WHERE Credit.UserID = @UserID
                 AND CAST(Credit.CreditAt AS DATE) = CAST(GETDATE() AS DATE)

              ORDER BY Credit.CreditAt DESC

END
GO

GO


-- ==========================================================

-- SP: ✔️spInsertCreditByUserID.sql

-- ==========================================================

CREATE PROCEDURE spInsertCreditByUserID
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
        FROM tblUserAuthentication UserAuthentication
        INNER JOIN tblUsers Users
            ON UserAuthentication.UserID = Users.UserID
        WHERE Users.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message
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
        SELECT 'SubCategory does not belong to selected Category' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblPaymentType
        WHERE PaymentID = @PaymentID
    )
    BEGIN
        SELECT 'Invalid PaymentID' AS Message
        RETURN
    END

    IF @Amount <= 0
    BEGIN
        SELECT 'Amount must be greater than zero' AS Message
        RETURN
    END

    SET @Description = LTRIM(RTRIM(@Description))

    IF @Description IS NULL
       OR @Description = ''
    BEGIN
        SELECT 'Description cannot be empty' AS Message
        RETURN
    END

    IF @CreditAt > GETDATE()
    BEGIN
        SELECT 'Future date is not allowed' AS Message
        RETURN
    END

    INSERT INTO tblCredit
    (
        UserID,
        CategoryID,
        SubCategoryID,
        Amount,
        Description,
        PaymentID,
        CreditAt
    )
    VALUES
    (
        @UserID,
        @CategoryID,
        @SubCategoryID,
        @Amount,
        @Description,
        @PaymentID,
        @CreditAt
    )

    SELECT 'Credit inserted successfully' AS Message

END
GO

GO


-- ==========================================================

-- SP: ✔️spGetUserDashboard.sql

-- ==========================================================

CREATE PROCEDURE spGetUserDashboard

    @UserID INT

AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
              AND Active = 1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS Message;
        RETURN;
    END;

    DECLARE @TotalExpense DECIMAL(18,2);
    DECLARE @TotalCredit DECIMAL(18,2);
    DECLARE @TotalLent DECIMAL(18,2);
    DECLARE @TotalBorrow DECIMAL(18,2);
    DECLARE @PendingTasks INT;
    DECLARE @NetBalance DECIMAL(18,2);


    SELECT @TotalExpense = ISNULL(SUM(Amount),0)
    FROM tblExpense
    WHERE UserID = @UserID;


    SELECT @TotalCredit = ISNULL(SUM(Amount),0)
    FROM tblCredit
    WHERE UserID = @UserID;


    SELECT @TotalLent = ISNULL(SUM(Amount),0)
    FROM tblLent
    WHERE UserID = @UserID;


    SELECT @TotalBorrow = ISNULL(SUM(Amount),0)
    FROM tblBorrow
    WHERE UserID = @UserID;


    SELECT @PendingTasks = COUNT(*)
    FROM tblTask T
    INNER JOIN tblTaskStatus TS
        ON T.TaskStatusID = TS.TaskStatusID
    WHERE T.UserID = @UserID
          AND TS.TaskStatusName = 'Pending';


    SET @NetBalance =
    (
        (@TotalCredit + @TotalBorrow)
        -
        (@TotalExpense + @TotalLent)
    );

    SELECT
        @TotalExpense AS TotalExpense,
        @TotalCredit AS TotalCredit,
        @TotalLent AS TotalLentAmount,
        @TotalBorrow AS TotalBorrowAmount,
        @NetBalance AS NetBalance,
        @PendingTasks AS PendingTaskCount;

END;
GO

GO


-- ==========================================================

-- SP: ✔️spFilterExpenseByAmountRange.sql

-- ==========================================================

CREATE PROCEDURE spFilterExpenseByAmountRange
(
    @UserID INT,
    @MinAmount DECIMAL(10,2),
    @MaxAmount DECIMAL(10,2)
)
AS
BEGIN
    SET NOCOUNT ON
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END
    
    IF @MinAmount < 0 OR @MaxAmount < 0
    BEGIN
        SELECT 'Amount cannot be negative' AS MESSAGE
        RETURN
    END
    
    IF @MinAmount > @MaxAmount
    BEGIN
        SELECT 'MinAmount cannot be greater than MaxAmount' AS MESSAGE
        RETURN
    END
    
    SELECT 
        E.ExpenseID,
        E.UserID,
        E.CategoryID,
        C.CategoryName,
        E.SubCategoryID,
        SC.SubCategoryName,
        E.PaymentID,
        P.PaymentName,
        E.Amount,
        E.Description,
        E.ExpenseAt
    FROM tblExpense E
    INNER JOIN tblExpenseCategory C ON E.CategoryID = C.CategoryID
    INNER JOIN tblExpenseSubCategory SC ON E.SubCategoryID = SC.SubCategoryID
    INNER JOIN tblPaymentType P ON E.PaymentID = P.PaymentID
    WHERE E.UserID = @UserID
    AND E.Amount >= @MinAmount
    AND E.Amount <= @MaxAmount
    ORDER BY E.Amount DESC, E.ExpenseAt DESC

END
GO


GO


-- ==========================================================

-- SP: ✔️spFilterExpenseByCategory.sql

-- ==========================================================

CREATE PROCEDURE spFilterExpenseByCategory
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
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpense
        WHERE UserID = @UserID
        AND CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'No Record Found' AS Message
        RETURN
    END
    SELECT
        Expense.ExpenseID,
        ExpenseCategory.CategoryName,
        ExpenseSubCategory.SubCategoryName,
        Expense.Amount,
        LTRIM(RTRIM(Expense.Description)) AS Description,
        PaymentType.PaymentName,
        Expense.ExpenseAt

    FROM tblExpense Expense

    LEFT JOIN tblExpenseCategory ExpenseCategory
        ON Expense.CategoryID = ExpenseCategory.CategoryID

    LEFT JOIN tblExpenseSubCategory ExpenseSubCategory
        ON Expense.SubCategoryID = ExpenseSubCategory.SubCategoryID

    LEFT JOIN tblPaymentType PaymentType
        ON Expense.PaymentID = PaymentType.PaymentID

    WHERE Expense.UserID = @UserID
    AND Expense.CategoryID = @CategoryID

    ORDER BY Expense.ExpenseAt DESC

END
GO

GO


-- ==========================================================

-- SP: ✔️spFilterExpenseByCategoryAndSubCategory.sql

-- ==========================================================

CREATE PROCEDURE spFilterExpenseByCategoryAndSubCategory
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
        FROM tblUserAuthentication UserAuthentication
        WHERE UserAuthentication.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message
        RETURN
    END

    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'SubCategory does not belong to selected Category' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpense
        WHERE UserID = @UserID
        AND CategoryID = @CategoryID
        AND SubCategoryID = @SubCategoryID
    )
    BEGIN
        SELECT 'No Record Found' AS Message
        RETURN
    END

    SELECT
        Expense.ExpenseID,
        ExpenseCategory.CategoryName,
        ExpenseSubCategory.SubCategoryName,
        Expense.Amount,
        LTRIM(RTRIM(Expense.Description)) AS Description,
        PaymentType.PaymentName,
        Expense.ExpenseAt

    FROM tblExpense Expense

    LEFT JOIN tblExpenseCategory ExpenseCategory
        ON Expense.CategoryID = ExpenseCategory.CategoryID

    LEFT JOIN tblExpenseSubCategory ExpenseSubCategory
        ON Expense.SubCategoryID = ExpenseSubCategory.SubCategoryID

    LEFT JOIN tblPaymentType PaymentType
        ON Expense.PaymentID = PaymentType.PaymentID

    WHERE Expense.UserID = @UserID
    AND Expense.CategoryID = @CategoryID
    AND Expense.SubCategoryID = @SubCategoryID

    ORDER BY Expense.ExpenseAt DESC

END
GO

GO


-- ==========================================================

-- SP: ✔️spFilterExpenseByDateRange.sql

-- ==========================================================

CREATE PROCEDURE spFilterExpenseByDateRange
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
			   FROM tblExpense
			    WHERE UserID = @UserID
				AND CAST(ExpenseAt AS DATE)
				BETWEEN @FromDate AND @ToDate
			)
			BEGIN
			  SELECT 'NO RECORD FOUND' AS MESSAGE
			  RETURN
			END

			SELECT
			   Expense.ExpenseID,
			   ExpenseCategory.CategoryName,
               ExpenseSubCategory.SubCategoryName,
			   Expense.Amount,
			   LTRIM(RTRIM(Expense.Description)) AS Description,
			   PaymentType.PaymentName,
			   Expense.ExpenseAt

			  FROM tblExpense Expense

			  LEFT JOIN tblExpenseCategory ExpenseCategory
			    ON Expense.CategoryID =ExpenseCategory.CategoryID

				LEFT JOIN tblExpenseSubCategory ExpenseSubCategory
				 ON Expense.SubCategoryID = ExpenseSubCategory.SubCategoryID

				 LEFT JOIN tblPaymentType  PaymentType
				  ON Expense.PaymentID = PaymentType.PaymentID

				  WHERE Expense.UserID =@UserID
				  AND CAST(Expense.ExpenseAt AS DATE)
				  BETWEEN @FromDate AND @ToDate

                ORDER BY Expense.ExpenseAt DESC
END
GO

GO


-- ==========================================================

-- SP: ✔️spGetAllExpensesByID.sql

-- ==========================================================

CREATE PROCEDURE spGetAllExpensesByID
(
    @UserID INT
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
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpense
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Expense Record Found' AS Message
        RETURN
    END

    SELECT
        Expense.ExpenseID,
        ExpenseCategory.CategoryName,
        ExpenseSubCategory.SubCategoryName,
        Expense.Amount,
        LTRIM(RTRIM(Expense.Description)) AS Description,
        PaymentType.PaymentName,
        Expense.ExpenseAt
    FROM tblExpense Expense

    LEFT JOIN tblExpenseCategory ExpenseCategory
        ON Expense.CategoryID = ExpenseCategory.CategoryID

    LEFT JOIN tblExpenseSubCategory ExpenseSubCategory
        ON Expense.SubCategoryID = ExpenseSubCategory.SubCategoryID

    LEFT JOIN tblPaymentType PaymentType
        ON Expense.PaymentID = PaymentType.PaymentID

    WHERE Expense.UserID = @UserID

    ORDER BY Expense.ExpenseAt DESC

END
GO

GO


-- ==========================================================

-- SP: ✔️spGetCategoryWiseExpenseReport.sql

-- ==========================================================

CREATE PROCEDURE spGetCategoryWiseExpenseReport
(
    @UserID INT
)
AS
BEGIN

    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication UserAuthentication
        INNER JOIN tblUsers Users
            ON UserAuthentication.UserID = Users.UserID
        WHERE Users.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpense
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Expense Record Found' AS Message
        RETURN
    END

    SELECT 
        ISNULL(ExpenseCategory.CategoryName, 'Category Deleted') AS CategoryName,
        SUM(Expense.Amount) AS TotalExpense
    FROM tblExpense Expense
    LEFT JOIN tblExpenseCategory ExpenseCategory
        ON Expense.CategoryID = ExpenseCategory.CategoryID
    WHERE Expense.UserID = @UserID
    GROUP BY 
        ISNULL(ExpenseCategory.CategoryName, 'Category Deleted')
    ORDER BY TotalExpense DESC;

END
GO

GO


-- ==========================================================

-- SP: ✔️spGetMonthlyExpenseSummary.sql

-- ==========================================================

CREATE PROCEDURE spGetMonthlyExpenseSummary
(
    @UserID INT
)
AS
BEGIN

    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication UserAuthentication
        INNER JOIN tblUsers Users
            ON UserAuthentication.UserID = Users.UserID
        WHERE Users.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpense
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Expense Record Found' AS Message
        RETURN
    END

    SELECT 
        YEAR(ExpenseAt) AS [Year],
        MONTH(ExpenseAt) AS [Month],
        SUM(Amount) AS TotalExpense
    FROM tblExpense
    WHERE UserID = @UserID
    GROUP BY 
        YEAR(ExpenseAt),
        MONTH(ExpenseAt)
    ORDER BY 
        [Year] DESC,
        [Month] DESC;

END
GO

GO


-- ==========================================================

-- SP: ✔️spGetTodayExpense.sql

-- ==========================================================

CREATE PROCEDURE spGetTodayExpense
(
    @UserID INT
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
        SELECT 'Invalid Or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpense
        WHERE UserID = @UserID
        AND CAST(ExpenseAt AS DATE) = CAST(GETDATE() AS DATE)
    )
    BEGIN
        SELECT 'No Record Found' AS Message
        RETURN
    END

         SELECT
			   Expense.ExpenseID,
			   ExpenseCategory.CategoryName,
               ExpenseSubCategory.SubCategoryName,
			   Expense.Amount,
			   LTRIM(RTRIM(Expense.Description)) AS Description,
			   PaymentType.PaymentName,
			   Expense.ExpenseAt

			  FROM tblExpense Expense

			  LEFT JOIN tblExpenseCategory ExpenseCategory
			    ON Expense.CategoryID =ExpenseCategory.CategoryID

				LEFT JOIN tblExpenseSubCategory ExpenseSubCategory
				 ON Expense.SubCategoryID = ExpenseSubCategory.SubCategoryID

				 LEFT JOIN tblPaymentType  PaymentType
				  ON Expense.PaymentID = PaymentType.PaymentID

				  WHERE Expense.UserID =@UserID
                 AND CAST(Expense.ExpenseAt AS DATE) = CAST(GETDATE() AS DATE)

              ORDER BY Expense.ExpenseAt  DESC

END
GO

GO


-- ==========================================================

-- SP: ✔️spInsertExpenseByUserID.sql

-- ==========================================================

CREATE PROCEDURE spInsertExpenseByUserID
(
    @UserID INT,
    @CategoryID INT,
    @SubCategoryID INT,
    @Amount DECIMAL(10,2),
    @Description VARCHAR(MAX),
    @PaymentID INT,
    @ExpenseAt DATETIME
)
AS
BEGIN

    SET NOCOUNT ON

      IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication UserAuthentication
        INNER JOIN tblUsers Users
            ON UserAuthentication.UserID = Users.UserID
        WHERE Users.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'SubCategory does not belong to selected Category' AS Message
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblPaymentType
        WHERE PaymentID = @PaymentID
    )
    BEGIN
        SELECT 'Invalid PaymentID' AS Message
        RETURN
    END

    IF @Amount <= 0
    BEGIN
        SELECT 'Amount must be greater than zero' AS Message
        RETURN
    END

    SET @Description = LTRIM(RTRIM(@Description))

    IF @Description IS NULL
       OR @Description = ''
    BEGIN
        SELECT 'Description cannot be empty' AS Message
        RETURN
    END

    IF @ExpenseAt > GETDATE()
    BEGIN
        SELECT 'Future date is not allowed' AS Message
        RETURN
    END

    INSERT INTO tblExpense
    (
        UserID,
        CategoryID,
        SubCategoryID,
        Amount,
        Description,
        PaymentID,
        ExpenseAt
    )
    VALUES
    (
        @UserID,
        @CategoryID,
        @SubCategoryID,
        @Amount,
        @Description,
        @PaymentID,
        @ExpenseAt
    )

    SELECT 'Expense inserted successfully' AS Message

END
GO

GO


-- ==========================================================

-- SP: ✔️spGetAllLent.sql

-- ==========================================================

CREATE PROC spGetAllLent
	@UserID INT
AS
BEGIN
	
	IF NOT EXISTS (SELECT 1 
					FROM tblUserAuthentication
					WHERE UserID = @UserID AND Active = 1)
	BEGIN
		SELECT 'Invalid OR Inactive UserID!!' AS Message
		RETURN
	END

	SELECT L.LentID,
			Prsn.PersonName,
			L.Amount,
			L.ReturnedAmount,
			L.RemainingAmount,
			Pay.PaymentName,
			S.StatusName,
			L.LentAt,
			L.DeadlineAt,
			L.Description
	FROM tblLent L
	LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
	LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID
	WHERE L.UserID = @UserID ORDER BY L.LentAt DESC

END



GO


-- ==========================================================

-- SP: ✔️SpGetCompletedLentByStatusName.sql

-- ==========================================================

CREATE PROC spGetCompletedLentByStatusName
@UserID INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 
					FROM tblUserAuthentication
					WHERE UserID = @UserID AND Active = 1)
	BEGIN
		SELECT 'Invalid OR Inactive UserID!!' AS Message
		RETURN
	END

	SELECT Prsn.PersonName,
			L.Amount,
			L.ReturnedAmount,
			L.RemainingAmount,
			Pay.PaymentName,
			S.StatusName,
			L.LentAt,
			L.DeadlineAt,
			L.Description
	FROM tblLent L
	LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
	LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID

	WHERE L.UserID = @UserID AND S.StatusName = 'Paid'
	ORDER BY L.LentAt DESC;
END


GO


-- ==========================================================

-- SP: ✔️spGetLentPersonHistory.sql

-- ==========================================================

CREATE PROC spGetLentPersonHistory
@PersonID INT, @UserID INT
AS
BEGIN

	IF NOT EXISTS (SELECT 1 
				   FROM tblLent L
					JOIN tblPersons LP 
						ON L.PersonID = LP.PersonID
					WHERE L.UserID = @UserID
					AND L.PersonID = @PersonID)
	BEGIN
		SELECT 'Invalid PersonID OR No Lent History Found!' AS Message
		RETURN
	END

	SELECT L.LentID,
			Prsn.PersonName,
			L.Amount,
			L.ReturnedAmount,
			L.RemainingAmount,
			Pay.PaymentName,
			S.StatusName,
			L.LentAt,
			L.DeadlineAt,
			L.Description
	FROM tblLent L
	LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
	LEFT JOIN tblPaymentType Pay ON L.PaymentID = Pay.PaymentID
	LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	WHERE L.PersonID = @PersonID AND L.UserID = @UserID
	ORDER BY L.LentAt DESC;

END

GO


-- ==========================================================

-- SP: ✔️spDeleteNote.sql

-- ==========================================================

CREATE PROCEDURE  spDeleteNote
(
@UserID INT,
@NoteID INT
)
AS
BEGIN


IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
AND NoteID=@NoteID
)
BEGIN
SELECT 'Invalid UserID Or NoteID' AS Message
RETURN 
END

BEGIN TRY

DELETE FROM tblNote
WHERE UserID=@UserID
AND NoteID=@NoteID

SELECT 'Note Deleted Successfully' AS Message
END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END

GO


-- ==========================================================

-- SP: ✔️spFilterNotesByPriority.sql

-- ==========================================================

CREATE PROCEDURE  spFilterNotesByPriority

@UserID INT,
@PriorityID INT

AS
BEGIN

IF NOT EXISTS
(
SELECT 1 FROM tblUsers
WHERE UserID=@UserID
)
BEGIN
SELECT 'UserID Does Not Exist' AS Message
RETURN
END

IF NOT EXISTS
(
SELECT 1 FROM tblNotePriorities
WHERE NotePriorityID=@PriorityID
)
BEGIN 
SELECT 'Invalid Note PriorityID' AS Message
RETURN
END

IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
AND NotePriorityID=@PriorityID
)
BEGIN
SELECT 'No Notes Found' AS Message
RETURN
END

BEGIN TRY
SELECT
tblNote.NoteID,
tblNote.NotePriorityID,
tblNote.NoteTitle,
tblNote.Description,
tblNotePriorities.NotePriorityName,
tblNote.CreatedAt
FROM tblNote
LEFT JOIN tblNotePriorities ON tblNote.NotePriorityID=tblNotePriorities.NotePriorityID
WHERE tblNote.UserID=@UserID
AND tblNote.NotePriorityID=@PriorityID

ORDER BY tblNote.CreatedAt DESC

END TRY
BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END

GO


-- ==========================================================

-- SP: ✔️spGetAllNotes.sql

-- ==========================================================

CREATE PROCEDURE spGetAllNotes
(
@UserID INT
)
AS
BEGIN

IF NOT EXISTS
(
SELECT 1 FROM tblUsers
WHERE UserID=@UserID
)
BEGIN
SELECT 'UserID Does Not Exists' AS Message
RETURN
END

IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
)
BEGIN
SELECT 'No Notes Found For This User' AS Message
RETURN
END
BEGIN TRY

SELECT
tblNote.NoteID,
tblNote.NotePriorityID,
tblNote.NoteTitle,
tblNote.Description,
tblNotePriorities.NotePriorityName,
tblNote.CreatedAt 

FROM tblNote
LEFT JOIN tblNotePriorities ON tblNote.NotePriorityID=tblNotePriorities.NotePriorityID
WHERE tblNote.UserID=@UserID
ORDER BY tblNote.CreatedAt DESC

END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END

GO


-- ==========================================================

-- SP: ✔️spGetNotesBetweenDates.sql

-- ==========================================================

CREATE PROCEDURE  spGetNotesBetweenDates

@UserID INT,
@FromDate DATE,
@ToDate DATE

AS
BEGIN

IF NOT EXISTS
(
SELECT 1 FROM tblUsers
WHERE UserID=@UserID
)
BEGIN
SELECT 'UserID Does Not Exist' AS Message
RETURN
END

IF @FromDate>@ToDate
BEGIN
SELECT 'Start Date Cannot Be Greater Than End Date' AS Message
RETURN
END


IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
AND CAST(tblNote.CreatedAt AS DATE)
BETWEEN @FromDate AND @ToDate
)
BEGIN
SELECT 'No Notes Found Between These Dates' AS Message
RETURN
END

BEGIN TRY

SELECT
tblNote.NoteID,
tblNote.NoteTitle,
tblNote.Description,
tblNotePriorities.NotePriorityName,
tblNote.CreatedAt
FROM tblNote
LEFT JOIN tblNotePriorities ON tblNote.NotePriorityID=tblNotePriorities.NotePriorityID
WHERE tblNote.UserID=@UserID
AND CAST(tblNote.CreatedAt AS DATE)
BETWEEN @FromDate AND @ToDate
ORDER BY tblNote.CreatedAt DESC

END TRY
BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END

GO


-- ==========================================================

-- SP: ✔️spInsertNote.sql

-- ==========================================================

CREATE PROCEDURE spInsertNote

@UserID INT,
@PriorityID INT,
@NoteTitle VARCHAR(MAX),
@Description VARCHAR(MAX)

AS
BEGIN

SET @NoteTitle=LTRIM(RTRIM(@NoteTitle))
SET @Description=LTRIM(RTRIM(@Description))

IF @NoteTitle IS NULL OR @NoteTitle= ''
BEGIN
SELECT 'Note Title Cannot be Empty' AS Message
RETURN
END

IF @Description IS NULL OR @Description= ''
BEGIN
SELECT 'Description Cannot be Empty' AS Message
RETURN
END

IF NOT EXISTS
(
SELECT 1 FROM tblUsers
WHERE UserID=@UserID
)
BEGIN
SELECT 'UserID Does Not Exist' AS Message 
RETURN 
END

IF NOT EXISTS
(
SELECT 1 FROM tblUserAuthentication
WHERE UserID=@UserID
AND Active=1
)
BEGIN
SELECT 'Inactive User Cannot Add Notes' AS Message 
RETURN 
END

IF NOT EXISTS
(
SELECT 1 FROM tblNotePriorities
WHERE NotePriorityID=@PriorityID
)
BEGIN
SELECT 'Invalid Note PriorityID' AS Message 
RETURN 
END

BEGIN TRY

INSERT INTO tblNote (UserID, NotePriorityID, NoteTitle, Description)
VALUES
(@UserID,@PriorityID,@NoteTitle,@Description)

SELECT 'Note Inserted Successfully' AS Message

END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END

GO


-- ==========================================================

-- SP: ✔️spUpdateNote.sql

-- ==========================================================

CREATE PROCEDURE spUpdateNote
(
@UserID INT,
@NoteID INT,
@PriorityID INT,
@NoteTitle VARCHAR(MAX),
@Description VARCHAR(MAX)
)
AS
BEGIN

SET @NoteTitle=LTRIM(RTRIM(@NoteTitle))
SET @Description=LTRIM(RTRIM(@Description))

IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
AND NoteID=@NoteID
)
BEGIN
SELECT 'Invalid UserID Or NoteID' AS Message
RETURN 
END

IF @NoteTitle IS NULL OR @NoteTitle= ''
BEGIN
SELECT 'Note Title Cannot be Empty' AS Message
RETURN
END

IF @Description IS NULL OR @Description= ''
BEGIN
SELECT 'Description Cannot be Empty' AS Message
RETURN
END

IF NOT EXISTS
(
SELECT 1 FROM tblNotePriorities
WHERE NotePriorityID=@PriorityID
)
BEGIN
SELECT 'Invalid Note PriorityID' AS Message
RETURN 
END


BEGIN TRY

UPDATE tblNote 
SET
    NotePriorityID=@PriorityID,
    NoteTitle=@NoteTitle,
    Description=@Description
WHERE UserID=@UserID 
AND NoteID=@NoteID
SELECT 'Note Updated Successfully' AS Message


END TRY
BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END

GO


-- ==========================================================

-- SP: ✔️spUpdateNotePriority.sql

-- ==========================================================

CREATE PROCEDURE  spUpdateNotePriority
(
@UserID INT,
@NoteID INT,
@PriorityID INT
)
AS
BEGIN

IF NOT EXISTS
(
SELECT 1 FROM tblNote
WHERE UserID=@UserID
AND NoteID=@NoteID
)
BEGIN
SELECT 'Invalid UserID Or NoteID' AS Message
RETURN 
END

IF NOT EXISTS
(
SELECT 1 FROM tblNotePriorities
WHERE NotePriorityID=@PriorityID
)
BEGIN
SELECT 'Invalid Note PriorityID' AS Message
RETURN 
END

BEGIN TRY

UPDATE tblNote 
SET
    NotePriorityID=@PriorityID
WHERE UserID=@UserID 
AND NoteID=@NoteID

SELECT 'Note Priority Updated Successfully' AS Message
END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() AS Message
END CATCH
END

GO


-- ==========================================================

-- SP: ✔️spDeleteCreditCategoryByUserID.sql

-- ==========================================================

CREATE PROCEDURE spDeleteCreditCategoryByUserID
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
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS MESSAGE
        RETURN
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot delete default categories or categories owned by other users' AS MESSAGE
        RETURN
    END

    UPDATE tblCreditCategory
    SET IsActive = 0
    WHERE CategoryID = @CategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Credit Category Deleted Successfully' AS Message
END
GO


GO


-- ==========================================================

-- SP: ✔️spDeleteCreditSubCategoryByUserID.sql

-- ==========================================================

CREATE PROCEDURE spDeleteCreditSubCategoryByUserID
(
 @UserID INT,
 @SubCategoryID INT
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
        FROM tblCreditSubCategory
        WHERE SubCategoryID = @SubCategoryID
    )
    BEGIN
        SELECT 'Invalid SubCategoryID' AS MESSAGE
        RETURN
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot delete default subcategories or subcategories owned by other users' AS MESSAGE
        RETURN
    END

    
    
    UPDATE tblCreditSubCategory
    SET IsActive = 0
    WHERE SubCategoryID = @SubCategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Credit SubCategory Deleted Successfully' AS Message
END
GO


GO


-- ==========================================================

-- SP: ✔️spDeleteExpenseCategoryByUserID.sql

-- ==========================================================

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
        RETURN
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot delete default categories or categories owned by other users' AS MESSAGE
        RETURN
    END

    
    
    UPDATE tblExpenseCategory
    SET IsActive = 0
    WHERE CategoryID = @CategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Expense Category Deleted Successfully' AS Message
END
GO


GO


-- ==========================================================

-- SP: ✔️spDeleteExpenseSubCategoryByUserID.sql

-- ==========================================================

CREATE PROCEDURE spDeleteExpenseSubCategoryByUserID
(
 @UserID INT,
 @SubCategoryID INT
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
        FROM tblExpenseSubCategory
        WHERE SubCategoryID = @SubCategoryID
    )
    BEGIN
        SELECT 'Invalid SubCategoryID' AS MESSAGE
        RETURN
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot delete default subcategories or subcategories owned by other users' AS MESSAGE
        RETURN
    END

    
    
    UPDATE tblExpenseSubCategory
    SET IsActive = 0
    WHERE SubCategoryID = @SubCategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Expense SubCategory Deleted Successfully' AS Message
END
GO


GO


-- ==========================================================

-- SP: ✔️spGetAllPaymentTypes.sql

-- ==========================================================

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


GO


-- ==========================================================

-- SP: ✔️spGetCreditCategoriesByUserID.sql

-- ==========================================================

CREATE PROCEDURE spGetCreditCategoriesByUserID
(
    @UserID INT
)
AS
BEGIN
    SET NOCOUNT ON
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END
    
    
    SELECT 
        CategoryID,
        UserID,
        CategoryName,
        IsDefault,
        IsActive
    FROM tblCreditCategory
    WHERE IsActive = 1
    AND (UserID IS NULL OR UserID = @UserID)
    ORDER BY IsDefault DESC, CategoryName ASC

END
GO


GO


-- ==========================================================

-- SP: ✔️spGetCreditSubCategoriesByUserID.sql

-- ==========================================================

CREATE PROCEDURE spGetCreditSubCategoriesByUserID
(
    @UserID INT
)
AS
BEGIN
    SET NOCOUNT ON
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END
    
    
    SELECT 
        SubCategoryID,
        CategoryID,
        UserID,
        SubCategoryName,
        IsDefault,
        IsActive
    FROM tblCreditSubCategory
    WHERE IsActive = 1
    AND (UserID IS NULL OR UserID = @UserID)
    ORDER BY IsDefault DESC, SubCategoryName ASC

END
GO


GO


-- ==========================================================

-- SP: ✔️spGetExpenseCategoriesByUserID.sql

-- ==========================================================

CREATE PROCEDURE spGetExpenseCategoriesByUserID
(
    @UserID INT
)
AS
BEGIN
    SET NOCOUNT ON
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END
    
    
    SELECT 
        CategoryID,
        UserID,
        CategoryName,
        IsDefault,
        IsActive
    FROM tblExpenseCategory
    WHERE IsActive = 1
    AND (UserID IS NULL OR UserID = @UserID)
    ORDER BY IsDefault DESC, CategoryName ASC

END
GO


GO


-- ==========================================================

-- SP: ✔️spGetExpenseSubCategoriesByUserID.sql

-- ==========================================================

CREATE PROCEDURE spGetExpenseSubCategoriesByUserID
(
    @UserID INT
)
AS
BEGIN
    SET NOCOUNT ON
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END
    
    
    SELECT 
        SubCategoryID,
        CategoryID,
        UserID,
        SubCategoryName,
        IsDefault,
        IsActive
    FROM tblExpenseSubCategory
    WHERE IsActive = 1
    AND (UserID IS NULL OR UserID = @UserID)
    ORDER BY IsDefault DESC, SubCategoryName ASC

END
GO


GO


-- ==========================================================

-- SP: ✔️spInsertNewCreditCategoryByUserID.sql

-- ==========================================================

CREATE PROCEDURE spInsertNewCreditCategoryByUserID
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
        WHERE UserAuthentication.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END
    
    
    SET @CategoryName = LTRIM(RTRIM(@CategoryName))
    
    IF @CategoryName IS NULL
    OR @CategoryName = ''
    BEGIN
        SELECT 'Category Name cannot be empty' AS Message
        RETURN
    END
    

    IF EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryName = @CategoryName
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'Category Already Exists for this user' AS Message
        RETURN
    END
    
    
    INSERT INTO tblCreditCategory(UserID, CategoryName, IsDefault, IsActive)
    VALUES(@UserID, @CategoryName, 0, 1)
    
    SELECT 'Credit Category Inserted Successfully' AS Message

END
GO


GO


-- ==========================================================

-- SP: ✔️spInsertNewCreditSubCategoryByUserID.sql

-- ==========================================================

CREATE PROCEDURE spInsertNewCreditSubCategoryByUserID
(
   @UserID INT,
   @CategoryID INT,
   @SubCategoryName VARCHAR(MAX)
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
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
        AND IsActive = 1
        AND (UserID IS NULL OR UserID = @UserID)
    )
    BEGIN
        SELECT 'Invalid or inactive category' AS Message
        RETURN
    END
    
    
    SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName))
    
    IF @SubCategoryName IS NULL
    OR @SubCategoryName = ''
    BEGIN
        SELECT 'SubCategory Name cannot be empty' AS Message
        RETURN
    END
    
    
    IF EXISTS
    (
        SELECT 1
        FROM tblCreditSubCategory
        WHERE SubCategoryName = @SubCategoryName
        AND CategoryID = @CategoryID
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'SubCategory Already Exists for this user in this category' AS Message
        RETURN
    END
    
    
    INSERT INTO tblCreditSubCategory(CategoryID, UserID, SubCategoryName, IsDefault, IsActive)
    VALUES(@CategoryID, @UserID, @SubCategoryName, 0, 1)
    
    SELECT 'Credit SubCategory Inserted Successfully' AS Message

END
GO


GO


-- ==========================================================

-- SP: ✔️spInsertNewExpenseCategoryByUserID.SQL

-- ==========================================================

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
        WHERE UserAuthentication.UserID = @UserID
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END
    
    
    SET @CategoryName = LTRIM(RTRIM(@CategoryName))
    
    IF @CategoryName IS NULL
    OR @CategoryName = ''
    BEGIN
        SELECT 'Category Name cannot be empty' AS Message
        RETURN
    END
    
    
    IF EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryName = @CategoryName
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'Category Already Exists for this user' AS Message
        RETURN
    END
    
    
    INSERT INTO tblExpenseCategory(UserID, CategoryName, IsDefault, IsActive)
    VALUES(@UserID, @CategoryName, 0, 1)
    
    SELECT 'Expense Category Inserted Successfully' AS Message

END
GO


GO


-- ==========================================================

-- SP: ✔️spInsertNewExpenseSubCategoryByUserID.sql

-- ==========================================================

CREATE PROCEDURE spInsertNewExpenseSubCategoryByUserID
(
   @UserID INT,
   @CategoryID INT,
   @SubCategoryName VARCHAR(MAX)
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
        SELECT 'Invalid or Inactive User' AS Message
        RETURN
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
        AND IsActive = 1
        AND (UserID IS NULL OR UserID = @UserID)
    )
    BEGIN
        SELECT 'Invalid or inactive category' AS Message
        RETURN
    END
    
    
    SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName))
    
    IF @SubCategoryName IS NULL
    OR @SubCategoryName = ''
    BEGIN
        SELECT 'SubCategory Name cannot be empty' AS Message
        RETURN
    END
    
    
    IF EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryName = @SubCategoryName
        AND CategoryID = @CategoryID
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'SubCategory Already Exists for this user in this category' AS Message
        RETURN
    END
    
    
    INSERT INTO tblExpenseSubCategory(CategoryID, UserID, SubCategoryName, IsDefault, IsActive)
    VALUES(@CategoryID, @UserID, @SubCategoryName, 0, 1)
    
    SELECT 'Expense SubCategory Inserted Successfully' AS Message

END
GO


GO


-- ==========================================================

-- SP: ✔️spUpdateCreditCategoryByUserID.sql

-- ==========================================================

CREATE PROCEDURE spUpdateCreditCategoryByUserID
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
        AND UserAuthentication.Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS MESSAGE
        RETURN
    END

    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditCategory 
        WHERE CategoryID = @CategoryID
    )
    BEGIN
        SELECT 'Invalid CategoryID' AS MESSAGE
        RETURN 
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditCategory
        WHERE CategoryID = @CategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot update default categories or categories owned by other users' AS MESSAGE
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
        FROM tblCreditCategory
        WHERE CategoryName = @CategoryName
        AND CategoryID != @CategoryID
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'Category Name Already Exists for this user' AS MESSAGE
        RETURN
    END

    
    UPDATE tblCreditCategory
    SET CategoryName = @CategoryName
    WHERE CategoryID = @CategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Credit Category Updated Successfully' AS MESSAGE

END
GO


GO


-- ==========================================================

-- SP: ✔️spUpdateCreditSubCategoryByUserID.sql

-- ==========================================================

CREATE PROCEDURE spUpdateCreditSubCategoryByUserID
(
  @UserID INT,
  @SubCategoryID INT,
  @SubCategoryName VARCHAR(MAX)
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
        FROM tblCreditSubCategory 
        WHERE SubCategoryID = @SubCategoryID
    )
    BEGIN
        SELECT 'Invalid SubCategoryID' AS MESSAGE
        RETURN 
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblCreditSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot update default subcategories or subcategories owned by other users' AS MESSAGE
        RETURN
    END

    
    SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName))

    IF @SubCategoryName IS NULL
    OR @SubCategoryName = ''
    BEGIN
        SELECT 'SubCategory Name Cannot Be Empty' AS MESSAGE
        RETURN
    END

    
    IF EXISTS
    (
        SELECT 1
        FROM tblCreditSubCategory
        WHERE SubCategoryName = @SubCategoryName
        AND SubCategoryID != @SubCategoryID
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'SubCategory Name Already Exists for this user' AS MESSAGE
        RETURN
    END

    
    UPDATE tblCreditSubCategory
    SET SubCategoryName = @SubCategoryName
    WHERE SubCategoryID = @SubCategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Credit SubCategory Updated Successfully' AS MESSAGE

END
GO


GO


-- ==========================================================

-- SP: ✔️spUpdateExpenseCategoryByUserID.sql

-- ==========================================================

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
        RETURN 
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseCategory
        WHERE CategoryID = @CategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot update default categories or categories owned by other users' AS MESSAGE
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
        WHERE CategoryName = @CategoryName
        AND CategoryID != @CategoryID
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'Category Name Already Exists for this user' AS MESSAGE
        RETURN
    END

    
    UPDATE tblExpenseCategory
    SET CategoryName = @CategoryName
    WHERE CategoryID = @CategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Expense Category Updated Successfully' AS MESSAGE

END
GO


GO


-- ==========================================================

-- SP: ✔️spUpdateExpenseSubCategoryByUserID.sql

-- ==========================================================

CREATE PROCEDURE spUpdateExpenseSubCategoryByUserID
(
  @UserID INT,
  @SubCategoryID INT,
  @SubCategoryName VARCHAR(MAX)
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
        FROM tblExpenseSubCategory 
        WHERE SubCategoryID = @SubCategoryID
    )
    BEGIN
        SELECT 'Invalid SubCategoryID' AS MESSAGE
        RETURN 
    END
    
    
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryID = @SubCategoryID
        AND UserID = @UserID
        AND IsDefault = 0
    )
    BEGIN
        SELECT 'Cannot update default subcategories or subcategories owned by other users' AS MESSAGE
        RETURN
    END

    
    SET @SubCategoryName = LTRIM(RTRIM(@SubCategoryName))

    IF @SubCategoryName IS NULL
    OR @SubCategoryName = ''
    BEGIN
        SELECT 'SubCategory Name Cannot Be Empty' AS MESSAGE
        RETURN
    END

    
    IF EXISTS
    (
        SELECT 1
        FROM tblExpenseSubCategory
        WHERE SubCategoryName = @SubCategoryName
        AND SubCategoryID != @SubCategoryID
        AND UserID = @UserID
        AND IsActive = 1
    )
    BEGIN
        SELECT 'SubCategory Name Already Exists for this user' AS MESSAGE
        RETURN
    END

    
    UPDATE tblExpenseSubCategory
    SET SubCategoryName = @SubCategoryName
    WHERE SubCategoryID = @SubCategoryID
    AND UserID = @UserID
    AND IsDefault = 0

    SELECT 'Expense SubCategory Updated Successfully' AS MESSAGE

END
GO


GO


-- ==========================================================

-- SP: ✔️spDeleteTask.sql

-- ==========================================================

CREATE PROCEDURE spDeleteTask
    @TaskID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE TaskID = @TaskID
    )
    BEGIN
        SELECT 'Invalid TaskID' AS Message;
        RETURN;
    END


    BEGIN TRY

        DELETE FROM tblTask
        WHERE TaskID = @TaskID;

        SELECT 'Task Deleted Successfully' AS Message;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;


GO


-- ==========================================================

-- SP: ✔️spFilterTasksByStatus.sql

-- ==========================================================

CREATE PROCEDURE spFilterTasksByStatus
    @UserID INT,
    @TaskStatusID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END


    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTaskStatus
        WHERE TaskStatusID = @TaskStatusID
    )
    BEGIN
        SELECT 'Invalid TaskStatusID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE UserID = @UserID
        AND TaskStatusID = @TaskStatusID
    )
    BEGIN
        SELECT 'No Tasks Found' AS Message;
        RETURN;
    END


    SELECT
        Task.TaskID,
        Task.TaskTitle,
        TaskPriorities.PriorityName,
        TaskStatus.TaskStatusName,
        Task.Deadline
    FROM tblTask Task

    INNER JOIN tblTaskPriorities TaskPriorities
        ON Task.PriorityID = TaskPriorities.PriorityID

    INNER JOIN tblTaskStatus TaskStatus
        ON Task.TaskStatusID = TaskStatus.TaskStatusID

    WHERE Task.UserID = @UserID
    AND Task.TaskStatusID = @TaskStatusID

    ORDER BY Task.Deadline ASC;

END;


GO


-- ==========================================================

-- SP: ✔️spGetAllTasks.sql

-- ==========================================================

CREATE PROCEDURE spGetAllTasks
    @UserID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Tasks Found' AS Message;
        RETURN;
    END


    SELECT
        Task.TaskID,
        Task.TaskTitle,
        TaskPriorities.PriorityName,
        TaskStatus.TaskStatusName,
        Task.Deadline
    FROM tblTask Task

    INNER JOIN tblTaskPriorities TaskPriorities
        ON Task.PriorityID = TaskPriorities.PriorityID

    INNER JOIN tblTaskStatus TaskStatus
        ON Task.TaskStatusID = TaskStatus.TaskStatusID

    WHERE Task.UserID = @UserID

END;


GO


-- ==========================================================

-- SP: ✔️spGetCompletedTasks.sql

-- ==========================================================

CREATE PROCEDURE spGetCompletedTasks
    @UserID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE UserID = @UserID
        AND TaskStatusID = 2
    )
    BEGIN
        SELECT 'No Completed Tasks Found' AS Message;
        RETURN;
    END


    SELECT
        Task.TaskID,
        Task.TaskTitle,
        TaskPriorities.PriorityName,
        TaskStatus.TaskStatusName,
        Task.Deadline
    FROM tblTask Task

    INNER JOIN tblTaskPriorities TaskPriorities
        ON Task.PriorityID = TaskPriorities.PriorityID

    INNER JOIN tblTaskStatus TaskStatus
        ON Task.TaskStatusID = TaskStatus.TaskStatusID

    WHERE Task.UserID = @UserID
    AND Task.TaskStatusID = 2

    ORDER BY Task.Deadline DESC;

END;


GO


-- ==========================================================

-- SP: ✔️spGetPendingTasks.sql

-- ==========================================================

CREATE PROCEDURE spGetPendingTasks
    @UserID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE UserID = @UserID
        AND TaskStatusID = 1
    )
    BEGIN
        SELECT 'No Pending Tasks Found' AS Message;
        RETURN;
    END

    SELECT
        Task.TaskID,
        Task.TaskTitle,
        TaskPriorities.PriorityName,
        TaskStatus.TaskStatusName,
        Task.Deadline
    FROM tblTask Task

    INNER JOIN tblTaskPriorities TaskPriorities
        ON Task.PriorityID = TaskPriorities.PriorityID

    INNER JOIN tblTaskStatus TaskStatus
        ON Task.TaskStatusID = TaskStatus.TaskStatusID

    WHERE Task.UserID = @UserID
    AND Task.TaskStatusID = 1

    ORDER BY Task.Deadline ASC;

END;



GO


-- ==========================================================

-- SP: ✔️spGetTasksBetweenDates.sql

-- ==========================================================

CREATE PROCEDURE spGetTasksBetweenDates
(
    @UserID INT,
    @FromDate DATE,
    @ToDate DATE
)
AS
BEGIN


    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUsers
            WHERE UserID = @UserID
        )
        BEGIN
            SELECT 'Invalid UserID' AS Message
            RETURN
        END

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUserAuthentication
            WHERE UserID = @UserID
            AND Active = 1
        )
        BEGIN
            SELECT 'Inactive User Cannot View Tasks' AS Message
            RETURN
        END

        IF @FromDate IS NULL OR @ToDate IS NULL
        BEGIN
            SELECT 'Date Cannot Be NULL' AS Message
            RETURN
        END

        IF @FromDate > @ToDate
        BEGIN
            SELECT 'FromDate Cannot Be Greater Than ToDate' AS Message
            RETURN
        END

        SELECT
            T.TaskID,
            T.TaskTitle,
            P.PriorityName,
            S.TaskStatusName,
            T.Deadline,
            T.CreatedAt
        FROM tblTask T
        INNER JOIN tblTaskPriorities P
            ON T.PriorityID = P.PriorityID
        INNER JOIN tblTaskStatus S
            ON T.TaskStatusID = S.TaskStatusID
        WHERE
            T.UserID = @UserID
            AND T.Deadline BETWEEN @FromDate AND @ToDate
        ORDER BY T.Deadline

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message

    END CATCH

END


GO


-- ==========================================================

-- SP: ✔️spGetUpcomingTaskReminders.sql

-- ==========================================================

CREATE PROCEDURE spGetUpcomingTaskReminders

    @UserID INT

AS
BEGIN


    DECLARE @Today DATE = CAST(GETDATE() AS DATE);

    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUsers
            WHERE UserID = @UserID
        )
        BEGIN
            SELECT 'UserID Does Not Exist' AS Message;
            RETURN;
        END

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblTask
            WHERE UserID = @UserID
            AND Deadline >= @Today
            AND TaskStatusID = 1
        )
        BEGIN
            SELECT 'No Pending Upcoming Tasks Found' AS Message;
            RETURN;
        END

        SELECT
            tblTask.TaskID,
            tblTask.TaskTitle,
            tblTask.Deadline,
            tblTaskStatus.TaskStatusName,
            tblTaskPriorities.PriorityName,
            DATEDIFF(DAY, @Today, tblTask.Deadline) AS RemainingDays,
            tblTask.CreatedAt

        FROM tblTask

        INNER JOIN tblTaskStatus
            ON tblTask.TaskStatusID = tblTaskStatus.TaskStatusID

        INNER JOIN tblTaskPriorities
            ON tblTask.PriorityID = tblTaskPriorities.PriorityID

        WHERE tblTask.UserID = @UserID
        AND tblTask.Deadline >= @Today
        AND tblTask.TaskStatusID = 1

        ORDER BY tblTask.Deadline ASC;

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH

END


GO


-- ==========================================================

-- SP: ✔️spInsertTask.sql

-- ==========================================================

CREATE PROCEDURE spInsertTask  
    @UserID INT,  
    @PriorityID INT,  
    @TaskTitle VARCHAR(150),  
    @Deadline DATE  
AS  
BEGIN  
  
    SET @TaskTitle = LTRIM(RTRIM(@TaskTitle));  
  

    IF @TaskTitle IS NULL OR @TaskTitle = ''  
    BEGIN  
        SELECT 'Task Title Cannot Be Empty' AS Message;  
        RETURN;  
    END  
  

    IF @Deadline IS NULL  
    BEGIN  
        SELECT 'Deadline Cannot Be Empty' AS Message;  
        RETURN;  
    END  

    IF @Deadline < CAST(GETDATE() AS DATE)  
    BEGIN  
        SELECT 'Invalid Deadline Date' AS Message;  
        RETURN;  
    END  
  

    IF NOT EXISTS  
    (  
        SELECT 1  
        FROM tblUsers  
        WHERE UserID = @UserID  
    )  
    BEGIN  
        SELECT 'Invalid UserID' AS Message;  
        RETURN;  
    END  

    IF NOT EXISTS  
    (  
        SELECT 1  
        FROM tblUserAuthentication  
        WHERE UserID = @UserID  
              AND Active = 1  
    )  
    BEGIN  
        SELECT 'User Account Is Not Active' AS Message;  
        RETURN;  
    END  

    IF NOT EXISTS  
    (  
        SELECT 1  
        FROM tblTaskPriorities  
        WHERE PriorityID = @PriorityID  
    )  
    BEGIN  
        SELECT 'Invalid PriorityID' AS Message;  
        RETURN;  
    END  
  
  
    BEGIN TRY  
  
        INSERT INTO tblTask  
        (  
            UserID,  
            PriorityID,  
            TaskStatusID,  
            TaskTitle,  
            Deadline  
        )  
        VALUES  
        (  
            @UserID,  
            @PriorityID,  
            1,  
            @TaskTitle,  
            @Deadline  
        );  
  
        SELECT 'Task Inserted Successfully' AS Message;  
  
    END TRY  
  
    BEGIN CATCH  
  
        SELECT ERROR_MESSAGE() AS Message;  
  
    END CATCH  
  
END;


GO


-- ==========================================================

-- SP: ✔️spUpdateTask.sql

-- ==========================================================

CREATE PROCEDURE spUpdateTask
    @UserID INT,
    @TaskID INT,
    @PriorityID INT,
    @TaskStatusID INT,
    @TaskTitle VARCHAR(150),
    @Deadline DATE
AS
BEGIN
  
  
    SET @TaskTitle = LTRIM(RTRIM(@TaskTitle));

    IF @TaskTitle IS NULL OR @TaskTitle = ''
    BEGIN
        SELECT 'Task Title Cannot Be Empty' AS Message;
        RETURN;
    END
  
    IF @Deadline IS NULL
    BEGIN
        SELECT 'Deadline Cannot Be Empty' AS Message;
        RETURN;
    END

    IF @Deadline < CAST(GETDATE() AS DATE)
    BEGIN
        SELECT 'Invalid Deadline Date' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User Account Is Not Active' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE TaskID = @TaskID
        AND UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid TaskID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTaskPriorities
        WHERE PriorityID = @PriorityID
    )
    BEGIN
        SELECT 'Invalid PriorityID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTaskStatus
        WHERE TaskStatusID = @TaskStatusID
    )
    BEGIN
        SELECT 'Invalid TaskStatusID' AS Message;
        RETURN;
    END
  
  
    BEGIN TRY
  
        UPDATE tblTask
        SET
            PriorityID = @PriorityID,
            TaskStatusID = @TaskStatusID,
            TaskTitle = @TaskTitle,
            Deadline = @Deadline
        WHERE TaskID = @TaskID
        AND UserID = @UserID;
  
        SELECT 'Task Updated Successfully' AS Message;
  
    END TRY
  
    BEGIN CATCH
  
        SELECT ERROR_MESSAGE() AS Message;
  
    END CATCH
  
END;


GO


-- ==========================================================

-- SP: ✔️spUpdateTaskStatus.sql

-- ==========================================================

CREATE PROCEDURE spUpdateTaskStatus
    @TaskID INT,
    @TaskStatusID INT
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblTask
        WHERE TaskID = @TaskID
    )
    BEGIN
        SELECT 'Invalid TaskID' AS Message;
        RETURN;
    END

			IF NOT EXISTS
			(
				SELECT 1
				FROM tblTaskStatus
				WHERE TaskStatusID = @TaskStatusID
			)
			BEGIN
				SELECT 'Invalid TaskStatusID' AS Message;
				RETURN;
			END

			IF EXISTS
			(
				SELECT 1
				FROM tblTask
				WHERE TaskID = @TaskID
				AND TaskStatusID = @TaskStatusID
			)
		BEGIN
			SELECT 'Task Already Has This Status' AS Message;
			RETURN;
		END

    BEGIN TRY

        UPDATE tblTask
        SET TaskStatusID = @TaskStatusID
        WHERE TaskID = @TaskID;

        SELECT 'Task Status Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END;



GO




-- ==========================================================

-- SP: ✔️spGetAllPersons.sql

-- ==========================================================

CREATE PROC spGetAllPersons
@UserID INT
AS
BEGIN
	BEGIN TRY
		IF NOT EXISTS (SELECT 1 
						FROM tblUserAuthentication
						WHERE UserID = @UserID AND Active = 1)
		BEGIN
			SELECT 'Invalid OR Inactive UserID!!' AS Message
			RETURN
		END

		--Check Person Exist
		IF NOT EXISTS (SELECT 1 FROM tblPersons
		WHERE UserID = @UserID)
		BEGIN
			SELECT 'No Persons Found' AS Message
			RETURN
		END

		--Print Persons of Person Table
		SELECT  PersonID,PersonName, PhoneNumber, Address
		FROM tblPersons
		WHERE UserID = @UserID  ORDER BY PersonName ASC;

	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END
GO
-- ==========================================================

-- SP: ✔️spGetPendingLentByStatusName.sql

-- ==========================================================

CREATE PROC spGetPendingLentByStatusName
@UserID INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 
					FROM tblUserAuthentication
					WHERE UserID = @UserID AND Active = 1)
	BEGIN
		SELECT 'Invalid OR Inactive UserID!!' AS Message
		RETURN
	END

	IF NOT EXISTS (SELECT 1 FROM tblLent L
	LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	WHERE L.UserID = @UserID AND S.StatusName IN ('Pending', 'Overdue', 'Partially Paid'))
	BEGIN
		SELECT 'No Pending Record Found' AS Message
		RETURN
	END

	SELECT Prsn.PersonName,
			L.Amount,
			L.ReturnedAmount,
			L.RemainingAmount,
			Pay.PaymentName,
			S.StatusName,
			L.LentAt,
			L.DeadlineAt,
			L.Description
	FROM tblLent L
	LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
	LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
	LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID

	WHERE L.UserID = @UserID AND S.StatusName IN ('Pending', 'Overdue', 'Partially Paid')
	ORDER BY L.LentAt DESC;
END
GO
-- ==========================================================

-- SP: ✔️spGetUpcomingLentReminders.sql

-- ==========================================================

CREATE PROC spGetUpcomingLentReminders
    @UserID INT
AS
BEGIN
    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
	DECLARE @StatusID INT;
    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUserAuthentication
            WHERE UserID = @UserID
			AND Active = 1
        )
        BEGIN
            SELECT 'UserID Does Not Exist' AS Message;
            RETURN;
        END


		SELECT TOP 1 @StatusID = StatusID FROM tblLentBorrowStatus
				WHERE StatusName = 'Pending';

		IF @StatusID IS NULL
		BEGIN
			SELECT 'Pending Status Not Found' AS Message;
			RETURN;
		END

        IF NOT EXISTS
		(
			SELECT 1
			FROM tblLent
			WHERE UserID = @UserID
			AND StatusID = @StatusID
			AND DATEDIFF
			(
				DAY,
				@Today,
				CAST(DeadlineAt AS DATE)
			) IN (7,3,1)
		)
		BEGIN
			SELECT 'No Upcoming Pending Lent Found' AS Message;
			RETURN;
		END

        SELECT L.LentID,
			Prsn.PersonName,
			L.Amount,
			L.ReturnedAmount,
			L.RemainingAmount,
			Pay.PaymentName,
			S.StatusName,
			L.LentAt,
			L.DeadlineAt,
			DATEDIFF(
					 DAY, 
					 @Today, 
					 CAST(L.DeadlineAt AS DATE)
					) AS RemainingDays,
			L.Description
		FROM tblLent L
		LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
		LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
		LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID
		WHERE L.UserID = @UserID
		AND L.DeadlineAt >= @Today
		AND L.StatusID = @StatusID
		AND DATEDIFF
		(
			DAY,
			@Today,
			CAST(L.DeadlineAt AS DATE)
		) IN (7,3,1)
		ORDER BY L.DeadlineAt ASC

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH

END
GO
-- ==========================================================

-- SP: ✔️spInsertLent.sql

-- ==========================================================

CREATE PROC spInsertLent
	@UserID INT,
	@PersonID INT,
	@PaymentID INT,
	@Amount DECIMAL(10,2),
	@DeadlineAT DATETIME,
	@Description VARCHAR(MAX)
AS
BEGIN
	
	-- Variable Declaration
	DECLARE @SubCategoryId INT;
	DECLARE @CategoryId INT;
	DECLARE @ReturnedAmount DECIMAL(10,2);
	DECLARE @RemainingAmount DECIMAL(10,2);
	DECLARE @StatusID INT;

	BEGIN TRY
		BEGIN TRANSACTION
			IF NOT EXISTS (SELECT 1 
							FROM tblUserAuthentication
							WHERE UserID = @UserID AND Active = 1)
			BEGIN
				SELECT 'Invalid OR Inactive UserID!!' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

        
			-- Check PersonID
			IF NOT EXISTS(SELECT 1
						  FROM tblPersons
						  WHERE PersonID = @PersonID AND UserID = @UserID)
			BEGIN
				SELECT 'Person Not Exist' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

        
			-- Check PaymentID
			IF NOT EXISTS(SELECT 1
						  FROM tblPaymentType
						  WHERE PaymentID = @PaymentID)
			BEGIN
				SELECT 'Invalid PaymentID!!' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END


			SELECT @StatusID = StatusID
			FROM tblLentBorrowStatus
			WHERE StatusName = 'Pending'

			IF CAST(@DeadlineAT AS DATE) < CAST(GETDATE() AS DATE)
			BEGIN
				SELECT 'Deadline Date Cannot Be Earlier Than Today' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END
              
			-- Check Amount
			IF @Amount <= 0
			BEGIN
				SELECT 'Amount Must Be Greater Than 0!!' AS Message
				ROLLBACK TRANSACTION
				RETURN

			END

			SET @ReturnedAmount = 0;
			SET @RemainingAmount = @Amount;

			-- Get CategoryId & SubCategoryId From tblExpenseSubCategory
			SELECT @CategoryId = CategoryId,
			@SubCategoryId = SubCategoryId
			FROM tblExpenseSubCategory
			WHERE SubCategoryName = 'Lent Given';
            
			IF @SubCategoryId IS NULL
			BEGIN
				SELECT 'Lent Given SubCategory Not Found' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

			IF @StatusID IS NULL
			BEGIN
				SELECT 'Pending Status Not Found' AS Message
				ROLLBACK TRANSACTION
				RETURN
			END

			--Insert Lent on Lent Table
			INSERT INTO tblLent
			(
				UserID,
				PersonID,
				PaymentID,
				StatusID,
				Amount,
				ReturnedAmount,
				RemainingAmount,
				DeadlineAt,
				Description
			)
			VALUES
			(
				@UserID,
				@PersonID,
				@PaymentID,
				@StatusID,
				@Amount,
				@ReturnedAmount,
				@RemainingAmount,
				@DeadlineAT,
				@Description
			);
			--Insert Lent on Expense Table
			INSERT INTO tblExpense
			(
				UserID,
				CategoryId,
				SubCategoryId,
				Amount,
				Description,
				PaymentID
			)
			VALUES
			(
				@UserID,
				@CategoryId,
				@SubCategoryId,
				@Amount,
				@Description,
				@PaymentID
			);

		COMMIT TRANSACTION

		SELECT 'Lent Insert Successfully' AS Message
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END
GO
-- ==========================================================

-- SP: ✔️spInsertPerson.sql

-- ==========================================================

CREATE PROC spInsertPerson
(
    @UserID INT,
    @PersonName VARCHAR(100),
    @PhoneNumber VARCHAR(20),
    @Address VARCHAR(MAX)
)
AS
BEGIN
    BEGIN TRY

	 IF @UserID IS NULL OR @UserID <= 0
        BEGIN
            SELECT 'User ID is Null' AS Message;
            RETURN;
        END


        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUserAuthentication
            WHERE UserID = @UserID
            AND Active = 1
        )
        BEGIN
            SELECT 'Invalid OR Inactive UserID!!' AS Message;
            RETURN;
        END

        SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));
        SET @PersonName = LTRIM(RTRIM(@PersonName));
        SET @Address = LTRIM(RTRIM(@Address));

        IF @PersonName = '' OR @PersonName = NULL
        BEGIN
            SELECT 'Person Name is Null' AS Message;
            RETURN;
        END

        IF @PhoneNumber = '' OR @PhoneNumber = NULL
        BEGIN
            SELECT 'Phone Number is Null' AS Message;
            RETURN;
        END

        IF EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE UserID = @UserID
            AND PhoneNumber = @PhoneNumber
        )
        BEGIN
            SELECT 'Phone Number Already Taken' AS Message;
            RETURN;
        END


        INSERT INTO tblPersons
        (
            UserID,
            PersonName,
            PhoneNumber,
            Address
        )
        VALUES
        (
            @UserID,
            @PersonName,
            @PhoneNumber,
            @Address
        );

     SELECT 'Person Details Inserted Successfully' AS Message;

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH
END
GO
-- ==========================================================

-- SP: ✔️spReturnLentByReturnAmount.sql

-- ==========================================================

CREATE PROC spReturnLentByReturnAmount
@LentID INT, @PaymentID INT, @ReturnedAmount DECIMAL(10,2), @Description VARCHAR(MAX)
AS
BEGIN
	DECLARE @TotalAmount DECIMAL(10,2);
	DECLARE @RemainingAmount DECIMAL(10,2);
	DECLARE @NewRemainingAmount DECIMAL(10,2);
	DECLARE @NewReturnedAmount DECIMAL(10,2);
	DECLARE @OldReturnedAmount DECIMAL(10,2);
	DECLARE @StatusID INT;
	DECLARE @UserID INT;
	DECLARE @CategoryID INT;
	DECLARE @SubCategoryID INT;

	BEGIN TRY
		BEGIN TRANSACTION
			----------------------------All Validation-----------------------------------------
			IF NOT EXISTS (SELECT 1 FROM tblLent WHERE LentID = @LentID)
			BEGIN
				SELECT 'Invalid LentID!!' AS MESSAGE
				ROLLBACK TRANSACTION
				RETURN
			END

			IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentID = @PaymentID)
			BEGIN
				SELECT 'Invalid PaymentID!!' AS MESSAGE
				ROLLBACK TRANSACTION
				RETURN
			END

			IF @ReturnedAmount <= 0
			BEGIN
				SELECT 'Returned Amount Must Be Greater Than 0!' AS MESSAGE
				ROLLBACK TRANSACTION
				RETURN
			END

			----------------------------All Validation-----------------------------------------
			

			--Get Amount & RemainingAmount
			SELECT @TotalAmount = Amount,
			@RemainingAmount = RemainingAmount,
			@OldReturnedAmount = ReturnedAmount,
			@UserID = UserID
			FROM tblLent
			WHERE LentID = @LentID;

			--IF RemainingAmount is NULL THEN @RemainingAmount = @TotalAmount
			IF @RemainingAmount is NULL
			BEGIN
				SET @RemainingAmount = @TotalAmount;
			END


			--Calculating  New RemainingAmount
			SET @NewRemainingAmount = @RemainingAmount - @ReturnedAmount;

			--Calculate Total Returned Amount
			SET @NewReturnedAmount = @ReturnedAmount + @OldReturnedAmount;

			SELECT @SubCategoryID = SubCategoryID,
			@CategoryID = CategoryID
			FROM tblCreditSubCategory
			WHERE SubCategoryName = 'Lent Returned';
             
            IF @CategoryID IS NULL OR @SubCategoryID IS NULL
            BEGIN
            SELECT 'Lent Returned Credit Category/SubCategory Not Found' AS Message
            ROLLBACK TRANSACTION
            RETURN
            END

			IF @NewRemainingAmount = 0
			BEGIN
				--Get 'Complete' StatusName ID
				SELECT @StatusID = StatusID FROM tblLentBorrowStatus
				WHERE StatusName = 'Paid';

				--Update Lent Table Data
				UPDATE tblLent
				SET RemainingAmount = 0,
				ReturnedAmount = @NewReturnedAmount,
				StatusID = @StatusID
				WHERE LentID = @LentID;

			END
			ELSE IF @NewRemainingAmount > 0
			BEGIN
				--Get 'Pending' StatusName ID
				SELECT @StatusID = StatusID FROM tblLentBorrowStatus
				WHERE StatusName = 'Pending';

				--Update Lent Table Data
				UPDATE tblLent
				SET RemainingAmount = @NewRemainingAmount,
				ReturnedAmount = @NewReturnedAmount,
				StatusID = @StatusID
				WHERE LentID = @LentID;

			END
			ELSE
			BEGIN
				RAISERROR('Returned amount exceeds remaining amount.',16,1);
			END

			--Data Insert On Credit Table
				INSERT INTO tblCredit(
					UserID,
					CategoryID,
					SubCategoryID,
					PaymentID,
					Amount,
					Description
					)
				VALUES(
					@UserID, 
					@CategoryID,
					@SubCategoryID,
					@PaymentID,
					@ReturnedAmount,
					@Description
					);

			COMMIT TRANSACTION
			SELECT 'Lent Returned Successfully' AS MESSAGE
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			SELECT
				ERROR_MESSAGE() AS ErrorMessage
		END CATCH
END
GO


-- SP: ✔️spFilterLentByStatus.sql

-- ==========================================================

CREATE PROCEDURE spFilterLentByStatus
    @UserID INT,
    @StatusID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate User
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;

    -- Validate Status
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblLentBorrowStatus
        WHERE StatusID = @StatusID
    )
    BEGIN
        SELECT 'Invalid Status' AS MESSAGE;
        RETURN;
    END;

    -- Check Records
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblLent
        WHERE UserID = @UserID
          AND StatusID = @StatusID
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;

    -- Fetch Records
    SELECT
        L.LentID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        L.Amount,
        L.ReturnedAmount,
        L.RemainingAmount,
        L.DeadlineAt,
        LTRIM(RTRIM(L.Description)) AS Description,
        L.LentAt
    FROM tblLent L
    LEFT JOIN tblPersons P
        ON L.PersonID = P.PersonID
    LEFT JOIN tblPaymentType PT
        ON L.PaymentID = PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S
        ON L.StatusID = S.StatusID
    WHERE L.UserID = @UserID
      AND L.StatusID = @StatusID
    ORDER BY L.LentAt DESC;
END
GO

-- eta ektu check korbi mne thik oo ache but problem oo ache null validatiopn nae kichu jaygay total ta dekhbi . partialy paid dekhache na kono khetre setao dekhbi 


-- ==========================================================

-- SP: ✔️spUpdatePerson.sql

-- ==========================================================

CREATE PROC spUpdatePerson
(
    @UserID INT,
    @PersonID INT,
    @PersonName VARCHAR(100),
    @PhoneNumber VARCHAR(20),
    @Address VARCHAR(MAX)
)
AS
BEGIN
    BEGIN TRY

        IF @UserID IS NULL OR @UserID <= 0
        BEGIN
            SELECT 'User ID is Null' AS Message;
            RETURN;
        END

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUserAuthentication
            WHERE UserID = @UserID
            AND Active = 1
        )
        BEGIN
            SELECT 'Invalid OR Inactive UserID!!' AS Message;
            RETURN;
        END

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE PersonID = @PersonID
            AND UserID = @UserID
        )
        BEGIN
            SELECT 'Invalid PersonID!!' AS Message;
            RETURN;
        END

        SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber));
        SET @PersonName = LTRIM(RTRIM(@PersonName));
        SET @Address = LTRIM(RTRIM(@Address));


        IF @PersonName IS NULL
           OR @PersonName = ''
           OR UPPER(@PersonName) = 'NULL'
        BEGIN
            SELECT 'Person Name is Null' AS Message;
            RETURN;
        END

        IF @PhoneNumber IS NULL
           OR @PhoneNumber = ''
           OR UPPER(@PhoneNumber) = 'NULL'
        BEGIN
            SELECT 'Phone Number is Null' AS Message;
            RETURN;
        END

        IF EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE UserID = @UserID
            AND PhoneNumber = @PhoneNumber
            AND PersonID <> @PersonID
        )
        BEGIN
            SELECT 'Phone Number Already Exists' AS Message;
            RETURN;
        END

        UPDATE tblPersons
        SET
            PersonName = @PersonName,
            PhoneNumber = @PhoneNumber,
            Address = @Address
        WHERE PersonID = @PersonID AND UserID = @UserID;

        SELECT 'Person Details Updated Successfully' AS Message;

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH
END
GO


-- ==========================================================

-- SP: ✔️spGetUpcomingBorrowReminders.sql

-- ==========================================================

CREATE PROCEDURE spGetUpcomingBorrowReminders
(
    @UserID INT
)
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);

    -------------------------------------------------
    -- User Validation
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'User does not exist or inactive' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Data Check
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID = @UserID
        AND RemainingAmount > 0
        AND
        (
           DeadlineAt < @Today
           OR DATEDIFF(DAY,@Today,CAST(DeadlineAt AS DATE)) IN (0,1,3,7)
        )
    )
    BEGIN
        SELECT 'No borrow records found' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Reminder Query
    -------------------------------------------------

    SELECT
        b.BorrowID,
        p.PersonName,
        pt.PaymentName,
        s.StatusName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        b.Description,

        DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) AS DaysRemaining,

        CASE

            -------------------------------------------------
            -- OVERDUE
            -------------------------------------------------
            WHEN b.DeadlineAt < @Today THEN
                'Overdue payment. Please clear it as soon as possible.'

            -------------------------------------------------
            -- DUE TODAY
            -------------------------------------------------
            WHEN DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) = 0 THEN
                'This payment is due today.'

            -------------------------------------------------
            -- BEFORE DEADLINE REMINDERS
            -------------------------------------------------
            WHEN DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) = 1 THEN
                'Reminder: payment is due tomorrow.'

            WHEN DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) = 3 THEN
                'Reminder: payment is due in 3 days.'

            WHEN DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) = 7 THEN
                'Reminder: payment is due in 7 days.'

            ELSE
                'Upcoming payment.'

        END AS ReminderMessage

    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.RemainingAmount > 0
      AND (
            b.DeadlineAt < @Today
            OR DATEDIFF(DAY, @Today, CAST(b.DeadlineAt AS DATE)) IN (7, 3, 1, 0)
          )

    ORDER BY b.DeadlineAt ASC;

END


GO



-- ==========================================================
-- SP: spGetOverduedBorrow
-- From File: ??spGetOverduedBorrow.sql
-- ==========================================================
CREATE PROCEDURE spGetOverduedBorrow
(
    @UserID INT
)
AS
BEGIN

    -------------------------------------------------
    -- User Validation
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID!' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'User does not exist or inactive!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Store Today's Date Once
    -------------------------------------------------

    DECLARE @Today DATE = GETDATE();

    -------------------------------------------------
    -- Overdue Existence Check
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID = @UserID
          AND RemainingAmount > 0
          AND DeadlineAt < @Today
    )
    BEGIN
        SELECT 'No overdue borrow records found!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Overdue Borrow Records
    -------------------------------------------------

    SELECT
        b.BorrowID,
        ISNULL(p.PersonName, 'Unknown') AS PersonName,
        ISNULL(pt.PaymentName, 'Unknown') AS PaymentName,
        ISNULL(s.StatusName, 'Unknown') AS StatusName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        b.Description,

        DATEDIFF
        (
            DAY,
            CAST(b.DeadlineAt AS DATE),
            @Today
        ) AS OverdueDays

    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.RemainingAmount > 0
      AND b.DeadlineAt < @Today

    ORDER BY b.DeadlineAt ASC;

END



GO

-- ==========================================================
-- SP: spGetPendingBorrow
-- From File: ??spGetPendingBorrow.sql
-- ==========================================================
CREATE PROCEDURE spGetPendingBorrow
(
    @UserID INT,
    @PersonID INT = NULL,
    @PaymentName VARCHAR(100) = NULL
)
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @PaymentID INT;

    -------------------------------------------------
    -- User Validation
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID!' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'User does not exist or inactive!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Validate PersonID (if provided)
    -------------------------------------------------

    IF @PersonID IS NOT NULL AND @PersonID > 0
    BEGIN
        IF NOT EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE PersonID = @PersonID
              AND UserID = @UserID
        )
        BEGIN
            SELECT 'Person does not belong to this user!' AS Message;
            RETURN;
        END
    END

    -------------------------------------------------
    -- Resolve PaymentName ? PaymentID (optional)
    -------------------------------------------------

    IF @PaymentName IS NOT NULL AND LTRIM(RTRIM(@PaymentName)) <> ''
    BEGIN
        SELECT @PaymentID = PaymentID
        FROM tblPaymentType
        WHERE LTRIM(RTRIM(PaymentName)) = LTRIM(RTRIM(@PaymentName));

        IF @PaymentID IS NULL
        BEGIN
            SELECT 'Invalid Payment Name!' AS Message;
            RETURN;
        END
    END

    -------------------------------------------------
    -- Check data exists
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID = @UserID
        AND RemainingAmount > 0 
        AND (@PersonID IS NULL OR PersonID = @PersonID)
        AND (@PaymentID IS NULL OR PaymentID = @PaymentID)
    )
    BEGIN
        SELECT 'No pending borrow records found!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Pending Borrow Records
    -------------------------------------------------

    SELECT
        b.BorrowID,
        p.PersonName,
        pt.PaymentName,
        s.StatusName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        b.Description
    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.RemainingAmount > 0

      -- filter by PersonID if provided
      AND (@PersonID IS NULL OR b.PersonID = @PersonID)

      -- filter by PaymentID if provided
      AND (@PaymentName IS NULL OR b.PaymentID = @PaymentID)

      AND s.StatusName IN ('Pending', 'Partially Paid', 'Overdue')

    ORDER BY b.DeadlineAt ASC;

END;
GO

-- ==========================================================
-- SP: spInsertBorrow
-- From File: ??spInsertBorrow.sql
-- ==========================================================
CREATE PROCEDURE spInsertBorrow
(
    @UserID INT,
    @PersonID INT,
    @PaymentName VARCHAR(100),
    @Amount DECIMAL(10,2),
    @DeadlineAt DATETIME,
    @Description VARCHAR(MAX)
)
AS
BEGIN

    DECLARE @PaymentID INT;
    DECLARE @StatusID INT;
    DECLARE @CreditCategoryID INT;
    DECLARE @CreditSubCategoryID INT;

    -------------------------------------------------
    -- Trim Inputs
    -------------------------------------------------

    SET @PaymentName = LTRIM(RTRIM(@PaymentName));
    SET @Description = LTRIM(RTRIM(@Description));

    BEGIN TRY

        -------------------------------------------------
        -- UserID Validation
        -------------------------------------------------

        IF @UserID IS NULL OR @UserID <= 0
        BEGIN
            SELECT 'Invalid UserID.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- PersonID Validation
        -------------------------------------------------

        IF @PersonID IS NULL OR @PersonID <= 0
        BEGIN
            SELECT 'Invalid PersonID.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- User Exists + Active Check
        -------------------------------------------------

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUsers U
            INNER JOIN tblUserAuthentication UA
                ON U.UserID = UA.UserID
            WHERE U.UserID = @UserID
              AND UA.Active = 1
        )
        BEGIN
            SELECT 'User does not exist or inactive.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Person Belongs To User Validation
        -------------------------------------------------

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblPersons
            WHERE PersonID = @PersonID
              AND UserID = @UserID
        )
        BEGIN
            SELECT 'Person does not belong to this user.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Payment Type Validation
        -------------------------------------------------

        SELECT @PaymentID = PaymentID
        FROM tblPaymentType
        WHERE LTRIM(RTRIM(PaymentName)) = @PaymentName;

        IF @PaymentID IS NULL
        BEGIN
            SELECT 'Invalid Payment Type.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Default Status = Pending
        -------------------------------------------------

        SELECT @StatusID = StatusID
        FROM tblLentBorrowStatus
        WHERE StatusName = 'Pending';

        IF @StatusID IS NULL
        BEGIN
            SELECT 'Pending Status Not Found.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Amount Validation
        -------------------------------------------------

        IF @Amount IS NULL OR @Amount <= 0
        BEGIN
            SELECT 'Amount must be greater than zero.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Description Validation
        -------------------------------------------------

        IF @Description IS NULL OR @Description = ''
        BEGIN
            SELECT 'Description is required.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Deadline Validation
        -------------------------------------------------

        IF @DeadlineAt IS NULL
        BEGIN
            SELECT 'Deadline date is required.' AS Message;
            RETURN;
        END

        IF CAST(@DeadlineAt AS DATE) < CAST(GETDATE() AS DATE)
        BEGIN
            SELECT 'Deadline cannot be in the past.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Get Credit Category ID
        -------------------------------------------------

        SELECT @CreditCategoryID = CategoryID
        FROM tblCreditCategory
        WHERE CategoryName = 'Borrow';

        IF @CreditCategoryID IS NULL
        BEGIN
            SELECT 'Credit Category Not Found.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Get Credit SubCategory ID
        -------------------------------------------------

        SELECT @CreditSubCategoryID = SubCategoryID
        FROM tblCreditSubCategory
        WHERE SubCategoryName = 'Borrow Received'
          AND CategoryID = @CreditCategoryID;

        IF @CreditSubCategoryID IS NULL
        BEGIN
            SELECT 'Borrow Credit SubCategory Not Found.' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Start Transaction
        -------------------------------------------------

        BEGIN TRANSACTION;

        -------------------------------------------------
        -- Insert Into Borrow
        -------------------------------------------------

        INSERT INTO tblBorrow
        (
            UserID,
            PersonID,
            PaymentID,
            StatusID,
            Amount,
            PaidAmount,
            RemainingAmount,
            BorrowAt,
            DeadlineAt,
            Description
        )
        VALUES
        (
            @UserID,
            @PersonID,
            @PaymentID,
            @StatusID,
            @Amount,
            0,
            @Amount,
            GETDATE(),
            @DeadlineAt,
            @Description
        );

        -------------------------------------------------
        -- Insert Into Credit
        -------------------------------------------------

        INSERT INTO tblCredit
        (
            UserID,
            CategoryID,
            SubCategoryID,
            PaymentID,
            Amount,
            Description,
            CreditAt
        )
        VALUES
        (
            @UserID,
            @CreditCategoryID,
            @CreditSubCategoryID,
            @PaymentID,
            @Amount,
            'Borrow Amount Credited : ' + @Description,
            GETDATE()
        );

        -------------------------------------------------
        -- Commit Transaction
        -------------------------------------------------

        COMMIT TRANSACTION;

        SELECT 'Borrow transaction inserted successfully.' AS Message;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END
GO

-- ==========================================================
-- SP: spUpdateOverdueStatus
-- From File: ??spUpdateOverdueStatus.sql
-- ==========================================================
CREATE PROCEDURE spUpdateOverdueStatus
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @OverdueStatusID INT;

    -------------------------------------------------
    -- Get Overdue Status ID
    -------------------------------------------------

    SELECT @OverdueStatusID = StatusID
    FROM tblLentBorrowStatus
    WHERE StatusName = 'Overdue';

	-------------------------------------------------
    -- Check OverdueStatusID is NULL
    -------------------------------------------------

	IF @OverdueStatusID IS NULL
	BEGIN
		SELECT 'Overdue Status Not Found!' AS Message;
		RETURN
	END
    -------------------------------------------------
    -- Update Overdue Records
    -------------------------------------------------

    UPDATE tblBorrow
    SET StatusID = @OverdueStatusID
    WHERE RemainingAmount > 0
      AND CAST(DeadlineAt AS DATE) < @Today
      AND StatusID <> @OverdueStatusID;

END
 

GO



-- ==========================================================

-- SP: ✔️spGetAllBorrow.sql

-- ==========================================================

CREATE PROCEDURE spGetAllBorrow
(
    @UserID INT
)
AS
BEGIN

    -------------------------------------------------
    -- Validate User
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'Invalid OR Inactive UserID!!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Check Borrow Records Exist Or Not
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'No Borrow Records Found!!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get All Borrow Details
    -------------------------------------------------

    SELECT
        b.BorrowID,
        p.PersonName,
        pt.PaymentName,
        s.StatusName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        b.Description
    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblPaymentType pt
        ON b.PaymentID = pt.PaymentID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID

    ORDER BY b.BorrowAt DESC;

END

GO


-- ==========================================================

-- SP: ✔️spGetBorrowPersonHistory.sql

-- ==========================================================

CREATE PROCEDURE spGetBorrowPersonHistory
(
    @PersonID INT,
    @UserID INT
)
AS
BEGIN

    -------------------------------------------------
    -- Check Person Belongs To User + Borrow History Exists
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow B
        JOIN tblPersons P
            ON B.PersonID = P.PersonID
        WHERE B.UserID = @UserID
        AND B.PersonID = @PersonID
    )
    BEGIN
        SELECT 'Invalid PersonID OR No Borrow History Found!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Borrow History Of Person
    -------------------------------------------------

    SELECT
        B.BorrowID,
        P.PersonName,
        P.PhoneNumber,
        P.Address,
        Pay.PaymentName,
        S.StatusName,
        B.Amount,
        B.PaidAmount,
        B.RemainingAmount,
        B.BorrowAt,
        B.DeadlineAt,
        B.Description

    FROM tblBorrow B

    LEFT JOIN tblPersons P
        ON B.PersonID = P.PersonID

    LEFT JOIN tblPaymentType Pay
        ON B.PaymentID = Pay.PaymentID

    LEFT JOIN tblLentBorrowStatus S
        ON B.StatusID = S.StatusID

    WHERE B.PersonID = @PersonID
    AND B.UserID = @UserID

    ORDER BY B.BorrowAt DESC;

END

GO


-- ==========================================================

-- SP: ✔️spGetCompletedBorrow.sql

-- ==========================================================

CREATE PROCEDURE spGetCompletedBorrow
(
    @UserID INT
)
AS
BEGIN

    DECLARE @PaidStatusID INT;

    -------------------------------------------------
    -- User Validation (Exist + Active)
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID!' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers U
        INNER JOIN tblUserAuthentication UA
            ON U.UserID = UA.UserID
        WHERE U.UserID = @UserID
        AND UA.Active = 1
    )
    BEGIN
        SELECT 'User does not exist or inactive!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get StatusID from StatusName (NO typo risk in logic)
    -------------------------------------------------

    SELECT @PaidStatusID = StatusID
    FROM tblLentBorrowStatus
    WHERE LTRIM(RTRIM(StatusName)) = 'Paid';

    IF @PaidStatusID IS NULL
    BEGIN
        SELECT 'Paid status not found in system!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- No Record Check
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow b
        WHERE b.UserID = @UserID
        AND b.StatusID = @PaidStatusID
        AND b.RemainingAmount = 0
    )
    BEGIN
        SELECT 'No completed borrow records found!' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Get Completed Borrow Records
    -------------------------------------------------

    SELECT
        b.BorrowID,
        ISNULL(p.PersonName,'Unknown') AS PersonName,
        b.Amount,
        b.PaidAmount,
        b.RemainingAmount,
        b.BorrowAt,
        b.DeadlineAt,
        ISNULL(s.StatusName,'Unknown') AS StatusName,
        b.Description
    FROM tblBorrow b

    LEFT JOIN tblPersons p
        ON b.PersonID = p.PersonID

    LEFT JOIN tblLentBorrowStatus s
        ON b.StatusID = s.StatusID

    WHERE b.UserID = @UserID
      AND b.StatusID = @PaidStatusID
      AND b.RemainingAmount = 0

    ORDER BY b.BorrowAt DESC;

END;

GO


-- ==========================================================

-- SP: ✔️spGetTotalBorrowByPerson.sql

-- ==========================================================

CREATE PROCEDURE spGetTotalBorrowByPerson
(
    @UserID INT,
    @PersonID INT
)
AS
BEGIN

    -------------------------------------------------
    -- User Validation
    -------------------------------------------------

    IF @UserID IS NULL OR @UserID <= 0
    BEGIN
        SELECT 'Invalid UserID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
        AND Active = 1
    )
    BEGIN
        SELECT 'User not found or inactive' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Person Validation
    -------------------------------------------------

    IF @PersonID IS NULL OR @PersonID <= 0
    BEGIN
        SELECT 'Invalid PersonID' AS Message;
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblPersons
        WHERE PersonID = @PersonID
        AND UserID = @UserID
    )
    BEGIN
        SELECT 'Person does not belong to this user' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Data Integrity Check (UPDATED AS REQUESTED)
    -------------------------------------------------

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE PersonID = @PersonID
          AND UserID = @UserID
          AND Amount IS NOT NULL
    )
    BEGIN
        SELECT 'No borrow transactions found for this person' AS Message;
        RETURN;
    END

    -------------------------------------------------
    -- Final Summary
    -------------------------------------------------

    SELECT
        p.PersonID,
        ISNULL(p.PersonName, 'Unknown Person') AS PersonName,

        ROUND(ISNULL(SUM(b.Amount), 0), 2) AS TotalBorrowAmount,
        ROUND(ISNULL(SUM(b.PaidAmount), 0), 2) AS TotalPaidAmount,
        ROUND(ISNULL(SUM(b.RemainingAmount), 0), 2) AS TotalRemainingAmount

    FROM tblPersons p

    LEFT JOIN tblBorrow b
        ON p.PersonID = b.PersonID
        AND b.UserID = @UserID
        AND b.Amount IS NOT NULL

    WHERE p.PersonID = @PersonID

    GROUP BY
        p.PersonID,
        p.PersonName;

END;

GO


-- ==========================================================

-- SP: ✔️spPayBorrow.sql

-- ==========================================================

CREATE PROCEDURE spPayBorrow
(
    @BorrowID INT,
    @PaidAmount DECIMAL(10,2),
    @PaymentName VARCHAR(100)
)
AS
BEGIN

    BEGIN TRY

        DECLARE @UserID INT;
        DECLARE @RemainingAmount DECIMAL(10,2);
        DECLARE @NewRemainingAmount DECIMAL(10,2);

        DECLARE @PaymentID INT;
        DECLARE @StatusID INT;
        DECLARE @CategoryID INT;
        DECLARE @SubCategoryID INT;

        -------------------------------------------------
        -- Validation
        -------------------------------------------------

        IF @BorrowID IS NULL OR @BorrowID <= 0
        BEGIN
            SELECT 'Invalid BorrowID' AS Message;
            RETURN;
        END

        IF @PaidAmount IS NULL OR @PaidAmount <= 0
        BEGIN
            SELECT 'Invalid Paid Amount' AS Message;
            RETURN;
        END

        IF @PaymentName IS NULL OR LTRIM(RTRIM(@PaymentName)) = ''
        BEGIN
            SELECT 'Payment Name required' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Borrow Exists Check
        -------------------------------------------------

        IF NOT EXISTS
        (
            SELECT 1 FROM tblBorrow WHERE BorrowID = @BorrowID
        )
        BEGIN
            SELECT 'Borrow record not found' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Get Borrow Details
        -------------------------------------------------

        SELECT
            @UserID = UserID,
            @RemainingAmount = RemainingAmount
        FROM tblBorrow
        WHERE BorrowID = @BorrowID;

        -------------------------------------------------
        -- Over Payment Check
        -------------------------------------------------

        IF @PaidAmount > @RemainingAmount
        BEGIN
            SELECT 'Paid amount exceeds remaining balance' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Payment Lookup
        -------------------------------------------------

        SELECT @PaymentID = PaymentID
        FROM tblPaymentType
        WHERE LTRIM(RTRIM(PaymentName)) = LTRIM(RTRIM(@PaymentName));

        IF @PaymentID IS NULL
        BEGIN
            SELECT 'Invalid Payment Name' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Status Lookup
        -------------------------------------------------

        SELECT @StatusID = StatusID
        FROM tblLentBorrowStatus
        WHERE StatusName =
            CASE 
                WHEN (@RemainingAmount - @PaidAmount) = 0 THEN 'Paid'
                ELSE 'Partially Paid'
            END;

        IF @StatusID IS NULL
        BEGIN
            SELECT 'Status not found' AS Message;
            RETURN;
        END

        -------------------------------------------------
        -- Expense Category Lookup
        -------------------------------------------------

        SELECT @CategoryID = CategoryID
        FROM tblExpenseCategory
        WHERE CategoryName = 'Borrow';

        SELECT @SubCategoryID = SubCategoryID
        FROM tblExpenseSubCategory
        WHERE SubCategoryName = 'Borrow Returned'
          AND CategoryID = @CategoryID;

        -------------------------------------------------
        -- Transaction Start
        -------------------------------------------------

        BEGIN TRANSACTION;

        -------------------------------------------------
        -- Update Borrow
        -------------------------------------------------

        SET @NewRemainingAmount = @RemainingAmount - @PaidAmount;

        UPDATE tblBorrow
        SET
            PaidAmount = PaidAmount + @PaidAmount,
            RemainingAmount = @NewRemainingAmount,
            StatusID = @StatusID
        WHERE BorrowID = @BorrowID;

        -------------------------------------------------
        -- Insert Expense
        -------------------------------------------------

        INSERT INTO tblExpense
        (
            UserID,
            CategoryID,
            SubCategoryID,
            PaymentID,
            Amount,
            Description,
            ExpenseAt
        )
        VALUES
        (
            @UserID,
            @CategoryID,
            @SubCategoryID,
            @PaymentID,
            @PaidAmount,
            'Borrow repayment payment',
            GETDATE()
        );

        -------------------------------------------------
        -- Commit
        -------------------------------------------------

        COMMIT TRANSACTION;

        -------------------------------------------------
        -- Result Output (no RETURN)
        -------------------------------------------------

        IF @NewRemainingAmount = 0
            SELECT 'Fully Paid' AS Message, 1 AS Result;
        ELSE
            SELECT 'Partially Paid' AS Message, 2 AS Result;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT ERROR_MESSAGE() AS Message, 0 AS Result;

    END CATCH

END;

GO


-- ==========================================================

-- SP: ✔️spGetAllLentBorrowStatus.sql

-- ==========================================================

CREATE PROCEDURE spGetAllLentBorrowStatus
AS
BEGIN
	BEGIN TRY
		
		--Print Status of LentBorrowStatus Table
		SELECT  StatusID ,StatusName 
		FROM tblLentBorrowStatus
		ORDER BY StatusName ASC;

	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message
	END CATCH
END;

GO

-- ==========================================================
-- SP: ✔️spFilterLentByAmountRange.sql
-- ==========================================================
CREATE PROCEDURE spFilterLentByAmountRange
    @UserID INT,
    @MinAmount DECIMAL(10,2),
    @MaxAmount DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    -- Validate User
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'Invalid or Inactive User' AS Message;
        RETURN;
    END;
    -- Validate Amount Range
    IF @MinAmount < 0 OR @MaxAmount < 0
    BEGIN
        SELECT 'Amount cannot be negative' AS Message;
        RETURN;
    END;
    IF @MinAmount > @MaxAmount
    BEGIN
        SELECT 'Minimum Amount cannot be greater than Maximum Amount' AS Message;
        RETURN;
    END;
    -- Filter Lent Records
    SELECT
        L.LentID,
        L.UserID,
        L.PersonID,
        PS.PersonName,
        L.PaymentID,
        PT.PaymentName,
        L.StatusID,
        S.StatusName,
        L.Amount,
        L.ReturnedAmount,
        L.RemainingAmount,
        L.LentAt,
        L.DeadlineAt,
        L.Description
    FROM tblLent L
        INNER JOIN tblPersons PS
            ON L.PersonID = PS.PersonID
        INNER JOIN tblPaymentType PT
            ON L.PaymentID = PT.PaymentID
        INNER JOIN tblLentBorrowStatus S
            ON L.StatusID = S.StatusID
    WHERE
        L.UserID = @UserID
        AND L.Amount BETWEEN @MinAmount AND @MaxAmount
    ORDER BY
        L.Amount DESC,
        L.LentAt DESC;
END;
GO

-- ==========================================================
-- SP: ✔️spFilterLentByDateRange.sql
-- ==========================================================
CREATE PROCEDURE spFilterLentByDateRange
    @UserID INT,
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    -- Validate User
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    -- Validate Date Range
    IF @FromDate > @ToDate
    BEGIN
        SELECT 'FromDate Cannot Be Greater Than ToDate' AS MESSAGE;
        RETURN;
    END;
    -- Check Records
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblLent
        WHERE UserID = @UserID
          AND CAST(LentAt AS DATE) BETWEEN CAST(@FromDate AS DATE) AND CAST(@ToDate AS DATE)
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    -- Fetch Records
    SELECT
        L.LentID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        L.Amount,
        L.ReturnedAmount,
        L.RemainingAmount,
		L.LentAt,
        L.DeadlineAt,
        LTRIM(RTRIM(L.Description)) AS Description,
        L.LentAt
    FROM tblLent L
    LEFT JOIN tblPersons P
        ON L.PersonID = P.PersonID
    LEFT JOIN tblPaymentType PT
        ON L.PaymentID = PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S
        ON L.StatusID = S.StatusID
    WHERE L.UserID = @UserID
      AND CAST(L.LentAt AS DATE)
          BETWEEN CAST(@FromDate AS DATE) AND CAST(@ToDate AS DATE)
    ORDER BY L.LentAt DESC;
END;
GO

-- ==========================================================
-- SP: ✔️spFilterLentByPerson.sql
-- ==========================================================
CREATE PROCEDURE spFilterLentByPerson
    @UserID INT,
    @PersonID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Validate User
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    -- Validate Person
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblPersons
        WHERE PersonID = @PersonID
          AND UserID = @UserID
    )
    BEGIN
        SELECT 'Invalid Person' AS MESSAGE;
        RETURN;
    END;
    -- Check Records
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblLent
        WHERE UserID = @UserID
          AND PersonID = @PersonID
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    -- Fetch Records
    SELECT
        L.LentID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        L.Amount,
        L.ReturnedAmount,
        L.RemainingAmount,
        L.DeadlineAt,
        LTRIM(RTRIM(L.Description)) AS Description,
        L.LentAt
    FROM tblLent L
    LEFT JOIN tblPersons P
        ON L.PersonID = P.PersonID
    LEFT JOIN tblPaymentType PT
        ON L.PaymentID = PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S
        ON L.StatusID = S.StatusID
    WHERE L.UserID = @UserID
      AND L.PersonID = @PersonID
    ORDER BY L.LentAt DESC;
END;
GO

-- ==========================================================
-- SP: ✔️spFilterLentByPaymentMethod.sql
-- ==========================================================
CREATE PROCEDURE spFilterLentByPaymentMethod
    @UserID INT,
    @PaymentID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Validate User
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID = @UserID
          AND Active = 1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    -- Validate Payment Method
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblPaymentType
        WHERE PaymentID = @PaymentID
    )
    BEGIN
        SELECT 'Invalid Payment Method' AS MESSAGE;
        RETURN;
    END;
    -- Check Records
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblLent
        WHERE UserID = @UserID
          AND PaymentID = @PaymentID
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    -- Fetch Records
    SELECT
        L.LentID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        L.Amount,
        L.ReturnedAmount,
        L.RemainingAmount,
        L.DeadlineAt,
        LTRIM(RTRIM(L.Description)) AS Description,
        L.LentAt
    FROM tblLent L
    LEFT JOIN tblPersons P
        ON L.PersonID = P.PersonID
    LEFT JOIN tblPaymentType PT
        ON L.PaymentID = PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S
        ON L.StatusID = S.StatusID
    WHERE L.UserID = @UserID
      AND L.PaymentID = @PaymentID
    ORDER BY L.LentAt DESC;
END;
GO

-- ==========================================================
-- SP: ✔️spFilterBorrowByDateRange.sql
-- ==========================================================
CREATE PROCEDURE spFilterBorrowByDateRange
    @UserID INT,
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID=@UserID
        AND Active=1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    IF @FromDate>@ToDate
    BEGIN
        SELECT 'FromDate Cannot Be Greater Than ToDate' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID=@UserID
        AND CAST(BorrowAt AS DATE)
        BETWEEN @FromDate AND @ToDate
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    SELECT
        B.BorrowID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        B.Amount,
        B.PaidAmount,
        B.RemainingAmount,
        B.DeadlineAt,
        LTRIM(RTRIM(B.Description)) AS Description,
        B.BorrowAt
    FROM tblBorrow B
    LEFT JOIN tblPersons P ON B.PersonID=P.PersonID
    LEFT JOIN tblPaymentType PT ON B.PaymentID=PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S ON B.StatusID=S.StatusID
    WHERE B.UserID=@UserID
    AND CAST(B.BorrowAt AS DATE)
    BETWEEN @FromDate AND @ToDate
    ORDER BY B.BorrowAt DESC;
END;
GO

-- ==========================================================
-- SP: ✔️spFilterBorrowByAmountRange.sql
-- ==========================================================
CREATE PROCEDURE spFilterBorrowByAmountRange
    @UserID INT,
    @MinAmount DECIMAL(10,2),
    @MaxAmount DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUserAuthentication
        WHERE UserID=@UserID
        AND Active=1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    IF @MinAmount<0 OR @MaxAmount<0
    BEGIN
        SELECT 'Amount Cannot Be Negative' AS MESSAGE;
        RETURN;
    END;
    IF @MinAmount>@MaxAmount
    BEGIN
        SELECT 'Minimum Amount Cannot Be Greater Than Maximum Amount' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1
        FROM tblBorrow
        WHERE UserID=@UserID
        AND Amount BETWEEN @MinAmount AND @MaxAmount
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    SELECT
        B.BorrowID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        B.Amount,
        B.PaidAmount,
        B.RemainingAmount,
        B.DeadlineAt,
        LTRIM(RTRIM(B.Description)) AS Description,
        B.BorrowAt
    FROM tblBorrow B
    LEFT JOIN tblPersons P ON B.PersonID=P.PersonID
    LEFT JOIN tblPaymentType PT ON B.PaymentID=PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S ON B.StatusID=S.StatusID
    WHERE B.UserID=@UserID
    AND B.Amount BETWEEN @MinAmount AND @MaxAmount
    ORDER BY B.Amount DESC,B.BorrowAt DESC;
END;
GO

-- ==========================================================
-- SP: ✔️spFilterBorrowByPerson.sql
-- ==========================================================
CREATE PROCEDURE spFilterBorrowByPerson
    @UserID INT,
    @PersonID INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblUserAuthentication
        WHERE UserID=@UserID
        AND Active=1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblPersons
        WHERE PersonID=@PersonID
        AND UserID=@UserID
    )
    BEGIN
        SELECT 'Invalid Person' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblBorrow
        WHERE UserID=@UserID
        AND PersonID=@PersonID
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    SELECT
        B.BorrowID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        B.Amount,
        B.PaidAmount,
        B.RemainingAmount,
        B.DeadlineAt,
        LTRIM(RTRIM(B.Description)) AS Description,
        B.BorrowAt
    FROM tblBorrow B
    LEFT JOIN tblPersons P ON B.PersonID=P.PersonID
    LEFT JOIN tblPaymentType PT ON B.PaymentID=PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S ON B.StatusID=S.StatusID
    WHERE B.UserID=@UserID
    AND B.PersonID=@PersonID
    ORDER BY B.BorrowAt DESC;
END;
GO

-- ==========================================================
-- SP: ✔️spFilterBorrowByStatus.sql
-- ==========================================================
CREATE PROCEDURE spFilterBorrowByStatus
    @UserID INT,
    @StatusID INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblUserAuthentication
        WHERE UserID=@UserID
        AND Active=1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblLentBorrowStatus
        WHERE StatusID=@StatusID
    )
    BEGIN
        SELECT 'Invalid Status' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblBorrow
        WHERE UserID=@UserID
        AND StatusID=@StatusID
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    SELECT
        B.BorrowID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        B.Amount,
        B.PaidAmount,
        B.RemainingAmount,
        B.DeadlineAt,
        LTRIM(RTRIM(B.Description)) AS Description,
        B.BorrowAt
    FROM tblBorrow B
    LEFT JOIN tblPersons P ON B.PersonID=P.PersonID
    LEFT JOIN tblPaymentType PT ON B.PaymentID=PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S ON B.StatusID=S.StatusID
    WHERE B.UserID=@UserID
    AND B.StatusID=@StatusID
    ORDER BY B.BorrowAt DESC;
END;
GO

-- ==========================================================
-- SP: ✔️spFilterBorrowByPaymentMethod.sql
-- ==========================================================
CREATE PROCEDURE spFilterBorrowByPaymentMethod
    @UserID INT,
    @PaymentID INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblUserAuthentication
        WHERE UserID=@UserID
        AND Active=1
    )
    BEGIN
        SELECT 'Invalid Or Inactive User' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblPaymentType
        WHERE PaymentID=@PaymentID
    )
    BEGIN
        SELECT 'Invalid Payment Method' AS MESSAGE;
        RETURN;
    END;
    IF NOT EXISTS
    (
        SELECT 1 FROM tblBorrow
        WHERE UserID=@UserID
        AND PaymentID=@PaymentID
    )
    BEGIN
        SELECT 'NO RECORD FOUND' AS MESSAGE;
        RETURN;
    END;
    SELECT
        B.BorrowID,
        P.PersonName,
        PT.PaymentName,
        S.StatusName,
        B.Amount,
        B.PaidAmount,
        B.RemainingAmount,
        B.DeadlineAt,
        LTRIM(RTRIM(B.Description)) AS Description,
        B.BorrowAt
    FROM tblBorrow B
    LEFT JOIN tblPersons P ON B.PersonID=P.PersonID
    LEFT JOIN tblPaymentType PT ON B.PaymentID=PT.PaymentID
    LEFT JOIN tblLentBorrowStatus S ON B.StatusID=S.StatusID
    WHERE B.UserID=@UserID
    AND B.PaymentID=@PaymentID
    ORDER BY B.BorrowAt DESC;
END;
GO

