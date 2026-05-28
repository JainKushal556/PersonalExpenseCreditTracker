# Stored Procedure Status Report

**Project:** Personal Expense Credit Tracker  
**Requirement File:** `Documentation/STORED_PROCEDURE_REQUIREMENTS.md`  
**Procedure Folder:** `Database/Procedures`

## Status Legend

| Mark | Meaning |
| ---- | ------- |
| `âœ…` | **PUSH KORLE:** Developer pushed the code, pending review by Project Manager. |
| `âŒ` | **FIX NEEDED:** Project Manager checked, changes/fixes are required. |
| `âœ”ï¸` | **ALL OKAY:** Everything is perfect for the project, fully approved. |
| `[MISSING]` | Requirement exists but procedure file is not created yet. |

## Overall Summary

| Status | Count |
| ------ | ----: |
| Total Allotted Procedures | 85 |
| Total Procedures Found | 85 |
| `âœ”ï¸` (All Okay) | 68 |
| `âœ…` (Pending Review) | 0 |
| `âŒ` (Fix Needed) | 17 |

*(Note: There is also 1 unmarked file `spTest.sql` in the new `Dashboard` folder)*

---

## ðŸŸ£ Team A â€” User, Task, Note
**Assigned Area:** Authentication, User Management, Task, Note, Task Reminder  
**Total Allotted Procedures:** 30

### `âœ”ï¸` ALL OKAY (30)
- `spChangePassword`
- `spDeleteUserProfilePhotoByUserId`
- `spForgetPassword`
- `spGetActiveUserDetails`
- `spLoginUser`
- `spLogoutUser`
- `spRegisterUser`
- `spUpdateProfilePhoto`
- `spUpdateUserEmail`
- `spUpdateUserName`
- `spUpdateUserPhoneNumber`
- `spUpdateUserProfile`
- `spGetUserDashboard`
- `spDeleteTask`
- `spFilterTasksByStatus`
- `spGetAllTasks`
- `spGetCompletedTasks`
- `spGetPendingTasks`
- `spGetTasksBetweenDates`
- `spGetUpcomingTaskReminders`
- `spInsertTask`
- `spUpdateTask`
- `spUpdateTaskStatus`
- `spFilterNotesByPriority`
- `spGetNotesBetweenDates`
- `spInsertNote`
- `spDeleteNote`
- `spGetAllNotes`
- `spUpdateNote`
- `spUpdateNotePriority`

### `âœ…` PENDING REVIEW (0)
- None

### `âŒ` FIX NEEDED (0)
- None

### `[MISSING]` (0)
- None

---

## ðŸŸ¢ Team B â€” Expense, Credit, Settings
**Assigned Area:** Expense, Credit, Categories, Subcategories, Payment Type  
**Total Allotted Procedures:** 35

### `âœ”ï¸` ALL OKAY (35)
- `spDeleteCreditCategoryByUserID`
- `spDeleteCreditSubCategoryByUserID`
- `spDeleteExpenseCategoryByUserID`
- `spDeleteExpenseSubCategoryByUserID`
- `spGetAllPaymentTypes`
- `spGetCreditCategoriesByUserID`
- `spGetCreditSubCategoriesByUserID`
- `spGetExpenseCategoriesByUserID`
- `spGetExpenseSubCategoriesByUserID`
- `spInsertNewCreditCategoryByUserID`
- `spInsertNewCreditSubCategoryByUserID`
- `spInsertNewExpenseCategoryByUserID`
- `spInsertNewExpenseSubCategoryByUserID`
- `spUpdateCreditCategoryByUserID`
- `spUpdateCreditSubCategoryByUserID`
- `spUpdateExpenseCategoryByUserID`
- `spUpdateExpenseSubCategoryByUserID`
- `spFilterCreditByAmountRange`
- `spFilterCreditByCategory`
- `spFilterCreditByCategoryAndSubCategory`
- `spFilterCreditByDateRange`
- `spGetAllCreditsByID`
- `spGetCategoryWiseCreditReport`
- `spGetMonthlyCreditSummary`
- `spGetTodayCredit`
- `spInsertCreditByUserID`
- `spFilterExpenseByAmountRange`
- `spFilterExpenseByCategory`
- `spFilterExpenseByCategoryAndSubCategory`
- `spFilterExpenseByDateRange`
- `spGetAllExpensesByID`
- `spGetCategoryWiseExpenseReport`
- `spGetMonthlyExpenseSummary`
- `spGetTodayExpense`
- `spInsertExpenseByUserID`

### `âœ…` PENDING REVIEW (0)
- None

### `âŒ` FIX NEEDED (0)
- None

### `[MISSING]` (0)
- None

---

## ðŸŸ  Team C â€” Lent, Borrow
**Assigned Area:** Lent, Borrow, Persons, Status, Reminders  
**Total Allotted Procedures:** 20

### `âœ”ï¸` ALL OKAY (3)
- `spGetAllLent`
- `SpGetCompletedLentByStatusName`
- `spGetPendingLentByStatusName`

### `âœ…` PENDING REVIEW (0)
- None

### `âŒ` FIX NEEDED (17)
- `spGetAllBorrow`
- `spGetBorrowPersonHistory`
- `spGetCompletedBorrow`
- `spGetOverduedBorrow`
- `spGetPendingBorrow`
- `spGetTotalBorrowByPerson`
- `spGetUpcomingBorrowReminders`
- `spInsertBorrow`
- `spPayBorrow`
- `spGetPendingLentByStatusName`
- `spInsertLent`
- `spReturnLentByReturnAmount`
- `PersonIDspGetAllPersons`
- `spDeletePerson`
- `spGetUpcomingLentReminders`
- `spInsertPerson`
- `spUpdatePerson`

### `[MISSING]` (0)
- None

