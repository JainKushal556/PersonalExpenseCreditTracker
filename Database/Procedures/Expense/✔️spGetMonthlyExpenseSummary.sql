create proc spGetMonthlyExpenseSummary(
@UserID int,
@Month int,
@Year int
)
as
begin
select isnull(sum(Amount),0) as TotalExpense from tblExpense where UserID=@UserID and year(ExpenseAt)=@Year and month(ExpenseAt)=@Month 
end

exec spGetMonthlyExpenseSummary
@UserID=1,
@Month=5,
@Year=2026

