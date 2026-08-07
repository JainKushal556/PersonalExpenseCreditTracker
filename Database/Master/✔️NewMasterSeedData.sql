-- =========================================================================
-- NEW MASTER SEED DATA SCRIPT (In Proper Dependency Order)
-- Database: dbPersonalExpenseCreditTracker
-- =========================================================================

USE dbPersonalExpenseCreditTracker;
GO

-- 1. INDEPENDENT LOOKUP TABLES (কোনো ফরেন কি ডিপেন্ডেন্সি নেই)
-- =========================================================================

-- Insert Genders
IF NOT EXISTS (SELECT 1 FROM tblGender WHERE GenderName = 'Male') INSERT INTO tblGender (GenderName) VALUES ('Male');
IF NOT EXISTS (SELECT 1 FROM tblGender WHERE GenderName = 'Female') INSERT INTO tblGender (GenderName) VALUES ('Female');
IF NOT EXISTS (SELECT 1 FROM tblGender WHERE GenderName = 'Other') INSERT INTO tblGender (GenderName) VALUES ('Other');
GO

-- Insert Payment Methods
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Bank Transfer') INSERT INTO tblPaymentType (PaymentName) VALUES ('Bank Transfer');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Cash') INSERT INTO tblPaymentType (PaymentName) VALUES ('Cash');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Cheque') INSERT INTO tblPaymentType (PaymentName) VALUES ('Cheque');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Credit Card') INSERT INTO tblPaymentType (PaymentName) VALUES ('Credit Card');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Debit Card') INSERT INTO tblPaymentType (PaymentName) VALUES ('Debit Card');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Google Pay') INSERT INTO tblPaymentType (PaymentName) VALUES ('Google Pay');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Net Banking') INSERT INTO tblPaymentType (PaymentName) VALUES ('Net Banking');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Paytm') INSERT INTO tblPaymentType (PaymentName) VALUES ('Paytm');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'PhonePe') INSERT INTO tblPaymentType (PaymentName) VALUES ('PhonePe');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'UPI') INSERT INTO tblPaymentType (PaymentName) VALUES ('UPI');
GO

-- Insert Lent and Borrow Status
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Pending') INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Pending');
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Paid') INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Paid');
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Overdue') INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Overdue');
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Cancelled') INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Cancelled');
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Partially Paid') INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Partially Paid');
GO

-- Insert Task Priority Levels
IF NOT EXISTS (SELECT 1 FROM tblTaskPriorities WHERE PriorityName = 'Low') INSERT INTO tblTaskPriorities (PriorityName) VALUES ('Low');
IF NOT EXISTS (SELECT 1 FROM tblTaskPriorities WHERE PriorityName = 'Medium') INSERT INTO tblTaskPriorities (PriorityName) VALUES ('Medium');
IF NOT EXISTS (SELECT 1 FROM tblTaskPriorities WHERE PriorityName = 'High') INSERT INTO tblTaskPriorities (PriorityName) VALUES ('High');
GO

-- Insert Task Status Types
IF NOT EXISTS (SELECT 1 FROM tblTaskStatus WHERE TaskStatusName = 'Pending') INSERT INTO tblTaskStatus (TaskStatusName) VALUES ('Pending');
IF NOT EXISTS (SELECT 1 FROM tblTaskStatus WHERE TaskStatusName = 'Partially Complete') INSERT INTO tblTaskStatus (TaskStatusName) VALUES ('Partially Complete');
IF NOT EXISTS (SELECT 1 FROM tblTaskStatus WHERE TaskStatusName = 'Complete') INSERT INTO tblTaskStatus (TaskStatusName) VALUES ('Complete');
GO

-- Insert Note Priority Levels
IF NOT EXISTS (SELECT 1 FROM tblNotePriorities WHERE NotePriorityName = 'Normal') INSERT INTO tblNotePriorities (NotePriorityName) VALUES ('Normal');
IF NOT EXISTS (SELECT 1 FROM tblNotePriorities WHERE NotePriorityName = 'Important') INSERT INTO tblNotePriorities (NotePriorityName) VALUES ('Important');
IF NOT EXISTS (SELECT 1 FROM tblNotePriorities WHERE NotePriorityName = 'Urgent') INSERT INTO tblNotePriorities (NotePriorityName) VALUES ('Urgent');
GO

