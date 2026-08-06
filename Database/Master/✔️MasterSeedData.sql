INSERT INTO tblUsers (UserName)
VALUES 
('Sujit Kar'),
('Kushal Jain'),
('Debojyoti Jana'),
('Arindam Sahoo'),
('Deepjyoti Das'),
('Arpita Jana'),
('Sampriti Maity'),
('Rahul Das'),
('Priya Sharma'),
('Anik Paul');
GO

INSERT INTO tblExpenseCategory (UserID, CategoryName, IsDefault, IsActive)
VALUES
(NULL, 'Food', 1, 1),
(NULL, 'Travel', 1, 1),
(NULL, 'Shopping', 1, 1),
(NULL, 'Bills', 1, 1),
(NULL, 'Entertainment', 1, 1),
(NULL, 'Health', 1, 1),
(NULL, 'Education', 1, 1),
(NULL, 'Transportation', 1, 1),
(NULL, 'Personal Care', 1, 1),
(NULL, 'Borrow', 1, 1),
(NULL, 'Tution', 1, 1),
(NULL, 'Lent', 1, 1);
GO

INSERT INTO tblPaymentType (PaymentName)
VALUES
('Cash'),
('UPI'),
('Credit Card'),
('Debit Card'),
('Net Banking'),
('Google Pay'),
('PhonePe'),
('Paytm'),
('Bank Transfer'),
('Cheque');
GO

 INSERT INTO tblCreditCategory (UserID, CategoryName, IsDefault, IsActive)
VALUES
(NULL, 'Salary', 1, 1),
(NULL, 'Business', 1, 1),
(NULL, 'Investment', 1, 1),
(NULL, 'Gift', 1, 1),
(NULL, 'Freelancing', 1, 1),
(NULL, 'Rental', 1, 1),
(NULL, 'Cashback', 1, 1),
(NULL, 'Scholarship', 1, 1),
(NULL, 'Bonus', 1, 1),
(NULL, 'Refund', 1, 1),
(NULL, 'Lent', 1, 1),
(NULL, 'Borrow', 1, 1);
GO

INSERT INTO tblPersons (UserID, PersonName, PhoneNumber, Address)
VALUES
(1, 'Rahul Sharma', '9876543210', 'Kolkata'),
(2, 'Priya Das', '9123456780', 'Howrah'),
(3, 'Amit Roy', '9988776655', 'Durgapur'),
(4, 'Sneha Paul', '9090909090', 'Siliguri'),
(5, 'Rohan Gupta', '9871234567', 'Asansol'),
(6, 'Anjali Singh', '9012345678', 'Midnapore'),
(7, 'Vikash Kumar', '8899776655', 'Malda'),
(8, 'Neha Verma', '9765432101', 'Kharagpur'),
(9, 'Arjun Sen', '9345678901', 'Barrackpore'),
(10, 'Pooja Mishra', '9876501234', 'Haldia');
GO

INSERT INTO tblLentBorrowStatus (StatusName)
VALUES
('Pending'),
('Paid'),
('Overdue'),
('Cancelled'),
('Partially Paid');
GO

INSERT INTO tblTaskPriorities (PriorityName)
VALUES
('Low'),
('Medium'),
('High');
GO

INSERT INTO tblTaskStatus (TaskStatusName)
VALUES
('Pending'),
('Complete');
GO

INSERT INTO tblNotePriorities (NotePriorityName)
VALUES
('Low'),
('Medium'),
('High');
GO

INSERT INTO tblNoteColor (ColorName, ColorHexCode) VALUES
('White', '#FFFFFF'),
('Red', '#FF6B6B'),
('Orange', '#FFB74D'),
('Yellow', '#FDD835'),
('Green', '#81C784'),
('Teal', '#4DB6AC'),
('Blue', '#64B5F6'),
('Purple', '#9575CD'),
('Pink', '#F06292'),
('Grey', '#90A4AE'),
('Lavender', '#BA68C8'),
('Coral', '#FF8A65'),
('Mint', '#80CBC4'),
('Indigo', '#7986CB');
GO

INSERT INTO tblGender (GenderName)
VALUES
('Male'),
('Female'),
('Other');
GO



INSERT INTO tblUserProfile (UserID, FullName, ProfilePhoto)
VALUES
(1, 'Sujit Kar', 0xFFD8FFE000104A46494600010101006000600000),
(2, 'Kushal Jain', 0xFFD8FFE000104A46494600010101006000600001),
(3, 'Debojyoti Jana', 0xFFD8FFE000104A46494600010101006000600002),
(4, 'Arindam Sahoo', 0xFFD8FFE000104A46494600010101006000600003),
(5, 'Deepjyoti Das', 0xFFD8FFE000104A46494600010101006000600004),
(6, 'Arpita Jana', 0xFFD8FFE000104A46494600010101006000600005),
(7, 'Sampriti Maity', 0xFFD8FFE000104A46494600010101006000600006),
(8, 'Rahul Das', 0xFFD8FFE000104A46494600010101006000600007),
(9, 'Priya Sharma', 0xFFD8FFE000104A46494600010101006000600008),
(10, 'Anik Paul', 0xFFD8FFE000104A46494600010101006000600009);
GO

INSERT INTO tblUserContact (UserID, Email, PhoneNumber)
VALUES
(1, 'sujitkar@gmail.com', '7047104672'),
(2, 'kushaljain@gmail.com', '7679673882'),
(3, 'debojyoti@gmail.com', '9382332862'),
(4, 'arindam@gmail.com', '7407528941'),
(5, 'deepjyoti@gmail.com', '9679786764'),
(6, 'arpitajana@gmail.com', '8637574895'),
(7, 'sampriti@gmail.com', '9832790186'),
(8, 'rahuldas@gmail.com', '9547890123'),
(9, 'priyasharma@gmail.com', '8446789012'),
(10, 'anikpaul@gmail.com', '9345678901');
GO

INSERT INTO tblUserAuthentication (UserID, Password, Active)
VALUES
(1, 'Sujit@101', 0),
(2, 'Kushal@202', 0),
(3, 'Debojyoti@303', 0),
(4, 'Arindam@404', 0),
(5, 'Deepjyoti@505', 0),
(6, 'Arpita@606', 0),
(7, 'Sampriti@707', 0),
(8, 'Rahul@808', 0),
(9, 'Priya@909', 0),
(10, 'Anik@111', 0);
GO

INSERT INTO tblCreditSubCategory (CategoryID, UserID, SubCategoryName, IsDefault, IsActive)
VALUES
(1, NULL, 'General', 1, 1),
(2, NULL, 'Shop Income', 1, 1),
(3, NULL, 'Stock Profit', 1, 1),
(4, NULL, 'General', 1, 1),
(5, NULL, 'Web Development', 1, 1),
(6, NULL, 'House Rent', 1, 1),
(7, NULL, 'General', 1, 1),
(8, NULL, 'General', 1, 1),
(9, NULL, 'General', 1, 1),
(10, NULL, 'General', 1, 1),
(11, NULL, 'Lent Returned', 1, 1),
(12, NULL, 'Borrow Received', 1, 1);
GO

