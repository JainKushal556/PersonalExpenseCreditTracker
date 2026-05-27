create proc spFilterExpenseByDateRange(
@UserID int,
@FromDate date,
@ToDate date
)
as 
begin
select * from tblExpense where UserID=@UserID and cast(ExpenseAt as date) between @FromDate and @ToDate 
end
select * from tblExpense;

exec spFilterExpenseByDateRange
@UserID=1,
@FromDate='2026-05-26',
@ToDate='2026-05-27'



