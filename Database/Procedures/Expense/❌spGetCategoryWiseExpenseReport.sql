create proc spGetCategoryWiseExpenseReport(
@UserID int,
@FromDate date,
@ToDate date
)
as
begin
select tblExCat.CategoryName,isnull(sum(tblEx.Amount),0) as TotalExpense from tblExpense tblEx 
inner join tblExpenseCategory tblExCat on tblEx.CategoryID=tblExCat.CategoryID 
where tblEx.UserID=@UserID and cast(tblEx.ExpenseAt as date) between @FromDate and @ToDate  
group by tblExCat.CategoryName order by TotalExpense desc
end

exec spGetCategoryWiseExpenseReport
@UserID=1,
@FromDate='2026-05-26',
@ToDate='2026-05-27'


