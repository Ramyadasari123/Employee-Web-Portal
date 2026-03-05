# Employee Web Portal

## 📌 Project Overview

The **Employee Web Portal** is a web-based application developed using **ASP.NET Core and SQL Server** that helps manage employee information efficiently.
This system allows administrators to **add, update, delete, and view employee details** through a user-friendly interface.

The project demonstrates the implementation of **RESTful APIs, Entity Framework Core, and database operations** in a real-world employee management system.

---

## 🚀 Features

* Add new employee details
* Update employee information
* Delete employee records
* View employee details
* Get total employee count
* Search employees by ID
* Search employees by Department
* Search employees by Job type
* Database integration with SQL Server
* RESTful API implementation

---

## 🛠 Technologies Used

* **Backend:** ASP.NET Core Web API (.NET)
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Version Control:** Git & GitHub
* **IDE:** Visual Studio

---

## 🗄 Database Structure

The system uses an **Employees table** with the following fields:

* Id
* FullName
* Email
* Position
* Department
* HireDate
* DateOfBirth
* Gender
* Salary
* Type

---

## ⚙️ Project Setup

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/Ramyadasari123/Employee-Web-Portal.git
```

### 2️⃣ Open the Project

Open the solution in **Visual Studio**.

### 3️⃣ Configure Database

Update the **connection string** in `appsettings.json`.

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=EmployeeDB2;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 4️⃣ Run the Project

Run the application using:

```
Ctrl + F5
```

---

## 📡 API Endpoints

| Method | Endpoint            | Description        |
| ------ | ------------------- | ------------------ |
| GET    | /api/employees      | Get all employees  |
| GET    | /api/employees/{id} | Get employee by ID |
| POST   | /api/employees      | Add new employee   |
| PUT    | /api/employees/{id} | Update employee    |
| DELETE | /api/employees/{id} | Delete employee    |

---

## 📊 Example Employee JSON

```json
{
  "fullName": "Ramya Dasari",
  "email": "ramya@example.com",
  "position": "Software Developer",
  "department": "IT",
  "salary": 50000
}
```

---

## 🎯 Purpose of the Project

This project was developed to demonstrate practical knowledge of:

* ASP.NET Core Web API
* Entity Framework Core
* CRUD operations
* SQL Server database integration
* RESTful service architecture
* Git version control

---

## 👩‍💻 Author

**Ramya Dasari**

GitHub:
https://github.com/Ramyadasari123

---