INSERT INTO tblExpenseSubCategory (CategoryID, UserID, SubCategoryName, IsDefault, IsActive)
VALUES
(1, NULL, 'Restaurant', 1, 1),
(2, NULL, 'Bus Fare', 1, 1),
(3, NULL, 'Clothes', 1, 1),
(4, NULL, 'Electricity Bill', 1, 1),
(5, NULL, 'Movie', 1, 1),
(6, NULL, 'Medicine', 1, 1),
(7, NULL, 'Books', 1, 1),
(8, NULL, 'Fuel', 1, 1),
(9, NULL, 'Salon', 1, 1),
(10, NULL, 'Borrow Returned', 1, 1),
(11, NULL, 'Pradeep Sir', 1, 1),
(11, NULL, 'Adity Mam', 1, 1),
(11, NULL, 'Sanjay Sir', 1, 1),
(11, NULL, 'PS Sir', 1, 1),
(11, NULL, 'Surja Sir', 1, 1),
(11, NULL, 'Partha Sir', 1, 1),
(12, NULL, 'Lent Given', 1, 1);
GO

INSERT INTO tblExpense
(UserID,CategoryID, SubCategoryID, PaymentID, Amount, Description)
VALUES
(1,1, 1, 1, 350.00, 'Dinner at restaurant'),
(2,2, 2, 2, 120.00, 'Daily bus travel'),
(3,3, 3, 3, 2500.00, 'Purchased new clothes'),
(4,4, 4, 4, 1800.00, 'Monthly electricity payment'),
(5,5, 5, 5, 450.00, 'Movie ticket purchase'),
(6,6, 6, 6, 900.00, 'Medicine from pharmacy'),
(7,7, 7, 7, 1500.00, 'Bought academic books'),
(8,8, 8, 8, 2200.00, 'Bike fuel expense'),
(9,9, 9, 9, 700.00, 'Salon and grooming');
GO

INSERT INTO tblCredit
(UserID,CategoryID, SubCategoryID, PaymentID, Amount, Description)
VALUES
(1,1, 1, 1, 25000.00, 'Monthly salary credited'),
(2,2, 2, 2, 18000.00, 'Income from clothing business'),
(3,3, 3, 3, 5000.00, 'Stock market profit'),
(4,4, 4, 1, 2000.00, 'Birthday gift received'),
(5,5, 5, 2, 7500.00, 'Freelance web development project'),
(6,6, 6, 1, 12000.00, 'House rent received'),
(7,7, 7, 2, 600.00, 'Cashback from online shopping'),
(8,8, 8, 3, 10000.00, 'Scholarship credited'),
(9,9, 9, 1, 3500.00, 'Festival performance bonus'),
(10,10,10, 2, 1500.00, 'Refund from cancelled order');
GO

INSERT INTO tblLent
(UserID, PersonID, PaymentID, StatusID, Amount, ReturnedAmount, RemainingAmount, DeadlineAt, Description)
VALUES
(1, 1, 1, 1, 5000.00, 0.00, 5000.00, '2026-06-12', 'Lent money for hospital expense'),
(2, 2, 2, 1, 3000.00, 0.00, 3000.00, '2026-06-20', 'Short-term personal loan'),
(3, 3, 1, 1, 12000.00, 0.00, 12000.00, '2026-07-01', 'Business support'),
(4, 4, 9, 2, 2500.00, 2500.00, 0.00, '2026-06-18', 'Emergency financial help'),
(5, 5, 2, 2, 7000.00, 7000.00, 0.00, '2026-07-05', 'Education fees'),
(6, 6, 1, 1, 4500.00, 0.00, 4500.00, '2026-06-25', 'House maintenance'),
(7, 7, 8, 1, 6000.00, 0.00, 6000.00, '2026-06-30', 'Travel assistance'),
(8, 8, 2, 2, 8500.00, 8500.00, 0.00, '2026-07-10', 'Family requirement'),
(9, 9, 1, 4, 4000.00, 0.00, 4000.00, '2026-06-28', 'Cancelled lent case'),
(10, 10, 10, 1, 9500.00, 0.00, 9500.00, '2026-07-15', 'Laptop purchase support');
GO

INSERT INTO tblBorrow
(UserID, PersonID, PaymentID, StatusID, Amount, PaidAmount, RemainingAmount, DeadlineAt, Description)
VALUES
(1, 1, 1, 1, 5000.00, 0.00, 5000.00, '2026-06-10', 'Borrowed for medical expense'),
(2, 2, 2, 1, 2500.00, 0.00, 2500.00, '2026-06-15', 'Personal loan'),
(3, 3, 1, 1, 10000.00, 0.00, 10000.00, '2026-07-01', 'Business investment'),
(4, 4, 7, 1, 1500.00, 0.00, 1500.00, '2026-06-20', 'Emergency cash'),
(5, 5, 2, 1, 7000.00, 0.00, 7000.00, '2026-07-05', 'Education purpose'),
(6, 6, 8, 1, 3200.00, 0.00, 3200.00, '2026-06-18', 'House rent'),
(7, 7, 9, 2, 4500.00, 4500.00, 0.00, '2026-06-25', 'Travel expense'),
(8, 8, 2, 1, 8000.00, 0.00, 8000.00, '2026-07-10', 'Family support'),
(9, 9, 1, 4, 6000.00, 0.00, 6000.00, '2026-06-30', 'Cancelled borrow case'),
(10,10,10, 2, 9000.00, 9000.00, 0.00, '2026-07-15', 'Laptop purchase');
GO

INSERT INTO tblTask (UserID, PriorityID, TaskStatusID, TaskTitle, Deadline, CreatedAt)
VALUES
(1, 1, 1, 'Database table create', '2026-06-01', '2026-05-20 09:00:00'),
(2, 2, 1, 'Pay Electricity Bill', '2026-06-03', '2026-05-20 10:00:00'),
(3, 3, 2, 'Buy Grocery Items', '2026-06-05', '2026-05-21 09:30:00'),
(4, 1, 1, 'Design ER Diagram', '2026-06-06', '2026-05-21 11:00:00'),
(5, 2, 2, 'Update GitHub Repository', '2026-06-07', '2026-05-22 09:15:00'),
(6, 3, 1, 'Practice Js Programs', '2026-06-08', '2026-05-22 12:00:00'),
(7, 1, 2, 'Complete Compiler note', '2026-06-10', '2026-05-23 08:45:00'),
(8, 2, 1, 'Write Documentation', '2026-06-12', '2026-05-23 14:20:00'),
(9, 3, 2, 'Clean Study Room', '2026-06-14', '2026-05-24 10:10:00'),
(10, 1, 1, 'Prepare Exam Notes', '2026-06-15', '2026-05-24 16:30:00');
GO

