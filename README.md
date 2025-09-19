# MyShop
# 🛒 E-Commerce Web Application

A complete **E-Commerce Web Application** built with **ASP.NET Core MVC ** following **N-Tier Architecture** and applying **Repository & Unit of Work patterns**.  
This project demonstrates a real-world implementation of modern backend development techniques with role-based authentication, email verification, and Stripe payment integration.

---

## 📌 Table of Contents
- [Overview](#-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Project Structure](#-project-structure)
- [Installation](#-installation)
- [Usage](#-usage)
- [Demo](#-demo)
- [Contributing](#-contributing)

---

## 🔍 Overview
This project is designed to simulate a real **online shopping platform**, featuring:
- Customer registration and account verification using email codes.
- Secure login/logout with **ASP.NET Core Identity**.
- An **Admin dashboard** for product and order management.
- A complete checkout workflow integrated with **Stripe** for payments.

It’s an end-to-end project covering both the **customer experience** and the **admin management system**.

---

## ✨ Features
✅ User registration with **email verification code**.  
✅ Role-based authentication: **Customer** & **Admin**.  
✅ Admin dashboard to manage products, categories, and orders.  
✅ Shopping cart and checkout system.  
✅ Secure payment gateway integration with **Stripe**.  
✅ Session management & dependency injection.  
✅ Repository & Unit of Work patterns for clean architecture.  
✅ Responsive UI with Bootstrap.  

---

## 🛠 Tech Stack
- **Backend:** ASP.NET Core MVC, Web API, C#  
- **Frontend:** Razor Pages, Bootstrap, CSS  
- **Database:** SQL Server + LINQ  
- **Authentication & Authorization:** ASP.NET Core Identity  
- **Payment Integration:** Stripe API  
- **Architecture:** N-Tier, Repository Pattern, Unit of Work Pattern  
- **Others:** Dependency Injection, Session Management, Logging  
- **Version Control:** Git & GitHub  

---

## 🏗 Architecture
The project follows **N-Tier Architecture**:

- **myShop.Entities** → Entities & Models  
- **myShop.DataAccess** → Database Context, Repositories, EF Core setup  
- **myShop.Utilities** → Services (Email, Verification, etc.)  
- **myShop.Web** → Presentation Layer (MVC, Razor Pages, Controllers)  

This ensures a clean separation of concerns and high maintainability.

---

## 📂 Project Structure
```
myShop.sln
│
├── myShop.Entities        # Models & Entities
├── myShop.DataAccess      # EF Core, Repositories, Unit of Work
├── myShop.Utilities       # Services (Email, Verification, Helpers)
└── myShop.Web             # MVC + Razor Pages (UI Layer)
```

---

## ⚙ Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/YourUsername/YourRepoName.git
   ```

2. Navigate to the project folder:
   ```bash
   cd YourRepoName
   ```

3. Update the database connection string in **appsettings.json**:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=myShop;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

4. Apply migrations:
   ```bash
   dotnet ef database update
   ```

5. Run the project:
   ```bash
   dotnet run
   ```

---

## 🚀 Usage
- **Customer:**  
  - Register → Verify Email (with code) → Login → Browse Products → Add to Cart → Checkout (Stripe Payment).  

- **Admin:**  
  - Login as Admin → Manage Products → Manage Orders.  

---

## 🎥 Demo
📽️ You can watch a short demo video of the project here:  
👉 [LinkedIn Post](https://lnkd.in/p/dTk6KwUR)  

---

## 🤝 Contributing
Contributions are welcome! Please fork this repository and submit a pull request for any improvements.


