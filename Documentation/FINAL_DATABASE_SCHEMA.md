# 💾 PERSONAL EXPENSE CREDIT TRACKER DATABASE

> Structured Database Documentation for WinForms + SQL Server Project

---

# 📌 USER MODULE

---

## 🟣 TABLE: tblUsers

### Purpose

Stores registration information of users.

| Column Name | Data Type    | Null Allowed | Constraints                | Description                                                         |
| ----------- | ------------ | ------------ | -------------------------- | ------------------------------------------------------------------- |
| UserID      | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for every registered user |
| UserName    | VARCHAR(MAX) | No           | —                          | Stores the full name or display name of the user in the system      |
| CreatedAt   | DATETIME     | No           | —                          | Stores the exact date and time when the user account was created    |

---

## 🟣 TABLE: tblUserProfile

### Purpose

Stores user profile details.

| Column Name  | Data Type      | Constraints                |
| ------------ | -------------- | -------------------------- |
| ProfileID    | INT            | PRIMARY KEY, IDENTITY(1,1) |
| UserID       | INT            | FOREIGN KEY, NOT NULL      |
| Name         | VARCHAR(100)   | NOT NULL                   |
| ProfilePhoto | VARBINARY(MAX) | NULL                       |

---

## 🟣 TABLE: tblUserContact

### Purpose

Stores contact information of users.

| Column Name | Data Type    | Constraints                |
| ----------- | ------------ | -------------------------- |
| ContactID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| UserID      | INT          | FOREIGN KEY, NOT NULL      |
| Email       | VARCHAR(100) | UNIQUE, NOT NULL           |
| PhoneNumber | VARCHAR(15)  | UNIQUE, NOT NULL           |

---

## 🟣 TABLE: tblUserAuthentication

### Purpose

Stores authentication details of users.

| Column Name | Data Type    | Constraints                |
| ----------- | ------------ | -------------------------- |
| AuthID      | INT          | PRIMARY KEY, IDENTITY(1,1) |
| UserID      | INT          | FOREIGN KEY, NOT NULL      |
| Password    | VARCHAR(255) | NOT NULL                   |
| Active      | BOOLEAN      | NOT NULL                   |

---

# 💸 EXPENSE MODULE

---

## 🟢 TABLE: tblExpense

### Purpose

Stores all expense transactions of users.

| Column Name   | Data Type     | Constraints                |
| ------------- | ------------- | -------------------------- |
| ExpenseID     | INT           | PRIMARY KEY, IDENTITY(1,1) |
| UserID        | INT           | FOREIGN KEY, NOT NULL      |
| CategoryID    | INT           | FOREIGN KEY, NOT NULL      |
| SubCategoryID | INT           | FOREIGN KEY, NOT NULL      |
| Amount        | DECIMAL(10,2) | NOT NULL                   |
| Description   | VARCHAR(255)  | NOT NULL                   |
| PaymentID     | INT           | FOREIGN KEY, NOT NULL      |
| ExpenseAt     | DATETIME      | NOT NULL                   |

---

## 🟢 TABLE: tblExpenseCategory

### Purpose

Stores expense categories.

| Column Name  | Data Type    | Constraints                |
| ------------ | ------------ | -------------------------- |
| CategoryID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| CategoryName | VARCHAR(100) | UNIQUE, NOT NULL           |

---

## 🟢 TABLE: tblExpenseSubCategory

### Purpose

Stores expense sub-categories.

| Column Name     | Data Type    | Constraints                |
| --------------- | ------------ | -------------------------- |
| SubCategoryID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| CategoryID      | INT          | FOREIGN KEY, NOT NULL      |
| SubCategoryName | VARCHAR(100) | UNIQUE, NOT NULL           |

---

## 🟢 TABLE: tblPaymentType

### Purpose

Stores different payment methods.

| Column Name | Data Type   | Constraints                |
| ----------- | ----------- | -------------------------- |
| PaymentID   | INT         | PRIMARY KEY, IDENTITY(1,1) |
| PaymentName | VARCHAR(50) | UNIQUE, NOT NULL           |

---

# 💳 CREDIT MODULE

---

## 🔵 TABLE: tblCredit

### Purpose

Stores all credit transactions.

| Column Name   | Data Type     | Constraints                |
| ------------- | ------------- | -------------------------- |
| CreditID      | INT           | PRIMARY KEY, IDENTITY(1,1) |
| UserID        | INT           | FOREIGN KEY, NOT NULL      |
| CategoryID    | INT           | FOREIGN KEY, NOT NULL      |
| SubCategoryID | INT           | FOREIGN KEY, NOT NULL      |
| Amount        | DECIMAL(10,2) | NOT NULL                   |
| Description   | VARCHAR(255)  | NOT NULL                   |
| PaymentID     | INT           | FOREIGN KEY, NOT NULL      |
| Creditat      | DATETIME      | NOT NULL                   |

---

## 🔵 TABLE: tblCreditCategory

### Purpose

Stores credit categories.

| Column Name  | Data Type    | Constraints                |
| ------------ | ------------ | -------------------------- |
| CategoryID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| CategoryName | VARCHAR(100) | UNIQUE, NOT NULL           |

---

## 🔵 TABLE: tblCreditSubCategory

### Purpose

Stores credit sub-categories.

| Column Name     | Data Type    | Constraints                |
| --------------- | ------------ | -------------------------- |
| SubCategoryID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| CategoryID      | INT          | FOREIGN KEY, NOT NULL      |
| SubCategoryName | VARCHAR(100) | UNIQUE, NOT NULL           |

---

# 🤝 LENT & BORROW MODULE

---

## 🟠 TABLE: tblLent