INSERT INTO tblNote (UserID, NotePriorityID, NoteColorID, NoteTitle, Description)
VALUES
(1, 1, 1, 'Project Meeting Notes', 'Discussed project requirements and deadlines.'),
(2, 2, 2, 'Shopping List', 'Buy groceries, vegetables, and snacks for the week.'),
(3, 1, 3, 'Exam Preparation', 'Complete revision of DBMS and Compiler Design topics.'),
(4, 2, 4, 'Workout Plan', 'Morning cardio and evening strength training routine.'),
(5, 1, 5, 'React Ideas', 'Create a task manager UI using React and Tailwind CSS.'),
(6, 2, 6, 'Birthday Reminder', 'Plan surprise birthday party for friend next weekend.'),
(7, 1, 7, 'Office Tasks', 'Submit monthly report and attend team meeting.'),
(8, 2, 8, 'Travel Plan', 'Book train tickets and hotel for vacation trip.'),
(9, 1, 9, 'Daily Goals', 'Finish coding assignment and practice SQL queries.'),
(10, 2, 10, 'Movie Watchlist', 'Add new action and sci-fi movies to watch later.');
GO

-- Two year test data for main tables only. Type/status/priority tables are intentionally not repeated.

INSERT INTO tblUsers (UserName, CreatedAt)
VALUES
('Ritwik Ghosh', '2024-06-05 09:10:00'),
('Moumita Dey', '2024-10-12 11:30:00'),
('Sayan Mondal', '2025-02-18 15:45:00'),
('Tania Biswas', '2025-08-24 18:20:00'),
('Subham Nandi', '2026-01-11 10:05:00');
GO

INSERT INTO tblUserProfile (UserID, FullName, ProfilePhoto)
VALUES
(11, 'Ritwik Ghosh', NULL),
(12, 'Moumita Dey', NULL),
(13, 'Sayan Mondal', NULL),
(14, 'Tania Biswas', NULL),
(15, 'Subham Nandi', NULL);
GO

INSERT INTO tblUserContact (UserID, Email, PhoneNumber)
VALUES
(11, 'ritwik.ghosh@gmail.com', '9000000011'),
(12, 'moumita.dey@gmail.com', '9000000012'),
(13, 'sayan.mondal@gmail.com', '9000000013'),
(14, 'tania.biswas@gmail.com', '9000000014'),
(15, 'subham.nandi@gmail.com', '9000000015');
GO

INSERT INTO tblUserAuthentication (UserID, Password, Active)
VALUES
(11, 'Ritwik@112', 1),
(12, 'Moumita@113', 0),
(13, 'Sayan@114', 0),
(14, 'Tania@115', 0),
(15, 'Subham@116', 0);
GO

INSERT INTO tblPersons (UserID, PersonName, PhoneNumber, Address)
VALUES
(11, 'Debjit Roy', '9222222211', 'Kolkata'),
(12, 'Trisha Paul', '9222222212', 'Howrah'),
(13, 'Aritra Das', '9222222213', 'Durgapur'),
(14, 'Sohini Ghosh', '9222222214', 'Siliguri'),
(15, 'Niloy Dutta', '9222222215', 'Asansol');
GO



INSERT INTO tblExpense
(UserID, CategoryID, SubCategoryID, PaymentID, Amount, Description, ExpenseAt)
VALUES
(1, 1, 1, 1, 420.00, 'Lunch and snacks', '2024-06-10 13:15:00'),
(2, 2, 2, 2, 180.00, 'Bus and local travel', '2024-07-08 09:45:00'),
(3, 3, 3, 3, 3200.00, 'Festival clothes purchase', '2024-08-19 17:20:00'),
(4, 4, 4, 4, 2100.00, 'Electricity bill payment', '2024-09-12 11:05:00'),
(5, 5, 5, 5, 650.00, 'Movie and snacks', '2024-10-21 20:30:00'),
(6, 6, 6, 6, 980.00, 'Doctor and medicine expense', '2024-11-16 18:10:00'),
(7, 7, 7, 7, 1750.00, 'Books and stationery', '2024-12-05 16:40:00'),
(8, 8, 8, 8, 2400.00, 'Fuel refill', '2025-01-14 08:25:00'),
(9, 9, 9, 9, 850.00, 'Personal grooming', '2025-02-09 19:50:00'),
(10, 1, 1, 10, 560.00, 'Dinner with family', '2025-03-18 21:10:00'),
(11, 2, 2, 1, 220.00, 'Train ticket booking', '2025-04-22 07:35:00'),
(12, 3, 3, 2, 2850.00, 'Office wear purchase', '2025-05-11 14:15:00'),
(13, 4, 4, 3, 1950.00, 'Monthly utility bill', '2025-06-07 10:00:00'),
(14, 5, 5, 4, 780.00, 'Weekend entertainment', '2025-07-26 19:15:00'),
(15, 6, 6, 5, 1250.00, 'Health checkup', '2025-08-13 12:40:00'),
(1, 7, 7, 6, 2100.00, 'Course material purchase', '2025-09-09 15:25:00'),
(2, 8, 8, 7, 2600.00, 'Monthly fuel expense', '2025-10-15 08:45:00'),
(3, 9, 9, 8, 720.00, 'Salon expense', '2025-11-04 18:05:00'),
(4, 1, 1, 9, 480.00, 'Breakfast and lunch', '2025-12-20 13:50:00'),
(5, 2, 2, 10, 350.00, 'Intercity travel', '2026-01-17 06:30:00'),
(6, 3, 3, 1, 4100.00, 'Winter clothes purchase', '2026-02-08 17:30:00'),
(7, 4, 4, 2, 2300.00, 'Electricity and water bill', '2026-03-19 11:20:00'),
(8, 5, 5, 3, 900.00, 'Concert ticket', '2026-04-09 20:00:00'),
(9, 6, 6, 4, 1350.00, 'Medicine purchase', '2026-05-06 16:10:00');
GO

