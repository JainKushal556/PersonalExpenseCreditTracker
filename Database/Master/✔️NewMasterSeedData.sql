-- =========================================================================
-- NEW MASTER SEED DATA SCRIPT (In Proper Dependency Order)
-- Database: dbPersonalExpenseCreditTracker
-- Last Updated: 2026-08-10
-- =========================================================================

USE dbPersonalExpenseCreditTracker;
GO

-- =========================================================================
-- 1. INDEPENDENT LOOKUP TABLES
-- =========================================================================

-- Genders
IF NOT EXISTS (SELECT 1 FROM tblGender WHERE GenderName = 'Male')   INSERT INTO tblGender (GenderName) VALUES ('Male');
IF NOT EXISTS (SELECT 1 FROM tblGender WHERE GenderName = 'Female') INSERT INTO tblGender (GenderName) VALUES ('Female');
IF NOT EXISTS (SELECT 1 FROM tblGender WHERE GenderName = 'Other')  INSERT INTO tblGender (GenderName) VALUES ('Other');
GO

-- Payment Methods
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Bank Transfer') INSERT INTO tblPaymentType (PaymentName) VALUES ('Bank Transfer');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Cash')          INSERT INTO tblPaymentType (PaymentName) VALUES ('Cash');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Cheque')        INSERT INTO tblPaymentType (PaymentName) VALUES ('Cheque');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Credit Card')   INSERT INTO tblPaymentType (PaymentName) VALUES ('Credit Card');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Debit Card')    INSERT INTO tblPaymentType (PaymentName) VALUES ('Debit Card');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Google Pay')    INSERT INTO tblPaymentType (PaymentName) VALUES ('Google Pay');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Net Banking')   INSERT INTO tblPaymentType (PaymentName) VALUES ('Net Banking');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'Paytm')         INSERT INTO tblPaymentType (PaymentName) VALUES ('Paytm');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'PhonePe')       INSERT INTO tblPaymentType (PaymentName) VALUES ('PhonePe');
IF NOT EXISTS (SELECT 1 FROM tblPaymentType WHERE PaymentName = 'UPI')           INSERT INTO tblPaymentType (PaymentName) VALUES ('UPI');
GO

-- Lent & Borrow Status
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Pending')          INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Pending');
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Paid')             INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Paid');
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Overdue')          INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Overdue');
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Cancelled')        INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Cancelled');
IF NOT EXISTS (SELECT 1 FROM tblLentBorrowStatus WHERE StatusName = 'Partially Paid')   INSERT INTO tblLentBorrowStatus (StatusName) VALUES ('Partially Paid');
GO

-- Task Priorities
IF NOT EXISTS (SELECT 1 FROM tblTaskPriorities WHERE PriorityName = 'Low')    INSERT INTO tblTaskPriorities (PriorityName) VALUES ('Low');
IF NOT EXISTS (SELECT 1 FROM tblTaskPriorities WHERE PriorityName = 'Medium') INSERT INTO tblTaskPriorities (PriorityName) VALUES ('Medium');
IF NOT EXISTS (SELECT 1 FROM tblTaskPriorities WHERE PriorityName = 'High')   INSERT INTO tblTaskPriorities (PriorityName) VALUES ('High');
GO

-- Task Status
IF NOT EXISTS (SELECT 1 FROM tblTaskStatus WHERE TaskStatusName = 'Pending')            INSERT INTO tblTaskStatus (TaskStatusName) VALUES ('Pending');
IF NOT EXISTS (SELECT 1 FROM tblTaskStatus WHERE TaskStatusName = 'Partially Complete') INSERT INTO tblTaskStatus (TaskStatusName) VALUES ('Partially Complete');
IF NOT EXISTS (SELECT 1 FROM tblTaskStatus WHERE TaskStatusName = 'Complete')           INSERT INTO tblTaskStatus (TaskStatusName) VALUES ('Complete');
GO

-- Note Priorities
IF NOT EXISTS (SELECT 1 FROM tblNotePriorities WHERE NotePriorityName = 'Low')    INSERT INTO tblNotePriorities (NotePriorityName) VALUES ('Normal');
IF NOT EXISTS (SELECT 1 FROM tblNotePriorities WHERE NotePriorityName = 'Medium') INSERT INTO tblNotePriorities (NotePriorityName) VALUES ('Important');
IF NOT EXISTS (SELECT 1 FROM tblNotePriorities WHERE NotePriorityName = 'High')    INSERT INTO tblNotePriorities (NotePriorityName) VALUES ('Urgent');
GO

