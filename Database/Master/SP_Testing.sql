-- =========================================================================
-- STORED PROCEDURES TESTING TEMPLATE
-- Fill in the blanks with test data to execute the SPs.
-- =========================================================================

-- ==========================================================
-- SP: spChangePassword
-- From File: ✔️spChangePassword.sql
-- ==========================================================
EXEC spChangePassword
    @UserID = '',
    @OldPassword = '',
    @NewPassword = ''

GO

-- ==========================================================
-- SP: spDeleteUserProfilePhotoByUserId
-- From File: ✔️spDeleteUserProfilePhotoByUserId.sql
-- ==========================================================
EXEC spDeleteUserProfilePhotoByUserId
    @UserID = ''

GO

-- ==========================================================
-- SP: spForgetPassword
-- From File: ✔️spForgetPassword.sql
-- ==========================================================
EXEC spForgetPassword
    @Email = '',
    @PhoneNumber = '',
    @NewPassword = ''

GO

-- ==========================================================
-- SP: spLoginUser
-- From File: ✔️spLoginUser.sql
-- ==========================================================
EXEC spLoginUser
    @Email = '',
    @Password = ''

GO

-- ==========================================================
-- SP: spLogoutUser
-- From File: ✔️spLogoutUser.sql
-- ==========================================================
EXEC spLogoutUser
    @UserID = ''

GO

-- ==========================================================
-- SP: spRegisterUser
-- From File: ✔️spRegisterUser.sql
-- ==========================================================
EXEC spRegisterUser
    @UserName = '',
    @Email = '',
    @PhoneNumber = '',
    @Password = ''

GO

-- ==========================================================
-- SP: spUpdateProfilePhoto
-- From File: ✔️spUpdateProfilePhoto.sql
-- ==========================================================
EXEC spUpdateProfilePhoto
    @UserID = '',
    @ProfilePhoto = ''

GO

-- ==========================================================
-- SP: spUpdateUserEmail
-- From File: ✔️spUpdateUserEmail.sql
-- ==========================================================
EXEC spUpdateUserEmail
    @UserID = '',
    @Email = ''

GO

-- ==========================================================
-- SP: spUpdateUserPhoneNumber
-- From File: ✔️spUpdateUserPhoneNumber.sql
-- ==========================================================
EXEC spUpdateUserPhoneNumber
    @UserID = '',
    @PhoneNumber = ''

GO

-- ==========================================================
-- SP: spUpdateUserProfile
-- From File: ✔️spUpdateUserProfile.sql
-- ==========================================================
EXEC spUpdateUserProfile
    @UserID = '',
    @Name = '',
    @Email = '',
    @PhoneNumber = '',
    @ProfilePhoto = ''

GO

-- ==========================================================
-- SP: spFilterCreditByAmountRange
-- From File: ✔️spFilterCreditByAmountRange.sql
-- ==========================================================
EXEC spFilterCreditByAmountRange
    @UserID = '',
    @MinAmount = '',
    @MaxAmount = ''

GO

-- ==========================================================
-- SP: spFilterCreditByCategory
-- From File: ✔️spFilterCreditByCategory.sql
-- ==========================================================
EXEC spFilterCreditByCategory
    @UserID = '',
    @CategoryID = ''

GO

-- ==========================================================
-- SP: spFilterCreditByCategoryAndSubCategory
-- From File: ✔️spFilterCreditByCategoryAndSubCategory.sql
-- ==========================================================
EXEC spFilterCreditByCategoryAndSubCategory
    @UserID = '',
    @CategoryID = '',
    @SubCategoryID = ''

GO

-- ==========================================================
-- SP: spFilterCreditByDateRange
-- From File: ✔️spFilterCreditByDateRange.sql
-- ==========================================================
EXEC spFilterCreditByDateRange
    @UserID = '',
    @FromDate = '',
    @ToDate = ''

GO

-- ==========================================================
-- SP: spGetAllCreditsByID
-- From File: ✔️spGetAllCreditsByID.sql
-- ==========================================================
EXEC spGetAllCreditsByID
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetCategoryWiseCreditReport
-- From File: ✔️spGetCategoryWiseCreditReport.sql
-- ==========================================================
EXEC spGetCategoryWiseCreditReport
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetMonthlyCreditSummary
-- From File: ✔️spGetMonthlyCreditSummary.sql
-- ==========================================================
EXEC spGetMonthlyCreditSummary
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetTodayCredit
-- From File: ✔️spGetTodayCredit.sql
-- ==========================================================
EXEC spGetTodayCredit
    @UserID = ''

