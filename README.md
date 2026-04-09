# 🚀 Job Board API

A robust Backend API built with **.NET 9**, designed for managing job postings, companies, and user profiles. This project is built using professional-grade standards and follows the **Clean Architecture** pattern.

## 🏛 Architecture: Clean Architecture
The project is strictly organized into layers to ensure separation of concerns and maintainability:

* **Domain:** The core layer containing Entities and Business Logic.
* **Application:** Contains Service interfaces, Business Logic implementations, and **DTOs (Data Transfer Objects)**.
* **Infrastructure:** Handles Data Access (SQL Server via **EF Core**) and external configurations.
* **API (Presentation):** The entry point handling Controllers, Routing, and Middleware.

## 🛠 Tech Stack & Tools

* **Framework:** .NET 9
* **Database:** SQL Server (Entity Framework Core)
* **Object Mapping:** **AutoMapper** (for seamless Entity-DTO conversion)
* **Validation:** Integrated logic for validating request data (ensuring data integrity before processing).
* **Testing:** **xUnit** & **Moq** (Comprehensive Unit Testing suite).
* **Logging:** **ILogger** for tracking application behavior and debugging.
* **Error Handling:** Custom **ApiException** middleware for consistent global error responses.

## 🚀 Key Features

* **DTO Implementation:** Used across all services to decouple the internal database structure from the public API.
* **Business Validation:** Robust validation checks for input data (e.g., ensuring unique emails, non-empty fields).
* **Clean API Responses:** Standardized JSON responses for both success and error scenarios.
* **Unit Testing:** Over 80%+ coverage for core services ensuring the API is "production-ready".

## 🧪 Testing Strategy
The project features a comprehensive testing suite for `UserService`, `CompanyService`, and `JobService`. 
- **Isolation:** Each service is tested in isolation using **Moq**.
- **Database:** Uses **InMemory Database** for fast, side-effect-free testing.
- **Coverage:** Tests cover CRUD operations, validation rules, and `ApiException` scenarios.

To run the tests:
```bash
dotnet test