-- Note Colors
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'White')    INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('White',    '#FFFFFF');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Red')      INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Red',      '#FF6B6B');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Orange')   INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Orange',   '#FFB74D');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Yellow')   INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Yellow',   '#FDD835');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Green')    INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Green',    '#81C784');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Teal')     INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Teal',     '#4DB6AC');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Blue')     INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Blue',     '#64B5F6');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Purple')   INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Purple',   '#9575CD');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Pink')     INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Pink',     '#F06292');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Grey')     INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Grey',     '#90A4AE');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Lavender') INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Lavender', '#BA68C8');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Coral')    INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Coral',    '#FF8A65');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Mint')     INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Mint',     '#80CBC4');
IF NOT EXISTS (SELECT 1 FROM tblNoteColor WHERE ColorName = 'Indigo')   INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES ('Indigo',   '#7986CB');
GO

-- =========================================================================
-- 2. ROOT USERS TABLE
-- =========================================================================

SET IDENTITY_INSERT tblUsers ON;

INSERT INTO tblUsers (UserID, UserName, CreatedAt)
VALUES
(1, 'Ram Das',       '2026-01-12 09:00:00'),
(2, 'Rahul Sharma',  '2026-05-21 10:00:00'),
(3, 'Priya Das',     '2026-06-16 11:00:00'),
(4, 'Ankit Verma',   '2026-03-29 09:30:00'),
(5, 'Sneha Roy',     '2026-02-28 08:45:00');

SET IDENTITY_INSERT tblUsers OFF;
GO

-- =========================================================================
-- 3. TABLES DIRECTLY DEPENDING ON USERS / LOOKUPS
-- =========================================================================

-- User Profile
SET IDENTITY_INSERT tblUserProfile ON;

INSERT INTO tblUserProfile (ProfileID, UserID, FullName, ProfilePhoto)
VALUES
(1, 1, 'Ram Das',      NULL),
(2, 2, 'Rahul Sharma', NULL),
(3, 3, 'Priya Das',    NULL),
(4, 4, 'Ankit Verma',  NULL),
(5, 5, 'Sneha Roy',    NULL);

SET IDENTITY_INSERT tblUserProfile OFF;
GO

-- User Contact
INSERT INTO tblUserContact (UserID, Email, PhoneNumber)
VALUES
(1, 'ram143@gmail.com',          '9876543210'),
(2, 'sharmarahul123@gmail.com',  '9823456789'),
(3, 'priya4556@gmail.com',       '9712345678'),
(4, 'verma2005@gmail.com',       '9634567890'),
(5, 'sneharoy1@gmail.com',       '9587654321');
GO

-- User Authentication (User 5 Active for testing)
INSERT INTO tblUserAuthentication (UserID, Password, Active)
VALUES
(1, 'Ram342@',    0),
(2, 'Rahul#126',  0),
(3, 'Nibu@1234',  0),
(4, 'Solo#3472',  0),
(5, 'Roy#1678',   0);
GO

-- =========================================================================
-- Person Contact List (2 persons per user — proper realistic data)
-- =========================================================================

INSERT INTO tblPersons (UserID, PersonName, PhoneNumber, Address)
VALUES
-- User 1: Ram Das
(1, 'Aarav Sharma',    '9876543201', 'Salt Lake, Kolkata'),
(1, 'Sourav Das',      '9845612300', 'Howrah, West Bengal'),

-- User 2: Rahul Sharma
(2, 'Priya Sharma',    '9123456780', 'Durgapur, West Bengal'),
(2, 'Kabir Mehta',     '9934567812', 'Siliguri, West Bengal'),

-- User 3: Priya Das
(3, 'Ananya Iyer',     '9654321089', 'Ballygunge, Kolkata'),
(3, 'Diya Banerjee',   '9743210987', 'Asansol, West Bengal'),

-- User 4: Ankit Verma
(4, 'Rohan Verma',     '9812345670', 'Panskura, West Bengal'),
(4, 'Neha Kapoor',     '9678901234', 'Malda, West Bengal'),

-- User 5: Sneha Roy
(5, 'Vikram Patel',    '9887766554', 'Howrah, West Bengal'),
(5, 'Siddharth Joshi', '9567890123', 'Midnapore, West Bengal');
GO

-- =========================================================================
-- 4. EXPENSE & CREDIT CATEGORIES
-- =========================================================================

-- Default Expense Categories
INSERT INTO tblExpenseCategory (UserID, CategoryName, IsDefault, IsActive)
VALUES
(NULL, 'Food',          1, 1),
(NULL, 'Travel',        1, 1),
(NULL, 'Shopping',      1, 1),
(NULL, 'Entertainment', 1, 1),
(NULL, 'Health',        1, 1),
(NULL, 'Education',     1, 1),
(NULL, 'Transportation',1, 1),
(NULL, 'Personal Care', 1, 1),
(NULL, 'Lent',          1, 1),
(NULL, 'Tuition',       1, 1),
(NULL, 'Borrow',        1, 1),
(NULL, 'Miscellaneous', 1, 1);
GO