-- Insert Note Colors
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'White') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('White', '#FFFFFF');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Red') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Red', '#FF6B6B');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Orange') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Orange', '#FFB74D');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Yellow') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Yellow', '#FDD835');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Green') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Green', '#81C784');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Teal') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Teal', '#4DB6AC');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Blue') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Blue', '#64B5F6');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Purple') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Purple', '#9575CD');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Pink') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Pink', '#F06292');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Grey') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Grey', '#90A4AE');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Lavender') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Lavender', '#BA68C8');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Coral') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Coral', '#FF8A65');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Mint') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Mint', '#80CBC4');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Indigo') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Indigo', '#7986CB');
GO

-- 2. ROOT USERS TABLE (মেইন ইউজার টেবিল)
-- =========================================================================

-- Insert User Details
SET IDENTITY_INSERT tblUsers ON;

INSERT INTO tblUsers (UserID, UserName, CreatedAt) 
VALUES
(1, 'Ram Das', '2026-01-12 00:00:00'),
(2, 'Rahul Sharma', '2026-05-21 00:00:00'),
(3, 'Priya Das', '2026-06-16 00:00:00'),
(4, 'Ankit Verma', '2026-03-29 00:00:00'),
(5, 'Sneha Roy', '2026-02-28 00:00:00');

SET IDENTITY_INSERT tblUsers OFF;
GO


-- 3. TABLES DIRECTLY DEPENDING ON USERS / LOOKUPS
-- =========================================================================

-- Insert User Profile Information
SET IDENTITY_INSERT tblUserProfile ON;

INSERT INTO tblUserProfile 
(ProfileID, UserID, FullName, ProfilePhoto)
VALUES
(1, 1, 'Ram Das', NULL),
(2, 2, 'Rahul Sharma', NULL),
(3, 3, 'Priya Das', NULL),
(4, 4, 'Ankit Verma', NULL),
(5, 5, 'Sneha Roy', NULL);

SET IDENTITY_INSERT tblUserProfile OFF;
GO

-- Insert User Contact Details
INSERT INTO tblUserContact 
(UserID, Email, PhoneNumber)
VALUES
(1, 'ram143@gmail.com', '2568910296'),
(2, 'sharmarahul123@gmail.com', '5555983670'),
(3, 'priya4556@gmail.com', '8597658973'),
(4, 'verma2005@gmail.com', '9025146832'),
(5, 'sneharoy1@gmail.com', '8897581146');
GO

-- Insert User Login Credentials
INSERT INTO tblUserAuthentication
(UserID, Password, Active)
VALUES
(1,'Ram342@',0),
(2,'Rahul#126',0),
(3,'Nibu@1234',0),
(4,'Solo#3472',0),
(5,'Roy#1678',1);
GO

-- Insert Person Contact List
INSERT INTO tblPersons
(UserID, PersonName, PhoneNumber, Address)
VALUES
(1,'Aarav Sharma','+91 98765 43210','Kharagpur'),
(2,'Priya Sharma','+91 98765 43211','Durgapur'),
(4,'Rohan Verma','+91 98123 45678','Panskura'),
(3,'Ananya Iyer','+91 97654 32109','Kolkata'),
(5,'Vikram Patel','+91 99887 76655','Howrah'),
(2,'Sneha Reddy','+91 91234 56789','Durgapur'),
(2,'Kabir Mehta','+91 93456 78901','Siliguri'),
(3,'Diya Banerjee','+91 94567 89012','Asansol'),
(5,'Siddharth Joshi','+91 95678 90123','Midnapore'),
(4,'Neha Kapoor','+91 96789 01234','Malda');
GO

-- Insert Default Expense Categories
INSERT INTO tblExpenseCategory 
(UserID, CategoryName, IsDefault, IsActive)
VALUES
(NULL, 'Food', 1, 1),
(NULL, 'Travel', 1, 1),
(NULL, 'Shopping', 1, 1),
(NULL, 'Entertainment', 1, 1),
(NULL, 'Health', 1, 1),
(NULL, 'Education', 1, 1),
(NULL, 'Transportation', 1, 1),
(NULL, 'Personal Care', 1, 1),
(NULL, 'Lent', 1, 1),
(NULL, 'Tuition', 1, 1),
(NULL, 'Borrow', 1, 1);
GO

-- Insert Default Credit Categories
INSERT INTO tblCreditCategory
(UserID, CategoryName, IsDefault, IsActive)
VALUES
(NULL,'Salary',1,1),
(NULL,'Business',1,1),
(NULL,'Investment',1,1),
(NULL,'Freelancing',1,1),
(NULL,'Rental',1,1),
(NULL,'Cashback',1,1),
(NULL,'Scholarship',1,1),
(NULL,'Bonus',1,1),
(NULL,'Refund',1,1),
(NULL,'Borrow',1,1),
(NULL,'Lent',1,1);
GO