GO

-- ==========================================================
-- SP: spInsertCreditByUserID
-- From File: ✔️spInsertCreditByUserID.sql
-- ==========================================================
EXEC spInsertCreditByUserID
    @UserID = '',
    @CategoryID = '',
    @SubCategoryID = '',
    @Amount = '',
    @Description = '',
    @PaymentID = '',
    @CreditAt = ''

GO

-- ==========================================================
-- SP: spGetUserDashboard
-- From File: ✔️spGetUserDashboard.sql
-- ==========================================================
EXEC spGetUserDashboard
    @UserID = ''

GO

-- ==========================================================
-- SP: spFilterExpenseByAmountRange
-- From File: ✔️spFilterExpenseByAmountRange.sql
-- ==========================================================
EXEC spFilterExpenseByAmountRange
    @UserID = '',
    @MinAmount = '',
    @MaxAmount = ''

GO

-- ==========================================================
-- SP: spFilterExpenseByCategory
-- From File: ✔️spFilterExpenseByCategory.sql
-- ==========================================================
EXEC spFilterExpenseByCategory
    @UserID = '',
    @CategoryID = ''

GO

-- ==========================================================
-- SP: spFilterExpenseByCategoryAndSubCategory
-- From File: ✔️spFilterExpenseByCategoryAndSubCategory.sql
-- ==========================================================
EXEC spFilterExpenseByCategoryAndSubCategory
    @UserID = '',
    @CategoryID = '',
    @SubCategoryID = ''

GO

-- ==========================================================
-- SP: spFilterExpenseByDateRange
-- From File: ✔️spFilterExpenseByDateRange.sql
-- ==========================================================
EXEC spFilterExpenseByDateRange
    @UserID = '',
    @FromDate = '',
    @ToDate = ''

GO

-- ==========================================================
-- SP: spGetAllExpensesByID
-- From File: ✔️spGetAllExpensesByID.sql
-- ==========================================================
EXEC spGetAllExpensesByID
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetCategoryWiseExpenseReport
-- From File: ✔️spGetCategoryWiseExpenseReport.sql
-- ==========================================================
EXEC spGetCategoryWiseExpenseReport
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetMonthlyExpenseSummary
-- From File: ✔️spGetMonthlyExpenseSummary.sql
-- ==========================================================
EXEC spGetMonthlyExpenseSummary
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetTodayExpense
-- From File: ✔️spGetTodayExpense.sql
-- ==========================================================
EXEC spGetTodayExpense
    @UserID = ''

GO

-- ==========================================================
-- SP: spInsertExpenseByUserID
-- From File: ✔️spInsertExpenseByUserID.sql
-- ==========================================================
EXEC spInsertExpenseByUserID
    @UserID = '',
    @CategoryID = '',
    @SubCategoryID = '',
    @Amount = '',
    @Description = '',
    @PaymentID = '',
    @ExpenseAt = ''

GO

-- ==========================================================
-- SP: spGetAllLent
-- From File: ✔️spGetAllLent.sql
-- ==========================================================
EXEC spGetAllLent
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetCompletedLentByStatusName
-- From File: ✔️SpGetCompletedLentByStatusName.sql
-- ==========================================================
EXEC spGetCompletedLentByStatusName
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetLentPersonHistory
-- From File: ✔️spGetLentPersonHistory.sql
-- ==========================================================
EXEC spGetLentPersonHistory
    @PersonID = '',
    @UserID = ''

GO

-- ==========================================================
-- SP: spDeleteNote
-- From File: ✔️spDeleteNote.sql
-- ==========================================================
EXEC spDeleteNote
    @UserID = '',
    @NoteID = ''

GO

-- ==========================================================
-- SP: spFilterNotesByPriority
-- From File: ✔️spFilterNotesByPriority.sql
-- ==========================================================
EXEC spFilterNotesByPriority
    @UserID = '',
    @PriorityID = ''

GO

-- ==========================================================
-- SP: spGetAllNotes
-- From File: ✔️spGetAllNotes.sql
-- ==========================================================
EXEC spGetAllNotes
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetNotesBetweenDates
-- From File: ✔️spGetNotesBetweenDates.sql
-- ==========================================================
EXEC spGetNotesBetweenDates
    @UserID = '',
    @FromDate = '',
    @ToDate = ''

