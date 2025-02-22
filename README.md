# EmployeeMicroservice

First we need to run the below command in code first approach to get the migration should create.

dotnet ef migrations add InitialCreate --project .\EmployeeMicroserviceAPI

once your application runs it will automatically creates the database and tables in your DB.

What is the use of CQRS Pattern with MediatR?
The CQRS (Command Query Responsibility Segregation) pattern with MediatR helps separate read (queries) and write (commands) operations in your application, improving maintainability, scalability, and testability.

1. Benefits of Using CQRS with MediatR
✅ Separation of Concerns
Commands: Handle mutating operations (Create, Update, Delete).
Queries: Handle read-only operations (Get by ID, List all, Search).
No business logic mixing in controllers.
✅ Simplifies Complex Business Logic
Commands and Queries are independent.
Makes the system easier to understand and maintain.
✅ Improves Testability
Since queries and commands are isolated, unit testing becomes easier.
Handlers can be tested separately.
✅ Better Performance & Scalability
Queries can be optimized separately using Read Replicas or NoSQL Databases.
Commands can be handled asynchronously using Event Sourcing.
✅ Loosely Coupled Code
MediatR acts as a mediator between handlers and controllers.
Components don’t directly depend on each other.