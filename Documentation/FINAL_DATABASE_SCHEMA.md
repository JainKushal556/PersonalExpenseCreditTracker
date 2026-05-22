# 💾 PERSONAL EXPENSE CREDIT TRACKER DATABASE

> Structured Database Documentation for WinForms + SQL Server Project

---

# 📌 USER MODULE

---

## 🟣 TABLE: Users

### Purpose
Stores registration information of users.

| Column Name | Data Type    | Constraints                |
|-------------|--------------|----------------------------|
| User_ID     | INT          | PRIMARY KEY, IDENTITY(1,1) |
| User_Name   | VARCHAR(MAX) | NOT NULL                   |
| Created_At  | DATETIME     | NOT NULL                   |

---

## 🟣 TABLE: User_Profile

### Purpose
Stores user profile details.

| Column Name   | Data Type       | Constraints                |
|---------------|-----------------|----------------------------|
| Profile_ID    | INT             | PRIMARY KEY, IDENTITY(1,1) |
| User_ID       | INT             | FOREIGN KEY, NOT NULL      |
| Name          | VARCHAR(100)    | NOT NULL                   |
| Profile_Photo | VARBINARY(MAX)  | NULL                       |

---

## 🟣 TABLE: User_Contact

### Purpose
Stores contact information of users.

| Column Name  | Data Type    | Constraints                |
|--------------|--------------|----------------------------|
| Contact_ID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| User_ID      | INT          | FOREIGN KEY, NOT NULL      |
| Email        | VARCHAR(100) | UNIQUE, NOT NULL           |
| Phone_Number | VARCHAR(15)  | UNIQUE, NOT NULL           |

---

## 🟣 TABLE: User_Authentication

### Purpose
Stores authentication details of users.

| Column Name | Data Type    | Constraints                |
|-------------|--------------|----------------------------|
| Auth_ID     | INT          | PRIMARY KEY, IDENTITY(1,1) |
| User_ID     | INT          | FOREIGN KEY, NOT NULL      |
| Password    | VARCHAR(255) | NOT NULL                   |
| Active      | BOOLEAN      | NOT NULL                   |

---

# 💸 EXPENSE MODULE

---

## 🟢 TABLE: Expense

### Purpose
Stores all expense transactions of users.

| Column Name     | Data Type      | Constraints                |
|-----------------|----------------|----------------------------|
| Expense_ID      | INT            | PRIMARY KEY, IDENTITY(1,1) |
| User_ID         | INT            | FOREIGN KEY, NOT NULL      |
| Category_ID     | INT            | FOREIGN KEY, NOT NULL      |
| Sub_Category_ID | INT            | FOREIGN KEY, NOT NULL      |
| Amount          | DECIMAL(10,2)  | NOT NULL                   |
| Description     | VARCHAR(255)   | NOT NULL                   |
| Payment_ID      | INT            | FOREIGN KEY, NOT NULL      |
| Expense_At      | DATETIME       | NOT NULL                   |

---

## 🟢 TABLE: Expense_Category

### Purpose
Stores expense categories.

| Column Name  | Data Type    | Constraints                |
|--------------|--------------|----------------------------|
| Category_ID  | INT          | PRIMARY KEY, IDENTITY(1,1) |
| Category_Name| VARCHAR(100) | UNIQUE, NOT NULL           |

---

## 🟢 TABLE: Expense_Sub_Category

### Purpose
Stores expense sub-categories.

| Column Name       | Data Type    | Constraints                |
|-------------------|--------------|----------------------------|
| Sub_Category_ID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| Category_ID       | INT          | FOREIGN KEY, NOT NULL      |
| Sub_Category_Name | VARCHAR(100) | UNIQUE, NOT NULL           |

---

## 🟢 TABLE: Payment_Type

### Purpose
Stores different payment methods.

| Column Name | Data Type   | Constraints                |
|-------------|-------------|----------------------------|
| Payment_ID  | INT         | PRIMARY KEY, IDENTITY(1,1) |
| Payment_Name| VARCHAR(50) | UNIQUE, NOT NULL           |

---

# 💳 CREDIT MODULE

---

## 🔵 TABLE: Credit

### Purpose
Stores all credit transactions.

| Column Name     | Data Type      | Constraints                |
|-----------------|----------------|----------------------------|
| Credit_ID       | INT            | PRIMARY KEY, IDENTITY(1,1) |
| User_ID         | INT            | FOREIGN KEY, NOT NULL      |
| Category_ID     | INT            | FOREIGN KEY, NOT NULL      |
| Sub_Category_ID | INT            | FOREIGN KEY, NOT NULL      |
| Amount          | DECIMAL(10,2)  | NOT NULL                   |
| Description     | VARCHAR(255)   | NOT NULL                   |
| Payment_ID      | INT            | FOREIGN KEY, NOT NULL      |
| Credit_at       | DATETIME       | NOT NULL                   |

---

## 🔵 TABLE: Credit_Category

### Purpose
Stores credit categories.

| Column Name  | Data Type    | Constraints                |
|--------------|--------------|----------------------------|
| Category_ID  | INT          | PRIMARY KEY, IDENTITY(1,1) |
| Category_Name| VARCHAR(100) | UNIQUE, NOT NULL           |

---

## 🔵 TABLE: Credit_Sub_Category

### Purpose
Stores credit sub-categories.

| Column Name       | Data Type    | Constraints                |
|-------------------|--------------|----------------------------|
| Sub_Category_ID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| Category_ID       | INT          | FOREIGN KEY, NOT NULL      |
| Sub_Category_Name | VARCHAR(100) | UNIQUE, NOT NULL           |

