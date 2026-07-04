# Final Stored Procedure Requirements

## Personal Expense Credit Tracker

## Based On

- Final Database Schema
- Final SRS
- Actual WinForms Requirements

## SRS Reference

:contentReference[oaicite:0]{index=0}

## ðŸ“„ SRS Addendums (Updated)

### Category Management
The system will provide default categories and subcategories for all users. Users may create their own custom categories and subcategories. Default categories cannot be edited or deleted by normal users. A user can edit or deactivate only their own custom categories and subcategories. Deleting a category or subcategory will not remove historical transaction records; it will only hide the item from future selection.

### Optional Edit Features
Editing expense, credit, lent, and borrow records is optional for the first version. The first version will focus on adding records, filtering records, viewing reports, and completing lent/borrow payments through return/payment procedures.

### Notifications
The first version will include borrow return reminders, lent return reminders, and task deadline reminders. Monthly expense alerts, low balance alerts, and daily summary notifications are future enhancements.

## ðŸ” AUTHENTICATION & USER MANAGEMENT

### 1. spRegisterUser

**Purpose:**

- Register a new user account into the system.

**Parameters:**

- `@UserName`
- `@Email`
- `@PhoneNumber`
- `@Password`

**Expected Output:**

- New user account successfully created.

**When Used:**

- Register button click.

**Tables Used:**

- `Users`
- `User_Profile`
- `User_Contact`
- `User_Authentication`

### 2. spLoginUser

**Purpose:**

- Verify user login credentials.

**Parameters:**

- `@Email`
- `@Password`

**Expected Output:**

- User authentication result.

**When Used:**

- Login button click.

**Tables Used:**

- `User_Contact`
- `User_Authentication`

### 3. spChangePassword

**Purpose:**

- Change existing user password.

**Parameters:**

- `@UserID`
- `@OldPassword`
- `@NewPassword`

**Expected Output:**

- Password updated successfully.

**When Used:**

- Change password option.

**Tables Used:**

- `User_Authentication`

### 4. spUpdateUserProfile

**Purpose:**

- Update all profile information together.

**Parameters:**

- `@UserID`
- `@Name`
- `@Email`
- `@PhoneNumber`
- `@ProfilePhoto`

**Expected Output:**

- Complete profile updated successfully.

**When Used:**

- Save profile changes button.

**Tables Used:**

- `User_Profile`
- `User_Contact`

### 5. spUpdateUserName

**Purpose:**

- Update only user name.

**Parameters:**

- `@UserID`
- `@Name`

**Expected Output:**

- User name updated successfully.

**When Used:**

- Edit name option.

**Tables Used:**

- `User_Profile`

### 6. spUpdateUserEmail

**Purpose:**

- Update only user email address.

**Parameters:**

- `@UserID`
- `@Email`

**Expected Output:**

- Email updated successfully.

**When Used:**

- Edit email option.

**Tables Used:**

- `User_Contact`

### 7. spUpdateUserPhoneNumber

**Purpose:**

- Update only user phone number.

**Parameters:**

- `@UserID`
- `@PhoneNumber`

**Expected Output:**

- Phone number updated successfully.

**When Used:**

- Edit phone number option.

**Tables Used:**

- `User_Contact`

### 8. spUpdateProfilePhoto

**Purpose:**

- Update only profile photo.

**Parameters:**

- `@UserID`
- `@ProfilePhoto`

**Expected Output:**

- Profile photo updated successfully.

**When Used:**

- Change profile photo option.

**Tables Used:**

- `User_Profile`

### 9. spDeleteUserProfilePhotoByUserId

**Purpose:**

- Remove existing profile photo of a user.

**Parameters:**

- `@UserID`

**Expected Output:**

- Profile photo removed successfully.

**When Used:**

- Remove profile photo option.

**Tables Used:**

- `User_Profile`



### 11. spForgetPassword

**Purpose:**

- Reset user password when the user is not logged in by verifying registered email and phone number.

**Parameters:**

- `@Email`
- `@PhoneNumber`
- `@NewPassword`

**Expected Output:**

- Password reset successfully after verification.