-- Default Credit Categories
INSERT INTO tblCreditCategory (UserID, CategoryName, IsDefault, IsActive)
VALUES
(NULL, 'Salary',      1, 1),
(NULL, 'Business',    1, 1),
(NULL, 'Investment',  1, 1),
(NULL, 'Freelancing', 1, 1),
(NULL, 'Rental',      1, 1),
(NULL, 'Cashback',    1, 1),
(NULL, 'Scholarship', 1, 1),
(NULL, 'Bonus',       1, 1),
(NULL, 'Refund',      1, 1),
(NULL, 'Borrow',      1, 1),
(NULL, 'Lent',        1, 1),
(5,    'Other Income',0, 1);
GO

-- =========================================================================
-- 5. SUBCATEGORY TABLES
-- =========================================================================

-- Expense Sub Categories
INSERT INTO tblExpenseSubCategory (CategoryID, UserID, SubCategoryName, IsDefault, IsActive)
VALUES
-- Food (CategoryID=1)
(1, NULL, 'Breakfast',     1, 1),
(1, NULL, 'Lunch',         1, 1),
(1, NULL, 'Dinner',        1, 1),
(1, NULL, 'Snacks',        1, 1),
(1, NULL, 'Restaurant',    1, 1),
(1, NULL, 'Fast Food',     1, 1),
(1, NULL, 'Beverages',     1, 1),
-- Travel (CategoryID=2)
(2, NULL, 'Train',         1, 1),
(2, NULL, 'Bus',           1, 1),
(2, NULL, 'Taxi',          1, 1),
(2, NULL, 'Hotel',         1, 1),
(2, NULL, 'Tour',          1, 1),
(2, NULL, 'Package',       1, 1),
(2, NULL, 'Fuel',          1, 1),
(2, NULL, 'Toll',          1, 1),
-- Shopping (CategoryID=3)
(3, NULL, 'Clothing',         1, 1),
(3, NULL, 'Electronics',      1, 1),
(3, NULL, 'Footwear',         1, 1),
(3, NULL, 'Accessories',      1, 1),
(3, NULL, 'Home Appliances',  1, 1),
(3, NULL, 'Furniture',        1, 1),
(3, NULL, 'Gifts',            1, 1),
(3, NULL, 'Online Shopping',  1, 1),
-- Entertainment (CategoryID=4)
(4, NULL, 'Movies',           1, 1),
(4, NULL, 'Games',            1, 1),
(4, NULL, 'OTT Subscription', 1, 1),
(4, NULL, 'Concert',          1, 1),
(4, NULL, 'Theme Park',       1, 1),
(4, NULL, 'Sports Event',     1, 1),
(4, NULL, 'Music',            1, 1),
-- Health (CategoryID=5)
(5, NULL, 'Doctor',           1, 1),
(5, NULL, 'Medicines',        1, 1),
(5, NULL, 'Medical Tests',    1, 1),
(5, NULL, 'Hospital Bill',    1, 1),
(5, NULL, 'Health Insurance', 1, 1),
(5, NULL, 'Gym Membership',   1, 1),
(5, NULL, 'Pharmacy',         1, 1),
-- Education (CategoryID=6)
(6, NULL, 'School Fees',      1, 1),
(6, NULL, 'College Fees',     1, 1),
(6, NULL, 'Tuition Fees',     1, 1),
(6, NULL, 'Books',            1, 1),
(6, NULL, 'Stationery',       1, 1),
(6, NULL, 'Online Course',    1, 1),
(6, NULL, 'Exam Fees',        1, 1),
-- Transportation (CategoryID=7)
(7, NULL, 'Petrol',           1, 1),
(7, NULL, 'Diesel',           1, 1),
(7, NULL, 'Auto Rickshaw',    1, 1),
(7, NULL, 'Cab',              1, 1),
(7, NULL, 'Metro',            1, 1),
(7, NULL, 'Parking',          1, 1),
(7, NULL, 'Vehicle Service',  1, 1),
(7, NULL, 'Bike Maintenance', 1, 1),
-- Personal Care (CategoryID=8)
(8, NULL, 'Haircut',          1, 1),
(8, NULL, 'Salon',            1, 1),
(8, NULL, 'Cosmetics',        1, 1),
(8, NULL, 'Skincare',         1, 1),
(8, NULL, 'Spa',              1, 1),
(8, NULL, 'Toiletries',       1, 1),
(8, NULL, 'Grooming',         1, 1),
-- Lent (CategoryID=9)
(9, NULL, 'Lent to Friend',       1, 1),
(9, NULL, 'Lent to Family Member',1, 1),
(9, NULL, 'Bank Loan EMI',        1, 1),
(9, NULL, 'Credit Card EMI',      1, 1),
(9, NULL, 'Personal Loan EMI',    1, 1),
(9, NULL, 'Lent Given',           1, 1),
-- Tuition (CategoryID=10)
(10, NULL, 'School Tuition',   1, 1),
(10, NULL, 'College Tuition',  1, 1),
(10, NULL, 'Private Tutor',    1, 1),
(10, NULL, 'Coaching Center',  1, 1),
(10, NULL, 'Online Tuition',   1, 1),
-- Borrow (CategoryID=11)
(11, NULL, 'Borrow Returned',  1, 1),
-- Miscellaneous (CategoryID=12)
(12, NULL, 'General',          1, 1);
GO

