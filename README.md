# 📊 Personal Expense & Credit Tracker

<div align="center">

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![WinForms](https://img.shields.io/badge/WinForms-0078D7?style=for-the-badge&logo=windows&logoColor=white)

**A comprehensive desktop application for managing personal finances — track expenses, monitor credits, manage lend/borrow records, stay on top of tasks, and never miss a deadline.**

[🐛 Report Bug](https://github.com/JainKushal556/PersonalExpenseCreditTracker/issues) · [✨ Request Feature](https://github.com/JainKushal556/PersonalExpenseCreditTracker/issues)

</div>

---

## 📖 Table of Contents

- [About the Project](#-about-the-project)
- [Features](#-features)
- [Architecture](#-architecture)
- [Technology Stack](#️-technology-stack)
- [Database Overview](#️-database-overview)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Database Setup](#database-setup)
  - [Connection String Setup](#connection-string-setup)
  - [Installation & Run](#installation--run)
- [Module Details](#-module-details)
- [Project Structure](#-project-structure)
- [Team](#-team)
- [Contributing](#-contributing)
- [License](#-license)

---

## 💡 About the Project

**Personal Expense & Credit Tracker** is a feature-rich **C# WinForms desktop application** built to help individuals take full control of their personal finances. Designed with a clean UI and a robust SQL Server backend, the application lets users:

- Log daily **expenses and credits** with category-level detail
- Track money **lent to** or **borrowed from** others with deadline awareness
- Manage **to-do tasks** with priorities and completion status
- Write and organize **personal notes**
- View a **live financial dashboard** with charts, summaries, and smart reminders

All data is secured per-user with login/registration, and every financial operation is driven by **stored procedures** for reliability, performance, and security against SQL injection.

---

## ✨ Features

| Module | Key Capabilities |
|--------|-----------------|
| 🔐 **Authentication** | Register, Login, Profile management, Password change |
| 📊 **Dashboard** | Financial summary cards, category-wise charts, credit vs expense comparison, smart reminders |
| 💸 **Expense** | Add, categorize, filter, and search expenses by date/category/amount |
| 💰 **Credit** | Log income sources, filter by category, date, and amount |
| 🤝 **Lent** | Track money you gave out, record partial/full returns, auto-status updates |
| 🏦 **Borrow** | Track money you owe, record partial/full repayments, auto-status updates |
| ✅ **Task** | Create tasks with deadline and priority, mark complete, get reminders |
| 📝 **Notes** | Write quick notes with priority levels, view and manage note list |
| ⚙️ **Settings** | Custom categories, unified person directory, profile editing |

### 🔔 Smart Reminder System

The app automatically notifies you when:
- A **borrow return deadline** is approaching (within 7 days) or already overdue
- A **lent return deadline** is approaching (within 7 days) or already overdue
- A **task deadline** is near (within 7 days) or overdue

Status is auto-updated to **Overdue** via `spUpdateOverdueStatus`, which runs on application start.

---

## 🏗️ Architecture

The application strictly follows a **3-Tier Architecture** to separate concerns and ensure maintainability:

```
┌─────────────────────────────────────┐
│         Presentation Layer          │
│      C# WinForms (UI Forms)         │
└────────────────┬────────────────────┘
                 │  User Events & Data Binding
┌────────────────▼────────────────────┐
│       Business Logic Layer (BLL)    │
│   Validation · Rules · Computation  │
└────────────────┬────────────────────┘
                 │  Clean Data Requests
┌────────────────▼────────────────────┐
│       Data Access Layer (DAL)       │
│   ADO.NET · SqlConnection · Calls   │
└────────────────┬────────────────────┘
                 │  Stored Procedure Calls
┌────────────────▼────────────────────┐
│     Microsoft SQL Server Database   │
│  Tables · Views · Stored Procedures │
└─────────────────────────────────────┘
```

> **No raw SQL queries in application code.** Every database interaction goes through a stored procedure — ensuring consistency, security against SQL injection, and easy maintainability.

---

## 🛠️ Technology Stack

| Layer | Technology |
|-------|-----------|
| **UI / Frontend** | C# Windows Forms (WinForms) |
| **Business Logic** | C# (.NET Framework) |
| **Data Access** | ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataAdapter`) |
| **Database** | Microsoft SQL Server (Express / Full) |
| **DB Scripting** | T-SQL Stored Procedures (116 procedures) |
| **Dev Tools** | Visual Studio 2022, SSMS, Git |

---

## 🗄️ Database Overview

The database is built across **22 tables** organized into logical modules, backed by **116 stored procedures**:

| Area | Tables |
|------|--------|
| **Users** | `tblUsers`, `tblUserProfile`, `tblUserContact`, `tblUserAuthentication` |
| **Expense** | `tblExpense`, `tblExpenseCategory`, `tblExpenseSubCategory` |
| **Credit** | `tblCredit`, `tblCreditCategory`, `tblCreditSubCategory` |
| **Lent** | `tblLent`, `tblLentPersons`, `tblLentBorrowStatus` |
| **Borrow** | `tblBorrow`, `tblBorrowPerson` |
| **Task** | `tblTask`, `tblTaskPriority`, `tblTaskStatus` |
| **Note** | `tblNote`, `tblNoteStatus`, `tblNotePriority` |
| **Shared** | `tblPaymentType` |

All stored procedures are version-controlled and deployable via a single PowerShell script.

---

## 🚀 Getting Started

### Prerequisites

Before running the application, ensure you have the following installed:

- **[Visual Studio 2022](https://visualstudio.microsoft.com/)** — with *.NET desktop development* workload
- **[Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)** — Express edition is sufficient
- **[SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)** — Optional but recommended
- **PowerShell 5.0+** — Pre-installed on Windows 10/11

---

### Database Setup

#### ⚡ Option A — Automated Setup (Recommended)

The `Database/Master/` folder contains a PowerShell script that deploys the **entire database** — schema, procedures, and seed data — in one command.

```powershell
# 1. Open PowerShell as Administrator
# 2. Navigate to the Database/Master/ folder
cd "path\to\PersonalExpenseCreditTracker\Database\Master"

# 3. Run the deployment script
.\sync_db.ps1
```

> ⚠️ **Note:** You may need to allow script execution first:
> `Set-ExecutionPolicy RemoteSigned -Scope CurrentUser`

#### 🔧 Option B — Manual Setup (Step-by-step)

1. Open **SSMS** and connect to your local SQL Server instance.
2. Navigate to the `Database/` folder in this repository.
3. Execute the scripts **in this exact order**:

| Step | Folder | Purpose |
|------|--------|---------|
| 1 | `Database/✔️Schema/` | Creates all 22 tables with constraints |
| 2 | `Database/Procedures/` | Installs all 116 stored procedures |
| 3 | `Database/✔️SeedData/` | Populates master/reference data (statuses, payment types, default categories) |

---

### Connection String Setup

After database setup, update the connection string in the WinForms project:

1. Navigate to `WinFormsApp/PersonalExpenseCreditTracker/PersonalExpenseCreditTracker/`
2. Open `App.config`
3. Update the `connectionString` value:

```xml
<connectionStrings>
  <add name="DefaultConnection"
       connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=PersonalExpenseCreditTracker;Integrated Security=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

> Replace `YOUR_SERVER_NAME` with your SQL Server instance name (e.g., `.\SQLEXPRESS` or `localhost`).

---

### Installation & Run

```bash
# 1. Clone the repository
git clone https://github.com/JainKushal556/PersonalExpenseCreditTracker.git

# 2. Open the solution in Visual Studio 2022
#    WinFormsApp/PersonalExpenseCreditTracker/PersonalExpenseCreditTracker.sln
```

3. Restore NuGet packages if prompted *(Build → Restore NuGet Packages)*
4. Update `App.config` with your connection string *(see above)*
5. Build the solution: `Ctrl + Shift + B`
6. Run the application: `F5`

---

## 📦 Module Details

<details>
<summary><strong>🔐 Authentication</strong></summary>

- **Register:** Full name, email, phone, password, optional profile photo
- **Login:** Email + password validation with active-user check
- **Profile:** Update name, email, phone, and profile picture from Settings
- **Password Change:** Requires current password confirmation before update
- **Logout:** Ends the current session and returns to the login screen

</details>

<details>
<summary><strong>📊 Dashboard</strong></summary>

- **Summary Cards:** Total Expense, Total Credit, Total Lent, Total Borrow, Net Balance (Credit − Expense)
- **Date Filter:** Custom date range to filter the entire dashboard view
- **Charts:**
  - 🥧 Pie chart — Expense breakdown by category
  - 📊 Bar/Line chart — Credit vs Expense comparison
- **Reminder Panel:** Shows overdue and upcoming deadlines for lent, borrow, and tasks (up to 7 days ahead)

</details>

<details>
<summary><strong>💸 Expense & 💰 Credit</strong></summary>

- Add transactions with **Amount, Category, Sub-category, Description, Payment Mode**
- System default categories are read-only; users can create and deactivate **custom categories** from Settings
- Sub-categories load dynamically based on the selected category
- **Filter & Search** by: date (day/month/year), category, sub-category, amount range, sort order

</details>

<details>
<summary><strong>🤝 Lent & 🏦 Borrow</strong></summary>

- Add records linked to a **Unified Person Directory** (shared between both modules — no duplicate contacts)
- Record **partial or full returns/repayments**
- Status auto-updates:
  - `Pending` → `Complete` when fully settled (RemainingAmount = 0)
  - `Pending` → `Overdue` when the deadline passes with an outstanding balance
- Dashboard reminders for records with remaining balance within 7 days

</details>

<details>
<summary><strong>✅ Task</strong></summary>

- Create tasks with **Title, Deadline, Priority** (Low / Medium / High)
- View all tasks in a sortable list
- One-click **Mark Complete** button
- Overdue tasks are auto-identified on application start via `spUpdateOverdueStatus`
- Dashboard shows upcoming tasks with deadlines within 7 days

</details>

<details>
<summary><strong>📝 Notes</strong></summary>

- Create notes with **Title, Description, Priority** (Low / Medium / High)
- View count of total and important notes
- Manage notes through a clean list/card interface

</details>

<details>
<summary><strong>⚙️ Settings</strong></summary>

- **Category Manager:** Add and deactivate custom Expense/Credit categories and sub-categories
- **Person Directory:** Add, view, and manage contacts used across Lent and Borrow modules
- **Profile Editor:** Update name, email, phone, and profile photo
- **Change Password:** Secure, validation-first password update flow
- **Logout**

</details>

---

## 📁 Project Structure

```
PersonalExpenseCreditTracker/
│
├── 📁 Database/
│   ├── 📁 ✔️Schema/              # 22 table creation scripts
│   ├── 📁 Procedures/            # 116 stored procedures (organized by module)
│   │   ├── Authentication/
│   │   ├── Dashboard/
│   │   ├── Expense/
│   │   ├── Credit/
│   │   ├── Lent/
│   │   ├── Borrow/
│   │   ├── Task/
│   │   ├── Note/
│   │   ├── Profile/
│   │   └── Settings/
│   ├── 📁 ✔️SeedData/            # Initial master/reference data
│   └── 📁 Master/
│       ├── ✔️MasterSchema.sql              # All 22 tables (single file)
│       ├── ✔️MasterStoredProcedures.sql    # All 116 procedures (single file)
│       ├── ✔️NewMasterSeedData.sql         # All seed/reference data
│       └── sync_db.ps1                    # One-command DB deployment script
│
├── 📁 Documentation/
│   ├── Personal_Expenses_Tracker_SRS.md   # System Requirements Specification
│   ├── DATABASE_SCHEMA.md                 # Full schema documentation
│   ├── STORED_PROCEDURE_REQUIREMENTS.md   # All SP requirements and design
│   ├── BLL_INPUT_VALIDATION_MAPPING.md    # Business layer validation rules
│   ├── LENT_MODULE_FLOW.md                # Lent module logic flow
│   ├── TEAM_MODULE_ASSIGNMENT.md          # Team structure and responsibilities
│   ├── ERD.pdf                            # Entity Relationship Diagram
│   └── UiDesignBluePrint.pdf              # UI Design Blueprint
│
└── 📁 WinFormsApp/
    └── PersonalExpenseCreditTracker/
        └── PersonalExpenseCreditTracker/
            └── 📁 Modules/
                ├── Authentication/
                ├── Dashboard/
                ├── Expense/
                ├── Credit/
                ├── Lent/
                ├── Borrow/
                ├── Task/
                ├── Note/
                ├── Profile/
                └── Settings/
```

---

## 👥 Team

This project was collaboratively built by a cross-functional team, each owning a specific module:

### 🔴 Project Manager

| Name | Role |
|------|------|
| **Kushal Jain** | Project Manager — DB Architecture, Master Schema, GitHub Management, Integration & Final Review |

---

### 🟣 Team A — User & Task Module

| Name | Role |
|------|------|
| **Sujit** | Team Lead |
| **Sampriti** | Member |

📌 *Modules: Users, User Profile, Authentication, Task, Note*

---

### 🟢 Team B — Expense & Credit Module

| Name | Role |
|------|------|
| **Arpita** | Team Lead |
| **Aniket** | Member |
| **Bidisha** | Member |

📌 *Modules: Expense, Credit, Category & Sub-category management, Payment Types*

---

### 🟠 Team C — Lent & Borrow Module

| Name | Role |
|------|------|
| **Debo** | Team Lead |
| **Akhmal** | Member |

📌 *Modules: Lent, Borrow, Unified Person Directory, Status Management*

---

## 🤝 Contributing

Contributions are welcome! Please follow the team conventions:

1. **Fork** the repository
2. Create your feature branch:
   ```bash
   git checkout -b feature/YourFeatureName
   ```
3. Commit your changes:
   ```bash
   git commit -m "feat: add YourFeatureName"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/YourFeatureName
   ```
5. Open a **Pull Request** and request a review

> ⚠️ **Important:** All database changes must be reflected in **both** the individual `.sql` file under `Database/Procedures/` **and** `Database/Master/✔️MasterStoredProcedures.sql`. Never make manual database changes without updating the corresponding scripts.

---

## 📝 License

This project is open-source. Feel free to use and modify it as per your needs.

---

<div align="center">

**Built with ❤️ by the Personal Expense & Credit Tracker Team**

</div>
