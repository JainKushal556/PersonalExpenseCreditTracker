create proc spGetAllExpenses(
@UserID int
)
as
begin
select * from tblExpense where UserID=@UserID;
end

exec spGetAllExpenses
@UserID=1