---

# 🤝 LENT & BORROW MODULE

---

## 🟠 TABLE: Lent

### Purpose
Stores money lent records.

| Column Name | Data Type      | Constraints                |
|-------------|----------------|----------------------------|
| Lent_ID     | INT            | PRIMARY KEY, IDENTITY(1,1) |
| User_ID     | INT            | FOREIGN KEY, NOT NULL      |
| Person_ID   | INT            | FOREIGN KEY, NOT NULL      |
| Payment_ID  | INT            | FOREIGN KEY, NOT NULL      |
| Status_ID   | INT            | FOREIGN KEY, NOT NULL      |
| Amount      | DECIMAL(10,2)  | NOT NULL                   |
| Lent_at     | DATETIME       | NOT NULL                   |
| Return_at   | DATETIME       | NOT NULL                   |
| Description | VARCHAR(255)   | NOT NULL                   |

---

## 🟠 TABLE: Lent_Persons

### Purpose
Stores information about persons involved in lending.

| Column Name | Data Type    | Constraints                |
|-------------|--------------|----------------------------|
| Person_ID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| Person_Name | VARCHAR(100) | NOT NULL                   |
| Phone_Number| VARCHAR(15)  | NOT NULL                   |
| Address     | VARCHAR(255) | NULL                       |

---

## 🟠 TABLE: Lent_Borrow_Status

### Purpose
Stores lending and borrowing status information.

| Column Name | Data Type   | Constraints                |
|-------------|-------------|----------------------------|
| Status_ID   | INT         | PRIMARY KEY, IDENTITY(1,1) |
| Status_Name | VARCHAR(50) | NOT NULL                   |

---

## 🟠 TABLE: Borrow

### Purpose
Stores money borrowing records.

| Column Name | Data Type      | Constraints                |
|-------------|----------------|----------------------------|
| Borrow_ID   | INT            | PRIMARY KEY, IDENTITY(1,1) |
| User_ID     | INT            | FOREIGN KEY, NOT NULL      |
| Person_ID   | INT            | FOREIGN KEY, NOT NULL      |
| Payment_ID  | INT            | FOREIGN KEY, NOT NULL      |
| Status_ID   | INT            | FOREIGN KEY, NOT NULL      |
| Amount      | DECIMAL(10,2)  | NOT NULL                   |
| Borrow_at   | DATETIME       | NOT NULL                   |
| Return_at   | DATETIME       | NOT NULL                   |
| Description | VARCHAR(255)   | NOT NULL                   |

---

## 🟠 TABLE: Borrow_Persons

### Purpose
Stores information about persons involved in borrowing.

| Column Name | Data Type    | Constraints                |
|-------------|--------------|----------------------------|
| Person_ID   | INT          | PRIMARY KEY, IDENTITY(1,1) |
| Person_Name | VARCHAR(100) | NOT NULL                   |
| Phone_Number| VARCHAR(15)  | NOT NULL                   |
| Address     | VARCHAR(255) | NULL                       |

---

# 📝 TASK MODULE

---

## 🟡 TABLE: Task

### Purpose
Stores user task information.

| Column Name | Data Type    | Constraints                |
|-------------|--------------|----------------------------|
| Task_ID     | INT          | PRIMARY KEY, IDENTITY(1,1) |
| User_ID     | INT          | FOREIGN KEY, NOT NULL      |
| Priority_ID | INT          | FOREIGN KEY, NOT NULL      |
| Status_ID   | INT          | FOREIGN KEY, NOT NULL      |
| Task_Title  | VARCHAR(150) | NOT NULL                   |
| Deadline    | DATE         | NOT NULL                   |

---

## 🟡 TABLE: Task_Priorities

### Purpose
Stores task priority levels.

| Column Name | Data Type   | Constraints                |
|-------------|-------------|----------------------------|
| Priority_ID | INT         | PRIMARY KEY, IDENTITY(1,1) |
| Priority_Name| VARCHAR(50)| UNIQUE, NOT NULL           |

---

## 🟡 TABLE: Task_Status

### Purpose
Stores task completion status.

| Column Name | Data Type   | Constraints                |
|-------------|-------------|----------------------------|
| Status_ID   | INT         | PRIMARY KEY, IDENTITY(1,1) |
| Status_Name | VARCHAR(50) | UNIQUE, NOT NULL           |

---

# 🗒️ NOTE MODULE

---

## 🟡 TABLE: Note

### Purpose
Stores user notes information.

| Column Name | Data Type    | Constraints                |
|-------------|--------------|----------------------------|
| Note_ID     | INT          | PRIMARY KEY, IDENTITY(1,1) |
| User_ID     | INT          | FOREIGN KEY, NOT NULL      |
| Status_ID   | INT          | FOREIGN KEY, NOT NULL      |
| Note_Title  | VARCHAR(150) | NOT NULL                   |
| Description | VARCHAR(500) | NOT NULL                   |
| Created_at  | DATETIME     | NOT NULL                   |

---

## 🟡 TABLE: Note_Status

### Purpose
Stores note priority or status information.

| Column Name | Data Type   | Constraints                |
|-------------|-------------|----------------------------|
| Status_ID   | INT         | PRIMARY KEY, IDENTITY(1,1) |
| Status_Name | VARCHAR(50) | UNIQUE, NOT NULL           |

---

# ✅ GENERAL NOTES

- All tables follow relational database structure.
- All primary keys use `IDENTITY(1,1)`.
- Foreign keys maintain table relationships.
- UNIQUE constraints are used where necessary.
- DATETIME fields are used for transaction tracking.
- Database structure is designed using normalization principles.