INSERT INTO tblCredit
(UserID, CategoryID, SubCategoryID, PaymentID, Amount, Description, CreditAt)
VALUES
(1, 1, 1, 2, 26000.00, 'Monthly salary credited', '2024-06-01 10:00:00'),
(2, 2, 2, 1, 14500.00, 'Business income received', '2024-07-03 12:15:00'),
(3, 3, 3, 9, 6200.00, 'Stock profit credited', '2024-08-13 14:20:00'),
(4, 4, 4, 1, 2500.00, 'Gift received', '2024-09-07 18:30:00'),
(5, 5, 5, 2, 8500.00, 'Freelance payment', '2024-10-18 16:45:00'),
(6, 6, 6, 9, 12500.00, 'House rent received', '2024-11-04 09:15:00'),
(7, 7, 7, 2, 740.00, 'Card cashback', '2024-12-22 20:10:00'),
(8, 8, 8, 3, 11000.00, 'Scholarship received', '2025-01-06 11:35:00'),
(9, 9, 9, 1, 4200.00, 'Festival bonus', '2025-02-14 13:00:00'),
(10, 10, 10, 2, 1800.00, 'Refund credited', '2025-03-09 15:40:00'),
(11, 1, 1, 9, 28000.00, 'Salary credited', '2025-04-01 10:00:00'),
(12, 2, 2, 1, 16000.00, 'Shop income received', '2025-05-05 12:10:00'),
(13, 3, 3, 2, 7100.00, 'Investment profit', '2025-06-17 14:25:00'),
(14, 4, 4, 1, 3000.00, 'Family gift', '2025-07-12 18:10:00'),
(15, 5, 5, 9, 9200.00, 'Project payment', '2025-08-21 16:00:00'),
(1, 6, 6, 1, 13500.00, 'Rental income', '2025-09-03 09:20:00'),
(2, 7, 7, 2, 860.00, 'Online cashback', '2025-10-25 21:05:00'),
(3, 8, 8, 3, 12000.00, 'Education scholarship', '2025-11-11 11:10:00'),
(4, 9, 9, 1, 4800.00, 'Annual bonus', '2025-12-19 13:45:00'),
(5, 10, 10, 2, 2100.00, 'Order refund', '2026-01-08 15:25:00'),
(6, 1, 1, 9, 30000.00, 'Salary credited', '2026-02-01 10:00:00'),
(7, 2, 2, 1, 17500.00, 'Business income', '2026-03-04 12:30:00'),
(8, 3, 3, 2, 7900.00, 'Stock profit', '2026-04-16 14:45:00'),
(9, 4, 4, 1, 3500.00, 'Gift amount received', '2026-05-13 18:05:00');
GO

INSERT INTO tblLent
(UserID, PersonID, PaymentID, StatusID, Amount, ReturnedAmount, RemainingAmount, LentAt, DeadlineAt, Description)
VALUES
(1, 1, 1, 2, 4000.00, 4000.00, 0.00, '2024-06-15 10:00:00', '2024-07-15 10:00:00', 'Lent for medical support'),
(2, 2, 2, 1, 3500.00, 1000.00, 2500.00, '2024-08-20 11:30:00', '2024-10-20 11:30:00', 'Short term lent amount'),
(3, 3, 3, 5, 9000.00, 4500.00, 4500.00, '2024-10-05 15:00:00', '2024-12-05 15:00:00', 'Business help lent amount'),
(4, 4, 4, 2, 2800.00, 2800.00, 0.00, '2024-12-11 09:45:00', '2025-01-11 09:45:00', 'Emergency support'),
(5, 5, 5, 1, 7500.00, 0.00, 7500.00, '2025-02-18 17:10:00', '2025-04-18 17:10:00', 'Education support'),
(6, 6, 6, 5, 5200.00, 2000.00, 3200.00, '2025-04-24 12:25:00', '2025-06-24 12:25:00', 'Home repair support'),
(7, 7, 7, 2, 6100.00, 6100.00, 0.00, '2025-06-09 13:40:00', '2025-07-09 13:40:00', 'Travel support'),
(8, 8, 8, 1, 8300.00, 3000.00, 5300.00, '2025-08-14 16:15:00', '2025-10-14 16:15:00', 'Family support'),
(9, 9, 9, 3, 4600.00, 0.00, 4600.00, '2025-10-28 18:00:00', '2025-12-28 18:00:00', 'Festival support'),
(10, 10, 10, 2, 10000.00, 10000.00, 0.00, '2025-12-06 10:30:00', '2026-01-06 10:30:00', 'Laptop purchase support'),
(11, 11, 1, 1, 5500.00, 0.00, 5500.00, '2026-02-12 14:20:00', '2026-04-12 14:20:00', 'Personal loan support'),
(12, 12, 2, 5, 6800.00, 2500.00, 4300.00, '2026-04-03 11:50:00', '2026-06-03 11:50:00', 'Urgent cash support');
GO

INSERT INTO tblBorrow
(UserID, PersonID, PaymentID, StatusID, Amount, PaidAmount, RemainingAmount, BorrowAt, DeadlineAt, Description)
VALUES
(1, 1, 1, 2, 3000.00, 3000.00, 0.00, '2024-06-18 12:00:00', '2024-07-18 12:00:00', 'Borrowed for medicines'),
(2, 2, 2, 1, 4500.00, 1000.00, 3500.00, '2024-08-22 10:10:00', '2024-10-22 10:10:00', 'Personal borrowing'),
(3, 3, 3, 5, 8000.00, 4000.00, 4000.00, '2024-10-09 15:35:00', '2024-12-09 15:35:00', 'Borrowed for business'),
(4, 4, 4, 2, 2200.00, 2200.00, 0.00, '2024-12-16 09:20:00', '2025-01-16 09:20:00', 'Emergency cash borrowed'),
(5, 5, 5, 1, 7200.00, 0.00, 7200.00, '2025-02-21 17:30:00', '2025-04-21 17:30:00', 'Borrowed for education'),
(6, 6, 6, 5, 3600.00, 1600.00, 2000.00, '2025-04-27 13:00:00', '2025-06-27 13:00:00', 'Borrowed for rent'),
(7, 7, 7, 2, 4800.00, 4800.00, 0.00, '2025-06-12 14:10:00', '2025-07-12 14:10:00', 'Travel borrowing'),
(8, 8, 8, 1, 7600.00, 2500.00, 5100.00, '2025-08-17 16:40:00', '2025-10-17 16:40:00', 'Family need borrowing'),
(9, 9, 9, 3, 6200.00, 0.00, 6200.00, '2025-10-31 18:25:00', '2025-12-31 18:25:00', 'Festival shopping borrowing'),
(10, 10, 10, 2, 9500.00, 9500.00, 0.00, '2025-12-09 10:55:00', '2026-01-09 10:55:00', 'Laptop purchase borrowing'),
(11, 11, 1, 1, 5000.00, 0.00, 5000.00, '2026-02-15 14:45:00', '2026-04-15 14:45:00', 'Borrowed for personal use'),
(12, 12, 2, 5, 6400.00, 2200.00, 4200.00, '2026-04-06 12:05:00', '2026-06-06 12:05:00', 'Urgent cash borrowing');
GO

INSERT INTO tblTask (UserID, PriorityID, TaskStatusID, TaskTitle, Deadline, CreatedAt)
VALUES
(1, 1, 2, 'Review monthly expense', '2024-06-30', '2024-06-01 09:00:00'),
(2, 2, 1, 'Pay travel dues', '2024-08-31', '2024-08-01 10:15:00'),
(3, 3, 2, 'Close business loan note', '2024-10-31', '2024-10-01 11:20:00'),
(4, 1, 1, 'Update bill records', '2024-12-31', '2024-12-01 12:25:00'),
(5, 2, 2, 'Check education payments', '2025-02-28', '2025-02-01 13:30:00'),
(6, 3, 1, 'Follow up rent borrowing', '2025-04-30', '2025-04-01 14:35:00'),
(7, 1, 2, 'Archive travel expense', '2025-06-30', '2025-06-01 15:40:00'),
(8, 2, 1, 'Track family support', '2025-08-31', '2025-08-01 16:45:00'),
(9, 3, 1, 'Check overdue lent amount', '2025-10-31', '2025-10-01 17:50:00'),
(10, 1, 2, 'Close laptop borrowing', '2025-12-31', '2025-12-01 18:55:00'),
(11, 2, 1, 'Review personal lending', '2026-02-28', '2026-02-01 09:30:00'),
(12, 3, 1, 'Follow up urgent cash', '2026-04-30', '2026-04-01 10:45:00');
GO

