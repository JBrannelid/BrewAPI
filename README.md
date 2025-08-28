@'
# BrewAPI

![Café Interior](https://images.unsplash.com/photo-1554118811-1e0d58224f24?w=800&auto=format&fit=crop&q=80)

## Description
BrewAPI is an ASP.NET Core Web API for restaurant booking system developed as part of Lab 1: ASP.NET API + Database at Chas Academy - Stockholm.

## Features
- Restaurant menu management with CRUD operations
- Booking system with availability management
- Customer management
- Table management and capacity planning
- JWT authentication for administrators
- RESTful API with Swagger documentation

## Technologies
- ASP.NET Core Web API 8.0
- Entity Framework Core 9.0.8
- SQL Server
- JWT Authentication 8.0.19
- BCrypt.Net-Next 4.0.3
- Swagger/OpenAPI

## Installation and Usage
1. Clone repository
2. Configure connection string in `appsettings.Development.json`
3. Run migrations: `Update-Database`
4. Start the project: `dotnet run`

## API Endpoints
The API provides comprehensive endpoints for managing restaurant operations including menu items, table bookings, customer information, and administrative functions. Authentication is required for administrative operations, while public endpoints are available for viewing menus and creating bookings.
'@ | Out-File -FilePath README.md -Encoding utf8