-- Credit Sub Categories
INSERT INTO tblCreditSubCategory (CategoryID, UserID, SubCategoryName, IsDefault, IsActive)
VALUES
-- Salary (CategoryID=1)
(1, NULL, 'Basic Salary',    1, 1),
(1, NULL, 'Overtime Pay',    1, 1),
(1, NULL, 'Allowances',      1, 1),
(1, NULL, 'Incentives',      1, 1),
-- Business (CategoryID=2)
(2, NULL, 'Product Sales',   1, 1),
(2, NULL, 'Service Income',  1, 1),
(2, NULL, 'Commission',      1, 1),
(2, NULL, 'Franchise Income',1, 1),
-- Investment (CategoryID=3)
(3, NULL, 'Stock Dividend',        1, 1),
(3, NULL, 'Mutual Fund Returns',   1, 1),
(3, NULL, 'Fixed Deposit Interest',1, 1),
(3, NULL, 'Gold Investment',       1, 1),
-- Freelancing (CategoryID=4)
(4, NULL, 'Web Development',  1, 1),
(4, NULL, 'Graphic Design',   1, 1),
(4, NULL, 'Content Writing',  1, 1),
(4, NULL, 'Video Editing',    1, 1),
-- Rental (CategoryID=5)
(5, NULL, 'House Rent',       1, 1),
(5, NULL, 'Shop Rent',        1, 1),
(5, NULL, 'Vehicle Rent',     1, 1),
(5, NULL, 'Equipment Rent',   1, 1),
-- Cashback (CategoryID=6)
(6, NULL, 'Credit Card Cashback', 1, 1),
(6, NULL, 'UPI Cashback',         1, 1),
(6, NULL, 'Shopping Cashback',    1, 1),
(6, NULL, 'Wallet Cashback',      1, 1),
-- Scholarship (CategoryID=7)
(7, NULL, 'Merit Scholarship',    1, 1),
(7, NULL, 'Government Scholarship',1,1),
(7, NULL, 'Private Scholarship',  1, 1),
(7, NULL, 'Research Scholarship', 1, 1),
-- Bonus (CategoryID=8)
(8, NULL, 'Performance Bonus', 1, 1),
(8, NULL, 'Festival Bonus',    1, 1),
(8, NULL, 'Referral Bonus',    1, 1),
(8, NULL, 'Annual Bonus',      1, 1),
-- Refund (CategoryID=9)
(9, NULL, 'Tax Refund',                   1, 1),
(9, NULL, 'Product Return Refund',        1, 1),
(9, NULL, 'Ticket Cancellation Refund',   1, 1),
(9, NULL, 'Fee Refund',                   1, 1),
-- Borrow (CategoryID=10)
(10, NULL, 'Friend Loan',      1, 1),
(10, NULL, 'Family Loan',      1, 1),
(10, NULL, 'Bank Loan',        1, 1),
(10, NULL, 'Personal Loan',    1, 1),
(10, NULL, 'Borrow Received',  1, 1),
-- Lent (CategoryID=11)
(11, NULL, 'Lent Returned',    1, 1),
-- Other Income (CategoryID=12, User 5 custom)
(12, 5, 'General',             0, 1);
GO

-- =========================================================================
-- 6. TRANSACTION & OPERATIONAL TABLES
-- =========================================================================

