### 🚀 Job Board API
A robust ASP.NET Core Web API designed for managing job listings, companies, and users. This project follows modern architectural patterns and best practices for building scalable, maintainable, and secure backend systems.

🏗 Architecture & Design Patterns
Clean Architecture: Implemented a clear separation of concerns using Controllers, Services, and Data Layers to ensure code maintainability.

Dependency Injection (DI): Fully utilized DI for managing service lifetimes and database context, promoting loose coupling and testability.

Data Modeling: Designed structured schemas with One-to-Many relationships (e.g., a Company owning multiple Job listings).

### 🌟 Advanced Features
Custom Error Handling: Developed a global ApiException middleware that follows the RFC 7807 (Problem Details) standard for consistent and professional error responses.

User Management & Security: Integrated secure user registration and profile updates with robust validation logic.

Data Integrity: Implemented conflict detection (e.g., unique email checks) returning appropriate 409 Conflict HTTP status codes.

Logging: Configured Serilog for structured logging, allowing for efficient tracking of application behavior and error debugging.

### 🛠 Tech Stack
Backend: .NET 8 / ASP.NET Core Web API

Database: SQL Server

ORM: Entity Framework Core (EF Core)

API Documentation: Interactive Swagger/OpenAPI for real-time endpoint testing.

### 🚀 Getting Started
Clone the Repository: git clone <your-repo-url>

Configure Database: Update the connection string in appsettings.json.

Apply Migrations: Run dotnet ef database update to sync the SQL Server schema.

Run Application: Launch the API and explore the documentation via Swagger.
