# 🚀 Job Board API

A robust **ASP.NET Core Web API** designed for managing job listings and companies. This project follows modern architectural patterns and best practices for scalable backend development.

###  Tech Stack & Core Concepts
* **Framework:** .NET 8 / ASP.NET Core Web API
* **Database:** SQL Server
* **ORM:** Entity Framework Core (EF Core)
* **Design Pattern:** Dependency Injection (DI) for services and database context.
* **Data Modeling:** Implemented **One-to-Many** relationships (e.g., One Company can have many Jobs).
* **Architecture:** Clean separation of concerns with Controllers, Services, and Data Layers.

###  Key Features
* **Full CRUD Operations:** Complete Create, Read, Update, and Delete functionality for Jobs, Companies, and Users.
* **Advanced Error Handling:** Global exception handling using a custom `ApiException` middleware (following RFC 7807 Problem Details).
* **Data Validation:** Secure registration and update logic with unique email constraints (409 Conflict handling).
* **Logging:** Integrated **Serilog** for structured logging and easier debugging.
* **API Documentation:** Fully interactive **Swagger/OpenAPI** UI.