-- Expense Records
INSERT INTO tblExpense (UserID, CategoryID, SubCategoryID, Amount, Description, PaymentID, ExpenseAt)
VALUES
-- User 1
(1, 1,  1,  120.50,   'Morning breakfast at cafe',             10, '2026-12-04'),
(1, 6,  39, 11000.00, 'College admission fees payment',         2, '2026-06-17'),
(1, 7,  45, 1500.00,  'Car petrol fill-up at pump',             2, '2026-02-10'),
-- User 2
(2, 2,  8,  850.00,   'Train ticket to Kolkata',                2, '2026-04-25'),
(2, 7,  47, 200.00,   'Auto fare to office',                    1, '2026-06-19'),
(2, 2,  8,  920.00,   'Vande Bharat Express ticket',            1, '2026-02-20'),
-- User 3
(3, 3,  16, 1200.00,  'Online shopping order',                  3, '2026-01-05'),
(3, 8,  53, 100.00,   'Haircut at barber shop',                 8, '2026-06-27'),
(3, 3,  16, 3500.00,  'Festival ethnic wear shopping',          5, '2026-03-25'),
-- User 4
(4, 4,  26, 700.00,   'Netflix Subscription monthly',           4, '2026-11-05'),
(4, 9,  60, 1700.00,  'Lent amount to friend',                  5, '2026-07-18'),
(4, 4,  24, 750.00,   'PVR IMAX Movie tickets for 2',           8, '2026-05-01'),
-- User 5: Sneha Roy (realistic day-to-day expenses)
(5, 5,  31, 1000.00,  'Doctor checkup for fever',               8, '2026-05-29'),
(5, 10, 67, 1000.00,  'Tuition fees for semester',              6, '2026-07-18'),
(5, 1,  1,  150.00,   'Morning breakfast at South Indian tiffin center', 10, '2026-01-15'),
(5, 7,  45, 1200.00,  'Petrol fill-up for scooty',              2, '2026-01-18'),
(5, 3,  23, 2499.00,  'Wireless Bluetooth Earbuds from Amazon', 4, '2026-01-22'),
(5, 5,  32, 480.00,   'Monthly fever and cold medicines',       6, '2026-02-05'),
(5, 1,  5,  1850.00,  'Family dinner at Spice Garden restaurant',9,'2026-02-14'),
(5, 8,  54, 1800.00,  'Facial and beauty salon treatment',      9, '2026-03-03'),
(5, 3,  17, 14500.00, 'Realme Smartphone purchase',             4, '2026-03-10'),
(5, 1,  7,  140.00,   'Cold coffee and smoothie at cafe',       10,'2026-03-15'),
(5, 7,  49, 120.00,   'Kolkata Metro Smart Card recharge',      8, '2026-03-21'),
(5, 10, 67, 3000.00,  'College semester tuition fees',          1, '2026-03-28'),
(5, 4,  26, 649.00,   'Disney+ Hotstar Annual Plan',            4, '2026-04-05'),
(5, 1,  4,  220.00,   'Evening snacks and momos',               10,'2026-04-12'),
(5, 3,  16, 2800.00,  'Kurti and jeans shopping',               5, '2026-04-20'),
(5, 2,  10, 450.00,   'Cab fare to office',                     6, '2026-04-28'),
(5, 5,  36, 1200.00,  'Gym monthly membership fee',             9, '2026-05-05'),
(5, 1,  2,  350.00,   'Weekend buffet lunch with colleagues',   10,'2026-05-12'),
(5, 6,  41, 850.00,   'Notebooks and study stationary',         2, '2026-05-22'),
(5, 8,  55, 1250.00,  'Nykaa skincare and cosmetics',           4, '2026-06-01'),
(5, 1,  3,  580.00,   'Zomato Pizza order for dinner',          10,'2026-06-08'),
(5, 7,  45, 1000.00,  'Scooty petrol refilling',                2, '2026-06-16'),
(5, 2,  8,  780.00,   'Express train ticket to Durgapur',       1, '2026-06-25'),
(5, 5,  33, 1500.00,  'Routine health blood test',              6, '2026-07-02'),
(5, 3,  22, 1600.00,  'Birthday gift for best friend',          2, '2026-07-10'),
(5, 4,  24, 600.00,   'Movie tickets for weekend show',         8, '2026-07-20'),
(5, 1,  5,  1100.00,  'Dinner at Barbeque Nation',              9, '2026-07-29'),
(5, 7,  52, 950.00,   'Scooty servicing and oil change',        1, '2026-08-05'),
(5, 12, 72, 500.00,   'Miscellaneous local festival donation',  2, '2026-04-10'),
(5, 12, 72, 300.00,   'Unplanned household miscellaneous repair',10,'2026-07-15');
GO

-- Credit / Income Records
INSERT INTO tblCredit (UserID, CategoryID, SubCategoryID, Amount, Description, PaymentID, CreditAt)
VALUES
-- User 1
(1, 1,  1,  35000.00, 'Software Engineer Monthly Salary',          2, '2026-07-31'),
(1, 1,  1,  5000.00,  'Monthly Basic Salary advance',              1, '2026-03-31'),
(1, 10, 37, 300.00,   'Short-term Loan from Friend',               4, '2026-04-18'),
-- User 2
(2, 2,  5,  14500.00, 'Boutique store weekly sales revenue',       1, '2026-08-01'),
(2, 6,  22, 15.00,    'Cashback Reward on Utility Bill',           4, '2026-06-18'),
(2, 8,  29, 800.00,   'Mid-Year Performance Bonus',                6, '2026-07-07'),
-- User 3
(3, 5,  17, 12500.00, 'Residential flat monthly rent credit',      2, '2026-08-05'),
(3, 5,  17, 1500.00,  'Apartment Rent Received',                   1, '2026-07-18'),
-- User 4
(4, 4,  13, 1200.00,  'Landing Page Development project fee',      3, '2026-03-29'),
-- User 5: Sneha Roy
(5, 1,  2,  450.00,   'Weekend Overtime Pay',                      10,'2026-08-03'),
(5, 3,  9,  250.00,   'Stock Dividend credited',                   2, '2026-12-04'),
(5, 7,  25, 2000.00,  'Semester Academic Merit Scholarship',       7, '2026-05-14'),
(5, 12, 43, 3500.00,  'Freelance project consulting fee',          10,'2026-07-20'),
(5, 12, 43, 1200.00,  'Sold old college textbooks',                1, '2026-08-01'),
(5, 1,  1,  22000.00, 'Monthly Stipend and Research Allowance',    2, '2026-07-30'),
(5, 1,  4,  1500.00,  'Project completion incentive bonus',        10,'2026-08-02'),
(5, 4,  13, 4500.00,  'Frontend React project milestone payment',  10,'2026-08-05'),
(5, 4,  14, 2800.00,  'Logo and Banner design for client',         10,'2026-07-12'),
(5, 4,  16, 3200.00,  'YouTube video editing project',             2, '2026-07-25'),
(5, 3,  11, 4200.00,  'Fixed deposit quarterly interest payout',   2, '2026-06-30'),
(5, 6,  21, 150.00,   'Credit Card reward points cash convert',    3, '2026-07-05'),
(5, 6,  23, 75.00,    'Google Pay Rewards Cashback',               10,'2026-08-02'),
(5, 8,  30, 2500.00,  'Independence Day festival bonus',           10,'2026-08-08'),
(5, 9,  34, 890.00,   'Amazon product return refund credited',     4, '2026-07-14'),
(5, 7,  26, 5000.00,  'State Higher Education Support Grant',      2, '2026-06-15'),
(5, 2,  6,  3000.00,  'Technical consultation service charge',     10,'2026-07-18');
GO

