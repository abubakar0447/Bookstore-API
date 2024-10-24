# Bookstore API

## Overview

The **Bookstore API** is a comprehensive RESTful API built with ASP.NET Core 6 and Entity Framework Core. This project demonstrates core concepts of ASP.NET Core, including Dependency Injection, Middleware, Authentication, Authorization, and more, and is designed to handle essential operations for a bookstore.

The project includes user management, product management (Books, Authors), order processing, inventory management, and reporting features for sales and analytics.

This API serves as a demonstration of modern web development practices and the implementation of common patterns, making it an ideal project for learning and showcasing skills in ASP.NET Core.

---

## Features

- **User Management**: Authentication (JWT), registration, login, profile management.
- **Book and Author Management**: Add, update, list, and delete books and authors.
- **Order Management**: Placing orders, tracking statuses, admin control for updates.
- **Inventory Control**: Manage stock, alert low-stock books.
- **Wishlist and Cart**: Customer wishlists and cart functionalities.
- **Reporting**: Sales, inventory, and user activity reporting for admins.
- **Role-based Authorization**: Role-based access control using JWT.
- **Logging**: Integrated logging using Serilog for request tracking and error monitoring.
- **Middleware**: Custom middlewares for logging, exception handling, and request-response monitoring.

---

## Technologies Used

- **ASP.NET Core 6.0**: Framework for building modern web applications and services.
- **Entity Framework Core**: ORM for database management.
- **JWT (JSON Web Tokens)**: Authentication mechanism for secure API calls.
- **Serilog**: Structured logging for error and request tracking.
- **SQL Server**: Database used to store application data.
- **Swagger**: API documentation and testing.
- **Dependency Injection**: For managing service lifetimes and decoupling components.

---

## Project Setup

### Prerequisites

Ensure you have the following installed on your machine:

- **.NET 6 SDK**: [Download here](https://dotnet.microsoft.com/download/dotnet/6.0)
- **SQL Server**: Ensure you have a running instance of SQL Server.
- **Visual Studio or VS Code**: For development and testing.

### Cloning the Repository

To get started, clone the repository using Git:

```bash
git clone https://github.com/your-username/BookstoreAPI.git
cd BookstoreAPI
```

### Database Setup

1. Ensure you have SQL Server running.
2. Update the **appsettings.json** file with your SQL Server connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR-SERVER;Database=BookstoreDb;Trusted_Connection=True;"
}
```

3. Run the following commands to apply database migrations and create the database:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Running the Application

1. After setting up the database, run the application:

```bash
dotnet run
```

2. Open your browser and navigate to `https://localhost:5001/swagger` to view and test the API via Swagger UI.

---

## API Endpoints

Here is a brief list of the available API endpoints, grouped by functionality. You can explore all endpoints via Swagger.

### Authentication & User Management

- `POST /api/customer/register`: Register a new user.
- `POST /api/customer/login`: Login and obtain a JWT token.
- `GET /api/customer/profile`: View user profile (requires authentication).

### Book Management

- `GET /api/book`: Retrieve a list of books.
- `POST /api/book`: Add a new book (Admin only).
- `PUT /api/book/{id}`: Update a book (Admin only).

### Order Management

- `POST /api/order`: Place a new order (User).
- `PATCH /api/order/{id}/status`: Update order status (Admin only).
- `GET /api/order/{id}`: Get order details (Authenticated user).

### Wishlist and Cart

- `POST /api/wishlist`: Add a book to wishlist (Authenticated user).
- `POST /api/cart`: Add a book to cart (Authenticated user).

### Reporting

- `GET /api/reporting/sales?startDate=yyyy-MM-dd&endDate=yyyy-MM-dd`: Sales report for a given period.
- `GET /api/reporting/top-customers`: List of top customers.
- `GET /api/reporting/low-stock`: Books that are low in stock.

---

## Key Concepts Covered

Here are the main concepts demonstrated in this project:

1. **Dependency Injection**: Services like repositories, JWT token generators, and middlewares are injected via the DI container.
   
2. **Repository Pattern**: Separation of concerns for data access, with a dedicated repository for each entity (e.g., `IBookRepository`, `IOrderRepository`).

3. **Middleware**: Custom middlewares for logging and error handling were implemented.

4. **Authentication & Authorization**: Implemented JWT authentication and role-based authorization (Admin/User roles).

5. **Action Filters**: Used for tracking execution time and handling exceptions at the action level.

6. **Logging with Serilog**: Implemented structured logging for better insights into API behavior and errors.

7. **LINQ & Async Programming**: Efficient use of LINQ queries and async/await patterns for data processing.

8. **Environment-Specific Settings**: Configured different settings for development and production environments via `appsettings.json`.

---

## Future Enhancements

1. **Email Notifications**: Add email notification on order confirmation.
2. **Unit Tests**: Implement unit and integration tests for the core functionalities.
3. **Cache Mechanism**: Introduce a caching mechanism to optimize repeated queries for books and authors.

---

## Conclusion

This project demonstrates modern web development practices using ASP.NET Core and showcases the following skills:

- API design and architecture
- Database management with Entity Framework Core
- Secure authentication and authorization
- Logging and error tracking
- Reporting and analytics generation

Feel free to reach out if you have any questions or suggestions!

---
