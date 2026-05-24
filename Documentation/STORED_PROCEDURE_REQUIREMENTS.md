# =========================================
# FINAL STORED PROCEDURE REQUIREMENTS
# Personal Expense Credit Tracker
# =========================================

Based On:
- Final Database Schema
- Final SRS
- Actual WinForms Requirements

SRS Reference:
:contentReference[oaicite:0]{index=0}

=========================================
🔐 AUTHENTICATION & USER MANAGEMENT
=========================================

1. sp_RegisterUser
Purpose:
- Register new user account

Parameters:
@UserName
@Email
@PhoneNumber
@Password

Tables Used:
Users
User_Profile
User_Contact
User_Authentication

-----------------------------------------

2. sp_LoginUser
Purpose:
- Verify user login credentials

Parameters:
@Email
@Password

Tables Used:
User_Contact
User_Authentication

-----------------------------------------

3. sp_ChangePassword
Purpose:
- Change existing password

Parameters:
@UserID
@OldPassword
@NewPassword

Tables Used:
User_Authentication

-----------------------------------------

4. sp_UpdateUserProfile
Purpose:
- Update profile information

Parameters:
@UserID
@Name
@Email
@PhoneNumber
@ProfilePhoto

Tables Used:
User_Profile
User_Contact

-----------------------------------------

5. sp_GetUserDashboard
Purpose:
- Load dashboard data for logged user

Parameters:
@UserID

Tables Used:
Expense
Credit
Lent
Borrow
Task

=========================================
💸 EXPENSE MODULE
=========================================

6. sp_InsertExpense
Purpose:
- Add new expense transaction

Parameters:
@UserID
@CategoryID
@SubCategoryID
@Amount
@Description
@PaymentID
@ExpenseAt

Tables Used:
Expense

-----------------------------------------

7. sp_UpdateExpense
Purpose:
- Update expense information

Parameters:
@ExpenseID
@CategoryID
@SubCategoryID
@Amount
@Description
@PaymentID

Tables Used:
Expense

-----------------------------------------

8. sp_DeleteExpense
Purpose:
- Delete expense transaction

Parameters:
@ExpenseID

Tables Used:
Expense

-----------------------------------------

9. sp_GetExpenseByID
Purpose:
- Get single expense details

Parameters:
@ExpenseID

Tables Used:
Expense

-----------------------------------------

10. sp_GetAllExpenses
Purpose:
- Get all user expenses

Parameters:
@UserID

Tables Used:
Expense

-----------------------------------------

11. sp_FilterExpense
Purpose:
- Filter expense records

Parameters:
@UserID
@FromDate
@ToDate
@CategoryID
@SubCategoryID

Tables Used:
Expense

-----------------------------------------

12. sp_SearchExpense
Purpose:
- Search expenses using text

Parameters:
@UserID
@SearchText

Tables Used:
Expense

-----------------------------------------

13. sp_GetMonthlyExpenseSummary
Purpose:
- Monthly expense report

Parameters:
@UserID
@Month
@Year

Tables Used:
Expense

-----------------------------------------

14. sp_GetCategoryWiseExpenseReport
Purpose:
- Expense category graph report

Parameters:
@UserID
@FromDate
@ToDate

Tables Used:
Expense
Expense_Category

=========================================
💰 CREDIT MODULE
=========================================

15. sp_InsertCredit
Purpose:
- Add new credit transaction

Parameters:
@UserID
@CategoryID
@SubCategoryID
@Amount
@Description
@PaymentID
@CreditAt

Tables Used:
Credit

-----------------------------------------

16. sp_UpdateCredit
Purpose:
- Update credit information

Parameters:
@CreditID
@CategoryID
@SubCategoryID
@Amount
@Description
@PaymentID

Tables Used:
Credit

-----------------------------------------

17. sp_DeleteCredit
Purpose:
- Delete credit transaction

Parameters:
@CreditID

Tables Used:
Credit

-----------------------------------------

18. sp_GetCreditByID
Purpose:
- Get single credit details

Parameters:
@CreditID

Tables Used:
Credit

-----------------------------------------

19. sp_GetAllCredits
Purpose:
- Get all credit records

Parameters:
@UserID

Tables Used:
Credit

-----------------------------------------

20. sp_FilterCredit
Purpose:
- Filter credit records

Parameters:
@UserID
@FromDate
@ToDate
@CategoryID

Tables Used:
Credit

-----------------------------------------

21. sp_SearchCredit
Purpose:
- Search credit records