### Purpose

Stores money lent records.

| Column Name | Data Type     | Constraints                |
| ----------- | ------------- | -------------------------- |
| LentID      | INT           | PRIMARY KEY, IDENTITY(1,1) |
| UserID      | INT           | FOREIGN KEY, NOT NULL      |
| PersonID    | INT           | FOREIGN KEY, NOT NULL      |
| PaymentID   | INT           | FOREIGN KEY, NOT NULL      |
| StatusID    | INT           | FOREIGN KEY, NOT NULL      |
| Amount      | DECIMAL(10,2) | NOT NULL                   |
| Lentat      | DATETIME      | NOT NULL                   |
| Returnat    | DATETIME      | NOT NULL                   |
| Description | VARCHAR(255)  | NOT NULL                   |

---

## 🟠 TABLE: tblLentPersons

### Purpose

Stores information about persons involved in lending.

| Column Name | Data Type    | Constraints                |
| ----------- | ------------ | -------------------------- |
| PersonID    | INT          | PRIMARY KEY, IDENTITY(1,1) |
| PersonName  | VARCHAR(100) | NOT NULL                   |
| PhoneNumber | VARCHAR(15)  | NOT NULL                   |
| Address     | VARCHAR(255) | NULL                       |

---

## 🟠 TABLE: tblLentBorrowStatus

### Purpose

Stores lending and borrowing status information.

| Column Name | Data Type   | Constraints                |
| ----------- | ----------- | -------------------------- |
| StatusID    | INT         | PRIMARY KEY, IDENTITY(1,1) |
| StatusName  | VARCHAR(50) | NOT NULL                   |

---

## 🟠 TABLE: tblBorrow

### Purpose

Stores money borrowing records.

| Column Name | Data Type     | Constraints                |
| ----------- | ------------- | -------------------------- |
| BorrowID    | INT           | PRIMARY KEY, IDENTITY(1,1) |
| UserID      | INT           | FOREIGN KEY, NOT NULL      |
| PersonID    | INT           | FOREIGN KEY, NOT NULL      |
| PaymentID   | INT           | FOREIGN KEY, NOT NULL      |
| StatusID    | INT           | FOREIGN KEY, NOT NULL      |
| Amount      | DECIMAL(10,2) | NOT NULL                   |
| Borrowat    | DATETIME      | NOT NULL                   |
| Returnat    | DATETIME      | NOT NULL                   |
| Description | VARCHAR(255)  | NOT NULL                   |

---

## 🟠 TABLE: tblBorrowPersons

### Purpose

Stores information about persons involved in borrowing.

| Column Name | Data Type    | Constraints                |
| ----------- | ------------ | -------------------------- |
| PersonID    | INT          | PRIMARY KEY, IDENTITY(1,1) |
| PersonName  | VARCHAR(100) | NOT NULL                   |
| PhoneNumber | VARCHAR(15)  | NOT NULL                   |
| Address     | VARCHAR(255) | NULL                       |

---

# 📝 TASK MODULE

---

## 🟡 TABLE: tblTask

### Purpose

Stores user task information.

| Column Name | Data Type    | Constraints                |
| ----------- | ------------ | -------------------------- |
| TaskID      | INT          | PRIMARY KEY, IDENTITY(1,1) |
| UserID      | INT          | FOREIGN KEY, NOT NULL      |
| PriorityID  | INT          | FOREIGN KEY, NOT NULL      |
| StatusID    | INT          | FOREIGN KEY, NOT NULL      |
| TaskTitle   | VARCHAR(150) | NOT NULL                   |
| Deadline    | DATE         | NOT NULL                   |

---

## 🟡 TABLE: tblTaskPriorities

### Purpose

Stores task priority levels.

| Column Name  | Data Type   | Constraints                |
| ------------ | ----------- | -------------------------- |
| PriorityID   | INT         | PRIMARY KEY, IDENTITY(1,1) |
| PriorityName | VARCHAR(50) | UNIQUE, NOT NULL           |

---

## 🟡 TABLE: tblTaskStatus

### Purpose

Stores task completion status.

| Column Name    | Data Type   | Constraints                |
| -------------- | ----------- | -------------------------- |
| TaskStatusID   | INT         | PRIMARY KEY, IDENTITY(1,1) |
| TaskStatusName | VARCHAR(50) | UNIQUE, NOT NULL           |

---

# 🗒️ NOTE MODULE

---

## 🟡 TABLE: tblNote

### Purpose

Stores user notes information.

| Column Name | Data Type    | Constraints                |
| ----------- | ------------ | -------------------------- |
| NoteID      | INT          | PRIMARY KEY, IDENTITY(1,1) |
| UserID      | INT          | FOREIGN KEY, NOT NULL      |
| StatusID    | INT          | FOREIGN KEY, NOT NULL      |
| NoteTitle   | VARCHAR(150) | NOT NULL                   |
| Description | VARCHAR(500) | NOT NULL                   |
| Createdat   | DATETIME     | NOT NULL                   |

---

## 🟡 TABLE: tblNoteStatus

### Purpose

Stores note priority or status information.

| Column Name | Data Type   | Constraints                |
| ----------- | ----------- | -------------------------- |
| StatusID    | INT         | PRIMARY KEY, IDENTITY(1,1) |
| StatusName  | VARCHAR(50) | UNIQUE, NOT NULL           |

---

# ✅ GENERAL NOTES

- All tables follow relational database structure.
- All primary keys use `IDENTITY(1,1)`.
- Foreign keys maintain table relationships.
- UNIQUE constraints are used where necessary.
- DATETIME fields are used for transaction tracking.
- Database structure is designed using normalization principles.
