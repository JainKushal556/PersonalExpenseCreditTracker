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

INSERT INTO tblExpenseCategory (CategoryName)
VALUES
('Food'),
('Travel'),
('Shopping'),
('Bills'),
('Entertainment'),
('Health'),
('Education'),
('Transportation'),
('Personal Care');
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

 INSERT INTO tblCreditCategory (CategoryName)
VALUES
('Salary'),
('Business'),
('Investment'),
('Gift'),
('Freelancing'),
('Rental'),
('Cashback'),
('Scholarship'),
('Bonus'),
('Refund');
GO

INSERT INTO tblLentPersons (PersonName, PhoneNumber, Address)
VALUES
('Sourav Das', '9876543201', 'Kolkata'),
('Onoya Roy', '9123456701', 'Howrah'),
('Rakesh Sharma', '9988776601', 'Durgapur'),
('Madhuri Sen', '9090909001', 'Siliguri'),
('Vivek Gupta', '9871234501', 'Asansol'),
('Priyanka Paul', '9012345601', 'Midnapore'),
('Abhishek Kumar', '8899776601', 'Malda'),
('Nisha Verma', '9765432109', 'Kharagpur'),
('Karan Singh', '9345678910', 'Barrackpore'),
('Simran Mishra', '9876501200', 'Haldia');
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

INSERT INTO tblNoteStatus (StatusName)
VALUES
('Pending'),
('Complete');
GO

INSERT INTO tblBorrowPersons (PersonName, PhoneNumber, Address)
VALUES
('Rahul Sharma', '9876543210', 'Kolkata'),
('Priya Das', '9123456780', 'Howrah'),
('Amit Roy', '9988776655', 'Durgapur'),
('Sneha Paul', '9090909090', 'Siliguri'),
('Rohan Gupta', '9871234567', 'Asansol'),
('Anjali Singh', '9012345678', 'Midnapore'),
('Vikash Kumar', '8899776655', 'Malda'),
('Neha Verma', '9765432101', 'Kharagpur'),
('Arjun Sen', '9345678901', 'Barrackpore'),
('Pooja Mishra', '9876501234', 'Haldia');
GO

INSERT INTO tblUserProfile (UserID, Name, ProfilePhoto)
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

INSERT INTO tblCreditSubCategory (CategoryID, SubCategoryName)
VALUES
(1, 'Monthly Salary'),
(2, 'Shop Income'),
(3, 'Stock Profit'),
(4, 'Birthday Gift'),
(5, 'Web Development'),
(6, 'House Rent'),
(7, 'Card Cashback'),
(8, 'College Scholarship'),
(9, 'Festival Bonus'),
(10, 'Product Refund');
GO

INSERT INTO tblExpenseSubCategory (CategoryID, SubCategoryName)
VALUES
(1, 'Restaurant'),
(2, 'Bus Fare'),
(3, 'Clothes'),
(4, 'Electricity Bill'),
(5, 'Movie'),
(6, 'Medicine'),
(7, 'Books'),
(8, 'Fuel'),
(9, 'Salon');
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
(UserID, PersonID, PaymentID, StatusID, Amount, DeadlineAt, Description)
VALUES
(1, 1, 1, 1, 5000.00, '2026-06-12', 'Lent money for hospital expense'),
(2, 2, 2, 1, 3000.00, '2026-06-20', 'Short-term personal loan'),
(3, 3, 1, 1, 12000.00, '2026-07-01', 'Business support'),
(4, 4, 9, 2, 2500.00, '2026-06-18', 'Emergency financial help'),
(5, 5, 2, 2, 7000.00, '2026-07-05', 'Education fees'),
(6, 6, 1, 1, 4500.00, '2026-06-25', 'House maintenance'),
(7, 7, 8, 1, 6000.00, '2026-06-30', 'Travel assistance'),
(8, 8, 2, 2, 8500.00, '2026-07-10', 'Family requirement'),
(9, 9, 1, 1, 4000.00, '2026-06-28', 'Festival expenses'),
(10, 10, 10, 1, 9500.00, '2026-07-15', 'Laptop purchase support');
GO

INSERT INTO tblBorrow
(UserID, PersonID, PaymentID, StatusID, Amount, DeadlineAt, Description)
VALUES
(1, 1, 1, 1, 5000.00, '2026-06-10', 'Borrowed for medical expense'),
(2, 2, 2, 1, 2500.00, '2026-06-15', 'Personal loan'),
(3, 3, 1, 1, 10000.00, '2026-07-01', 'Business investment'),
(4, 4, 7, 1, 1500.00, '2026-06-20', 'Emergency cash'),
(5, 5, 2, 1, 7000.00, '2026-07-05', 'Education purpose'),
(6, 6, 8, 1, 3200.00, '2026-06-18', 'House rent'),
(7, 7, 9, 2, 4500.00, '2026-06-25', 'Travel expense'),
(8, 8, 2, 1, 8000.00, '2026-07-10', 'Family support'),
(9, 9, 1, 1, 6000.00, '2026-06-30', 'Festival shopping'),
(10,10,10, 2, 9000.00, '2026-07-15', 'Laptop purchase');
GO

INSERT INTO tblTask (UserID, PriorityID, TaskStatusID, TaskTitle, Deadline)
VALUES
(1, 1, 1, 'Database table create', '2026-06-01'),
(2, 2, 1, 'Pay Electricity Bill', '2026-06-03'),
(3, 3, 2, 'Buy Grocery Items', '2026-06-05'),
(4, 1, 1, 'Design ER Diagram', '2026-06-06'),
(5, 2, 2, 'Update GitHub Repository', '2026-06-07'),
(6, 3, 1, 'Practice Js Programs', '2026-06-08'),
(7, 1, 2, 'Complete Compiler note', '2026-06-10'),
(8, 2, 1, 'Write Documentation', '2026-06-12'),
(9, 3, 2, 'Clean Study Room', '2026-06-14'),
(10, 1, 1, 'Prepare Exam Notes', '2026-06-15');
GO

INSERT INTO tblNote (UserID, NoteStatusID, NoteTitle, Description)
VALUES
(1, 1, 'Project Meeting Notes', 'Discussed project requirements and deadlines.'),
(2, 2, 'Shopping List', 'Buy groceries, vegetables, and snacks for the week.'),
(3, 1, 'Exam Preparation', 'Complete revision of DBMS and Compiler Design topics.'),
(4, 2, 'Workout Plan', 'Morning cardio and evening strength training routine.'),
(5, 1, 'React Ideas', 'Create a task manager UI using React and Tailwind CSS.'),
(6, 2, 'Birthday Reminder', 'Plan surprise birthday party for friend next weekend.'),
(7, 1, 'Office Tasks', 'Submit monthly report and attend team meeting.'),
(8, 2, 'Travel Plan', 'Book train tickets and hotel for vacation trip.'),
(9, 1, 'Daily Goals', 'Finish coding assignment and practice SQL queries.'),
(10, 2, 'Movie Watchlist', 'Add new action and sci-fi movies to watch later.');
GO
