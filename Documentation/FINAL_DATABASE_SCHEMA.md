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

| Column Name  | Data Type      | Null Allowed | Constraints                | Description                                                            |
| ------------ | -------------- | ------------ | -------------------------- | ---------------------------------------------------------------------- |
| ProfileID    | INT            | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each user profile record |
| UserID       | INT            | No           | FOREIGN KEY                | Stores the reference ID of the user associated with this profile       |
| Name         | VARCHAR(MAX)   | No           | —                          | Stores the full name or profile display name of the user               |
| ProfilePhoto | VARBINARY(MAX) | Yes          | —                          | Stores the profile image of the user in binary format if uploaded      |

---

## 🟣 TABLE: tblUserContact

### Purpose

Stores contact information of users.

| Column Name | Data Type    | Null Allowed | Constraints                | Description                                                                   |
| ----------- | ------------ | ------------ | -------------------------- | ----------------------------------------------------------------------------- |
| ContactID   | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each contact information record |
| UserID      | INT          | No           | FOREIGN KEY                | Stores the reference ID of the user associated with this contact information  |
| Email       | VARCHAR(100) | No           | UNIQUE                     | Stores the unique email address used by the user for communication or login   |
| PhoneNumber | VARCHAR(15)  | No           | UNIQUE                     | Stores the unique phone number of the user for contact purposes               |

---

## 🟣 TABLE: tblUserAuthentication

### Purpose

Stores authentication details of users.

| Column Name | Data Type    | Null Allowed | Constraints                | Description                                                                    |
| ----------- | ------------ | ------------ | -------------------------- | ------------------------------------------------------------------------------ |
| AuthID      | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each authentication record       |
| UserID      | INT          | No           | FOREIGN KEY                | Stores the reference ID of the user associated with this authentication record |
| Password    | VARCHAR(255) | No           | —                          | Stores the password information used for user account authentication           |
| Active      | BIT          | No           | —                          | Stores the current active or inactive status of the user account               |

---

# 💸 EXPENSE MODULE

---

## 🟢 TABLE: tblExpense

### Purpose

Stores all expense transactions of users.

| Column Name   | Data Type     | Null Allowed | Constraints                | Description                                                              |
| ------------- | ------------- | ------------ | -------------------------- | ------------------------------------------------------------------------ |
| ExpenseID     | INT           | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each expense transaction   |
| UserID        | INT           | No           | FOREIGN KEY                | Stores the ID of the user who created or owns this expense record        |
| CategoryID    | INT           | No           | FOREIGN KEY                | Stores the reference ID of the main expense category for this expense    |
| SubCategoryID | INT           | No           | FOREIGN KEY                | Stores the reference ID of the sub-category related to this expense      |
| Amount        | DECIMAL(10,2) | No           | —                          | Stores the total amount spent in this expense transaction                |
| Description   | VARCHAR(MAX)  | No           | —                          | Stores additional details or notes related to the expense transaction    |
| PaymentID     | INT           | No           | FOREIGN KEY                | Stores the reference ID of the payment method used for this expense      |
| ExpenseAt     | DATETIME      | No           | —                          | Stores the exact date and time when the expense transaction was recorded |

---

## 🟢 TABLE: tblExpenseCategory

### Purpose

Stores expense categories.

| Column Name  | Data Type    | Null Allowed | Constraints                | Description                                                                   |
| ------------ | ------------ | ------------ | -------------------------- | ----------------------------------------------------------------------------- |
| CategoryID   | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each expense or credit category |
| CategoryName | VARCHAR(100) | No           | UNIQUE                     | Stores the unique name of the category used to classify transactions          |

---

## 🟢 TABLE: tblExpenseSubCategory

### Purpose

Stores expense sub-categories.

| Column Name     | Data Type    | Null Allowed | Constraints                | Description                                                                            |
| --------------- | ------------ | ------------ | -------------------------- | -------------------------------------------------------------------------------------- |
| SubCategoryID   | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each sub-category record                 |
| CategoryID      | INT          | No           | FOREIGN KEY                | Stores the reference ID of the main category to which this sub-category belongs        |
| SubCategoryName | VARCHAR(100) | No           | UNIQUE                     | Stores the unique name of the sub-category used for more detailed transaction grouping |

---

## 🟢 TABLE: tblPaymentType

### Purpose

Stores different payment methods.

| Column Name | Data Type   | Null Allowed | Constraints                | Description                                                              |
| ----------- | ----------- | ------------ | -------------------------- | ------------------------------------------------------------------------ |
| PaymentID   | INT         | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each payment method record |
| PaymentName | VARCHAR(50) | No           | UNIQUE                     | Stores the unique name of the payment method used for transactions       |

---

# 💳 CREDIT MODULE

---

## 🔵 TABLE: tblCredit

### Purpose

Stores all credit transactions.