-- 4. SUBCATEGORY TABLES (ক্যাটাগরির ওপর ডিপেন্ডেন্ট)
-- =========================================================================

-- Insert Expense Sub Categories
INSERT INTO tblExpenseSubCategory
(CategoryID, UserID, SubCategoryName, IsDefault, IsActive)
VALUES
-- Food
(1,NULL,'Breakfast',1,1),
(1,NULL,'Lunch',1,1),
(1,NULL,'Dinner',1,1),
(1,NULL,'Snacks',1,1),
(1,NULL,'Restaurant',1,1),
(1,NULL,'Fast Food',1,1),
(1,NULL,'Beverages',1,1),
-- Travel
(2,NULL,'Train',1,1),
(2,NULL,'Bus',1,1),
(2,NULL,'Taxi',1,1),
(2,NULL,'Hotel',1,1),
(2,NULL,'Tour',1,1),
(2,NULL,'Package',1,1),
(2,NULL,'Fuel',1,1),
(2,NULL,'Toll',1,1),
-- Shopping
(3,NULL,'Clothing',1,1),
(3,NULL,'Electronics',1,1),
(3,NULL,'Footwear',1,1),
(3,NULL,'Accessories',1,1),
(3,NULL,'Home Appliances',1,1),
(3,NULL,'Furniture',1,1),
(3,NULL,'Gifts',1,1),
(3,NULL,'Online Shopping',1,1),
-- Entertainment
(4,NULL,'Movies',1,1),
(4,NULL,'Games',1,1),
(4,NULL,'OTT Subscription',1,1),
(4,NULL,'Concert',1,1),
(4,NULL,'Theme Park',1,1),
(4,NULL,'Sports Event',1,1),
(4,NULL,'Music',1,1),
-- Health
(5,NULL,'Doctor',1,1),
(5,NULL,'Medicines',1,1),
(5,NULL,'Medical Tests',1,1),
(5,NULL,'Hospital Bill',1,1),
(5,NULL,'Health Insurance',1,1),
(5,NULL,'Gym Membership',1,1),
(5,NULL,'Pharmacy',1,1),
-- Education
(6,NULL,'School Fees',1,1),
(6,NULL,'College Fees',1,1),
(6,NULL,'Tuition Fees',1,1),
(6,NULL,'Books',1,1),
(6,NULL,'Stationery',1,1),
(6,NULL,'Online Course',1,1),
(6,NULL,'Exam Fees',1,1),
-- Transportation
(7,NULL,'Petrol',1,1),
(7,NULL,'Diesel',1,1),
(7,NULL,'Auto Rickshaw',1,1),
(7,NULL,'Cab',1,1),
(7,NULL,'Metro',1,1),
(7,NULL,'Parking',1,1),
(7,NULL,'Vehicle Service',1,1),
(7,NULL,'Bike Maintenance',1,1),
-- Personal Care
(8,NULL,'Haircut',1,1),
(8,NULL,'Salon',1,1),
(8,NULL,'Cosmetics',1,1),
(8,NULL,'Skincare',1,1),
(8,NULL,'Spa',1,1),
(8,NULL,'Toiletries',1,1),
(8,NULL,'Grooming',1,1),
-- Lent
(9,NULL,'Lent to Friend',1,1),
(9,NULL,'Lent to Family Member',1,1),
(9,NULL,'Bank Loan EMI',1,1),
(9,NULL,'Credit Card EMI',1,1),
(9,NULL,'Personal Loan EMI',1,1),
(9,NULL,'Lent Given',1,1),
-- Tuition
(10,NULL,'School Tuition',1,1),
(10,NULL,'College Tuition',1,1),
(10,NULL,'Private Tutor',1,1),
(10,NULL,'Coaching Center',1,1),
(10,NULL,'Online Tuition',1,1),
-- Borrow
(11,NULL,'Borrow Returned',1,1);
GO

