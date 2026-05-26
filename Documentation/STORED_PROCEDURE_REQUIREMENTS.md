# Final Stored Procedure Requirements

## Personal Expense Credit Tracker

## Based On

- Final Database Schema
- Final SRS
- Actual WinForms Requirements

## SRS Reference

:contentReference[oaicite:0]{index=0}

## 🔐 AUTHENTICATION & USER MANAGEMENT

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

### 8. spUpdateUserProfilePhoto

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

### 9. spRemoveProfilePhoto

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

### 11. spForgotPassword

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

### 12. spCheckUserActiveStatus

**Purpose:**

- Check whether the user account is active or inactive during login authentication.

**Parameters:**

- `@Email`
- `@Password`

**Expected Output:**

- Returns Active status of the user.
- If Active = 1 → Allow login.
- If Active = 0 → Block login.

**When Used:**

- During user login verification process.

**Tables Used:**

- `tblUserAuthentication`
- `tblUserContact`

**Logic:**

- Verify email and password.
- Check Active column value.
- Return authentication result with account status.

## 💸 EXPENSE MODULE

### 13. spInsertExpense

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

### 14. spGetAllExpenses

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

### 18. spGetMonthlyExpenseSummary (UI)

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

### 19. spGetCategoryWiseExpenseReport (UI)

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

## 💰 CREDIT MODULE

### 21. spInsertCredit

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

### 22. spGetAllCredits

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

## 🤝 LENT MODULE

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

### 31. spGetPendingLent

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

### 32. spGetCompletedLent

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

### 35. spGetAllLentPersons

**Purpose:**

- Get all persons involved in lent transactions.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete lent persons list.

**When Used:**

- Lent person dropdown.
- Lent person management page.
- Person selection section.

**Tables Used:**

- `Lent`
- `Person`

## 📥 BORROW MODULE

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

### 42. spGetAllBorrowPersons

**Purpose:**

- Get all persons involved in borrow transactions.

**Parameters:**

- `@UserID`

**Expected Output:**

- Complete borrow persons list.

**When Used:**

- Borrow person dropdown.
- Borrow person management page.
- Person selection section.

**Tables Used:**

- `Borrow`
- `Person`

### 43. spGetTotalBorrowByPerson

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

## ✅ TASK MODULE

### 44. spInsertTask

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

### 45. spUpdateTask

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

### 46. spUpdateTaskStatus

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

### 47. spDeleteTask

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

### 48. spGetAllTasks

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

### 49. spGetPendingTasks

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

### 50. spGetCompletedTasks

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

### 51. spGetTasksByDate

**Purpose:**

- Get tasks of a specific date.

**Parameters:**

- `@UserID`
- `@TaskDate`

**Expected Output:**

- Tasks of selected date.

**When Used:**

- Calendar/date wise task section.

**Tables Used:**

- `Task`

### 52. spGetTasksBetweenDates

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

### 53. spFilterTasksByStatus

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

## 📝 NOTE MODULE

### 54. spInsertNote

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

### 55. spUpdateNote

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

### 56. spUpdateNotePriority

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

### 57. spDeleteNote

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

### 58. spGetAllNotes

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

### 59. spFilterNotesByPriority

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

### 60. spGetNotesByDate

**Purpose:**

- Get notes of a specific date.

**Parameters:**

- `@UserID`
- `@NoteDate`

**Expected Output:**

- Notes of selected date.

**When Used:**

- Calendar/date wise note section.

**Tables Used:**

- `Note`

### 61. spGetNotesBetweenDates

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

## ⚙️ CATEGORY & SETTINGS MODULE (WE WILL Re-Search Later)

### 62. spInsertExpenseCategory

**Purpose:**

- Add new expense category.

**Parameters:**

- `@CategoryName`

**Expected Output:**

- Expense category inserted successfully.

**When Used:**

- Add expense category option.

**Tables Used:**

- `Expense_Category`

### 63. spUpdateExpenseCategory

**Purpose:**

- Update expense category name.

**Parameters:**

- `@CategoryID`
- `@CategoryName`

**Expected Output:**

- Expense category updated successfully.

**When Used:**

- Edit expense category option.

**Tables Used:**

- `Expense_Category`

### 64. spDeleteExpenseCategory

**Purpose:**

- Delete expense category.

**Parameters:**

- `@CategoryID`

**Expected Output:**

- Expense category deleted successfully.

**When Used:**

- Delete expense category option.

**Tables Used:**

- `Expense_Category`

### 65. spInsertExpenseSubCategory

