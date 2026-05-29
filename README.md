# 📊 Personal Expense & Credit Tracker

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQLServer-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

A comprehensive and intuitive **C# WinForms desktop application** designed to help you seamlessly manage your personal finances. Track your daily expenses, monitor credits and borrowings, and maintain a clear log of all financial transactions—all backed by a robust **SQL Server** database.

---

## ✨ Features

- **💰 Expense Tracking:** Log and categorize your daily expenses effortlessly.
- **🤝 Credit & Borrowing Management:** Keep a clear record of money you owe or are owed.
- **📈 Transaction History:** View a detailed ledger of all your financial activities.
- **🖥️ Intuitive User Interface:** A clean, responsive, and easy-to-use WinForms dashboard.
- **🗄️ Robust Database:** Secure and structured data management using Microsoft SQL Server.

## 🛠️ Technology Stack

- **Frontend:** C# Windows Forms (WinForms)
- **Backend/Logic:** .NET Framework / .NET Core (C#)
- **Database:** Microsoft SQL Server
- **Architecture:** Standard multi-layer architecture (UI, Business Logic, Data Access)

## 🚀 Getting Started

Follow these steps to get the project up and running on your local machine.

### Prerequisites
- [Visual Studio](https://visualstudio.microsoft.com/) (2019 or later recommended) with .NET desktop development workload.
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express edition is fine).
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (Optional but recommended).

### Database Setup
1. Open SSMS or your preferred SQL client and connect to your local SQL Server instance.
2. Navigate to the `Database/` folder in this repository.
3. Execute the SQL scripts in the following order to set up your database:
   - Run the scripts inside `✔️Schema/` to create the tables.
   - Run the scripts inside `Procedures/` to add stored procedures.
   - Run the scripts inside `✔️SeedData/` to populate initial master data and categories.
   - Configure anything extra using the scripts in `Master/`.
4. Update the SQL connection string in the application's configuration file (`App.config` or similar) inside the `WinFormsApp/` folder to point to your local database instance.

### Installation & Run
1. Clone the repository:
   ```bash
   git clone https://github.com/YourUsername/PersonalExpenseCreditTracker.git
   ```
2. Open the solution file in Visual Studio.
3. Restore NuGet packages if prompted.
4. Build the solution (`Ctrl + Shift + B`).
5. Start the application (`F5`).

## 🤝 Contributing
Contributions, issues, and feature requests are welcome! 

1. Fork the project.
2. Create your feature branch (`git checkout -b feature/AmazingFeature`).
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the branch (`git push origin feature/AmazingFeature`).
5. Open a Pull Request.

## 📝 License
This project is open-source. Feel free to use and modify it as per your needs!
