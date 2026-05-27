# Stored Procedure Status Report

**Project:** Personal Expense Credit Tracker  
**Requirement File:** `Documentation/STORED_PROCEDURE_REQUIREMENTS.md`  
**Procedure Folder:** `Database/Procedures`

## Status Legend

| Mark           | Meaning                                              |
| -------------- | ---------------------------------------------------- |
| `[OK]`         | Procedure file exists and is marked completed        |
| `[FIX NEEDED]` | Procedure file exists but is marked with cross       |
| `[MISSING]`    | Requirement exists but procedure file is not created |

## Overall Summary

| Status                    | Count |
| ------------------------- | ----: |
| Total Required Procedures |    81 |
| `[OK]`                    |    26 |
| `[FIX NEEDED]`            |    11 |
| `[MISSING]`               |    44 |

## Team A - User, Task, Note

**Assigned Area:** Authentication, User Management, Task, Note, Task Reminder

| Status         | Count |
| -------------- | ----: |
| Total          |    31 |
| `[OK]`         |    17 |
| `[FIX NEEDED]` |     7 |
| `[MISSING]`    |     7 |

### `[FIX NEEDED]`

| No. | Procedure              |
| --: | ---------------------- |
|   1 | `spRegisterUser`       |
|   2 | `spInsertTask`         |
|   3 | `spInsertNote`         |
|   4 | `spUpdateNote`         |
|   5 | `spUpdateNotePriority` |
|   6 | `spDeleteNote`         |
|   7 | `spGetAllNotes`        |

### `[MISSING]`

| No. | Procedure                    |
| --: | ---------------------------- |
|   1 | `spRemoveProfilePhoto`       |
|   2 | `spGetUserDashboard`         |
|   3 | `spGetTasksByDate`           |
|   4 | `spFilterNotesByPriority`    |
|   5 | `spGetNotesByDate`           |
|   6 | `spGetNotesBetweenDates`     |
|   7 | `spGetUpcomingTaskReminders` |

## Team B - Expense, Credit, Settings

**Assigned Area:** Expense, Credit, Categories, Subcategories, Payment Type

| Status         | Count |
| -------------- | ----: |
| Total          |    29 |
| `[OK]`         |     5 |
| `[FIX NEEDED]` |     0 |
| `[MISSING]`    |    24 |

### `[OK]`

| No. | Procedure                                |
| --: | ---------------------------------------- |
|   1 | `spInsertCreditByUserID`                 |
|   2 | `spGetAllCreditsByID`                    |
|   3 | `spFilterCreditByCategory`               |
|   4 | `spFilterCreditByCategoryAndSubCategory` |
|   5 | `spInsertNewExpenseCategoryByUserID`     |

### `[MISSING]`

| No. | Procedure                                 |
| --: | ----------------------------------------- |
|   1 | `spInsertExpense`                         |
|   2 | `spGetAllExpenses`                        |
|   3 | `spFilterExpenseByCategory`               |
|   4 | `spFilterExpenseByCategoryAndSubCategory` |
|   5 | `spFilterExpenseByDateRange`              |
|   6 | `spGetMonthlyExpenseSummary`              |
|   7 | `spGetCategoryWiseExpenseReport`          |
|   8 | `spGetTodayExpense`                       |
|   9 | `spFilterCreditByDateRange`               |
|  10 | `spGetMonthlyCreditSummary`               |
|  11 | `spGetCategoryWiseCreditReport`           |
|  12 | `spGetTodayCredit`                        |
|  13 | `spUpdateExpenseCategory`                 |
|  14 | `spDeleteExpenseCategory`                 |
|  15 | `spInsertExpenseSubCategory`              |
|  16 | `spUpdateExpenseSubCategory`              |
|  17 | `spDeleteExpenseSubCategory`              |
|  18 | `spInsertCreditCategory`                  |
|  19 | `spUpdateCreditCategory`                  |
|  20 | `spDeleteCreditCategory`                  |
|  21 | `spInsertCreditSubCategory`               |
|  22 | `spUpdateCreditSubCategory`               |
|  23 | `spDeleteCreditSubCategory`               |
|  24 | `spGetAllPaymentTypes`                    |

## Team C - Lent, Borrow

**Assigned Area:** Lent, Borrow, Lent Persons, Borrow Persons, Lent/Borrow Status, Person Management, Lent/Borrow Reminders

| Status         | Count |
| -------------- | ----: |
| Total          |    21 |
| `[OK]`         |     4 |
| `[FIX NEEDED]` |     4 |
| `[MISSING]`    |    13 |

### `[OK]`

| No. | Procedure                        |
| --: | -------------------------------- |
|   1 | `spGetCompletedLentByStatusName` |
|   2 | `spReturnLentByReturnAmount`     |
|   3 | `spGetAllLentPersons`            |
|   4 | `spGetAllBorrowPersons`          |

### `[FIX NEEDED]`

| No. | Procedure                      |
| --: | ------------------------------ |
|   1 | `spInsertLent`                 |
|   2 | `spGetAllLent`                 |
|   3 | `spGetPendingLentByStatusName` |
|   4 | `spGetLentPersonHistory`       |

### `[MISSING]`

| No. | Procedure                      |
| --: | ------------------------------ |
|   1 | `spInsertBorrow`               |
|   2 | `spGetAllBorrow`               |
|   3 | `spGetPendingBorrow`           |
|   4 | `spGetCompletedBorrow`         |
|   5 | `spPayBorrow`                  |
|   6 | `spGetBorrowPersonHistory`     |
|   7 | `spGetTotalBorrowByPerson`     |
|   8 | `spInsertPerson`               |
|   9 | `spUpdatePerson`               |
|  10 | `spDeletePerson`               |
|  11 | `spGetAllPersons`              |
|  12 | `spGetUpcomingBorrowReminders` |
|  13 | `spGetUpcomingLentReminders`   |

## Final Notes

| Topic               | Result                            |
| ------------------- | --------------------------------- |
| Most completed area | Team A                            |
| Most pending area   | Team B                            |
| First priority      | Fix all `[FIX NEEDED]` procedures |
| Second priority     | Create all `[MISSING]` procedures |
