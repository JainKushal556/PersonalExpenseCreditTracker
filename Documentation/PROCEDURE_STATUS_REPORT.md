# Stored Procedure Status Report

**Project:** Personal Expense Credit Tracker  
**Requirement File:** `Documentation/STORED_PROCEDURE_REQUIREMENTS.md`  
**Procedure Folder:** `Database/Procedures`

## Status Legend

| Mark | Meaning |
| ---- | ------- |
| `✅` | **PUSH KORLE:** Developer pushed the code, pending review by Project Manager. |
| `❌` | **FIX NEEDED:** Project Manager checked, changes/fixes are required. |
| `✔️` | **ALL OKAY:** Everything is perfect for the project, fully approved. |
| `[MISSING]` | Requirement exists but procedure file is not created yet. |

## Overall Summary

| Status | Count |
| ------ | ----: |
| Total Allotted Procedures | 85 |
| Total Procedures Found | 86 |
| `✔️` (All Okay) | 73 |
| `✅` (Pending Review) | 0 |
| `❌` (Fix Needed) | 12 |

*(Note: There is also 1 unmarked file `spTest.sql` in the new `Dashboard` folder)*

---

## 🟣 Team A â€” User, Task, Note
**Assigned Area:** Authentication, User Management, Task, Note, Task Reminder  
**Total Allotted Procedures:** 30

### `✔️` ALL OKAY (29)
- `spChangePassword`
- `spDeleteNote`
- `spDeleteTask`
- `spDeleteUserProfilePhotoByUserId`
- `spFilterNotesByPriority`
- `spFilterTasksByStatus`
- `spForgetPassword`
- `spGetActiveUserDetails`
- `spGetAllNotes`
- `spGetAllTasks`
- `spGetCompletedTasks`
- `spGetNotesBetweenDates`
- `spGetPendingTasks`
- `spGetTasksBetweenDates`
- `spGetUpcomingTaskReminders`
- `spGetUserDashboard`
- `spInsertNote`
- `spInsertTask`
- `spLogoutUser`
- `spRegisterUser`
- `spUpdateNote`
- `spUpdateNotePriority`
- `spUpdateProfilePhoto`
- `spUpdateTask`
- `spUpdateTaskStatus`
- `spUpdateUserEmail`
- `spUpdateUserName`
- `spUpdateUserPhoneNumber`
- `spUpdateUserProfile`

### `✅` PENDING REVIEW (0)
- None

### `❌` FIX NEEDED (1)
- `spLoginUser`

### `[MISSING]` (0)
- None

---

## 🟢 Team B â€” Expense, Credit, Settings
**Assigned Area:** Expense, Credit, Categories, Subcategories, Payment Type  
**Total Allotted Procedures:** 35

### `✔️` ALL OKAY (35)
- `spDeleteCreditCategoryByUserID`
- `spDeleteCreditSubCategoryByUserID`
- `spDeleteExpenseCategoryByUserID`
- `spDeleteExpenseSubCategoryByUserID`
- `spFilterCreditByAmountRange`
- `spFilterCreditByCategory`
- `spFilterCreditByCategoryAndSubCategory`
- `spFilterCreditByDateRange`
- `spFilterExpenseByAmountRange`
- `spFilterExpenseByCategory`
- `spFilterExpenseByCategoryAndSubCategory`
- `spFilterExpenseByDateRange`
- `spGetAllCreditsByID`
- `spGetAllExpensesByID`
- `spGetAllPaymentTypes`
- `spGetCategoryWiseCreditReport`
- `spGetCategoryWiseExpenseReport`
- `spGetCreditCategoriesByUserID`
- `spGetCreditSubCategoriesByUserID`
- `spGetExpenseCategoriesByUserID`
- `spGetExpenseSubCategoriesByUserID`
- `spGetMonthlyCreditSummary`
- `spGetMonthlyExpenseSummary`
- `spGetTodayCredit`
- `spGetTodayExpense`
- `spInsertCreditByUserID`
- `spInsertExpenseByUserID`
- `spInsertNewCreditCategoryByUserID`
- `spInsertNewCreditSubCategoryByUserID`
- `spInsertNewExpenseCategoryByUserID`
- `spInsertNewExpenseSubCategoryByUserID`
- `spUpdateCreditCategoryByUserID`
- `spUpdateCreditSubCategoryByUserID`
- `spUpdateExpenseCategoryByUserID`
- `spUpdateExpenseSubCategoryByUserID`

### `✅` PENDING REVIEW (0)
- None

### `❌` FIX NEEDED (0)
- None

### `[MISSING]` (0)
- None

---

## 🟠 Team C â€” Lent, Borrow
**Assigned Area:** Lent, Borrow, Persons, Status, Reminders  
**Total Allotted Procedures:** 20

### `✔️` ALL OKAY (8)
- `spGetAllBorrow`
- `spGetAllLent`
- `spGetBorrowPersonHistory`
- `spGetCompletedBorrow`
- `SpGetCompletedLentByStatusName`
- `spGetLentPersonHistory`
- `spGetTotalBorrowByPerson`
- `spPayBorrow`

### `✅` PENDING REVIEW (0)
- None

### `❌` FIX NEEDED (11)
- `spDeletePerson`
- `spGetAllPersons`
- `spGetOverduedBorrow`
- `spGetPendingBorrow`
- `spGetPendingLentByStatusName`
- `spGetUpcomingBorrowReminders`
- `spGetUpcomingLentReminders`
- `spInsertBorrow`
- `spInsertLent`
- `spReturnLentByReturnAmount`
- `spUpdatePerson`

### `[MISSING]` (1)
- `spInsertPerson`