Parameters:
@UserID
@SearchText

Tables Used:
Credit

-----------------------------------------

22. sp_GetMonthlyCreditSummary
Purpose:
- Monthly credit report

Parameters:
@UserID
@Month
@Year

Tables Used:
Credit

-----------------------------------------

23. sp_GetCategoryWiseCreditReport
Purpose:
- Credit category graph report

Parameters:
@UserID
@FromDate
@ToDate

Tables Used:
Credit
Credit_Category

=========================================
🤝 LENT MODULE
=========================================

24. sp_InsertLent
Purpose:
- Add new lent transaction

Parameters:
@UserID
@PersonID
@PaymentID
@StatusID
@Amount
@DeadlineAt
@Description

Tables Used:
Lent

-----------------------------------------

25. sp_UpdateLent
Purpose:
- Update lent information

Parameters:
@LentID
@Amount
@DeadlineAt
@Description
@StatusID

Tables Used:
Lent

-----------------------------------------

26. sp_DeleteLent
Purpose:
- Delete lent transaction

Parameters:
@LentID

Tables Used:
Lent

-----------------------------------------

27. sp_GetAllLent
Purpose:
- Get all lent records

Parameters:
@UserID

Tables Used:
Lent

-----------------------------------------

28. sp_GetPendingLent
Purpose:
- Get pending lent payments

Parameters:
@UserID

Tables Used:
Lent

-----------------------------------------

29. sp_GetCompletedLent
Purpose:
- Get completed lent records

Parameters:
@UserID

Tables Used:
Lent

-----------------------------------------

30. sp_ReturnLent
Purpose:
- Mark lent as returned
- Automatically insert transaction into Credit table

Parameters:
@LentID
@Amount
@PaymentID

Tables Used:
Lent
Credit

-----------------------------------------

31. sp_GetLentPersonHistory
Purpose:
- Show all lent history of a person

Parameters:
@PersonID

Tables Used:
Lent
Person

=========================================
📥 BORROW MODULE
=========================================

32. sp_InsertBorrow
Purpose:
- Add new borrow transaction

Parameters:
@UserID
@PersonID
@PaymentID
@StatusID
@Amount
@DeadlineAt
@Description

Tables Used:
Borrow

-----------------------------------------

33. sp_UpdateBorrow
Purpose:
- Update borrow information

Parameters:
@BorrowID
@Amount
@DeadlineAt
@Description
@StatusID

Tables Used:
Borrow

-----------------------------------------

34. sp_DeleteBorrow
Purpose:
- Delete borrow transaction

Parameters:
@BorrowID

Tables Used:
Borrow

-----------------------------------------

35. sp_GetAllBorrow
Purpose:
- Get all borrow records

Parameters:
@UserID

Tables Used:
Borrow

-----------------------------------------

36. sp_GetPendingBorrow
Purpose:
- Get pending borrow payments

Parameters:
@UserID

Tables Used:
Borrow

-----------------------------------------

37. sp_GetCompletedBorrow
Purpose:
- Get completed borrow records

Parameters:
@UserID

Tables Used:
Borrow

-----------------------------------------

38. sp_PayBorrow
Purpose:
- Mark borrow as paid
- Automatically insert transaction into Expense table

Parameters:
@BorrowID
@Amount
@PaymentID

Tables Used:
Borrow
Expense

-----------------------------------------

39. sp_GetBorrowPersonHistory
Purpose:
- Show all borrow history of a person

Parameters:
@PersonID

Tables Used:
Borrow
Person

=========================================
✅ TASK MODULE
=========================================

40. sp_InsertTask
Purpose:
- Add new task

Parameters:
@UserID
@PriorityID
@StatusID
@TaskTitle
@Deadline

Tables Used:
Task

-----------------------------------------

41. sp_UpdateTask
Purpose:
- Update task information

Parameters:
@TaskID
@PriorityID
@StatusID
@TaskTitle
@Deadline

Tables Used:
Task

-----------------------------------------

42. sp_DeleteTask
Purpose:
- Delete task

Parameters:
@TaskID

Tables Used:
Task

-----------------------------------------

43. sp_GetAllTasks
Purpose:
- Get all user tasks

Parameters:
@UserID

Tables Used:
Task

-----------------------------------------

44. sp_GetPendingTasks
Purpose:
- Get pending tasks

Parameters:
@UserID

Tables Used:
Task

-----------------------------------------

45. sp_GetCompletedTasks
Purpose:
- Get completed tasks

Parameters:
@UserID