-- =========================================================================
-- Lent Records (using subquery for PersonID — no hardcoded IDs)
-- =========================================================================

INSERT INTO tblLent (UserID, PersonID, PaymentID, StatusID, Amount, ReturnedAmount, RemainingAmount, LentAt, DeadlineAt, Description)
VALUES
-- User 1
(1, (SELECT PersonID FROM tblPersons WHERE UserID=1 AND PersonName='Aarav Sharma'),
    2, 2, 5000.00, 5000.00, 0.00, '2026-01-15', '2026-02-15', 'Emergency medical expense loan'),
(1, (SELECT PersonID FROM tblPersons WHERE UserID=1 AND PersonName='Sourav Das'),
    10, 1, 3500.00, 0.00, 3500.00, '2026-07-02', '2026-09-20', 'Lent for laptop repair advance'),

-- User 2
(2, (SELECT PersonID FROM tblPersons WHERE UserID=2 AND PersonName='Priya Sharma'),
    10, 1, 2500.00, 0.00, 2500.00, '2026-06-01', '2026-08-15', 'Laptop repair assistance'),
(2, (SELECT PersonID FROM tblPersons WHERE UserID=2 AND PersonName='Kabir Mehta'),
    6, 2, 1200.00, 1200.00, 0.00, '2026-06-10', '2026-07-10', 'Travel ticket booking advance'),

-- User 3
(3, (SELECT PersonID FROM tblPersons WHERE UserID=3 AND PersonName='Ananya Iyer'),
    10, 3, 3000.00, 0.00, 3000.00, '2026-06-20', '2026-07-20', 'Course registration fee loan'),
(3, (SELECT PersonID FROM tblPersons WHERE UserID=3 AND PersonName='Diya Banerjee'),
    2, 2, 5000.00, 5000.00, 0.00, '2026-07-05', '2026-08-01', 'College fest sponsorship lent'),

-- User 4
(4, (SELECT PersonID FROM tblPersons WHERE UserID=4 AND PersonName='Rohan Verma'),
    2, 1, 6000.00, 0.00, 6000.00, '2026-04-05', '2026-09-01', 'Home renovation short-term loan'),
(4, (SELECT PersonID FROM tblPersons WHERE UserID=4 AND PersonName='Neha Kapoor'),
    7, 5, 10000.00, 4000.00, 6000.00, '2026-04-10', '2026-08-10', 'Small business start-up loan'),

-- User 5: Sneha Roy
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Vikram Patel'),
    1, 2, 15000.00, 15000.00, 0.00, '2026-03-01', '2026-06-01', 'Security deposit assistance'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Siddharth Joshi'),
    9, 3, 3500.00, 500.00, 3000.00, '2026-03-10', '2026-05-10', 'Personal emergency cash advance'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Vikram Patel'),
    10, 1, 2000.00, 0.00, 2000.00, '2026-07-12', '2026-08-25', 'Lent for college semester textbook purchase'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Siddharth Joshi'),
    2, 2, 6000.00, 6000.00, 0.00, '2026-06-05', '2026-07-10', 'Lent for urgent smartphone screen repair'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Vikram Patel'),
    10, 5, 10000.00, 3000.00, 7000.00, '2026-05-15', '2026-09-05', 'Lent for hostel room advance deposit'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Siddharth Joshi'),
    8, 3, 2500.00, 0.00, 2500.00, '2026-04-01', '2026-05-30', 'Lent for monthly coaching fee gap');
GO

-- =========================================================================
-- Borrow Records (using subquery for PersonID — no hardcoded IDs)
-- =========================================================================

