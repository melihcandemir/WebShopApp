# WebShopApp 🛒

WebShopApp is a comprehensive e-commerce application built with ASP.NET Core. It demonstrates modern software development practices, including authentication, authorization, middleware, and more. Below is a detailed breakdown of the features and technologies used in the project.

---

## Features 🚀

### 1. **API Endpoints** 🌐
- **CRUD Operations**: 
  - `GET`, `POST`, `PUT`, `PATCH`, and `DELETE` endpoints are implemented for managing resources like products, orders, and settings.
  - Example: `ProductsController` and `OrdersController` handle product and order management.
- **RESTful Design**: Follows REST principles for resource management.

---

### 2. **Entity Framework Core** 🗄️
- **Code-First Approach**: 
  - Database schema is defined using C# classes and migrations.
  - Example: `WebShopAppDbContext` defines entities like `ProductEntity`, `OrderEntity`, and `UserEntity`.
- **Many-to-Many Relationships**:
  - Example: `OrderEntity` and `ProductEntity` are connected via `OrderProductEntity`.

---

### 3. **Authentication & Authorization** 🔒
- **JWT (JSON Web Token)**:
  - Secure token-based authentication implemented using `JwtHelper`.
  - Example: `AuthController` generates JWT tokens for users.
- **Role-Based Authorization**:
  - Example: Admin-only endpoints are protected using `[Authorize(Roles = "Admin")]`.

---

### 4. **Custom User Management** 👤
- **Custom User Entity**:
  - `UserEntity` manages user details like email, password, and roles.
- **Password Encryption**:
  - Passwords are encrypted using `DataProtection`.

---

### 5. **Middleware** 🛠️
- **Global Exception Handling**:
  - `ExceptionMiddleware` handles unhandled exceptions and returns structured error responses.
- **Maintenance Mode**:
  - `MaintenanceMiddleware` restricts access to the API during maintenance.

---

### 6. **Action Filters** 🎯
- **Custom Filters**:
  - Example: `TimeControlFilter` restricts API access during specific hours.

---

### 7. **Model Validation** ✅
- **Data Annotations**:
  - Example: `RegisterRequest` and `AddProductRequest` use `[Required]` and `[EmailAddress]` for validation.

---

### 8. **Dependency Injection** 💉
- **Service Lifetimes**:
  - Scoped services like `IUserService`, `IProductService`, and `IOrderService` are injected into controllers.

---

### 9. **Data Protection** 🔐
- **Encryption**:
  - Sensitive data like passwords are encrypted using `IDataProtection`.

---

### 10. **Global Exception Handling** ⚠️
- **Centralized Error Handling**:
  - `ExceptionMiddleware` logs errors and returns consistent error responses.

---

## Project Structure 📂

```
WebShopApp/
├── WebShopApp.WebApi/       # API Layer
│   ├── Controllers/         # API Controllers
│   ├── Middlewares/         # Custom Middleware
│   ├── Filters/             # Action Filters
│   ├── Jwt/                 # JWT Helper Classes
│   ├── Models/              # Request/Response Models
│   └── Program.cs           # Application Startup
├── WebShopApp.Business/     # Business Logic Layer
│   ├── Operations/          # Services for Business Logic
│   ├── DataProtection/      # Encryption Logic
│   └── Types/               # Shared Types
├── WebShopApp.Data/         # Data Access Layer
│   ├── Context/             # EF Core DbContext
│   ├── Entities/            # Database Entities
│   ├── Repositories/        # Generic Repositories
│   └── UnitOfWork/          # Unit of Work Pattern
└── README.md                # Project Documentation
```

---

## How to Run 🏃‍♂️

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/melihcandemir/WebShopApp.git
   cd WebShopApp
   ```

2. **Setup Database**:
   - Update the connection string in `appsettings.json`.
   - Run migrations:
     ```bash
     dotnet ef database update
     ```

3. **Run the Application**:
   ```bash
   dotnet watch --project WebShopApp.WebApi
   ```

4. **Access the API**:
   - Swagger UI: [http://localhost:5244/swagger](http://localhost:5244/swagger)

---

## Technologies Used 🛠️

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **JWT Authentication**
- **Dependency Injection**
- **Middleware**
- **Action Filters**
- **Data Protection**
- **Swagger**

---