INSERT INTO tblNote (UserID, NotePriorityID, NoteTitle, Description, CreatedAt)
VALUES
(1, 1, 'June 2024 cash note', 'Checked expense and credit entries for June 2024.', '2024-06-20 09:10:00'),
(2, 2, 'August 2024 follow up', 'Pending borrowed amount needs review.', '2024-08-24 10:20:00'),
(3, 1, 'October 2024 record', 'Business support transaction tracked.', '2024-10-14 11:30:00'),
(4, 2, 'December 2024 reminder', 'Verify emergency support repayment.', '2024-12-18 12:40:00'),
(5, 1, 'February 2025 education note', 'Education related payment added.', '2025-02-25 13:50:00'),
(6, 2, 'April 2025 rent note', 'Rent borrowing is partially paid.', '2025-04-29 15:00:00'),
(7, 1, 'June 2025 travel note', 'Travel support entry completed.', '2025-06-18 16:10:00'),
(8, 2, 'August 2025 family note', 'Family support amount still pending.', '2025-08-22 17:20:00'),
(9, 2, 'October 2025 overdue note', 'Festival support should be checked.', '2025-10-31 18:30:00'),
(10, 1, 'December 2025 laptop note', 'Laptop related borrowing closed.', '2025-12-14 19:40:00'),
(11, 2, 'February 2026 lending note', 'New personal lending is pending.', '2026-02-20 10:05:00'),
(12, 1, 'April 2026 cash note', 'Urgent cash transaction partially settled.', '2026-04-18 11:15:00');
GO

-- Additional test volume for report, filter, and dashboard checks.

INSERT INTO tblExpense
(UserID, CategoryID, SubCategoryID, PaymentID, Amount, Description, ExpenseAt)
VALUES
(10, 7, 7, 5, 1650.00, 'Reference books purchase', '2024-06-25 16:15:00'),
(11, 8, 8, 6, 2700.00, 'Bike servicing and fuel', '2024-07-19 09:25:00'),
(12, 9, 9, 7, 950.00, 'Haircut and grooming', '2024-08-23 18:45:00'),
(13, 1, 1, 8, 620.00, 'Team lunch expense', '2024-09-27 13:30:00'),
(14, 2, 2, 9, 410.00, 'Cab and bus fare', '2024-10-29 20:10:00'),
(15, 3, 3, 10, 3600.00, 'Festive shopping', '2024-11-18 15:50:00'),
(1, 4, 4, 5, 2450.00, 'Quarterly electricity bill', '2024-12-28 11:15:00'),
(2, 5, 5, 6, 1100.00, 'New year event ticket', '2025-01-03 19:40:00'),
(3, 6, 6, 7, 1420.00, 'Clinic visit expense', '2025-02-17 12:05:00'),
(4, 7, 7, 8, 1900.00, 'Exam form and books', '2025-03-22 10:30:00'),
(5, 8, 8, 9, 2550.00, 'Fuel and parking', '2025-04-26 08:20:00'),
(6, 9, 9, 10, 680.00, 'Monthly grooming', '2025-05-30 18:00:00'),
(7, 1, 1, 5, 730.00, 'Cafe meeting', '2025-06-24 17:10:00'),
(8, 2, 2, 6, 520.00, 'Local commute', '2025-07-29 09:35:00'),
(9, 3, 3, 7, 2950.00, 'Office bag purchase', '2025-08-31 14:55:00'),
(10, 4, 4, 8, 2650.00, 'Utility bill payment', '2025-09-23 11:45:00'),
(11, 5, 5, 9, 1250.00, 'Sports event ticket', '2025-10-27 18:20:00'),
(12, 6, 6, 10, 1580.00, 'Medical test expense', '2025-11-29 10:25:00'),
(13, 7, 7, 5, 2350.00, 'Online course material', '2025-12-22 21:05:00'),
(14, 8, 8, 6, 2800.00, 'Long ride fuel expense', '2026-01-28 07:50:00'),
(15, 9, 9, 7, 1050.00, 'Salon package', '2026-02-21 16:35:00'),
(1, 1, 1, 8, 890.00, 'Weekend restaurant bill', '2026-03-26 20:15:00'),
(2, 2, 2, 9, 640.00, 'Office commute', '2026-04-30 09:05:00'),
(3, 3, 3, 10, 3350.00, 'Summer clothes purchase', '2026-05-22 15:30:00');
GO

INSERT INTO tblCredit
(UserID, CategoryID, SubCategoryID, PaymentID, Amount, Description, CreditAt)
VALUES
(10, 5, 5, 4, 7800.00, 'Side project payment', '2024-06-28 14:00:00'),
(11, 6, 6, 5, 14000.00, 'Rental income received', '2024-07-26 09:30:00'),
(12, 7, 7, 6, 920.00, 'Wallet cashback', '2024-08-24 21:15:00'),
(13, 8, 8, 7, 9500.00, 'Training scholarship', '2024-09-29 11:00:00'),
(14, 9, 9, 8, 5200.00, 'Performance bonus', '2024-10-30 13:25:00'),
(15, 10, 10, 9, 2400.00, 'Returned order refund', '2024-11-21 16:45:00'),
(1, 1, 1, 10, 27500.00, 'Salary credited', '2024-12-31 10:00:00'),
(2, 2, 2, 4, 18500.00, 'Business settlement', '2025-01-05 12:40:00'),
(3, 3, 3, 5, 5600.00, 'Mutual fund profit', '2025-02-20 15:10:00'),
(4, 4, 4, 6, 2800.00, 'Anniversary gift', '2025-03-24 18:50:00'),
(5, 5, 5, 7, 9800.00, 'Freelance invoice paid', '2025-04-28 17:30:00'),
(6, 6, 6, 8, 15000.00, 'Monthly rent credited', '2025-05-31 09:10:00'),
(7, 7, 7, 9, 1040.00, 'Shopping cashback', '2025-06-27 20:35:00'),
(8, 8, 8, 10, 13000.00, 'College scholarship', '2025-07-31 11:20:00'),
(9, 9, 9, 4, 5600.00, 'Project bonus', '2025-08-30 13:55:00'),
(10, 10, 10, 5, 2600.00, 'Refund processed', '2025-09-25 16:05:00'),
(11, 1, 1, 6, 31000.00, 'Monthly salary', '2025-10-29 10:00:00'),
(12, 2, 2, 7, 19500.00, 'Business income', '2025-11-30 12:20:00'),
(13, 3, 3, 8, 8400.00, 'Investment return', '2025-12-26 14:35:00'),
(14, 4, 4, 9, 4200.00, 'Gift credited', '2026-01-31 18:15:00'),
(15, 5, 5, 10, 10500.00, 'Freelance project paid', '2026-02-24 17:45:00'),
(1, 6, 6, 4, 14500.00, 'Property rent received', '2026-03-29 09:40:00'),
(2, 7, 7, 5, 1180.00, 'Card reward cashback', '2026-04-30 21:25:00'),
(3, 8, 8, 6, 14000.00, 'Scholarship credited', '2026-05-25 11:55:00');
GO