| Column Name   | Data Type     | Null Allowed | Constraints                | Description                                                                    |
| ------------- | ------------- | ------------ | -------------------------- | ------------------------------------------------------------------------------ |
| CreditID      | INT           | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each credit transaction          |
| UserID        | INT           | No           | FOREIGN KEY                | Stores the ID of the user who created or owns this credit transaction          |
| CategoryID    | INT           | No           | FOREIGN KEY                | Stores the reference ID of the main credit category for this transaction       |
| SubCategoryID | INT           | No           | FOREIGN KEY                | Stores the reference ID of the sub-category related to this credit transaction |
| Amount        | DECIMAL(10,2) | No           | —                          | Stores the total monetary amount received in this credit transaction           |
| Description   | VARCHAR(MAX)  | No           | —                          | Stores additional details or notes related to the credit transaction           |
| PaymentID     | INT           | No           | FOREIGN KEY                | Stores the reference ID of the payment method used for this credit transaction |
| CreditAt      | DATETIME      | No           | —                          | Stores the exact date and time when the credit transaction was recorded        |

---

## 🔵 TABLE: tblCreditCategory

### Purpose

Stores credit categories.

| Column Name  | Data Type    | Null Allowed | Constraints                | Description                                                                  |
| ------------ | ------------ | ------------ | -------------------------- | ---------------------------------------------------------------------------- |
| CategoryID   | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each expense category record   |
| CategoryName | VARCHAR(100) | No           | UNIQUE                     | Stores the unique name of the category used to organize expense transactions |

---

## 🔵 TABLE: tblCreditSubCategory

### Purpose

Stores credit sub-categories.

| Column Name     | Data Type    | Null Allowed | Constraints                | Description                                                                         |
| --------------- | ------------ | ------------ | -------------------------- | ----------------------------------------------------------------------------------- |
| SubCategoryID   | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each expense sub-category record      |
| CategoryID      | INT          | No           | FOREIGN KEY                | Stores the reference ID of the main expense category linked to this sub-category    |
| SubCategoryName | VARCHAR(100) | No           | UNIQUE                     | Stores the unique name of the sub-category used for detailed expense classification |

---

# 🤝 LENT & BORROW MODULE

---

## 🟠 TABLE: tblLent

### Purpose

Stores money lent records.

| Column Name | Data Type     | Null Allowed | Constraints                | Description                                                                                        |
| ----------- | ------------- | ------------ | -------------------------- | -------------------------------------------------------------------------------------------------- |
| LentID      | INT           | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each lent transaction                                |
| UserID      | INT           | No           | FOREIGN KEY                | Stores the ID of the user who provided the money or item on loan                                   |
| PersonID    | INT           | No           | FOREIGN KEY                | Stores the reference ID of the person from the Lent Person table involved in this lent transaction |
| PaymentID   | INT           | No           | FOREIGN KEY                | Stores the reference ID of the payment method used in the lent transaction                         |
| StatusID    | INT           | No           | FOREIGN KEY                | Stores the current status of the lent transaction                                                  |
| Amount      | DECIMAL(10,2) | No           | —                          | Stores the total amount involved in the lent transaction                                           |
| LentAt      | DATETIME      | No           | —                          | Stores the exact date and time when the money or item was lent                                     |
| DeadlineAt  | DATETIME      | Yes          | —                          | Stores the expected deadline date and time for returning the lent amount or item                   |
| Description | VARCHAR(MAX)  | No           | —                          | Stores additional details or notes related to the lent transaction                                 |

---

## 🟠 TABLE: tblLentPersons

### Purpose

Stores information about persons involved in lending.

| Column Name | Data Type    | Null Allowed | Constraints                | Description                                                                |
| ----------- | ------------ | ------------ | -------------------------- | -------------------------------------------------------------------------- |
| PersonID    | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each person record           |
| PersonName  | VARCHAR(100) | No           | —                          | Stores the full name of the person involved in lent or borrow transactions |
| PhoneNumber | VARCHAR(15)  | No           | —                          | Stores the contact phone number of the person for communication purposes   |
| Address     | VARCHAR(255) | Yes          | —                          | Stores the residential or contact address information of the person        |

---

## 🟠 TABLE: tblLentBorrowStatus

### Purpose

Stores lending and borrowing status information.

| Column Name | Data Type   | Null Allowed | Constraints                | Description                                                              |
| ----------- | ----------- | ------------ | -------------------------- | ------------------------------------------------------------------------ |
| StatusID    | INT         | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each lent or borrow status |
| StatusName  | VARCHAR(50) | No           | —                          | Stores the name of the current status used in lent or transactions       |

---

## 🟠 TABLE: tblBorrow

### Purpose

Stores money borrowing records.

