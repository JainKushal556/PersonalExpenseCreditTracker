create proc spFilterExpenseByCategoryAndSubCategory(
@UserID int,
@CategoryID int,
@SubCategoryID int
)
as
begin
select * from tblExpense where UserID=@UserID and CategoryID=@CategoryID and SubCategoryID=@SubCategoryID ;
end

exec spFilterExpenseByCategoryAndSubCategory
@UserID=1,
@CategoryID=1,
@SubCategoryID=2