INSERT INTO tblLent
(UserID, PersonID, PaymentID, StatusID, Amount, ReturnedAmount, RemainingAmount, LentAt, DeadlineAt, Description)
VALUES
(13, 13, 3, 1, 7200.00, 1200.00, 6000.00, '2024-07-04 10:20:00', '2024-09-04 10:20:00', 'Lent for shop repair'),
(14, 14, 4, 2, 3100.00, 3100.00, 0.00, '2024-09-14 15:45:00', '2024-10-14 15:45:00', 'Small emergency loan'),
(15, 15, 5, 5, 8400.00, 3000.00, 5400.00, '2024-11-26 12:35:00', '2025-01-26 12:35:00', 'Family medical support'),
(1, 1, 6, 3, 2500.00, 0.00, 2500.00, '2025-01-18 09:10:00', '2025-02-18 09:10:00', 'Temporary cash help'),
(2, 2, 7, 2, 6600.00, 6600.00, 0.00, '2025-03-08 17:25:00', '2025-05-08 17:25:00', 'Tuition fee support'),
(3, 3, 8, 1, 11800.00, 2800.00, 9000.00, '2025-05-19 11:40:00', '2025-07-19 11:40:00', 'Business cash support'),
(4, 4, 9, 5, 3900.00, 1500.00, 2400.00, '2025-07-07 16:55:00', '2025-09-07 16:55:00', 'Personal support'),
(5, 5, 10, 2, 9200.00, 9200.00, 0.00, '2025-09-17 13:30:00', '2025-10-17 13:30:00', 'Device purchase support'),
(6, 6, 1, 1, 4800.00, 800.00, 4000.00, '2025-11-12 10:05:00', '2026-01-12 10:05:00', 'House shifting support'),
(7, 7, 2, 3, 7000.00, 0.00, 7000.00, '2026-01-23 18:10:00', '2026-03-23 18:10:00', 'Urgent family support'),
(8, 8, 3, 5, 8600.00, 3600.00, 5000.00, '2026-03-11 14:15:00', '2026-05-11 14:15:00', 'Course fee support'),
(9, 9, 4, 1, 5300.00, 0.00, 5300.00, '2026-05-18 12:45:00', '2026-07-18 12:45:00', 'Summer expense support');
GO

INSERT INTO tblBorrow
(UserID, PersonID, PaymentID, StatusID, Amount, PaidAmount, RemainingAmount, BorrowAt, DeadlineAt, Description)
VALUES
(13, 13, 3, 1, 6900.00, 900.00, 6000.00, '2024-07-08 10:50:00', '2024-09-08 10:50:00', 'Borrowed for shop stock'),
(14, 14, 4, 2, 2800.00, 2800.00, 0.00, '2024-09-18 16:10:00', '2024-10-18 16:10:00', 'Small emergency borrowing'),
(15, 15, 5, 5, 7900.00, 2500.00, 5400.00, '2024-11-29 13:05:00', '2025-01-29 13:05:00', 'Medical borrowing'),
(1, 1, 6, 3, 2300.00, 0.00, 2300.00, '2025-01-22 09:35:00', '2025-02-22 09:35:00', 'Temporary cash borrowing'),
(2, 2, 7, 2, 6100.00, 6100.00, 0.00, '2025-03-12 17:50:00', '2025-05-12 17:50:00', 'Tuition fee borrowing'),
(3, 3, 8, 1, 10900.00, 1900.00, 9000.00, '2025-05-23 12:05:00', '2025-07-23 12:05:00', 'Business stock borrowing'),
(4, 4, 9, 5, 3600.00, 1200.00, 2400.00, '2025-07-11 17:15:00', '2025-09-11 17:15:00', 'Personal borrowing'),
(5, 5, 10, 2, 8800.00, 8800.00, 0.00, '2025-09-21 13:55:00', '2025-10-21 13:55:00', 'Device purchase borrowing'),
(6, 6, 1, 1, 4500.00, 500.00, 4000.00, '2025-11-16 10:30:00', '2026-01-16 10:30:00', 'House shifting borrowing'),
(7, 7, 2, 3, 6800.00, 0.00, 6800.00, '2026-01-27 18:35:00', '2026-03-27 18:35:00', 'Family borrowing'),
(8, 8, 3, 5, 8200.00, 3200.00, 5000.00, '2026-03-15 14:40:00', '2026-05-15 14:40:00', 'Course fee borrowing'),
(9, 9, 4, 1, 5000.00, 0.00, 5000.00, '2026-05-21 13:10:00', '2026-07-21 13:10:00', 'Summer expense borrowing');
GO

INSERT INTO tblTask (UserID, PriorityID, TaskStatusID, TaskTitle, Deadline, CreatedAt)
VALUES
(13, 1, 1, 'Check shop repair lending', '2024-09-10', '2024-07-05 10:00:00'),
(14, 2, 2, 'Close emergency lending', '2024-10-20', '2024-09-15 11:10:00'),
(15, 3, 1, 'Review medical support', '2025-01-30', '2024-11-27 12:20:00'),
(1, 1, 1, 'Follow temporary cash', '2025-02-25', '2025-01-19 13:30:00'),
(2, 2, 2, 'Archive tuition payment', '2025-05-15', '2025-03-09 14:40:00'),
(3, 3, 1, 'Follow business support', '2025-07-25', '2025-05-20 15:50:00'),
(4, 1, 1, 'Track personal support', '2025-09-15', '2025-07-08 16:00:00'),
(5, 2, 2, 'Close device support', '2025-10-25', '2025-09-18 17:10:00'),
(6, 3, 1, 'Review shifting support', '2026-01-20', '2025-11-13 18:20:00'),
(7, 1, 1, 'Check family pending', '2026-03-30', '2026-01-24 09:30:00'),
(8, 2, 1, 'Track course support', '2026-05-20', '2026-03-12 10:40:00'),
(9, 3, 1, 'Follow summer borrowing', '2026-07-25', '2026-05-19 11:50:00');
GO