| Column Name | Data Type     | Null Allowed | Constraints                | Description                                                                                               |
| ----------- | ------------- | ------------ | -------------------------- | --------------------------------------------------------------------------------------------------------- |
| BorrowID    | INT           | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each borrow transaction                                     |
| UserID      | INT           | No           | FOREIGN KEY                | Stores the reference ID of the user from the Users table involved in this borrow transaction              |
| PersonID    | INT           | No           | FOREIGN KEY                | Stores the reference ID of the person from the Borrow Person table involved in this borrow transaction    |
| PaymentID   | INT           | No           | FOREIGN KEY                | Stores the reference ID of the payment method from the Payment_Type table used in this borrow transaction |
| StatusID    | INT           | No           | FOREIGN KEY                | Stores the reference ID of the current status from the Lent_Borrow_Status table                           |
| Amount      | DECIMAL(10,2) | No           | —                          | Stores the total amount involved in the borrow transaction                                                |
| BorrowAt    | DATETIME      | No           | —                          | Stores the exact date and time when the money or item was borrowed                                        |
| DeadlineAt  | DATETIME      | Yes          | —                          | Stores the expected deadline date and time for returning the borrowed amount or item                      |
| Description | VARCHAR(MAX)  | No           | —                          | Stores additional details or notes related to the borrow transaction                                      |

---

## 🟠 TABLE: tblBorrowPersons

### Purpose

Stores information about persons involved in borrowing.

| Column Name | Data Type    | Null Allowed | Constraints                | Description                                                              |
| ----------- | ------------ | ------------ | -------------------------- | ------------------------------------------------------------------------ |
| PersonID    | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each person record         |
| PersonName  | VARCHAR(100) | No           | —                          | Stores the full name of the person involved in borrow transactions       |
| PhoneNumber | VARCHAR(15)  | No           | —                          | Stores the contact phone number of the person for communication purposes |
| Address     | VARCHAR(255) | Yes          | —                          | Stores the residential or contact address information of the person      |

---

# 📝 TASK MODULE

---

## 🟡 TABLE: tblTask

### Purpose

Stores user task information.

| Column Name | Data Type    | Null Allowed | Constraints                | Description                                                                   |
| ----------- | ------------ | ------------ | -------------------------- | ----------------------------------------------------------------------------- |
| TaskID      | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each task record                |
| UserID      | INT          | No           | FOREIGN KEY                | Stores the reference ID of the user from the Users table assigned to the task |
| PriorityID  | INT          | No           | FOREIGN KEY                | Stores the reference ID of the task priority from the Task Priorities table   |
| StatusID    | INT          | No           | FOREIGN KEY                | Stores the reference ID of the task status from the Task Status table         |
| TaskTitle   | VARCHAR(150) | No           | —                          | Stores the title or short name of the task created by the user                |
| Deadline    | DATE         | No           | —                          | Stores the final deadline date for completing the assigned task               |

---

## 🟡 TABLE: tblTaskPriorities

### Purpose

Stores task priority levels.

| Column Name  | Data Type   | Null Allowed | Constraints                | Description                                                                                        |
| ------------ | ----------- | ------------ | -------------------------- | -------------------------------------------------------------------------------------------------- |
| PriorityID   | INT         | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each task priority record                            |
| PriorityName | VARCHAR(50) | No           | UNIQUE                     | Stores the unique priority level name used for organizing and managing tasks (low , medium , high) |

---

## 🟡 TABLE: tblTaskStatus

### Purpose

Stores task completion status.

| Column Name    | Data Type   | Null Allowed | Constraints                | Description                                                                                                  |
| -------------- | ----------- | ------------ | -------------------------- | ------------------------------------------------------------------------------------------------------------ |
| TaskStatusID   | INT         | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each task status record                                        |
| TaskStatusName | VARCHAR(50) | No           | UNIQUE                     | Stores the unique status name used to represent the current state or progress of a task (Complete , Pending) |

---

# 🗒️ NOTE MODULE

---

## 🟡 TABLE: tblNote

### Purpose

Stores user notes information.

| Column Name | Data Type    | Null Allowed | Constraints                | Description                                                                   |
| ----------- | ------------ | ------------ | -------------------------- | ----------------------------------------------------------------------------- |
| NoteID      | INT          | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each note record                |
| UserID      | INT          | No           | FOREIGN KEY                | Stores the reference ID of the user from the Users table who created the note |
| StatusID    | INT          | No           | FOREIGN KEY                | Stores the reference ID of the note status from the Note_Status table         |
| NoteTitle   | VARCHAR(150) | No           | —                          | Stores the title or short heading of the note                                 |
| Description | VARCHAR(MAX) | No           | —                          | Stores the detailed content or information written inside the note            |
| CreatedAt   | DATETIME     | No           | —                          | Stores the exact date and time when the note was created                      |

---

## 🟡 TABLE: tblNoteStatus

### Purpose

Stores note priority or status information.

| Column Name | Data Type   | Null Allowed | Constraints                | Description                                                                 |
| ----------- | ----------- | ------------ | -------------------------- | --------------------------------------------------------------------------- |
| StatusID    | INT         | No           | PRIMARY KEY, IDENTITY(1,1) | Stores a unique auto-generated identifier for each note status record       |
| StatusName  | VARCHAR(50) | No           | UNIQUE                     | Stores the unique status name used to represent the current state of a note |

---

# ✅ GENERAL NOTES

- All tables follow relational database structure.
- All primary keys use `IDENTITY(1,1)`.
- Foreign keys maintain table relationships.
- UNIQUE constraints are used where necessary.
- DATETIME fields are used for transaction tracking.
- Database structure is designed using normalization principles.
