create proc spGetTodayExpense(
@UserID int
)
as
begin
select * from tblExpense where UserId=@UserID and cast(ExpenseAt as date)=cast(getdate() as date)
end

exec spGetTodayExpense
@UserID=1;