-- Insert Credit Sub Categories
INSERT INTO tblCreditSubCategory
(CategoryID, UserID, SubCategoryName, IsDefault, IsActive)
VALUES
-- Salary
(1,NULL,'Basic Salary',1,1),
(1,NULL,'Overtime Pay',1,1),
(1,NULL,'Allowances',1,1),
(1,NULL,'Incentives',1,1),
-- Business
(2,NULL,'Product Sales',1,1),
(2,NULL,'Service Income',1,1),
(2,NULL,'Commission',1,1),
(2,NULL,'Franchise Income',1,1),
-- Investment
(3,NULL,'Stock Dividend',1,1),
(3,NULL,'Mutual Fund Returns',1,1),
(3,NULL,'Fixed Deposit Interest',1,1),
(3,NULL,'Gold Investment',1,1),
-- Freelancing
(4,NULL,'Web Development',1,1),
(4,NULL,'Graphic Design',1,1),
(4,NULL,'Content Writing',1,1),
(4,NULL,'Video Editing',1,1),
-- Rental
(5,NULL,'House Rent',1,1),
(5,NULL,'Shop Rent',1,1),
(5,NULL,'Vehicle Rent',1,1),
(5,NULL,'Equipment Rent',1,1),
-- Cashback
(6,NULL,'Credit Card Cashback',1,1),
(6,NULL,'UPI Cashback',1,1),
(6,NULL,'Shopping Cashback',1,1),
(6,NULL,'Wallet Cashback',1,1),
-- Scholarship
(7,NULL,'Merit Scholarship',1,1),
(7,NULL,'Government Scholarship',1,1),
(7,NULL,'Private Scholarship',1,1),
(7,NULL,'Research Scholarship',1,1),
-- Bonus
(8,NULL,'Performance Bonus',1,1),
(8,NULL,'Festival Bonus',1,1),
(8,NULL,'Referral Bonus',1,1),
(8,NULL,'Annual Bonus',1,1),
-- Refund
(9,NULL,'Tax Refund',1,1),
(9,NULL,'Product Return Refund',1,1),
(9,NULL,'Ticket Cancellation Refund',1,1),
(9,NULL,'Fee Refund',1,1),
-- Borrow
(10,NULL,'Friend Loan',1,1),
(10,NULL,'Family Loan',1,1),
(10,NULL,'Bank Loan',1,1),
(10,NULL,'Personal Loan',1,1),
(10,NULL,'Borrow Received',1,1),
-- Lent
(11,NULL,'Lent Returned',1,1);
GO


-- 5. TRANSACTION & OPERATIONAL TABLES (সবচেয়ে শেষে ইনসার্ট করা হচ্ছে)
-- =========================================================================

-- Insert Expense Records
INSERT INTO tblExpense
(UserID, CategoryID, SubCategoryID, Amount, Description, PaymentID, ExpenseAt)
VALUES
(1,1,1,120.50,'Breakfast at restaurant',10,'20261204'),
(2,2,8,850.00,'Train ticket',2,'20260425'),
(3,3,16,1200.00,'Online shopping',3,'20260105'),
(4,4,26,700.00,'Netflix Subscription',4,'20261105'),
(5,5,31,1000.00,'Doctor checkup for fever',8,'20260529'),
(1,6,39,11000.00,'College admission fees',2,'20260617'),
(2,7,47,200.00,'Auto fare',1,'20260619'),
(3,8,53,100.00,'Haircut',8,'20260627'),
(4,9,60,1700.00,'Lent amount to Ram',5,'20260718'),
(5,10,67,1000.00,'Predeep sir tuition fees',6,'20260718');
GO

-- Insert Credit / Income Records
INSERT INTO tblCredit
(UserID, CategoryID, SubCategoryID, Amount, Description, PaymentID, CreditAt)
VALUES
(1,1,1,5000.00,'Monthly Basic Salary',1,'20260331'),
(5,1,2,450.00,'Weekend Overtime Pay',10,'20260803'),
(5,3,9,250.00,'Stock Dividend',2,'20261204'),
(4,4,13,1200.00,'Landing Page Development',3,'20260329'),
(3,5,17,1500.00,'Apartment Rent Received',1,'20260718'),
(2,6,22,15.00,'CashBack Reward on Utility Bill',4,'20260618'),
(5,7,25,2000.00,'Semester Academic Merit Scholarship',7,'20260514'),
(2,8,29,800.00,'Mid-Year Performance Bonus',6,'20260707'),
(3,9,35,120.00,'Flight Ticket Cancellation Refund',9,'20260523'),
(1,10,37,300.00,'Short-term Loan from Friend',4,'20260418');
GO

