create proc spInsertExpense(
 @UserID INT,
 @CategoryID INT,
 @SubCategoryID INT,
 @Amount DECIMAL(10,2),
 @Description VARCHAR(MAX),
 @PaymentID INT
)
as
begin
insert into tblExpense values(@UserID,@CategoryID,@SubCategoryID,@Amount,@Description,@PaymentID )
end

exec spInsertExpense
@UserID=1,
@CategoryID=1,
@SubCategoryID=2,
@Amount=2000.50,
@Description ='Electric bill for may month',
@PaymentID =2