GO

-- ==========================================================
-- SP: spInsertNote
-- From File: ✔️spInsertNote.sql
-- ==========================================================
EXEC spInsertNote
    @UserID = '',
    @PriorityID = '',
    @NoteTitle = '',
    @Description = ''

GO

-- ==========================================================
-- SP: spUpdateNote
-- From File: ✔️spUpdateNote.sql
-- ==========================================================
EXEC spUpdateNote
    @UserID = '',
    @NoteID = '',
    @PriorityID = '',
    @NoteTitle = '',
    @Description = ''

GO

-- ==========================================================
-- SP: spUpdateNotePriority
-- From File: ✔️spUpdateNotePriority.sql
-- ==========================================================
EXEC spUpdateNotePriority
    @UserID = '',
    @NoteID = '',
    @PriorityID = ''

GO

-- ==========================================================
-- SP: spDeleteCreditCategoryByUserID
-- From File: ✔️spDeleteCreditCategoryByUserID.sql
-- ==========================================================
EXEC spDeleteCreditCategoryByUserID
    @UserID = '',
    @CategoryID = ''

GO

-- ==========================================================
-- SP: spDeleteCreditSubCategoryByUserID
-- From File: ✔️spDeleteCreditSubCategoryByUserID.sql
-- ==========================================================
EXEC spDeleteCreditSubCategoryByUserID
    @UserID = '',
    @SubCategoryID = ''

GO

-- ==========================================================
-- SP: spDeleteExpenseCategoryByUserID
-- From File: ✔️spDeleteExpenseCategoryByUserID.sql
-- ==========================================================
EXEC spDeleteExpenseCategoryByUserID
    @UserID = '',
    @CategoryID = ''

GO

-- ==========================================================
-- SP: spDeleteExpenseSubCategoryByUserID
-- From File: ✔️spDeleteExpenseSubCategoryByUserID.sql
-- ==========================================================
EXEC spDeleteExpenseSubCategoryByUserID
    @UserID = '',
    @SubCategoryID = ''

GO

-- ==========================================================
-- SP: spGetAllPaymentTypes
-- From File: ✔️spGetAllPaymentTypes.sql
-- ==========================================================
EXEC spGetAllPaymentTypes

GO

-- ==========================================================
-- SP: spGetCreditCategoriesByUserID
-- From File: ✔️spGetCreditCategoriesByUserID.sql
-- ==========================================================
EXEC spGetCreditCategoriesByUserID
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetCreditSubCategoriesByUserID
-- From File: ✔️spGetCreditSubCategoriesByUserID.sql
-- ==========================================================
EXEC spGetCreditSubCategoriesByUserID
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetExpenseCategoriesByUserID
-- From File: ✔️spGetExpenseCategoriesByUserID.sql
-- ==========================================================
EXEC spGetExpenseCategoriesByUserID
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetExpenseSubCategoriesByUserID
-- From File: ✔️spGetExpenseSubCategoriesByUserID.sql
-- ==========================================================
EXEC spGetExpenseSubCategoriesByUserID
    @UserID = ''

GO

-- ==========================================================
-- SP: spInsertNewCreditCategoryByUserID
-- From File: ✔️spInsertNewCreditCategoryByUserID.sql
-- ==========================================================
EXEC spInsertNewCreditCategoryByUserID
    @UserID = '',
    @CategoryName = ''

GO

-- ==========================================================
-- SP: spInsertNewCreditSubCategoryByUserID
-- From File: ✔️spInsertNewCreditSubCategoryByUserID.sql
-- ==========================================================
EXEC spInsertNewCreditSubCategoryByUserID
    @UserID = '',
    @CategoryID = '',
    @SubCategoryName = ''

GO

-- ==========================================================
-- SP: spInsertNewExpenseCategoryByUserID
-- From File: ✔️spInsertNewExpenseCategoryByUserID.SQL
-- ==========================================================
EXEC spInsertNewExpenseCategoryByUserID
    @UserID = '',
    @CategoryName = ''

GO

