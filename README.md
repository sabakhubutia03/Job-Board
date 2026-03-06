### 🚀 Job Board API
A robust ASP.NET Core Web API for managing job listings, companies, and users.
This project follows modern architectural patterns and best practices for building scalable, maintainable, and secure backend systems.

### 🏗 Architecture & Design Patterns
Clean Architecture
Implemented a clear separation of concerns using Controllers, Services, and Data Layers to ensure maintainability and scalability.

Dependency Injection (DI)
Utilized ASP.NET Core's built-in dependency injection for managing services and database context, promoting loose coupling and better testability.

Entity Framework Core Data Modeling
Designed relational database schemas with One-to-Many relationships (e.g., a Company can have multiple Job listings).

### 🌟 Advanced Features
stom Error Handling
Implemented a custom ApiException system following RFC 7807 (Problem Details) standard to provide consistent and structured error responses.

User Management
Supports user creation, updates, and deletion with proper validation logic.

Company & Job Management
Companies can create and manage multiple job listings using RESTful API endpoints.

Logging
Configured Serilog for structured logging, enabling easier debugging and monitoring of application behavior.

### 🛠 Tech Stack
Backend: .NET 8 / ASP.NET Core Web API

Database: SQL Server

ORM: Entity Framework Core

Logging: Serilog

API Documentation: Swagger / OpenAPI

### 🚀 Getting Started
Clone the Repository: git clone <your-repo-url>

Configure Database: Update the connection string in appsettings.json.

Apply Migrations: Run dotnet ef database update to sync the SQL Server schema.

Run Application: Launch the API and explore the documentation via Swagger.
