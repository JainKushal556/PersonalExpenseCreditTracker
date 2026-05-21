# ⚙️ DEVELOPMENT WORKFLOW GUIDE

> Personal Expense Credit Tracker Database Project

---

# 📌 STEP 1 — Clone Repository

1. Install GitHub Desktop
2. Clone the repository
3. Open the project folder in VS Code

---

# 📌 STEP 2 — Pull Latest Changes

Before starting work:

1. Open GitHub Desktop
2. Click:
   - Fetch Origin
   - Pull Origin

Always pull latest code before working.

---

# 📌 STEP 3 — Create Database

Open SSMS and run:

```text
Database/Master/initial_db.sql
```

This creates the database:

```text
PersonalExpenseCreditTracker
```

---

# 📌 STEP 4 — Work Only Inside Assigned Folder

## Schema Folder

Path:

```text
Database/Schema/
```

Purpose:
- CREATE TABLE queries only

Example:

```text
users.sql
expense.sql
borrow.sql
```

---

## SeedData Folder

Path:

```text
Database/SeedData/
```

Purpose:
- INSERT INTO sample records

Example:

```text
users_seed.sql
expense_seed.sql
```

---

## Procedures Folder

Path:

```text
Database/Procedures/
```

Purpose:
- CREATE PROCEDURE queries

Example:

```text
sp_add_expense.sql
sp_add_credit.sql
```

---

# 📌 STEP 5 — Write SQL Code

Example:

```sql
CREATE TABLE Users (
    User_ID INT PRIMARY KEY IDENTITY(1,1),
    User_Name VARCHAR(MAX) NOT NULL,
    Created_At DATETIME NOT NULL
);
GO
```

Use:
- PRIMARY KEY
- FOREIGN KEY
- proper constraints

Add `GO` after each table/procedure block.

---

# 📌 STEP 6 — Test Queries Locally

Before pushing:

1. Open SSMS
2. Open your `.sql` file
3. Execute queries
4. Fix errors if any

Never push untested SQL code.

---

# 📌 STEP 7 — Add Sample Data

Each table must contain at least 10 meaningful records.

Example:

```sql
INSERT INTO Users (User_Name)
VALUES
('Kushal'),
('Sujit'),
('Sampriti');
GO
```

---

# 📌 STEP 8 — Commit Changes

In GitHub Desktop:

1. Write commit message
2. Commit changes
3. Push origin

Example commit messages:

✅ Good:
- Added Users table
- Added Expense seed data

❌ Bad:
- update
- done
- final

---

# 📌 STEP 9 — Important Rules

- Do not modify another team's files
- Always pull before work
- Test before push
- Use `.sql` files only
- Maintain naming consistency
- Avoid duplicate tables/files

---

# 📌 STEP 10 — Project Manager Responsibilities

Project Manager handles:
- Database Integration
- master_schema.sql
- Foreign Key Validation
- Final Testing
- GitHub Management
