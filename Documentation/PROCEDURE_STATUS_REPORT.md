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
| Total Allotted Procedures | 84 |
| Total Procedures Found | 74 |
| `✔️` (All Okay) | 64 |
| `✅` (Pending Review) | 0 |
| `❌` (Fix Needed) | 10 |

*(Note: There is also 1 unmarked file `spTest.sql` in the new `Dashboard` folder)*

---

## 🟣 Team A — User, Task, Note
**Assigned Area:** Authentication, User Management, Task, Note, Task Reminder  
**Total Allotted Procedures:** 30

### `✔️` ALL OKAY (26)
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

### `✅` PENDING REVIEW (0)
- None

### `❌` FIX NEEDED (4)
- `spDeleteNote`
- `spGetAllNotes`
- `spUpdateNote`
- `spUpdateNotePriority`

### `[MISSING]` (0)
- None

---

## 🟢 Team B — Expense, Credit, Settings
**Assigned Area:** Expense, Credit, Categories, Subcategories, Payment Type  
**Total Allotted Procedures:** 35

### `✔️` ALL OKAY (35)
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

### `✅` PENDING REVIEW (0)
- None

### `❌` FIX NEEDED (0)
- None

### `[MISSING]` (0)
- None

---

## 🟠 Team C — Lent, Borrow
**Assigned Area:** Lent, Borrow, Persons, Status, Reminders  
**Total Allotted Procedures:** 19

### `✔️` ALL OKAY (3)
- `spGetAllLent`
- `SpGetCompletedLentByStatusName`
- `spGetPendingLentByStatusName`

### `✅` PENDING REVIEW (0)
- None

### `❌` FIX NEEDED (6)
- `spGetLentPersonHistory`
- `spInsertLent`
- `spReturnLentByReturnAmount`
- `spDeletePerson`
- `spInsertPerson`
- `spUpdatePerson`

### `[MISSING]` (10)
- `spInsertBorrow`
- `spGetAllBorrow`
- `spGetPendingBorrow`
- `spGetCompletedBorrow`
- `spPayBorrow`
- `spGetBorrowPersonHistory`
- `spGetTotalBorrowByPerson`
- `spGetAllPersons`
- `spGetUpcomingBorrowReminders`
- `spGetUpcomingLentReminders`