**Purpose:**

- Add new expense sub-category.

**Parameters:**

- `@CategoryID`
- `@SubCategoryName`

**Expected Output:**

- Expense sub-category inserted successfully.

**When Used:**

- Add expense sub-category option.

**Tables Used:**

- `Expense_Sub_Category`

### 66. spUpdateExpenseSubCategory

**Purpose:**

- Update expense sub-category name.

**Parameters:**

- `@SubCategoryID`
- `@SubCategoryName`

**Expected Output:**

- Expense sub-category updated successfully.

**When Used:**

- Edit expense sub-category option.

**Tables Used:**

- `Expense_Sub_Category`

### 67. spDeleteExpenseSubCategory

**Purpose:**

- Delete expense sub-category.

**Parameters:**

- `@SubCategoryID`

**Expected Output:**

- Expense sub-category deleted successfully.

**When Used:**

- Delete expense sub-category option.

**Tables Used:**

- `Expense_Sub_Category`

### 68. spInsertCreditCategory

**Purpose:**

- Add new credit category.

**Parameters:**

- `@CategoryName`

**Expected Output:**

- Credit category inserted successfully.

**When Used:**

- Add credit category option.

**Tables Used:**

- `Credit_Category`

### 69. spUpdateCreditCategory

**Purpose:**

- Update credit category name.

**Parameters:**

- `@CategoryID`
- `@CategoryName`

**Expected Output:**

- Credit category updated successfully.

**When Used:**

- Edit credit category option.

**Tables Used:**

- `Credit_Category`

### 70. spDeleteCreditCategory

**Purpose:**

- Delete credit category.

**Parameters:**

- `@CategoryID`

**Expected Output:**

- Credit category deleted successfully.

**When Used:**

- Delete credit category option.

**Tables Used:**

- `Credit_Category`

### 71. spInsertCreditSubCategory

**Purpose:**

- Add new credit sub-category.

**Parameters:**

- `@CategoryID`
- `@SubCategoryName`

**Expected Output:**

- Credit sub-category inserted successfully.

**When Used:**

- Add credit sub-category option.

**Tables Used:**

- `Credit_Sub_Category`

### 72. spUpdateCreditSubCategory

**Purpose:**

- Update credit sub-category name.

**Parameters:**

- `@SubCategoryID`
- `@SubCategoryName`

**Expected Output:**

- Credit sub-category updated successfully.

**When Used:**

- Edit credit sub-category option.

**Tables Used:**

- `Credit_Sub_Category`

### 73. spDeleteCreditSubCategory

**Purpose:**

- Delete credit sub-category.

**Parameters:**

- `@SubCategoryID`

**Expected Output:**

- Credit sub-category deleted successfully.

**When Used:**

- Delete credit sub-category option.

**Tables Used:**

- `Credit_Sub_Category`

### 74. spGetAllPaymentTypes

**Purpose:**

- Get all payment methods.

**Parameters:**

- None

**Expected Output:**

- Complete payment methods list.

**When Used:**

- Expense, Credit, Lent and Borrow forms.

**Tables Used:**

- `Payment_Type`

### 75. spInsertPerson

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

- `Person`

### 76. spUpdatePerson

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

- `Person`

### 77. spDeletePerson

**Purpose:**

- Delete person details.

**Parameters:**

- `@PersonID`
- `@UserID`

**Expected Output:**

- Person deleted successfully.

**When Used:**

- Delete person option in settings.

**Tables Used:**

- `Person`

### 78. spGetAllPersons

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

- `Person`

## 🔔 REMINDER & NOTIFICATION QUERIES (WE WILL Re-Search Later)

### 79. spGetUpcomingBorrowReminders

**Purpose:**

- Get borrow deadline reminders

**Parameters:**

- `@UserID`

**Tables Used:**

- `Borrow`

### 80. spGetUpcomingLentReminders

**Purpose:**

- Get lent deadline reminders

**Parameters:**

- `@UserID`

**Tables Used:**

- `Lent`

### 81. spGetUpcomingTaskReminders

**Purpose:**

- Get task deadline reminders

**Parameters:**

- `@UserID`

**Tables Used:**

- `Task`

## 📌 FINAL TOTAL PROCEDURES

**Total Procedures:**

- 80

**Project Scope:**

- Fully Covers SRS
- Fully Covers All Tables
- Covers CRUD
- Covers Reports
- Covers Filters
- Covers Dashboard
- Covers Notifications
- Covers Lent/Borrow Business Logic
- Suitable For WinForms + SQL Server Project