-- Insert Lent Transaction Records
INSERT INTO tblLent
(UserID, PersonID, PaymentID, StatusID, Amount, ReturnedAmount, RemainingAmount, LentAt, DeadlineAt, Description)
VALUES
(1,1,1,2,5000.00,5000.00,0.00,'20260115','20260215','Emergency medical expense loan'),
(2,2,10,1,2500.00,0.00,2500.00,'20260601','20260815','Laptop repair assistance'),
(2,6,9,5,4000.00,1500.00,2500.00,'20260525','20260830','Project equipment purchase'),
(2,7,6,2,1200.00,1200.00,0.00,'20260610','20260710','Travel ticket booking advance'),
(3,4,10,3,3000.00,0.00,3000.00,'20260620','20260720','Course registration fee loan'),
(3,8,8,2,800.00,800.00,0.00,'20260625','20260715','Textbook purchase support'),
(4,3,2,1,6000.00,0.00,6000.00,'20260405','20260901','Home renovation short-term loan'),
(4,10,7,5,10000.00,4000.00,6000.00,'20260410','20260810','Small business start-up loan'),
(5,5,1,2,15000.00,15000.00,0.00,'20260301','20260601','Security deposit assistance'),
(5,9,9,3,3500.00,500.00,3000.00,'20260310','20260510','Personal emergency cash advance');
GO

-- Insert Borrow Transaction Records
INSERT INTO tblBorrow
(UserID, PersonID, PaymentID, StatusID, Amount, PaidAmount, RemainingAmount, BorrowAt, DeadlineAt, Description)
VALUES
(1,1,1,2,2000.00,2000.00,0.00,'20260120','20260220','Borrowed for college semester fee gap'),
(2,2,10,1,1500.00,0.00,1500.00,'20260605','20260820','Borrowed for medical store bill'),
(2,6,6,5,5000.00,2000.00,3000.00,'20260518','20260825','Borrowed for electronic gadget purchase'),
(2,7,9,2,3500.00,3500.00,0.00,'20260612','20260715','Borrowed for festival shopping advance'),
(3,4,10,3,4500.00,0.00,4500.00,'20260615','20260715','Borrowed for hostel mess advance'),
(3,8,2,2,1000.00,1000.00,0.00,'20260628','20260720','Borrowed for reference book purchase'),
(4,3,1,1,8000.00,0.00,8000.00,'20260412','20260910','Borrowed for house repair expense'),
(4,10,8,5,12000.00,5000.00,7000.00,'20260415','20260815','Borrowed for freelance setup gear'),
(5,5,7,2,10000.00,10000.00,0.00,'20260305','20260605','Borrowed for vehicle maintenance cost'),
(5,9,9,3,2500.00,500.00,2000.00,'20260315','20260515','Borrowed for urgent travel booking');
GO

-- Insert Task Records
INSERT INTO tblTask
(UserID, PriorityID, TaskStatusID, TaskTitle, Deadline, CreatedAt)
VALUES
(1,3,3,'Complete Semester Project Report','20260125','20260112'),
(1,2,2,'Review Computer Networks Notes','20260210','20260115'),
(2,3,1,'Complete Computer Networks Practical Record','20260615','20260522'),
(2,1,3,'Return Issued Reference Books to Central Library','20260530','20260525'),
(3,2,2,'Design Course Evaluation Form UI','20260710','20260618'),
(3,3,1,'Pay Upcoming Semester Examination and Tuition Fees','20260815','20260805'),
(4,1,3,'Database Schema Backup and Migration','20260415','20260330'),
(4,3,2,'Pay Upcoming Semester Examination and Tuition Fees','20260510','20260402'),
(5,2,1,'Laptop Repair','20260320','20260301'),
(5,1,3,'Review Mathematics III Linear Algebra Lectures','20260305','20260302');
GO

-- Insert Note Records
INSERT INTO tblNote
(UserID, NotePriorityID, NoteColorID, NoteTitle, Description, CreatedAt)
VALUES
(1,2,1,
'Project Meeting Notes',
'Discussed project architecture sprint milestones and deliverables.',
'20260115'),

(1,1,1,
'Shopping List',
'Buy groceries fresh vegetables snacks and household essentials.',
'20260118'),

(2,3,1,
'Exam Preparation',
'Review key core syllabus concepts previous papers and practice problems.',
'20260522'),

(2,1,1,
'Workout Plan',
'Morning cardio strength training routine and weekend core exercises.',
'20260528'),

(3,2,1,
'React Ideas',
'Brainstorm UI components state management setup and custom hooks.',
'20260618'),

(3,3,1,
'Birthday Reminder',
'Order custom cake organize gifts and plan surprise party gathering.',
'20260620'),

(4,1,1,
'Office Tasks',
'Clear pending emails update task status board and complete documentation.',
'20260330'),

(4,2,1,
'Travel Plan',
'Book flight tickets reserve hotel stays and prepare daily sightseeing itinerary.',
'20260405'),

(5,3,1,
'Daily Goals',
'Finish code refactoring complete 2 design tasks and read technical blogs.',
'20260301'),

(5,1,1,
'Movie Watchlist',
'Spider Man The New Arrival',
'20260304');
GO