INSERT INTO tblBorrow (UserID, PersonID, PaymentID, StatusID, Amount, PaidAmount, RemainingAmount, BorrowAt, DeadlineAt, Description)
VALUES
-- User 1
(1, (SELECT PersonID FROM tblPersons WHERE UserID=1 AND PersonName='Aarav Sharma'),
    1, 2, 2000.00, 2000.00, 0.00, '2026-01-20', '2026-02-20', 'Borrowed for college semester fee gap'),
(1, (SELECT PersonID FROM tblPersons WHERE UserID=1 AND PersonName='Sourav Das'),
    10, 2, 4000.00, 4000.00, 0.00, '2026-06-10', '2026-07-10', 'Borrowed for bike insurance renewal'),

-- User 2
(2, (SELECT PersonID FROM tblPersons WHERE UserID=2 AND PersonName='Priya Sharma'),
    10, 1, 1500.00, 0.00, 1500.00, '2026-06-05', '2026-08-20', 'Borrowed for medical store bill'),
(2, (SELECT PersonID FROM tblPersons WHERE UserID=2 AND PersonName='Kabir Mehta'),
    9, 2, 3500.00, 3500.00, 0.00, '2026-06-12', '2026-07-15', 'Borrowed for festival shopping advance'),

-- User 3
(3, (SELECT PersonID FROM tblPersons WHERE UserID=3 AND PersonName='Ananya Iyer'),
    10, 3, 4500.00, 0.00, 4500.00, '2026-06-15', '2026-07-15', 'Borrowed for hostel mess advance'),
(3, (SELECT PersonID FROM tblPersons WHERE UserID=3 AND PersonName='Diya Banerjee'),
    10, 2, 2200.00, 2200.00, 0.00, '2026-07-18', '2026-08-05', 'Borrowed for conference registration fee'),

-- User 4
(4, (SELECT PersonID FROM tblPersons WHERE UserID=4 AND PersonName='Rohan Verma'),
    1, 1, 8000.00, 0.00, 8000.00, '2026-04-12', '2026-09-10', 'Borrowed for house repair expense'),
(4, (SELECT PersonID FROM tblPersons WHERE UserID=4 AND PersonName='Neha Kapoor'),
    9, 5, 30000.00, 12000.00, 18000.00, '2026-05-05', '2026-11-01', 'Borrowed for high-end graphic workstation'),

-- User 5: Sneha Roy
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Vikram Patel'),
    7, 2, 10000.00, 10000.00, 0.00, '2026-03-05', '2026-06-05', 'Borrowed for vehicle maintenance cost'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Siddharth Joshi'),
    9, 3, 2500.00, 500.00, 2000.00, '2026-03-15', '2026-05-15', 'Borrowed for urgent travel booking'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Vikram Patel'),
    10, 1, 1500.00, 0.00, 1500.00, '2026-07-10', '2026-08-30', 'Borrowed for semester exam registration fee gap'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Siddharth Joshi'),
    2, 2, 8500.00, 8500.00, 0.00, '2026-06-01', '2026-07-15', 'Borrowed for new smartphone emergency purchase'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Vikram Patel'),
    10, 5, 12000.00, 4000.00, 8000.00, '2026-05-20', '2026-09-01', 'Borrowed for laptop RAM and SSD upgrade'),
(5, (SELECT PersonID FROM tblPersons WHERE UserID=5 AND PersonName='Siddharth Joshi'),
    8, 3, 3000.00, 0.00, 3000.00, '2026-04-05', '2026-06-05', 'Borrowed for hostel mess advance payment');
GO

-- =========================================================================
-- Task Records
-- =========================================================================

INSERT INTO tblTask (UserID, PriorityID, TaskStatusID, TaskTitle, Deadline, CreatedAt)
VALUES
-- User 1
(1, 3, 3, 'Complete Semester Project Report',              '2026-01-25', '2026-01-12 09:00:00'),
(1, 2, 2, 'Review Computer Networks Notes',               '2026-02-10', '2026-01-15 10:00:00'),
-- User 2
(2, 3, 1, 'Complete Computer Networks Practical Record',   '2026-06-15', '2026-05-22 08:00:00'),
(2, 1, 3, 'Return Issued Reference Books to Library',      '2026-05-30', '2026-05-25 09:00:00'),
-- User 3
(3, 2, 2, 'Design Course Evaluation Form UI',              '2026-07-10', '2026-06-18 11:00:00'),
(3, 3, 1, 'Pay Upcoming Semester Examination Fees',        '2026-08-15', '2026-08-05 08:30:00'),
-- User 4
(4, 1, 3, 'Database Schema Backup and Migration',          '2026-04-15', '2026-03-30 09:00:00'),
(4, 3, 2, 'Pay Upcoming Semester Tuition Fees',            '2026-05-10', '2026-04-02 10:00:00'),
-- User 5: Sneha Roy
(5, 2, 1, 'Laptop Repair',                                 '2026-03-20', '2026-03-01 08:00:00'),
(5, 1, 3, 'Review Mathematics III Linear Algebra Lectures','2026-03-05', '2026-03-02 09:00:00'),
(5, 1, 1, 'Setup Local Development Environment',           '2026-07-05', '2026-06-10 09:00:00'),
(5, 2, 2, 'Design Database Schema',                        '2026-07-08', '2026-06-10 10:30:00'),
(5, 3, 1, 'Submit Assignment to College Portal',           '2026-07-10', '2026-06-11 08:00:00'),
(5, 1, 3, 'Read Chapter 4 - OS Concepts',                  '2026-07-12', '2026-06-11 11:00:00'),
(5, 2, 1, 'Fix Login Page UI Bug',                         '2026-07-14', '2026-06-12 09:15:00'),
(5, 3, 2, 'Prepare Presentation Slides',                   '2026-07-16', '2026-06-12 14:00:00'),
(5, 1, 1, 'Call Internet Service Provider',                '2026-07-18', '2026-06-13 10:00:00'),
(5, 2, 3, 'Push Code to GitHub',                           '2026-07-20', '2026-06-13 15:30:00'),
(5, 3, 1, 'Complete React Project Module',                 '2026-07-22', '2026-06-14 09:00:00'),
(5, 1, 2, 'Write Unit Tests for BLL Layer',                '2026-07-25', '2026-06-14 11:45:00'),
(5, 2, 1, 'Review Pull Requests',                          '2026-07-28', '2026-06-15 13:00:00'),
(5, 3, 2, 'Deploy App to Staging Server',                  '2026-07-30', '2026-06-15 16:00:00');
GO

