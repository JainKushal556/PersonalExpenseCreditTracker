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
| Total Procedures Found | 69 |
| `✔️` (All Okay) | 46 |
| `✅` (Pending Review) | 21 |
| `❌` (Fix Needed) | 2 |

*(Note: There is also 1 unmarked file `spTest.sql` in the new `Dashboard` folder)*

---

## 🟣 Team A — User, Task, Note
**Assigned Area:** Authentication, User Management, Task, Note, Task Reminder

### `✔️` ALL OKAY (21)
- `spChangePassword`
- `spDeleteUserProfilePhotoByUserId` *(Approved!)*
- `spForgetPassword`
- `spGetActiveUserDetails`
- `spLoginUser`
- `spLogoutUser` *(Approved!)*
- `spRegisterUser` *(Approved!)*
- `spUpdateProfilePhoto`
- `spUpdateUserEmail`
- `spUpdateUserName`
- `spUpdateUserPhoneNumber`
- `spUpdateUserProfile`
- `spDeleteTask`
- `spFilterTasksByStatus`
- `spGetAllTasks`
- `spGetCompletedTasks`
- `spGetPendingTasks`
- `spGetTasksBetweenDates`
- `spInsertTask` *(Approved!)*
- `spUpdateTask`
- `spUpdateTaskStatus`

### `✅` PENDING REVIEW (5)
- `spDeleteNote`
- `spGetAllNotes`
- `spInsertNote`
- `spUpdateNote`
- `spUpdateNotePriority`

### `❌` FIX NEEDED (0)
- None

### `[MISSING]` (6)
- `spGetUserDashboard`
- `spGetTasksByDate`
- `spFilterNotesByPriority`
- `spGetNotesByDate`
- `spGetNotesBetweenDates`
- `spGetUpcomingTaskReminders`

---

## 🟢 Team B — Expense, Credit, Settings
**Assigned Area:** Expense, Credit, Categories, Subcategories, Payment Type

### `✔️` ALL OKAY (24)
- *All 16 Settings Procedures*
- `spFilterCreditByAmountRange`
- `spFilterCreditByCategory`
- `spFilterCreditByCategoryAndSubCategory`
- `spFilterCreditByDateRange` *(Approved!)*
- `spGetAllCreditsByID`
- `spGetTodayCredit` *(Approved!)*
- `spInsertCreditByUserID`
- `spFilterExpenseByAmountRange`

### `✅` PENDING REVIEW (6)
- `spFilterExpenseByCategory`
- `spFilterExpenseByCategoryAndSubCategory`
- `spFilterExpenseByDateRange`
- `spGetAllExpensesByID`
- `spGetTodayExpense`
- `spInsertExpenseByUserID`

### `❌` FIX NEEDED (2)
- `spGetCategoryWiseCreditReport`
- `spGetMonthlyCreditSummary`

### `[MISSING]` (3)
- `spGetMonthlyExpenseSummary`
- `spGetCategoryWiseExpenseReport`
- `spGetAllPaymentTypes`

---

## 🟠 Team C — Lent, Borrow
**Assigned Area:** Lent, Borrow, Persons, Status, Reminders

### `✔️` ALL OKAY (1)
- `spGetAllBorrowPersons`

### `✅` PENDING REVIEW (10)
- `SpGetAllLentPersons`
- `SpGetCompletedLentByStatusName`
- `spReturnLentByReturnAmount`
- `spGetAllLent`
- `spGetLentPersonHistory`
- `spGetPendingLentByStatusName`
- `spInsertLent`
- `spInsertPerson`
- `spUpdatePerson`
- `spDeletePerson`

### `❌` FIX NEEDED (0)
- None

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