Tables Used:
Task

-----------------------------------------

46. sp_GetUpcomingTasks
Purpose:
- Get upcoming tasks by deadline

Parameters:
@UserID

Tables Used:
Task

-----------------------------------------

47. sp_SearchTasks
Purpose:
- Search tasks

Parameters:
@UserID
@SearchText

Tables Used:
Task

=========================================
📝 NOTE MODULE
=========================================

48. sp_InsertNote
Purpose:
- Add new note

Parameters:
@UserID
@StatusID
@NoteTitle
@Description

Tables Used:
Note

-----------------------------------------

49. sp_UpdateNote
Purpose:
- Update note information

Parameters:
@NoteID
@StatusID
@NoteTitle
@Description

Tables Used:
Note

-----------------------------------------

50. sp_DeleteNote
Purpose:
- Delete note

Parameters:
@NoteID

Tables Used:
Note

-----------------------------------------

51. sp_GetAllNotes
Purpose:
- Get all notes

Parameters:
@UserID

Tables Used:
Note

-----------------------------------------

52. sp_SearchNotes
Purpose:
- Search notes

Parameters:
@UserID
@SearchText

Tables Used:
Note

=========================================
⚙️ CATEGORY & SETTINGS MODULE
=========================================

53. sp_InsertExpenseCategory
Purpose:
- Add expense category

Parameters:
@CategoryName

Tables Used:
Expense_Category

-----------------------------------------

54. sp_DeleteExpenseCategory
Purpose:
- Delete expense category

Parameters:
@CategoryID

Tables Used:
Expense_Category

-----------------------------------------

55. sp_InsertExpenseSubCategory
Purpose:
- Add expense sub-category

Parameters:
@CategoryID
@SubCategoryName

Tables Used:
Expense_Sub_Category

-----------------------------------------

56. sp_DeleteExpenseSubCategory
Purpose:
- Delete expense sub-category

Parameters:
@SubCategoryID

Tables Used:
Expense_Sub_Category

-----------------------------------------

57. sp_InsertCreditCategory
Purpose:
- Add credit category

Parameters:
@CategoryName

Tables Used:
Credit_Category

-----------------------------------------

58. sp_DeleteCreditCategory
Purpose:
- Delete credit category

Parameters:
@CategoryID

Tables Used:
Credit_Category

-----------------------------------------

59. sp_InsertCreditSubCategory
Purpose:
- Add credit sub-category

Parameters:
@CategoryID
@SubCategoryName

Tables Used:
Credit_Sub_Category

-----------------------------------------

60. sp_DeleteCreditSubCategory
Purpose:
- Delete credit sub-category

Parameters:
@SubCategoryID

Tables Used:
Credit_Sub_Category

-----------------------------------------

61. sp_GetAllPaymentTypes
Purpose:
- Get all payment methods

Parameters:
None

Tables Used:
Payment_Type

=========================================
📊 DASHBOARD & REPORTS
=========================================

62. sp_GetDashboardSummary
Purpose:
- Get dashboard overview summary

Parameters:
@UserID

Tables Used:
Expense
Credit
Lent
Borrow
Task

-----------------------------------------

63. sp_GetIncomeVsExpense
Purpose:
- Compare income and expenses

Parameters:
@UserID
@FromDate
@ToDate

Tables Used:
Expense
Credit

-----------------------------------------

64. sp_GetFinancialSummary
Purpose:
- Generate complete financial report

Parameters:
@UserID
@FromDate
@ToDate

Tables Used:
Expense
Credit
Lent
Borrow

=========================================
🔔 REMINDER & NOTIFICATION QUERIES
=========================================

65. sp_GetUpcomingBorrowReminders
Purpose:
- Get borrow deadline reminders

Parameters:
@UserID

Tables Used:
Borrow

-----------------------------------------

66. sp_GetUpcomingLentReminders
Purpose:
- Get lent deadline reminders

Parameters:
@UserID

Tables Used:
Lent

-----------------------------------------

67. sp_GetUpcomingTaskReminders
Purpose:
- Get task deadline reminders

Parameters:
@UserID

Tables Used:
Task

=========================================
📌 FINAL TOTAL PROCEDURES
=========================================

Total Procedures:
67

Project Scope:
- Fully Covers SRS
- Fully Covers All Tables
- Covers CRUD
- Covers Reports
- Covers Filters
- Covers Dashboard
- Covers Notifications
- Covers Lent/Borrow Business Logic
- Suitable For WinForms + SQL Server Project