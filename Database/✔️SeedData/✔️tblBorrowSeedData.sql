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
(9, 9, 1, 1, 6000.00, 0.00, 6000.00, '2026-06-30', 'Festival shopping'),
(10,10,10, 2, 9000.00, 9000.00, 0.00, '2026-07-15', 'Laptop purchase');