**When Used:**

- Forgot password option on login page.

**Tables Used:**

- `User_Contact`
- `User_Authentication`

### 12. spGetActiveUserDetails

**Purpose:**

- Check whether the user account is active or inactive during login authentication.

**Parameters:**

- `@Email`
- `@Password`

**Expected Output:**

- Returns Active status of the user.
- If Active = 1 â†’ Allow login.
- If Active = 0 â†’ Block login.

**When Used:**

- During user login verification process.

**Tables Used:**

- `tblUserAuthentication`
- `tblUserContact`

**Logic:**

- Verify email and password.
- Check Active column value.
- Return authentication result with account status.

### 12A. spLogoutUser

**Purpose:**

- Terminate the user session and clear active state if applicable.

**Parameters:**

- `@UserID` (or none if handled via local session)

**Expected Output:**

- User logged out successfully.

**When Used:**

- User clicks the Logout button.

**Tables Used:**

- `tblUserAuthentication`

## ðŸ’¸ EXPENSE MODULE

### 13. spInsertExpenseByUserID

**Purpose:**

- Add new expense transaction.

**Parameters:**

- `@UserID`
- `@CategoryID`
- `@SubCategoryID`
- `@Amount`
- `@Description`
- `@PaymentID`
- `@ExpenseAt`

**Expected Output:**

- Expense inserted successfully.

**When Used:**

- Add expense form submit button.

**Tables Used:**

- `Expense`

### 14. spGetAllExpensesByID

**Purpose:**

- Get all expense records of user.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete expense list.

**When Used:**

- Expense page load.

**Tables Used:**

- `Expense`

### 15. spFilterExpenseByCategory

**Purpose:**

- Filter expense records using category.

**Parameters:**

- `@UserID`
- `@CategoryID`

**Expected Output:**

- Expense records of selected category.

**When Used:**

- Expense category filter option.

**Tables Used:**

- `Expense`
- `Expense_Category`

### 16. spFilterExpenseByCategoryAndSubCategory

**Purpose:**

- Filter expense records using category and sub-category.

**Parameters:**

- `@UserID`
- `@CategoryID`
- `@SubCategoryID`

**Expected Output:**

- Expense records of selected category and sub-category.

**When Used:**

- Expense category and sub-category filter option.

**Tables Used:**

- `Expense`
- `Expense_Category`
- `Expense_Sub_Category`

### 17. spFilterExpenseByDateRange

**Purpose:**

- Filter expense records between selected dates.

**Parameters:**

- `@UserID`
- `@FromDate`
- `@ToDate`

**Expected Output:**

- Expense records between selected dates.

**When Used:**

- Expense date filter option.

**Tables Used:**

- `Expense`

### 18. spGetMonthlyExpenseSummary

**Purpose:**

- Generate monthly expense summary report.

**Parameters:**

- `@UserID`
- `@Month`
- `@Year`

**Expected Output:**

- Total monthly expenses.

**When Used:**

- Dashboard monthly summary section.

**Tables Used:**

- `Expense`

### 19. spGetCategoryWiseExpenseReport

**Purpose:**

- Generate category wise expense report for graphs and charts.

**Parameters:**

- `@UserID`
- `@FromDate`
- `@ToDate`

**Expected Output:**

- Expense totals grouped by category.

**When Used:**

- Dashboard pie chart and reports.

**Tables Used:**

- `Expense`
- `Expense_Category`

### 20. spGetTodayExpense

**Purpose:**

- Get today's expense records.

**Parameters:**

- `@UserID`

**Expected Output:**

- Today's expense list.

**When Used:**

- Dashboard daily summary section.

**Tables Used:**

- `Expense`

### 20A. spFilterExpenseByAmountRange

**Purpose:**

- Filter expense records by amount range (min to max).

**Parameters:**

- `@UserID`
- `@MinAmount`
- `@MaxAmount`

**Expected Output:**

- Expense records within specified amount range.

**When Used:**

- Expense amount filter option.

**Tables Used:**

- `Expense`
- `ExpenseCategory`
- `ExpenseSubCategory`
- `PaymentType`