-- =========================================================================
-- Note Records (CreatedAt always provided — no missing datetime)
-- =========================================================================

INSERT INTO tblNote (UserID, NotePriorityID, NoteColorID, NoteTitle, Description, CreatedAt)
VALUES
-- User 1
(1, 2, 1, 'Project Meeting Notes',
 'Discussed project architecture sprint milestones and deliverables.',
 '2026-01-15 09:00:00'),
(1, 1, 3, 'Shopping List',
 'Buy groceries fresh vegetables snacks and household essentials.',
 '2026-01-18 10:00:00'),

-- User 2
(2, 3, 1, 'Exam Preparation',
 'Review key core syllabus concepts previous papers and practice problems.',
 '2026-05-22 08:30:00'),
(2, 1, 4, 'Workout Plan',
 'Morning cardio strength training routine and weekend core exercises.',
 '2026-05-28 07:00:00'),

-- User 3
(3, 2, 5, 'React Ideas',
 'Brainstorm UI components state management setup and custom hooks.',
 '2026-06-18 11:00:00'),
(3, 3, 2, 'Birthday Reminder',
 'Order custom cake organize gifts and plan surprise party gathering.',
 '2026-06-20 12:00:00'),

-- User 4
(4, 1, 6, 'Office Tasks',
 'Clear pending emails update task status board and complete documentation.',
 '2026-03-30 09:00:00'),
(4, 2, 7, 'Travel Plan',
 'Book flight tickets reserve hotel stays and prepare daily sightseeing itinerary.',
 '2026-04-05 10:00:00'),

-- User 5: Sneha Roy
(5, 3, 1, 'Daily Goals',
 'Finish code refactoring complete 2 design tasks and read technical blogs.',
 '2026-03-01 08:00:00'),
(5, 1, 8, 'Movie Watchlist',
 'Spider Man The New Arrival and Avengers sequel.',
 '2026-03-04 20:00:00'),
(5, 1, 1, 'API Integration Notes',
 'REST API endpoints for authentication and user management. Use JWT tokens for authorization.',
 '2026-06-10 09:00:00'),
(5, 2, 2, 'Bug Tracker Ideas',
 'Create a simple bug tracking system with priority levels, assign developers, and track resolution status.',
 '2026-06-12 10:00:00'),
(5, 1, 3, 'Weekly Study Plan',
 'Monday: OS, Tuesday: DBMS, Wednesday: CN, Thursday: SE, Friday: COA, Weekend: Revision.',
 '2026-06-15 08:00:00'),
(5, 3, 4, 'Grocery List This Week',
 'Milk, eggs, bread, butter, vegetables, fruits, rice, dal, cooking oil, spices.',
 '2026-06-18 11:00:00'),
(5, 2, 5, 'WinForms UI Tips',
 'Use owner-draw for comboboxes. Apply double buffering on DataGridView. Use TableLayoutPanel for responsive layouts.',
 '2026-06-20 14:00:00'),
(5, 1, 6, 'Interview Preparation',
 'Review data structures, sorting algorithms, SQL joins, normalization, and system design basics.',
 '2026-07-01 09:00:00'),
(5, 3, 7, 'Books to Read',
 'Clean Code by Robert Martin, The Pragmatic Programmer, Design Patterns by GoF, SICP.',
 '2026-07-05 10:00:00'),
(5, 2, 8, 'Project Deadline Reminders',
 'Semester project due: 30th July. Internship report due: 15th August. Viva date: TBD.',
 '2026-07-08 08:30:00');
GO