-- ==========================================================
-- SP: spInsertNewExpenseSubCategoryByUserID
-- From File: ✔️spInsertNewExpenseSubCategoryByUserID.sql
-- ==========================================================
EXEC spInsertNewExpenseSubCategoryByUserID
    @UserID = '',
    @CategoryID = '',
    @SubCategoryName = ''

GO

-- ==========================================================
-- SP: spUpdateCreditCategoryByUserID
-- From File: ✔️spUpdateCreditCategoryByUserID.sql
-- ==========================================================
EXEC spUpdateCreditCategoryByUserID
    @UserID = '',
    @CategoryID = '',
    @CategoryName = ''

GO

-- ==========================================================
-- SP: spUpdateCreditSubCategoryByUserID
-- From File: ✔️spUpdateCreditSubCategoryByUserID.sql
-- ==========================================================
EXEC spUpdateCreditSubCategoryByUserID
    @UserID = '',
    @SubCategoryID = '',
    @SubCategoryName = ''

GO

-- ==========================================================
-- SP: spUpdateExpenseCategoryByUserID
-- From File: ✔️spUpdateExpenseCategoryByUserID.sql
-- ==========================================================
EXEC spUpdateExpenseCategoryByUserID
    @UserID = '',
    @CategoryID = '',
    @CategoryName = ''

GO

-- ==========================================================
-- SP: spUpdateExpenseSubCategoryByUserID
-- From File: ✔️spUpdateExpenseSubCategoryByUserID.sql
-- ==========================================================
EXEC spUpdateExpenseSubCategoryByUserID
    @UserID = '',
    @SubCategoryID = '',
    @SubCategoryName = ''

GO

-- ==========================================================
-- SP: spDeleteTask
-- From File: ✔️spDeleteTask.sql
-- ==========================================================
EXEC spDeleteTask
    @TaskID = ''

GO

-- ==========================================================
-- SP: spFilterTasksByStatus
-- From File: ✔️spFilterTasksByStatus.sql
-- ==========================================================
EXEC spFilterTasksByStatus
    @UserID = '',
    @TaskStatusID = ''

GO

-- ==========================================================
-- SP: spGetAllTasks
-- From File: ✔️spGetAllTasks.sql
-- ==========================================================
EXEC spGetAllTasks
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetCompletedTasks
-- From File: ✔️spGetCompletedTasks.sql
-- ==========================================================
EXEC spGetCompletedTasks
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetPendingTasks
-- From File: ✔️spGetPendingTasks.sql
-- ==========================================================
EXEC spGetPendingTasks
    @UserID = ''

GO

-- ==========================================================
-- SP: spGetTasksBetweenDates
-- From File: ✔️spGetTasksBetweenDates.sql
-- ==========================================================
EXEC spGetTasksBetweenDates
    @UserID = '',
    @FromDate = '',
    @ToDate = ''

GO

-- ==========================================================
-- SP: spGetUpcomingTaskReminders
-- From File: ✔️spGetUpcomingTaskReminders.sql
-- ==========================================================
EXEC spGetUpcomingTaskReminders
    @UserID = ''

GO

-- ==========================================================
-- SP: spInsertTask
-- From File: ✔️spInsertTask.sql
-- ==========================================================
EXEC spInsertTask
    @UserID = '',
    @PriorityID = '',
    @TaskTitle = '',
    @Deadline = ''

GO

-- ==========================================================
-- SP: spUpdateTask
-- From File: ✔️spUpdateTask.sql
-- ==========================================================
EXEC spUpdateTask
    @UserID = '',
    @TaskID = '',
    @PriorityID = '',
    @TaskStatusID = '',
    @TaskTitle = '',
    @Deadline = ''

GO

-- ==========================================================
-- SP: spUpdateTaskStatus
-- From File: ✔️spUpdateTaskStatus.sql
-- ==========================================================
EXEC spUpdateTaskStatus
    @TaskID = '',
    @TaskStatusID = ''

GO



-- ==========================================================
-- ❌ SPs THAT NEED FIXES (MOVED TO END FOR CONVENIENCE)
-- ==========================================================

-- ==========================================================
-- SP: spGetAllBorrow
-- From File: ❌spGetAllBorrow.sql
-- ==========================================================
-- EXEC spGetAllBorrow
--     @UserID = ''

-- GO

-- ==========================================================
-- SP: spGetBorrowPersonHistory
-- From File: ❌spGetBorrowPersonHistory.sql
-- ==========================================================
-- EXEC spGetBorrowPersonHistory
--     @PersonID = ''