## ðŸ’° CREDIT MODULE

### 21. spInsertCreditByUserID

**Purpose:**

- Add new credit transaction.

**Parameters:**

- `@UserID`
- `@CategoryID`
- `@SubCategoryID`
- `@Amount`
- `@Description`
- `@PaymentID`
- `@CreditAt`

**Expected Output:**

- Credit inserted successfully.

**When Used:**

- Add credit form submit button.

**Tables Used:**

- `Credit`

### 22. spGetAllCreditsByID

**Purpose:**

- Get all credit records of user.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete credit records list.

**When Used:**

- Credit page load.

**Tables Used:**

- `Credit`

### 23. spFilterCreditByCategory

**Purpose:**

- Filter credit records using category.

**Parameters:**

- `@UserID`
- `@CategoryID`

**Expected Output:**

- Credit records of selected category.

**When Used:**

- Credit category filter option.

**Tables Used:**

- `Credit`
- `Credit_Category`

### 24. spFilterCreditByCategoryAndSubCategory

**Purpose:**

- Filter credit records using category and sub-category.

**Parameters:**

- `@UserID`
- `@CategoryID`
- `@SubCategoryID`

**Expected Output:**

- Credit records of selected category and sub-category.

**When Used:**

- Credit category and sub-category filter option.

**Tables Used:**

- `Credit`
- `Credit_Category`
- `Credit_Sub_Category`

### 25. spFilterCreditByDateRange

**Purpose:**

- Filter credit records between selected dates.

**Parameters:**

- `@UserID`
- `@FromDate`
- `@ToDate`

**Expected Output:**

- Credit records between selected dates.

**When Used:**

- Credit date filter option.

**Tables Used:**

- `Credit`

### 26. spGetMonthlyCreditSummary

**Purpose:**

- Generate monthly credit summary report.

**Parameters:**

- `@UserID`
- `@Month`
- `@Year`

**Expected Output:**

- Total monthly credits.

**When Used:**

- Dashboard monthly summary section.

**Tables Used:**

- `Credit`

### 27. spGetCategoryWiseCreditReport

**Purpose:**

- Generate category wise credit report for graphs and charts.

**Parameters:**

- `@UserID`
- `@FromDate`
- `@ToDate`

**Expected Output:**

- Credit totals grouped by category.

**When Used:**

- Dashboard pie chart and reports.

**Tables Used:**

- `Credit`
- `Credit_Category`

### 28. spGetTodayCredit

**Purpose:**

- Get today's credit records.

**Parameters:**

- `@UserID`

**Expected Output:**

- Today's credit list.

**When Used:**

- Dashboard daily summary section.

**Tables Used:**

- `Credit`

### 28A. spFilterCreditByAmountRange

**Purpose:**

- Filter credit records by amount range (min to max).

**Parameters:**

- `@UserID`
- `@MinAmount`
- `@MaxAmount`

**Expected Output:**

- Credit records within specified amount range.

**When Used:**

- Credit amount filter option.

**Tables Used:**

- `Credit`
- `CreditCategory`
- `CreditSubCategory`
- `PaymentType`

## ðŸ¤ LENT MODULE

### 29. spInsertLent

**Purpose:**

- Add new lent transaction.

**Parameters:**

- `@UserID`
- `@PersonID`
- `@PaymentID`
- `@StatusID`
- `@Amount`
- `@DeadlineAt`
- `@Description`

**Expected Output:**

- New lent record inserted successfully.

**When Used:**

- Add lent form submit button.

**Tables Used:**

- `Lent`

### 30. spGetAllLent

**Purpose:**

- Get all lent records of user.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete lent records list.

**When Used:**

- Lent page load.

**Tables Used:**

- `Lent`

### 31. spGetPendingLentByStatusName

**Purpose:**

- Get all pending lent transactions.

**Parameters:**

- `@UserID`

**Expected Output:**

- Pending lent records list.

**When Used:**

- Pending lent section.
- Dashboard reminder section.

**Tables Used:**

- `Lent`

### 32. SpGetCompletedLentByStatusName

**Purpose:**

