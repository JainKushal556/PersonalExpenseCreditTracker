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
| Total Allotted Procedures | 86 |
| Total Procedures Found | 86 |
| `✔️` (All Okay) | 81 |
| `✅` (Pending Review) | 0 |
| `❌` (Fix Needed) | 5 |

---

## 🟣 Team A â€” User, Task, Note
**Assigned Area:** Authentication, User Management, Task, Note, Task Reminder
**Total Allotted Procedures:** 30

### `✔️` ALL OKAY (30)
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
- `spLoginUser`
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

### `❌` FIX NEEDED (0)
- None

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
**Total Allotted Procedures:** 21

### `✔️` ALL OKAY (16)
- `spGetAllBorrow`
- `spGetAllLent`
- `spGetAllPersons`
- `spGetBorrowPersonHistory`
- `spGetCompletedBorrow`
- `SpGetCompletedLentByStatusName`
- `spGetLentPersonHistory`
- `spGetPendingLentByStatusName`
- `spGetTotalBorrowByPerson`
- `spGetUpcomingBorrowReminders`
- `spGetUpcomingLentReminders`
- `spInsertLent`
- `spInsertPerson`
- `spPayBorrow`
- `spReturnLentByReturnAmount`
- `spUpdatePerson`

### `✅` PENDING REVIEW (0)
- None

### `❌` FIX NEEDED (5)
- `spDeletePerson`
- `spGetOverduedBorrow`
- `spGetPendingBorrow`
- `spInsertBorrow`
- `spUpdateOverdueStatus`

### `[MISSING]` (0)
- None