-- GO

-- ==========================================================
-- SP: spGetCompletedBorrow
-- From File: ❌spGetCompletedBorrow.sql
-- ==========================================================
-- EXEC spGetCompletedBorrow
--     @UserID = ''

-- GO

-- ==========================================================
-- SP: spGetOverduedBorrow
-- From File: ❌spGetOverduedBorrow(EXTRA PROCEDURE).sql
-- ==========================================================
-- EXEC spGetOverduedBorrow
--     @UserID = ''

-- GO

-- ==========================================================
-- SP: spGetPendingBorrow
-- From File: ❌spGetPendingBorrow.sql
-- ==========================================================
-- EXEC spGetPendingBorrow
--     @UserID = ''

-- GO

-- ==========================================================
-- SP: spGetTotalBorrowByPerson
-- From File: ❌spGetTotalBorrowByPerson.sql
-- ==========================================================
-- EXEC spGetTotalBorrowByPerson
--     @PersonID = ''

-- GO

-- ==========================================================
-- SP: spGetUpcomingBorrowReminders
-- From File: ❌spGetUpcomingBorrowReminders.sql
-- ==========================================================
-- EXEC spGetUpcomingBorrowReminders
--     @UserID = ''

-- GO

-- ==========================================================
-- SP: spInsertBorrow
-- From File: ❌spInsertBorrow.sql
-- ==========================================================
-- EXEC spInsertBorrow
--     @UserID = '',
--     @PersonID = '',
--     @PaymentID = '',
--     @StatusID = '',
--     @Amount = '',
--     @DeadlineAt = '',
--     @Description = ''

-- GO

-- ==========================================================
-- SP: spPayBorrow
-- From File: ❌spPayBorrow.sql
-- ==========================================================
-- EXEC spPayBorrow
--     @BorrowID = '',
--     @PaidAmount = '',
--     @PaymentID = ''

-- GO

-- ==========================================================
-- SP: spGetPendingLentByStatusName
-- From File: ❌spGetPendingLentByStatusName.sql
-- ==========================================================
-- EXEC spGetPendingLentByStatusName
--     @UserID = ''

-- GO

-- ==========================================================
-- SP: spInsertLent
-- From File: ❌spInsertLent.sql
-- ==========================================================
-- EXEC spInsertLent
--     @UserID = '',
--     @PersonID = '',
--     @PaymentID = '',
--     @StatusID = '',
--     @Amount = '',
--     @ReturnedAmount = '',
--     @RemainingAmount = '',
--     @DeadlineAT = '',
--     @Description = ''

-- GO

-- ==========================================================
-- SP: spReturnLentByReturnAmount
-- From File: ❌spReturnLentByReturnAmount.sql
-- ==========================================================
-- EXEC spReturnLentByReturnAmount
--     @LentID = '',
--     @PaymentID = '',
--     @ReturnedAmount = '',
--     @Description = '',
--     @SubCategoryID = '',
--     @CategoryID = ''

-- GO

-- ==========================================================
-- SP: spGetAllPersons
-- From File: ❌PersonIDspGetAllPersons.sql
-- ==========================================================
-- EXEC spGetAllPersons
--     @UserID = ''

-- GO

-- ==========================================================
-- SP: spDeletePerson
-- From File: ❌spDeletePerson.sql
-- ==========================================================
-- EXEC spDeletePerson
--     @UserID = '',
--     @PersonID = ''

-- GO

-- ==========================================================
-- SP: spGetUpcomingLentReminders
-- From File: ❌spGetUpcomingLentReminders.sql
-- ==========================================================
-- EXEC spGetUpcomingLentReminders
--     @UserID = ''

-- GO

-- ==========================================================
-- SP: spInsertPerson
-- From File: ❌spInsertPerson.sql
-- ==========================================================
-- EXEC spInsertPerson
--     @UserID = '',
--     @PersonName = '',
--     @PhoneNumber = '',
--     @Address = ''

-- GO

-- ==========================================================
-- SP: spUpdatePerson
-- From File: ❌spUpdatePerson.sql
-- ==========================================================
-- EXEC spUpdatePerson
--     @UserID = '',
--     @PersonID = '',
--     @PersonName = '',
--     @PhoneNumber = '',
--     @Address = ''

-- GO

