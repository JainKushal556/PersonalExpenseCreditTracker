# 📊 Personal Expenses Tracker - System Requirements Specification (SRS)

---

## 📌 INTRODUCTION

**a. Purpose:**
The purpose of the Personal Expenses Tracker system is to help users manage daily financial activities including expenses, credits, lent, borrow, tasks, and notes efficiently.

**b. Scope:**
The system allows users to record expenses, manage lending and borrowing using a unified person directory, track credits, create tasks and notes, and generate financial reports.

---

## ⚙️ FUNCTIONALITY

### 1️⃣ Login and Register
- **a. Name:** *(Required for creating user profile)*
- **b. Email:** *(Used for secure login and account recovery)*
- **c. Phone number:** *(Used for alternate contact details)*
- **d. Password:** *(Securely stored for authentication)*
- **e. Profile Photo:** *(Optional image upload for the user's dashboard)*

### 2️⃣ Aside (Navigation Menu)
- **a.** Dashboard
- **b.** Expenses
- **c.** Credit
- **d.** Lent
- **e.** Borrow
- **f.** Tasks
- **g.** Notes
- **h.** Settings

### 3️⃣ Dashboard
- **a. Total expense amount:** *(Sum of all expenses for the current logged-in user)*
- **b. Total credit amount:** *(Sum of all credits received)*
- **c. Total lent amount:** *(Sum of all money given to others)*
- **d. Total borrow amount:** *(Sum of all money taken from others)*
- **e. Net value:** *(Calculated dynamically as: Total Credit - Total Expense)*
- **f. Date ways filter:** *(Filter the entire dashboard summaries from a specific start date to an end date)*
- **g. Report**
  - **i.** Category ways expense graph *(A pie chart showing how much budget was spent on which category)*
  - **ii.** Credit v/s expense graph *(A comparative bar or line chart to track cash flow)*
- **h. Notifications**
  - **i.** Borrow return reminder *(Alerts the user if a borrow return deadline is near)*
  - **ii.** Lent return reminder *(Alerts the user if a lent return deadline is near)*
  - **iii.** Task deadline reminder *(Alerts the user for pending tasks nearing their deadline)*

### 4️⃣ Expense
- **a. Total expenses amount:** *(Shows total expense value based on filters)*
- **b. Total transaction numbers:** *(Shows count of expense transactions)*
- **c. Add expenses**
  - **i. Amount:** *(The monetary value of the expense)*
  - **ii. Category:** *(Users select a main category. System default categories cannot be modified. Custom categories can be added in settings)*
  - **iii. Sub-category:** *(Dynamically loads a dropdown list based on the selected main Category)*
  - **iv. Description:** *(Text note explaining the transaction details)*
  - **v. Payment mode:** *(Dropdown selection like Cash, Card, UPI, loaded from the database)*
- **d. Filter by (Search)**
  - **i.** This day, month, year *(Quick date filters or custom date range selection)*
  - **ii.** Category *(Filter all expenses by a specific category)*
    - **1.** Sub category *(Further refine the filter by sub-category)*
  - **iii.** Amount range *(Filter transactions between a Minimum and Maximum amount)*
  - **iv.** Oldest and latest *(Sorting options based on transaction date)*

### 5️⃣ Credit
- **a. Total credit amount:** *(Shows total credit value based on filters)*
- **b. Total transaction numbers:** *(Shows count of credit transactions)*
- **c. Add credit**
  - **i. Amount:** *(The value of the credit received)*
  - **ii. Category:** *(System default or custom created categories)*
  - **iii. Sub-category:** *(Dynamically loads based on the selected Category)*
  - **iv. Description:** *(Text note explaining the transaction details)*
  - **v. Payment mode:** *(Dropdown selection like Cash, Card, UPI)*
- **d. Filter by (Search)**
  - **i.** This day, month, year *(Date Range filtering)*
  - **ii.** Category *(Filter by specific credit category)*
    - **1.** Sub category *(Further refine by sub-category)*
  - **iii.** Amount range *(Filter transactions between a Minimum and Maximum amount)*
  - **iv.** Oldest and latest *(Sorting options)*

### 6️⃣ Lent *(Money you gave to someone)*
- **a. Total lent amount:** *(Sum of all money ever lent)*
- **b. Active Lents:** *(Sum of money currently given and not yet returned)*
- **c. Total returned amount:** *(Sum of money successfully collected back)*
- **d. Total pending payments:** *(Current outstanding amount left to be collected)*
- **e. Add lent**
  - **i. Select Person:** *(Instead of typing a name, user selects a person from the Unified Person Directory. This avoids duplicate names)*
  - **ii. Amount:** *(Total money being lent)*
  - **iii. Lent date:** *(Auto captured as current date)*
  - **iv. Return date:** *(Expected deadline to get the money back)*
  - **v. Description:** *(Reason for lending)*
  - **vi. Lent payment mode:** *(How the money was given)*
- **f. Money received (Recording returns)**
  - **i. Amount:** *(User can enter a partial amount or full amount returned)*
  - **ii. Payment mode:** *(How the money was returned)*
  - **iii. Status:** *(System auto-updates: if RemainingAmount > 0 it stays 'Pending', if RemainingAmount = 0 it automatically becomes 'Complete')*

### 7️⃣ Borrow *(Money you took from someone)*
- **a. Total borrow amount:** *(Sum of all money ever borrowed)*
- **b. Total active borrowings amount:** *(Sum of money currently owed to others)*
- **c. Total repaid amount:** *(Sum of money successfully paid back)*
- **d. Total pending amount:** *(Current outstanding amount left to be paid)*
- **e. Add borrow**
  - **i. Select Person:** *(Select from the Unified Person Directory)*
  - **ii. Amount:** *(Total money borrowed)*
  - **iii. Borrow date:** *(Auto captured as current date)*
  - **iv. Return date:** *(Expected deadline to pay back)*
  - **v. Description:** *(Reason for borrowing)*
  - **vi. Borrow payment mode:** *(How the money was received)*
- **f. Money paid (Recording repayments)**
  - **i. Amount:** *(User can enter a partial amount or full amount paid)*
  - **ii. Payment mode:** *(How the money was paid)*
  - **iii. Status:** *(System auto-updates: if RemainingAmount > 0 it stays 'Pending', if RemainingAmount = 0 it automatically becomes 'Complete')*

### 8️⃣ Task
- **a. Total tasks:** *(Total count of tasks created)*
- **b. Complete tasks:** *(Count of tasks marked as done)*
- **c. Total pending tasks:** *(Count of tasks yet to be done)*
- **d. Add task**
  - **i. Task title:** *(Name of the task)*
  - **ii. Deadline:** *(Target date to complete the task)*
  - **iii. Priority:** *(Dropdown: Low, Medium, High)*
- **e. Task complete button:** *(Clicking this directly updates the Task Status to Complete)*
- **f. Showing list of tasks:** *(Grid view of all tasks)*

### 9️⃣ Notes
- **a. Total number of notes:** *(Count of all notes)*
- **b. Important notes:** *(Count of notes marked with high priority)*
- **c. Add notes**
  - **i. Note title:** *(Heading of the note)*
  - **ii. Description:** *(Detailed content of the note)*
  - **iii. Priority:** *(Dropdown: Low, Medium, High)*
- **d. Showing list of notes:** *(Grid or card view of all saved notes)*

### 🔟 Settings
- **a. Expense and credit Categories**
  - **i.** Add category, sub category *(User can create custom categories for their own account)*
  - **ii.** Deactivate category, sub category *(System default categories cannot be modified or deleted. Custom categories can only be deactivated, not deleted, to prevent data loss in old transactions)*
- **b. Person Management (Unified Directory)**
  - **i.** Add Person *(Save Name, Phone number, and Address permanently in the database)*
  - **ii.** View list of persons *(This single list is used by both Lent and Borrow modules so you don't type the same person's details twice)*
- **c. Profile management**
  - **i.** Edit Profile photo *(Upload a new image)*
  - **ii.** Edit Name *(Update display name)*
  - **iii.** Edit Email *(Update registered email)*
  - **iv.** Edit Phone number *(Update contact number)*
- **d. Change Password:** *(Requires old password validation)*
- **e. Logout:** *(Ends the current session securely)*

---

> 💡 **Development Note:** Future version 2.0 will include features like editing old records and advanced monthly notifications. Version 1.0 strictly follows the above flow based on the current database architecture.