- Get all completed lent transactions.

**Parameters:**

- `@UserID`

**Expected Output:**

- Completed lent records list.

**When Used:**

- Lent history section.

**Tables Used:**

- `Lent`

### 33. spReturnLentByReturnAmount

**Purpose:**

- Return full or partial lent amount.
- Automatically insert returned amount into Credit table.
- Automatically update lent remaining amount.
- Automatically update lent status.

**Parameters:**

- `@LentID`
- `@ReturnAmount`
- `@PaymentID`

**Expected Output:**

**Scenario 1:**

- If full amount returned:
  - Lent status updated to Completed.
  - Remaining amount becomes 0.
  - Credit transaction inserted.

**Scenario 2:**

- If partial amount returned:
  - Remaining amount updated in Lent table.
  - Lent status remains Pending.
  - Partial amount inserted into Credit table.

**When Used:**

- Return money button.
- Receive payment option.

**Tables Used:**

- `Lent`
- `Credit`

### 34. spGetLentPersonHistory

**Purpose:**

- Get complete lent history of a specific person.

**Parameters:**

- `@PersonID`

**Expected Output:**

- All lent transactions of selected person.

**When Used:**

- Person details/history page.

**Tables Used:**

- `Lent`
- `Person`

### 35. spGetAllPersons

**Purpose:**

- Get all persons for lent and borrow transactions.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete persons list.

**When Used:**

- Person dropdown.
- Person management page.
- Person selection section.

**Tables Used:**

- `tblPersons`

## ðŸ“¥ BORROW MODULE

### 36. spInsertBorrow

**Purpose:**

- Add new borrow transaction.
- Automatically insert borrowed amount into Credit table.

**Parameters:**

- `@UserID`
- `@PersonID`
- `@PaymentID`
- `@StatusID`
- `@Amount`
- `@DeadlineAt`
- `@Description`

**Expected Output:**

- Borrow transaction inserted successfully.
- Credit transaction inserted successfully.
- Remaining amount initialized.

**When Used:**

- Add borrow form submit button.

**Tables Used:**

- `Borrow`
- `Credit`

### 37. spGetAllBorrow

**Purpose:**

- Get all borrow records of user.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete borrow records list.

**When Used:**

- Borrow page load.

**Tables Used:**

- `Borrow`

### 38. spGetPendingBorrow

**Purpose:**

- Get all pending borrow transactions.

**Parameters:**

- `@UserID`

**Expected Output:**

- Pending borrow records list.

**When Used:**

- Pending borrow section.
- Dashboard reminder section.

**Tables Used:**

- `Borrow`

### 39. spGetCompletedBorrow

**Purpose:**

- Get all completed borrow transactions.

**Parameters:**

- `@UserID`

**Expected Output:**

- Completed borrow records list.

**When Used:**

- Borrow history section.

**Tables Used:**

- `Borrow`

### 40. spPayBorrow

**Purpose:**

- Pay full or partial borrow amount.
- Automatically insert paid amount into Expense table.
- Automatically update borrow remaining amount.
- Automatically update borrow status.

**Parameters:**

- `@BorrowID`
- `@PaidAmount`
- `@PaymentID`

**Expected Output:**

**Scenario 1:**

- If full amount paid:
  - Borrow status updated to Completed.
  - Remaining amount becomes 0.
  - Expense transaction inserted.

**Scenario 2:**

- If partial amount paid:
  - Remaining amount updated in Borrow table.
  - Borrow status remains Pending.
  - Partial amount inserted into Expense table.

**When Used:**

- Pay money button.
- Borrow repayment option.

**Tables Used:**

- `Borrow`
- `Expense`

### 41. spGetBorrowPersonHistory

**Purpose:**

- Get complete borrow history of a specific person.

**Parameters:**

- `@PersonID`

**Expected Output:**

- All borrow transactions of selected person.

**When Used:**

- Person details/history page.

**Tables Used:**

- `Borrow`
- `Person`



### 42. spGetTotalBorrowByPerson

**Purpose:**

- Calculate total borrow amount of a specific person.

**Parameters:**

- `@PersonID`

