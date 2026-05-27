create proc spFilterExpenseByCategory(
@UserID int,
@CategoryID int
)
as
begin
select * from tblExpense where UserID=@UserID and CategoryID=@CategoryID ;
end

exec spFilterExpenseByCategory
@UserID =1,
@CategoryID=1


