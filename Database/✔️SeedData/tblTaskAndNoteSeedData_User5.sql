-- ================================================
-- TASK SEED DATA -- UserID = 5 (12 records)
-- PriorityID: 1=Low, 2=Medium, 3=High
-- TaskStatusID: 1=Pending, 2=In Progress, 3=Completed
-- ================================================

INSERT INTO tblTask (UserID, PriorityID, TaskStatusID, TaskTitle, Deadline, CreatedAt)
VALUES
(5, 1, 1, 'Setup Local Development Environment',  '2026-07-05', '2026-06-10 09:00:00'),
(5, 2, 2, 'Design Database Schema',               '2026-07-08', '2026-06-10 10:30:00'),
(5, 3, 1, 'Submit Assignment to College Portal',  '2026-07-10', '2026-06-11 08:00:00'),
(5, 1, 3, 'Read Chapter 4 - OS Concepts',         '2026-07-12', '2026-06-11 11:00:00'),
(5, 2, 1, 'Fix Login Page UI Bug',                '2026-07-14', '2026-06-12 09:15:00'),
(5, 3, 2, 'Prepare Presentation Slides',          '2026-07-16', '2026-06-12 14:00:00'),
(5, 1, 1, 'Call Internet Service Provider',       '2026-07-18', '2026-06-13 10:00:00'),
(5, 2, 3, 'Push Code to GitHub',                  '2026-07-20', '2026-06-13 15:30:00'),
(5, 3, 1, 'Complete React Project Module',        '2026-07-22', '2026-06-14 09:00:00'),
(5, 1, 2, 'Write Unit Tests for BLL Layer',       '2026-07-25', '2026-06-14 11:45:00'),
(5, 2, 1, 'Review Pull Requests',                 '2026-07-28', '2026-06-15 13:00:00'),
(5, 3, 2, 'Deploy App to Staging Server',         '2026-07-30', '2026-06-15 16:00:00');


-- ================================================
-- NOTE SEED DATA -- UserID = 5 (8 extra records)
-- NotePriorityID: 1=High, 2=Medium, 3=Low
-- NoteColorID: 1-10 (various colors)
-- ================================================

INSERT INTO tblNote (UserID, NotePriorityID, NoteColorID, NoteTitle, Description)
VALUES
(5, 1, 1, 'API Integration Notes',         'REST API endpoints for authentication and user management. Use JWT tokens for authorization.'),
(5, 2, 2, 'Bug Tracker Ideas',             'Create a simple bug tracking system with priority levels, assign developers, and track resolution status.'),
(5, 1, 3, 'Weekly Study Plan',             'Monday: OS, Tuesday: DBMS, Wednesday: CN, Thursday: SE, Friday: COA, Weekend: Revision.'),
(5, 3, 4, 'Grocery List This Week',        'Milk, eggs, bread, butter, vegetables, fruits, rice, dal, cooking oil, spices.'),
(5, 2, 5, 'WinForms UI Tips',              'Use owner-draw for comboboxes. Apply double buffering on DataGridView. Use TableLayoutPanel for responsive layouts.'),
(5, 1, 6, 'Interview Preparation',         'Review data structures, sorting algorithms, SQL joins, normalization, and system design basics.'),
(5, 3, 7, 'Books to Read',                 'Clean Code by Robert Martin, The Pragmatic Programmer, Design Patterns by GoF, SICP.'),
(5, 2, 8, 'Project Deadline Reminders',    'Semester project due: 30th July. Internship report due: 15th August. Viva date: TBD.');