**Expected Output:**

- Total borrow amount of selected person.
- Total paid amount.
- Remaining pending amount.

**When Used:**

- Person financial summary section.

**Tables Used:**

- `Borrow`
- `Person`

### 42A. spUpdateOverdueStatus

**Purpose:**

- Automatically update borrow records that have passed their deadline to 'Overdue' status.

**Parameters:**

- None

**Expected Output:**

- Overdue borrow statuses updated successfully.

**When Used:**

- Application load or dashboard to ensure overdue statuses are correctly reflected.

**Tables Used:**

- `tblBorrow`
- `tblLentBorrowStatus`

### 42B. spGetOverduedBorrow

**Purpose:**

- Retrieve all overdue borrow records for a specific user.

**Parameters:**

- `@UserID`

**Expected Output:**

- List of overdue borrow records.

**When Used:**

- Overdue section or notification panel load.

**Tables Used:**

- `tblBorrow`
- `tblPersons`
- `tblPaymentType`
- `tblLentBorrowStatus`

## ✅ TASK MODULE

### 43. spInsertTask

**Purpose:**

- Add new task.

**Parameters:**

- `@UserID`
- `@PriorityID`
- `@TaskStatusID`
- `@TaskTitle`
- `@Deadline`
- `@CreatedAt`

**Expected Output:**

- New task inserted successfully.

**When Used:**

- Add task form submit button.

**Tables Used:**

- `Task`

### 44. spUpdateTask

**Purpose:**

- Update task information.

**Parameters:**

- `@TaskID`
- `@PriorityID`
- `@TaskStatusID`
- `@TaskTitle`
- `@Deadline`

**Expected Output:**

- Task updated successfully.

**When Used:**

- Edit task option.

**Tables Used:**

- `Task`

### 45. spUpdateTaskStatus

**Purpose:**

- Update only task status.

**Parameters:**

- `@TaskID`
- `@TaskStatusID`

**Expected Output:**

- Task status updated successfully.

**When Used:**

- Mark as completed button.
- Change task status option.

**Tables Used:**

- `Task`

### 46. spDeleteTask

**Purpose:**

- Delete task.

**Parameters:**

- `@TaskID`

**Expected Output:**

- Task deleted successfully.

**When Used:**

- Delete task button.

**Tables Used:**

- `Task`

### 47. spGetAllTasks

**Purpose:**

- Get all user tasks.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete task list.

**When Used:**

- Task page load.

**Business Logic Note:**

- Total task count will be calculated in Business Logic Layer.

**Tables Used:**

- `Task`

### 48. spGetPendingTasks

**Purpose:**

- Get pending tasks.

**Parameters:**

- `@UserID`

**Expected Output:**

- Pending task records.

**When Used:**

- Pending task section.
- Dashboard summary.

**Tables Used:**

- `Task`

### 49. spGetCompletedTasks

**Purpose:**

- Get completed tasks.

**Parameters:**

- `@UserID`

**Expected Output:**

- Completed task records.

**When Used:**

- Completed task section.

**Tables Used:**

- `Task`

### 50. spGetTasksBetweenDates

**Purpose:**

- Get tasks between selected date range.

**Parameters:**

- `@UserID`
- `@FromDate`
- `@ToDate`

**Expected Output:**

- Tasks between selected dates.

**When Used:**

- Task date filter option.
- Task report section.

**Tables Used:**

- `Task`

### 51. spFilterTasksByStatus

**Purpose:**

- Filter tasks based on task status.

**Parameters:**

- `@UserID`
- `@TaskStatusID`

**Expected Output:**

- Tasks of selected status.

**Example:**

- Pending tasks
- Completed tasks
- In Progress tasks

**When Used:**

- Task status filter dropdown.
- Completed task section.
- Pending task section.

**Tables Used:**

- `Task`
- `Task_Status`

## ðŸ“ NOTE MODULE

### 52. spInsertNote

**Purpose:**

- Add new note.

**Parameters:**

- `@UserID`
- `@PriorityID`
- `@NoteTitle`
- `@Description`
- `@CreatedAt`