INSERT INTO tblNote (UserID, NotePriorityID, NoteColorID, NoteTitle, Description, CreatedAt)
VALUES
(13, 1, 1, 'July 2024 shop note', 'Shop repair lending and borrowing records created.', '2024-07-09 10:15:00'),
(14, 1, 2, 'September 2024 closed note', 'Emergency amount fully settled.', '2024-09-20 11:25:00'),
(15, 2, 3, 'November 2024 medical note', 'Medical support is partially settled.', '2024-11-30 12:35:00'),
(1, 2, 4, 'January 2025 overdue note', 'Temporary cash support needs follow up.', '2025-01-24 13:45:00'),
(2, 1, 5, 'March 2025 tuition note', 'Tuition related support is completed.', '2025-03-14 14:55:00'),
(3, 2, 6, 'May 2025 business note', 'Business support has pending balance.', '2025-05-25 16:05:00'),
(4, 1, 7, 'July 2025 personal note', 'Personal support is partially paid.', '2025-07-13 17:15:00'),
(5, 1, 8, 'September 2025 device note', 'Device support is completed.', '2025-09-23 18:25:00'),
(6, 2, 9, 'November 2025 shifting note', 'House shifting support needs review.', '2025-11-18 19:35:00'),
(7, 2, 10, 'January 2026 family note', 'Family support is overdue.', '2026-01-29 09:45:00'),
(8, 1, 1, 'March 2026 course note', 'Course fee support is partially settled.', '2026-03-17 10:55:00'),
(9, 2, 2, 'May 2026 summer note', 'Summer expense support is pending.', '2026-05-23 12:05:00');
GO


-- =========================================================================
-- EXPLICIT RECENT TEST DATA FOR USER 1 & 2 (FOR EASY UI & FILTER TESTING)
-- =========================================================================

-- Expense
INSERT INTO tblExpense (UserID, CategoryID, SubCategoryID, PaymentID, Amount, Description, ExpenseAt)
VALUES
-- User 1 (Sujit)
(1, 1, 1, 1, 500.00, 'Lunch today (Sujit)', GETDATE()),
(1, 2, 2, 2, 150.00, 'Commute yesterday (Sujit)', DATEADD(day, -1, GETDATE())),
(1, 3, 3, 3, 2500.00, 'Shopping this week (Sujit)', DATEADD(day, -4, GETDATE())),
(1, 4, 4, 1, 1200.00, 'Electricity bill this month (Sujit)', DATEADD(day, -12, GETDATE())),
(1, 6, 6, 2, 8000.00, 'Medical tests this year (Sujit)', DATEADD(day, -45, GETDATE())),
(1, 7, 7, 3, 15000.00, 'Course fee last year (Sujit)', DATEADD(day, -400, GETDATE())),
-- User 2 (Kushal)
(2, 1, 1, 1, 650.00, 'Dinner today (Kushal)', GETDATE()),
(2, 2, 2, 2, 120.00, 'Cab fare yesterday (Kushal)', DATEADD(day, -1, GETDATE())),
(2, 3, 3, 3, 4500.00, 'Gadget purchase this week (Kushal)', DATEADD(day, -3, GETDATE())),
(2, 4, 4, 1, 2200.00, 'Internet bill this month (Kushal)', DATEADD(day, -10, GETDATE())),
(2, 6, 6, 2, 3500.00, 'Dental checkup this year (Kushal)', DATEADD(day, -35, GETDATE())),
(2, 7, 7, 3, 25000.00, 'Tuition payment last year (Kushal)', DATEADD(day, -380, GETDATE()));
GO

-- Credit
INSERT INTO tblCredit (UserID, CategoryID, SubCategoryID, PaymentID, Amount, Description, CreditAt)
VALUES
-- User 1 (Sujit)
(1, 5, 5, 2, 15000.00, 'Freelance payment today (Sujit)', GETDATE()),
(1, 7, 7, 1, 500.00, 'Cashback yesterday (Sujit)', DATEADD(day, -1, GETDATE())),
(1, 4, 4, 2, 3000.00, 'Birthday gift this week (Sujit)', DATEADD(day, -5, GETDATE())),
(1, 1, 1, 9, 45000.00, 'Salary this month (Sujit)', DATEADD(day, -15, GETDATE())),
(1, 9, 9, 2, 12000.00, 'Bonus this year (Sujit)', DATEADD(day, -50, GETDATE())),
(1, 3, 3, 3, 2500.00, 'Mutual fund profit last year (Sujit)', DATEADD(day, -410, GETDATE())),
-- User 2 (Kushal)
(2, 5, 5, 2, 20000.00, 'Freelance project today (Kushal)', GETDATE()),
(2, 7, 7, 1, 750.00, 'App cashback yesterday (Kushal)', DATEADD(day, -1, GETDATE())),
(2, 4, 4, 2, 5000.00, 'Gift from uncle this week (Kushal)', DATEADD(day, -4, GETDATE())),
(2, 1, 1, 9, 55000.00, 'Salary this month (Kushal)', DATEADD(day, -12, GETDATE())),
(2, 9, 9, 2, 15000.00, 'Performance bonus this year (Kushal)', DATEADD(day, -45, GETDATE())),
(2, 3, 3, 3, 3500.00, 'Stock profit last year (Kushal)', DATEADD(day, -390, GETDATE()));
GO

-- Lent
INSERT INTO tblLent (UserID, PersonID, PaymentID, StatusID, Amount, ReturnedAmount, RemainingAmount, LentAt, DeadlineAt, Description)
VALUES
-- User 1 (Sujit)
(1, 1, 1, 1, 2000.00, 0.00, 2000.00, GETDATE(), DATEADD(day, 5, GETDATE()), 'Lent today (Sujit)'),
(1, 2, 2, 5, 5000.00, 1500.00, 3500.00, DATEADD(day, -4, GETDATE()), DATEADD(day, 10, GETDATE()), 'Lent this week (Sujit)'),
(1, 3, 3, 2, 6000.00, 6000.00, 0.00, DATEADD(day, -45, GETDATE()), DATEADD(day, -15, GETDATE()), 'Lent fully paid (Sujit)'),
(1, 4, 1, 3, 3000.00, 0.00, 3000.00, DATEADD(day, -35, GETDATE()), DATEADD(day, -5, GETDATE()), 'Lent overdue (Sujit)'),
-- User 2 (Kushal)
(2, 1, 1, 1, 3000.00, 0.00, 3000.00, GETDATE(), DATEADD(day, 7, GETDATE()), 'Lent today (Kushal)'),
(2, 2, 2, 5, 8000.00, 2000.00, 6000.00, DATEADD(day, -3, GETDATE()), DATEADD(day, 12, GETDATE()), 'Lent this week (Kushal)'),
(2, 3, 3, 2, 10000.00, 10000.00, 0.00, DATEADD(day, -50, GETDATE()), DATEADD(day, -20, GETDATE()), 'Lent fully paid (Kushal)'),
(2, 4, 1, 3, 4000.00, 0.00, 4000.00, DATEADD(day, -40, GETDATE()), DATEADD(day, -10, GETDATE()), 'Lent overdue (Kushal)');
GO

