-- =========================================================================
-- SCRIPT TO CLEAR ALL DATA FROM ALL TABLES AND RESET IDENTITY COUNTERS
-- Database: dbPersonalExpenseCreditTracker
-- =========================================================================

USE dbPersonalExpenseCreditTracker;
GO

-- Option 1: Safely Delete in Reverse Foreign-Key Dependency Order
BEGIN TRANSACTION;

BEGIN TRY
    -- 1. Delete from dependent child tables first
    DELETE FROM tblNote;
    DELETE FROM tblTask;
    DELETE FROM tblBorrow;
    DELETE FROM tblLent;
    DELETE FROM tblCredit;
    DELETE FROM tblExpense;
    DELETE FROM tblExpenseSubCategory;
    DELETE FROM tblCreditSubCategory;
    DELETE FROM tblUserAuthentication;
    DELETE FROM tblUserContact;
    DELETE FROM tblUserProfile;
    DELETE FROM tblPersons;
    
    -- 2. Delete from category / lookup master tables
    DELETE FROM tblNoteColor;
    DELETE FROM tblNotePriorities;
    DELETE FROM tblTaskStatus;
    DELETE FROM tblTaskPriorities;
    DELETE FROM tblLentBorrowStatus;
    DELETE FROM tblCreditCategory;
    DELETE FROM tblExpenseCategory;
    DELETE FROM tblPaymentType;

    -- 3. Delete from root users table last
    DELETE FROM tblUsers;

    -- 4. Reseed Identity values back to 0 so next insert starts at 1
    DBCC CHECKIDENT ('tblNote', RESEED, 0);
    DBCC CHECKIDENT ('tblTask', RESEED, 0);
    DBCC CHECKIDENT ('tblBorrow', RESEED, 0);
    DBCC CHECKIDENT ('tblLent', RESEED, 0);
    DBCC CHECKIDENT ('tblCredit', RESEED, 0);
    DBCC CHECKIDENT ('tblExpense', RESEED, 0);
    DBCC CHECKIDENT ('tblExpenseSubCategory', RESEED, 0);
    DBCC CHECKIDENT ('tblCreditSubCategory', RESEED, 0);
    DBCC CHECKIDENT ('tblUserAuthentication', RESEED, 0);
    DBCC CHECKIDENT ('tblUserContact', RESEED, 0);
    DBCC CHECKIDENT ('tblUserProfile', RESEED, 0);
    DBCC CHECKIDENT ('tblPersons', RESEED, 0);
    DBCC CHECKIDENT ('tblNoteColor', RESEED, 0);
    DBCC CHECKIDENT ('tblNotePriorities', RESEED, 0);
    DBCC CHECKIDENT ('tblTaskStatus', RESEED, 0);
    DBCC CHECKIDENT ('tblTaskPriorities', RESEED, 0);
    DBCC CHECKIDENT ('tblLentBorrowStatus', RESEED, 0);
    DBCC CHECKIDENT ('tblCreditCategory', RESEED, 0);
    DBCC CHECKIDENT ('tblExpenseCategory', RESEED, 0);
    DBCC CHECKIDENT ('tblPaymentType', RESEED, 0);
    DBCC CHECKIDENT ('tblUsers', RESEED, 0);

    COMMIT TRANSACTION;
    PRINT 'All table data deleted and identity counters reseeded successfully.';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'Error occurred while clearing database tables. Transaction rolled back.';
    THROW;
END CATCH;
GO