**Expected Output:**

- New note inserted successfully.

**When Used:**

- Add note form submit button.

**Tables Used:**

- `Note`

### 53. spUpdateNote

**Purpose:**

- Update note information.

**Parameters:**

- `@NoteID`
- `@PriorityID`
- `@NoteTitle`
- `@Description`

**Expected Output:**

- Note updated successfully.

**When Used:**

- Edit note option.

**Tables Used:**

- `Note`

### 54. spUpdateNotePriority

**Purpose:**

- Update only note priority.

**Parameters:**

- `@NoteID`
- `@PriorityID`

**Expected Output:**

- Note priority updated successfully.

**When Used:**

- Change note priority option.

**Tables Used:**

- `Note`

### 55. spDeleteNote

**Purpose:**

- Delete note.

**Parameters:**

- `@NoteID`

**Expected Output:**

- Note deleted successfully.

**When Used:**

- Delete note option.

**Tables Used:**

- `Note`

### 56. spGetAllNotes

**Purpose:**

- Get all notes of user.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete notes list.

**When Used:**

- Notes page load.

**Tables Used:**

- `Note`

### 57. spFilterNotesByPriority

**Purpose:**

- Filter notes based on priority.

**Parameters:**

- `@UserID`
- `@PriorityID`

**Expected Output:**

- Notes of selected priority.

**Example:**

- High priority notes
- Medium priority notes
- Low priority notes

**When Used:**

- Note priority filter dropdown.

**Tables Used:**

- `Note`
- `Note_Priority`

### 58. spGetNotesBetweenDates

**Purpose:**

- Get notes between selected dates.

**Parameters:**

- `@UserID`
- `@FromDate`
- `@ToDate`

**Expected Output:**

- Notes between selected dates.

**When Used:**

- Notes date filter option.
- Notes report section.

**Tables Used:**

- `Note`

## âš™ï¸ CATEGORY & SETTINGS MODULE

### 59. spInsertNewExpenseCategoryByUserID

**Purpose:**

- Add new expense category for user (user-specific custom categories).

**Parameters:**

- `@UserID`
- `@CategoryName`

**Expected Output:**

- Expense category inserted successfully with UserID, IsDefault=0, IsActive=1.

**When Used:**

- Add expense category option.

**Tables Used:**

- `tblExpenseCategory`

**Business Logic:**

- Validate user active status
- Check duplicate category name within same user only
- Insert with UserID=@UserID, IsDefault=0, IsActive=1

### 60. spUpdateExpenseCategoryByUserID

**Purpose:**