-- Borrow
INSERT INTO tblBorrow (UserID, PersonID, PaymentID, StatusID, Amount, PaidAmount, RemainingAmount, BorrowAt, DeadlineAt, Description)
VALUES
-- User 1 (Sujit)
(1, 1, 1, 1, 1200.00, 0.00, 1200.00, GETDATE(), DATEADD(day, 3, GETDATE()), 'Borrowed today (Sujit)'),
(1, 2, 2, 5, 3500.00, 1000.00, 2500.00, DATEADD(day, -4, GETDATE()), DATEADD(day, 7, GETDATE()), 'Borrowed this week (Sujit)'),
(1, 3, 3, 2, 4000.00, 4000.00, 0.00, DATEADD(day, -45, GETDATE()), DATEADD(day, -15, GETDATE()), 'Borrowed fully paid (Sujit)'),
(1, 4, 1, 3, 2500.00, 0.00, 2500.00, DATEADD(day, -35, GETDATE()), DATEADD(day, -5, GETDATE()), 'Borrowed overdue (Sujit)'),
-- User 2 (Kushal)
(2, 1, 1, 1, 1500.00, 0.00, 1500.00, GETDATE(), DATEADD(day, 4, GETDATE()), 'Borrowed today (Kushal)'),
(2, 2, 2, 5, 5000.00, 1500.00, 3500.00, DATEADD(day, -3, GETDATE()), DATEADD(day, 8, GETDATE()), 'Borrowed this week (Kushal)'),
(2, 3, 3, 2, 7000.00, 7000.00, 0.00, DATEADD(day, -50, GETDATE()), DATEADD(day, -20, GETDATE()), 'Borrowed fully paid (Kushal)'),
(2, 4, 1, 3, 3000.00, 0.00, 3000.00, DATEADD(day, -40, GETDATE()), DATEADD(day, -10, GETDATE()), 'Borrowed overdue (Kushal)');
GO

-- =========================================================================
-- LARGE SEED DATA GENERATION FOR LOAD TESTING AND EDGE CASES
-- =========================================================================
DECLARE @i INT = 1;
WHILE @i <= 500
BEGIN
    -- Expense
    INSERT INTO tblExpense (UserID, CategoryID, SubCategoryID, PaymentID, Amount, Description, ExpenseAt)
    VALUES (
        (@i % 15) + 1, 
        (@i % 10) + 1, 
        (@i % 10) + 1, 
        (@i % 10) + 1, 
        100.00 + (@i % 500) * 2.50, 
        'Bulk Expense Entry ' + CAST(@i AS VARCHAR), 
        DATEADD(day, -(@i % 730), GETDATE())
    );

    -- Credit
    INSERT INTO tblCredit (UserID, CategoryID, SubCategoryID, PaymentID, Amount, Description, CreditAt)
    VALUES (
        (@i % 15) + 1, 
        (@i % 11) + 1, 
        (@i % 11) + 1, 
        (@i % 10) + 1, 
        500.00 + (@i % 500) * 5.00, 
        'Bulk Credit Entry ' + CAST(@i AS VARCHAR), 
        DATEADD(day, -(@i % 730), GETDATE())
    );

    -- Lent
    DECLARE @lentAmount DECIMAL(10,2) = 1000.00 + (@i % 100) * 10;
    DECLARE @lentReturned DECIMAL(10,2) = CASE 
        WHEN (@i % 5) + 1 = 1 THEN 0.00 -- Pending
        WHEN (@i % 5) + 1 = 2 THEN @lentAmount -- Paid
        WHEN (@i % 5) + 1 = 3 THEN 0.00 -- Overdue
        WHEN (@i % 5) + 1 = 4 THEN 0.00 -- Cancelled
        ELSE @lentAmount / 2 -- Partially Paid
    END;

    INSERT INTO tblLent (UserID, PersonID, PaymentID, StatusID, Amount, ReturnedAmount, RemainingAmount, LentAt, DeadlineAt, Description)
    VALUES (
        (@i % 15) + 1, 
        (@i % 15) + 1, 
        (@i % 10) + 1, 
        (@i % 5) + 1, 
        @lentAmount, 
        @lentReturned, 
        @lentAmount - @lentReturned, 
        DATEADD(day, -((@i % 300) + 30), GETDATE()), 
        CASE 
            WHEN (@i % 5) + 1 = 3 THEN DATEADD(day, -(@i % 30) - 1, GETDATE()) -- Overdue: deadline in past
            ELSE DATEADD(day, (@i % 30) + 1, GETDATE()) -- Future deadline
        END, 
        'Bulk Lent Entry ' + CAST(@i AS VARCHAR)
    );

    -- Borrow
    DECLARE @borrowAmount DECIMAL(10,2) = 500.00 + (@i % 100) * 10;
    DECLARE @borrowPaid DECIMAL(10,2) = CASE 
        WHEN (@i % 5) + 1 = 1 THEN 0.00 
        WHEN (@i % 5) + 1 = 2 THEN @borrowAmount 
        WHEN (@i % 5) + 1 = 3 THEN 0.00 
        WHEN (@i % 5) + 1 = 4 THEN 0.00 
        ELSE @borrowAmount / 2 
    END;

    INSERT INTO tblBorrow (UserID, PersonID, PaymentID, StatusID, Amount, PaidAmount, RemainingAmount, BorrowAt, DeadlineAt, Description)
    VALUES (
        (@i % 15) + 1, 
        (@i % 15) + 1, 
        (@i % 10) + 1, 
        (@i % 5) + 1, 
        @borrowAmount, 
        @borrowPaid, 
        @borrowAmount - @borrowPaid, 
        DATEADD(day, -((@i % 300) + 30), GETDATE()), 
        CASE 
            WHEN (@i % 5) + 1 = 3 THEN DATEADD(day, -(@i % 30) - 1, GETDATE()) 
            ELSE DATEADD(day, (@i % 30) + 1, GETDATE()) 
        END, 
        'Bulk Borrow Entry ' + CAST(@i AS VARCHAR)
    );

    -- Tasks
    INSERT INTO tblTask (UserID, PriorityID, TaskStatusID, TaskTitle, Deadline, CreatedAt)
    VALUES (
        (@i % 15) + 1, 
        (@i % 3) + 1, 
        (@i % 2) + 1, 
        'Bulk Task ' + CAST(@i AS VARCHAR), 
        DATEADD(day, (@i % 60) - 10, GETDATE()), 
        DATEADD(day, -(@i % 30), GETDATE())
    );

    -- Notes
    INSERT INTO tblNote (UserID, NotePriorityID, NoteTitle, Description, CreatedAt)
    VALUES (
        (@i % 15) + 1, 
        (@i % 2) + 1, 
        'Bulk Note ' + CAST(@i AS VARCHAR), 
        'Detailed description for bulk note entry number ' + CAST(@i AS VARCHAR), 
        DATEADD(day, -(@i % 100), GETDATE())
    );

    SET @i = @i + 1;
END;
GO