- Update expense category name (only for user's custom categories, not default).

**Parameters:**

- `@UserID`
- `@CategoryID`
- `@CategoryName`

**Expected Output:**

- Expense category updated successfully.

**When Used:**

- Edit expense category option.

**Tables Used:**

- `tblExpenseCategory`

**Business Logic:**

- Validate category belongs to user and IsDefault=0
- Prevent update of default categories
- Check duplicate names within user

### 61. spDeleteExpenseCategoryByUserID

**Purpose:**

- Soft delete expense category (set IsActive=0 to preserve transaction history).

**Parameters:**

- `@UserID`
- `@CategoryID`

**Expected Output:**

- Expense category deleted successfully (soft delete).

**When Used:**

- Delete expense category option.

**Tables Used:**

- `tblExpenseCategory`

**Business Logic:**

- Validate category belongs to user and IsDefault=0
- Soft delete: SET IsActive=0 (not physical delete)
- Preserves linked transaction history

### 62. spGetExpenseCategoriesByUserID

**Purpose:**

- Get all active expense categories for dropdown (default + user's custom categories).

**Parameters:**

- `@UserID`

**Expected Output:**

- List of active categories where UserID IS NULL (default) or UserID=@UserID.

**When Used:**

- Category dropdown on expense form.

**Tables Used:**

- `tblExpenseCategory`

**Business Logic:**

- Return WHERE IsActive=1 AND (UserID IS NULL OR UserID=@UserID)
- Order by IsDefault DESC, CategoryName ASC

### 63. spInsertNewExpenseSubCategoryByUserID

**Purpose:**

- Add new expense sub-category for user.

**Parameters:**

- `@UserID`
- `@CategoryID`
- `@SubCategoryName`

**Expected Output:**

- Expense sub-category inserted successfully with UserID, IsDefault=0, IsActive=1.

**When Used:**

- Add expense sub-category option.

**Tables Used:**

- `tblExpenseSubCategory`

**Business Logic:**

- Validate category exists and is active
- Check duplicate subcategory name within user and category
- Insert with UserID=@UserID, IsDefault=0, IsActive=1

### 64. spUpdateExpenseSubCategoryByUserID

**Purpose:**

- Update expense sub-category name (only for user's custom subcategories).

**Parameters:**

- `@UserID`
- `@SubCategoryID`
- `@SubCategoryName`

**Expected Output:**

- Expense sub-category updated successfully.

**When Used:**

- Edit expense sub-category option.

**Tables Used:**

- `tblExpenseSubCategory`

### 65. spDeleteExpenseSubCategoryByUserID

**Purpose:**

- Soft delete expense sub-category (set IsActive=0).

**Parameters:**

- `@UserID`
- `@SubCategoryID`

**Expected Output:**

- Expense sub-category deleted successfully (soft delete).

**When Used:**

- Delete expense sub-category option.

**Tables Used:**

- `tblExpenseSubCategory`

### 66. spGetExpenseSubCategoriesByUserID

**Purpose:**

- Get all active expense sub-categories for dropdown (default + user's custom).

**Parameters:**

- `@UserID`

**Expected Output:**

- List of active subcategories where UserID IS NULL or UserID=@UserID.

**When Used:**

- Sub-category dropdown on expense form.

**Tables Used:**

- `tblExpenseSubCategory`

### 67. spInsertNewCreditCategoryByUserID

**Purpose:**

- Add new credit category for user (user-specific custom categories).

**Parameters:**

- `@UserID`
- `@CategoryName`

**Expected Output:**

- Credit category inserted successfully with UserID, IsDefault=0, IsActive=1.

**When Used:**

- Add credit category option.

**Tables Used:**

- `tblCreditCategory`

### 68. spUpdateCreditCategoryByUserID

**Purpose:**

- Update credit category name (only for user's custom categories).

**Parameters:**

- `@UserID`
- `@CategoryID`
- `@CategoryName`

**Expected Output:**

- Credit category updated successfully.

**When Used:**

- Edit credit category option.

**Tables Used:**

- `tblCreditCategory`

### 69. spDeleteCreditCategoryByUserID

**Purpose:**

- Soft delete credit category (set IsActive=0).

**Parameters:**

- `@UserID`
- `@CategoryID`

**Expected Output:**

- Credit category deleted successfully (soft delete).

**When Used:**

- Delete credit category option.

**Tables Used:**

- `tblCreditCategory`

### 70. spGetCreditCategoriesByUserID

**Purpose:**

- Get all active credit categories for dropdown (default + user's custom categories).

**Parameters:**

- `@UserID`

**Expected Output:**

- List of active categories where UserID IS NULL or UserID=@UserID.

**When Used:**

- Category dropdown on credit form.

**Tables Used:**

- `tblCreditCategory`

### 71. spInsertNewCreditSubCategoryByUserID

**Purpose:**

- Add new credit sub-category for user.

**Parameters:**

- `@UserID`
- `@CategoryID`
- `@SubCategoryName`

**Expected Output:**

- Credit sub-category inserted successfully with UserID, IsDefault=0, IsActive=1.

**When Used:**

- Add credit sub-category option.

**Tables Used:**

- `tblCreditSubCategory`

### 72. spUpdateCreditSubCategoryByUserID

**Purpose:**

- Update credit sub-category name (only for user's custom subcategories).

**Parameters:**

- `@UserID`
- `@SubCategoryID`
- `@SubCategoryName`

**Expected Output:**

- Credit sub-category updated successfully.

**When Used:**

- Edit credit sub-category option.

**Tables Used:**

- `tblCreditSubCategory`

### 73. spDeleteCreditSubCategoryByUserID

**Purpose:**

- Soft delete credit sub-category (set IsActive=0).

**Parameters:**

- `@UserID`
- `@SubCategoryID`

**Expected Output:**

- Credit sub-category deleted successfully (soft delete).

**When Used:**

- Delete credit sub-category option.

**Tables Used:**

- `tblCreditSubCategory`

### 74. spGetCreditSubCategoriesByUserID

**Purpose:**

- Get all active credit sub-categories for dropdown (default + user's custom).

**Parameters:**

- `@UserID`

**Expected Output:**

- List of active subcategories where UserID IS NULL or UserID=@UserID.

**When Used:**

- Sub-category dropdown on credit form.

**Tables Used:**

- `tblCreditSubCategory`

### 75. spGetAllPaymentTypes

**Purpose:**

- Get all payment methods.

**Parameters:**

- None

**Expected Output:**

- Complete payment methods list.

**When Used:**

- Expense, Credit, Lent and Borrow forms.

**Tables Used:**

- `tblPaymentType`

### 76. spInsertPerson

**Purpose:**

- Add new person for lent and borrow transactions.

**Parameters:**

- `@UserID`
- `@PersonName`
- `@PhoneNumber`
- `@Address`

**Expected Output:**

- Person inserted successfully.

**When Used:**

- Add person option in settings.

**Tables Used:**

- `tblPersons`

### 77. spUpdatePerson

**Purpose:**

- Update person details.

**Parameters:**

- `@PersonID`
- `@UserID`
- `@PersonName`
- `@PhoneNumber`
- `@Address`

**Expected Output:**

- Person details updated successfully.

**When Used:**

- Edit person option in settings.

**Tables Used:**

- `tblPersons`

### 79. spGetAllPersons

**Purpose:**

- Get all saved persons with phone numbers and address details.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete persons list.

**When Used:**

- Lent/Borrow person management section.
- Person dropdown selection.

**Tables Used:**

- `tblPersons`

## 📊 DASHBOARD & NOTIFICATION MODULE

### 10. spGetUserDashboard

**Purpose:**

- Load dashboard summary data for logged user.

**Parameters:**

- `@UserID`

**Expected Output:**

- Total expenses
- Total credits
- Total lent amount
- Total borrow amount
- Net balance
- Pending task summary

**When Used:**

- Dashboard page load.

**Tables Used:**

- `Expense`
- `Credit`
- `Lent`
- `Borrow`
- `Task`

### 80. spGetUpcomingBorrowReminders

**Purpose:**

- Get borrow deadline reminders

**Parameters:**

- `@UserID`

**Tables Used:**

- `tblBorrow`

### 81. spGetUpcomingLentReminders

**Purpose:**

- Get lent deadline reminders

**Parameters:**

- `@UserID`

**Tables Used:**

- `tblLent`

### 82. spGetUpcomingTaskReminders

**Purpose:**

- Get task deadline reminders

**Parameters:**

- `@UserID`

**Tables Used:**

- `tblTask`

## 🏁 FINAL TOTAL PROCEDURES

**Total Procedures:**

- 82

**Recent Additions (10 new procedures):**

- 20A. spFilterExpenseByAmountRange
- 28A. spFilterCreditByAmountRange
- 62-77: Updated Category Management (16 procedures with UserID, soft delete, multi-user support)
  - 62-65: Expense Category (Insert, Update, Delete, Get)
  - 66-69: Expense SubCategory (Insert, Update, Delete, Get)
  - 70-73: Credit Category (Insert, Update, Delete, Get)
  - 74-77: Credit SubCategory (Insert, Update, Delete, Get)
- 78-82: Settings & Persons (5 procedures)
- 83-85: Reminders (3 procedures)

**Project Scope:**

- Fully Covers SRS
- Fully Covers All Tables
- Covers CRUD
- Covers Reports
- Covers Filters (including new Amount Range filters)
- Covers Dashboard
- Covers Notifications
- Covers Lent/Borrow Business Logic
- Covers Multi-User Category Ownership & Soft Delete
- Suitable For WinForms + SQL Server Project


